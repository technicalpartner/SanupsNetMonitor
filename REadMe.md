# SanupsNet監視ツール

## 概要

SANUPS.NETの正時情報収集状況を監視する。  
「運転監視範囲 猶予時間」の期間内に正時情報を受信していないプラントの一覧を抽出しメールにて送信する。

### 使用テーブル

- T_SYSTEM_PARAMETERS  
  - F_GROUP_ID : 15  
  - F_GROUP_NAME : 運転監視範囲 猶予時間  
  - F_PARAMETER : 4時間  

- T_MONITOR_HISTORY  
  監視対象の結果（通知メール送信内容）をDBに保管  

- T_MONITOR_NOTICE  
  監視結果の送信先  

### メール送信内容

送信先 : 
sanupsnet_support@sanyodenki.com

tytle :
SANUPS.NET MONITOR <sanups.net.monitor@sanups.net>

body : 

プラント名
--------------------
（監視結果 正時情報の欠損ありプラント名）
