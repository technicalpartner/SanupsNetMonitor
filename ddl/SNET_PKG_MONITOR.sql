create or replace PACKAGE BODY            SNET_PKG_MONITOR AS

/*******************************************************************************
  起動：トリガ(総合値正時集計情報にレコードINSERTE)
  入力：T_HOURLY_TOTALS(総合値正時集計情報)
  参照：T_PLANTS(プラント情報)
  出力：T_DAILY_TOTALS(総合値日集計情報)

  ******INFO******
  ・Oracleでは24時のデータは登録できないため
    PHP側で次の日の0時データとして入力するので、
    同日の集計データだが日付が異なるデータになる。
    集計処理の際はF_YMDカラムでグルーピングする必要がある。
  ・F_YMD自体はDB登録処理の段階で
    日時-1時間したものを入力しているため
    こちらで時間を算出するような処理は行わない。
*******************************************************************************/


-- エラーレベル
C_LVL_DEBUG   CONSTANT NUMBER(1) := SNET_PKG_COMMON.C_LOGINFO_LEVEL_DEBUG;   -- エラーレベル  デバッグ
C_LVL_INFO    CONSTANT NUMBER(1) := SNET_PKG_COMMON.C_LOGINFO_LEVEL_INFO ;   -- エラーレベル  情報
C_LVL_WARNING CONSTANT NUMBER(1) := SNET_PKG_COMMON.C_LOGINFO_LEVEL_WARNING; -- エラーレベル  警告
C_LVL_ERROR   CONSTANT NUMBER(1) := SNET_PKG_COMMON.C_LOGINFO_LEVEL_ERROR;   -- エラーレベル  エラー

-- 変数定義
error_title   VARCHAR2(100);
error_code    NUMBER(5);
error_message VARCHAR2(2048);

program_name VARCHAR2(2048);

--存在確認用
    exist_flg                 NUMBER(1);

-- 更新データ内容
    sql_data                  NVARCHAR2(1028);

/*******************************************************************************
 各プログラム固有のエラーログ出力
 引数:ログレベル、タイトル、詳細内容
 戻り値:なし
*******************************************************************************/
  PROCEDURE out_log_local(p_log_level IN t_loginfos.f_log_level%TYPE,
                          p_title     IN t_loginfos.f_title%TYPE,
                          p_detail    IN t_loginfos.f_detail%TYPE)
  IS
    loginfos t_loginfos%ROWTYPE;

  BEGIN
    DBMS_OUTPUT.PUT_LINE(p_title);
    DBMS_OUTPUT.PUT_LINE(p_detail);

    loginfos.f_log_level  := p_log_level;
    loginfos.f_datatime   := SYSTIMESTAMP;
    loginfos.f_title      := p_title;
    loginfos.f_detail     := p_detail;
    --  loginfos.F_SERVER     := UTL_INADDR.GET_HOST_NAME;
    loginfos.f_program_nm := 'SNET_PKG_DAILY_TOTAL';
    -- エラーログをログテーブルに出力する。
    SNET_PKG_COMMON.out_log(loginfos);

  EXCEPTION
    WHEN others THEN
      NULL;
  END out_log_local;

  PROCEDURE set_program_name(p_name IN varchar) IS
  BEGIN
    program_name := p_name;
  END set_program_name;

/*******************************************************************************
 通知履歴を登録する
 引数:
    monitor_name : 監視機能名
    execute_date : 監視実施予定時刻
 戻り値:なし
*******************************************************************************/
  PROCEDURE SET_SEND_HISTORY(
    monitor_name IN VARCHAR
    ,execute_date IN DATE
    )IS
  BEGIN
      UPDATE T_MONITOR_HISTORY
      SET F_SEND_DONE = 1
          ,MODIFIED = SYSDATE
      WHERE
          F_MONITOR_NAME = monitor_name
      AND F_EXECUTE_DATE = execute_date;

  END SET_SEND_HISTORY;

/*******************************************************************************
 次回実行時間を登録する
 引数:
    plant_cd    : 対象プラントコード
    monitor_name: 監視機能名
 戻り値:なし
*******************************************************************************/
--  PROCEDURE SET_NEXT_TIME
--  (
--    plant_cd IN NUMBER
--    ,monitor_name IN VARCHAR2
--  )
--  IS
--    next_time_result DATE;
--  BEGIN
--      -- 次回実行時間を算出する
--      -- 次回実行時間
--      -- 　　基準時間に対してオフセットを加算した後で、インターバルを加算していき
--      -- 　　現在時間および、監視開始時間を超えた時間
--      -- 　　監視終了時間を超えた場合は翌日の開始時間までインターバルを加算する
--      SELECT NEXT_TIME INTO next_time_result
--        FROM (
--          SELECT
--            trunc(sysdate) + F_BASE_TIME  + F_INTERVAL * LEVEL + F_OFFSET AS NEXT_TIME
--          FROM T_MONITOR_SETTING
--          WHERE F_MONITOR_NAME='DataLackMonitor'
--          AND F_PLANT_CD = 1
--          connect by
--          (trunc(sysdate) <= (trunc(sysdate) + F_BASE_TIME  + F_INTERVAL * LEVEL + F_OFFSET))
--          AND
--          (
--            (trunc(sysdate) + F_MONITOR_START) <= (trunc(sysdate) + F_BASE_TIME  + F_INTERVAL * LEVEL + F_OFFSET)
--            AND (trunc(sysdate) + F_MONITOR_END) >= (trunc(sysdate) + F_BASE_TIME  + F_INTERVAL * LEVEL + F_OFFSET)
--            OR
--            (trunc(sysdate) + 1 + F_MONITOR_START) <= (trunc(sysdate) + F_BASE_TIME  + F_INTERVAL * LEVEL + F_OFFSET)
--            AND (trunc(sysdate) + 1 + F_MONITOR_END) >= (trunc(sysdate) + F_BASE_TIME  + F_INTERVAL * LEVEL + F_OFFSET)
--          )
--          ORDER BY trunc(sysdate) + F_BASE_TIME  + F_INTERVAL * LEVEL + F_OFFSET
--        )
--        WHERE rownum = 1;
--
--      -- 次回実行時間を登録
--      UPDATE T_MONITOR_SCHEDULE
--        SET F_SCHEDULE       = next_time_result
--        ,MODIFIED         = sysdate
--        WHERE F_MONITOR_NAME = monitor_name
--        AND F_PLANT_CD       = plant_cd;
--
--  END SET_NEXT_TIME;

/*******************************************************************************
 データ欠損監視
 引数:
    plant : 対象プラントコード 0以外は未指定
    monitor_day : 対象日 日付部のみ有効 未指定はnull
 戻り値:異常検出した件数
*******************************************************************************/
--  FUNCTION DATA_LACK
--  (
--    plant IN NUMBER
--    ,monitor_day IN DATE
--  ) RETURN NUMBER
--  IS
--    --SQL格納用
--    sql_str VARCHAR2(3000);
--    detail_str VARCHAR2(1000);
--    where_str VARCHAR2(1000);
--
--    --カーソル、レコード定義
--    TYPE CurTyp IS REF CURSOR;
--    TYPE ResultRec IS RECORD (
--      F_PLANT_CD T_MONITOR_SETTING.F_PLANT_CD%TYPE
--      ,F_UNITNO T_HOURLY_UNITS.F_UNITNO%TYPE
--      ,F_PLANT_NM T_PLANTS.F_PLANT_NM%TYPE
--      ,F_SCHEDULE T_MONITOR_SCHEDULE.F_SCHEDULE%TYPE
--      ,F_SUBSCRIBER_ACT T_PLANTS.F_SUBSCRIBER_ACT%TYPE
--      ,F_SUBSCRIBER T_SUBSCRIBERS.F_SUBSCRIBER%TYPE
--    );
--    csr CurTyp;
--    rec ResultRec;
--
--    error_count NUMBER(4,0);
--
--    exec_date DATE;
--  BEGIN
--    /* 関数の初期処理 */
--    set_program_name('SNET_PKG_MONITOR.DATA_LACK');
--
--    /* 検査対象期間中のデータ欠損を検索 */
--    /* チェック条件 実施日、監視時間範囲内にレコードが存在しない、または積算電力量がNULLの場合 */
--    sql_str := sql_str || 'SELECT DISTINCT';
--    sql_str := sql_str || '  T_KIJUN.F_PLANT_CD                  F_PLANT_CD';
--    sql_str := sql_str || '  ,T_DEVICES.F_UNITNO                 F_UNITNO';
--    sql_str := sql_str || '  ,T_PLANTS.F_PLANT_NM                F_PLANT_NM';
--    sql_str := sql_str || '  ,T_MONITOR_SCHEDULE.F_SCHEDULE      F_SCHEDULE';
--    sql_str := sql_str || '  ,T_PLANTS.F_SUBSCRIBER_ACT          F_SUBSCRIBER_ACT';
--    sql_str := sql_str || '  ,T_SUBSCRIBERS.F_SUBSCRIBER         F_SUBSCRIBER';
--    sql_str := sql_str || ' FROM ';
--    sql_str := sql_str || ' (';
--    sql_str := sql_str || ' SELECT';
--    sql_str := sql_str || '  T_MONITOR_SETTING.F_PLANT_CD F_PLANT_CD';
--    sql_str := sql_str || '  ,TRUNC(SYSDATE) + LEVEL /24 CHECK_HOUR';
--    sql_str := sql_str || ' FROM T_MONITOR_SETTING';
--    sql_str := sql_str || ' WHERE T_MONITOR_SETTING.F_PLANT_CD IN ';
--    sql_str := sql_str || '  (SELECT F_PLANT_CD FROM T_MONITOR_SCHEDULE ';
--    sql_str := sql_str || '   WHERE F_MONITOR_NAME = ''DataLackMonitor'' AND F_SCHEDULE < SYSDATE)';
--    sql_str := sql_str || ' AND (TRUNC(SYSDATE) + LEVEL /24)';
--    sql_str := sql_str || '              >= (TRUNC(SYSDATE) + NVL(T_MONITOR_SETTING.F_MONITOR_START,INTERVAL ''0 1'' DAY TO HOUR))';
--    sql_str := sql_str || ' CONNECT BY (TRUNC(SYSDATE) + LEVEL /24)';
--    sql_str := sql_str || '              <= (TRUNC(SYSDATE) + NVL(T_MONITOR_SETTING.F_MONITOR_END,INTERVAL ''1'' DAY))';
--    sql_str := sql_str || ' ) T_KIJUN';
--    sql_str := sql_str || '  INNER JOIN T_PLANTS ON T_PLANTS.F_PLANT_CD = T_KIJUN.F_PLANT_CD';
--    sql_str := sql_str || '  INNER JOIN T_DEVICES ON T_PLANTS.F_PLANT_CD = T_DEVICES.F_PLANT_CD';
--    sql_str := sql_str || '  INNER JOIN T_SUBSCRIBERS ON T_PLANTS.F_SUBSCRIBER_ACT = T_SUBSCRIBERS.F_ACCOUNT';
--    sql_str := sql_str || '  INNER JOIN T_MONITOR_SCHEDULE ON T_MONITOR_SCHEDULE.F_PLANT_CD = T_KIJUN.F_PLANT_CD';
--    sql_str := sql_str || '                        AND    F_MONITOR_NAME = ''DataLackMonitor''';
--    sql_str := sql_str || '  LEFT JOIN T_HOURLY_UNITS ON T_HOURLY_UNITS.F_PLANT_CD = T_KIJUN.F_PLANT_CD';
--    sql_str := sql_str || '                        AND T_HOURLY_UNITS.F_UNITNO = T_DEVICES.F_UNITNO';
--    sql_str := sql_str || '                        AND T_HOURLY_UNITS.F_DATETIME = T_KIJUN.CHECK_HOUR';
--    sql_str := sql_str || ' WHERE';
--    sql_str := sql_str || '     T_HOURLY_UNITS.F_MEASURE7 IS NULL';
--    where_str := ' AND';
--    IF plant > 0 THEN
--      sql_str := sql_str || where_str || ' T_KIJUN.F_PLANT_CD = ' || plant;
--      where_str := ' AND';
--    END IF;
--    IF monitor_day IS NOT NULL THEN
--      sql_str := sql_str || where_str || ' TRUNC(T_HOURLY_UNITS.F_DATETIME) = TRUNC(:monitor_day)';
--    ELSE
--      sql_str := sql_str || where_str || ' :monitor_day IS NULL';
--    END IF;
--
--    --
--    BEGIN
--      error_count := 0;
--      SELECT sysdate INTO exec_date FROM dual;
--
--      OPEN csr FOR sql_str USING monitor_day;
--      LOOP
--          FETCH csr INTO rec;
--            EXIT WHEN csr%NOTFOUND;
--            -- retValを組み立てる（割愛）
--            detail_str := 'データ欠落が発生しています';
--
--            INSERT
--              INTO T_MONITOR_HISTORY
--                (
--                  F_MONITOR_NAME,
--                  F_PLANT_CD,
--                  F_PLANT_NM,
--                  F_UNITNO,
--                  F_SUBSCRIBER_ACT,
--                  F_SUBSCRIBER_NM,
--                  F_SCHEDULE,
--                  F_EXECUTE_DATE,
--                  F_LEVEL,
--                  F_RESULT,
--                  F_DETAIL,
--                  F_SEND_DONE,
--                  CREATED,
--                  MODIFIED
--                )
--                VALUES
--                (
--                  'DataLackMonitor',
--                  rec.F_PLANT_CD,
--                  rec.F_PLANT_NM,
--                  rec.F_UNITNO,
--                  rec.F_SUBSCRIBER_ACT,
--                  rec.F_SUBSCRIBER,
--                  rec.F_SCHEDULE,
--                  exec_date,
--                  2,                    -- エラーレベル 復旧の必要な警告
--                  -102,                 -- データ欠損のエラーコード
--                  'データの欠落が発見されました',
--                  0,
--                  sysdate,
--                  null
--                );
--
--            SET_NEXT_TIME(rec.F_PLANT_CD,'DataLackMonitor');
--
--            error_count := error_count + 1;
--        END LOOP;
--
--        COMMIT;
--    EXCEPTION WHEN OTHERS THEN
--        ROLLBACK;
--        RAISE;
--    END;
--    CLOSE csr;
--
--    RETURN error_count;
--
--  END DATA_LACK;

/*******************************************************************************
 データ欠損監視
 監視仕様：
 　　(1) 対象プラント
   　　　実行時間に契約期間内にあるプラント
      　モニタリングが[有効]であるプラント
 　　(2) 監視条件
   　　　以下の正時データにNull値が含まれること
      　　①システム日前日の[1:00]以降の生時データ
      　　②システム日当日の[通知時刻の時(hour)]-[α]時間までの正時時データ
        　　※α値について
         　　・ システム設定にて変更可能
         　　・ 設定値範囲 整数[0] ～ [6]
 引数:
    なし
 戻り値:異常検出した件数
*******************************************************************************/
  FUNCTION DATA_LACK_MONITOR_0001 RETURN NUMBER
  IS
    --SQL格納用
    sql_str VARCHAR2(3000);
    detail_str VARCHAR2(1000);
    where_str VARCHAR2(1000);
    range_param01 VARCHAR2(1000);

    --カーソル、レコード定義
    TYPE CurTyp IS REF CURSOR;
    TYPE ResultRec IS RECORD (
      F_PLANT_CD T_PLANTS.F_PLANT_CD%TYPE
      ,F_PLANT_NM T_PLANTS.F_PLANT_NM%TYPE
      ,F_MEASURE_DATE DATE
    );
    csr CurTyp;
    rec ResultRec;

    error_count NUMBER(4,0);

    exec_date DATE;
  BEGIN
    /* 関数の初期処理 */
    set_program_name('SNET_PKG_MONITOR.DATA_LACK_MONITOR_0001');
    /* システムパラメータ - 監視猶予時間 - の取得 */
    SELECT F_PARAMETER INTO range_param01 FROM T_SYSTEM_PARAMETERS WHERE F_GROUP_ID='15';

    /* 検査対象期間中のデータ欠損を検索 */
    /* チェック条件 実施日、監視時間範囲内にレコードが存在しない、または積算電力量がNULLの場合 */
    sql_str := sql_str || ' SELECT DISTINCT ';
    sql_str := sql_str || '   T_PLANTS.F_PLANT_CD F_PLANT_CD,';
    sql_str := sql_str || '   T_PLANTS.F_PLANT_NM F_PLANT_NM,';
    sql_str := sql_str || '   MONITOR_RANGE.CHECK_HOUR F_MEASURE_DATE';
    sql_str := sql_str || ' FROM (SELECT';
    sql_str := sql_str || '    trunc(sysdate-1) + LEVEL /24 CHECK_HOUR';
    sql_str := sql_str || '  FROM dual';
    sql_str := sql_str || '  CONNECT BY (TRUNC(SYSDATE-1) + LEVEL /24)';
    sql_str := sql_str || '               <= TO_DATE(TO_CHAR(sysdate,''yyyy/mm/dd hh24''),''yyyy/mm/dd hh24'')';
    sql_str := sql_str || '                 - TO_DSINTERVAL(:range_param01)';
    sql_str := sql_str || '             ) MONITOR_RANGE';
    sql_str := sql_str || '     CROSS JOIN (';
    sql_str := sql_str || '           SELECT F_PLANT_CD,F_PLANT_NM';
    sql_str := sql_str || '               FROM T_PLANTS';
    sql_str := sql_str || '               WHERE F_MONITORING_FLAG=1 ';
    sql_str := sql_str || '               AND trunc(sysdate) >= (select F_FROM_DT from t_subscribers where f_account = T_PLANTS.F_SUBSCRIBER_ACT)';
    sql_str := sql_str || '               AND trunc(sysdate) <= (select F_TO_DT from t_subscribers where f_account = T_PLANTS.F_SUBSCRIBER_ACT)';
    sql_str := sql_str || '           ) T_PLANTS';
    sql_str := sql_str || '     LEFT JOIN T_HOURLY_UNITS ON T_HOURLY_UNITS.F_DATETIME = MONITOR_RANGE.CHECK_HOUR';
    sql_str := sql_str || '                              AND T_PLANTS.F_PLANT_CD = T_HOURLY_UNITS.F_PLANT_CD';
    sql_str := sql_str || ' where   T_HOURLY_UNITS.F_PLANT_CD is null';
    sql_str := sql_str || ' ORDER BY   T_PLANTS.F_PLANT_CD,MONITOR_RANGE.CHECK_HOUR';

    --
    BEGIN
      error_count := 0;
      SELECT sysdate INTO exec_date FROM dual;

      OPEN csr FOR sql_str USING range_param01;
      LOOP
          FETCH csr INTO rec ;
            EXIT WHEN csr%NOTFOUND;
            -- retValを組み立てる（割愛）
            detail_str := 'データ欠落が発生しています';

            INSERT
              INTO T_MONITOR_HISTORY
                (
                  F_MONITOR_NAME,
                  F_EXECUTE_DATE,
                  F_PLANT_CD,
                  F_PLANT_NM,
                  F_MEASURE_DATE,
                  F_LEVEL,
                  F_RESULT,
                  F_DETAIL,
                  F_SEND_DONE,
                  CREATED,
                  MODIFIED
                )
                VALUES
                (
                  'DataLackMonitor',
                  exec_date,
                  rec.F_PLANT_CD,
                  rec.F_PLANT_NM,
                  rec.F_MEASURE_DATE,
                  2,                    -- エラーレベル 復旧の必要な警告
                  -102,                 -- データ欠損のエラーコード
                  'データの欠落が発見されました',
                  0,
                  sysdate,
                  null
                );

            error_count := error_count + 1;
        END LOOP;

        COMMIT;
    EXCEPTION WHEN OTHERS THEN
        ROLLBACK;
        RAISE;
    END;
    CLOSE csr;

    RETURN error_count;

  END DATA_LACK_MONITOR_0001;
END SNET_PKG_MONITOR;