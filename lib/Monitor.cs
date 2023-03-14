// Decompiled with JetBrains decompiler
// Type: SanupsNetMonitor.lib.Monitor
// Assembly: SanupsNetMonitor, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CAE8435A-29A0-4A51-923B-367788891E7C
// Assembly location: C:\work\SanupsNetMonitor\bin\SanupsNetMonitor.exe

using Oracle.ManagedDataAccess.Client;
using SanupsNetMonitor.Properties;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Common;
using System.Text;

namespace SanupsNetMonitor.lib
{
  internal abstract class Monitor : IDisposable
  {
    protected OracleConnection Conn;
    protected int mLastResult;
    protected Monitor.Level mLevel;

    public Monitor() => this.Conn = new OracleConnection(Settings.Default.ConnectionString);

    public int LastResult
    {
      get => this.mLastResult;
      set => this.mLastResult = value;
    }

    public Monitor.Level MonitorLevel => this.mLevel;

    public bool IsExecutable(string plant_cd)
    {
      StringBuilder stringBuilder = new StringBuilder(1024);
      stringBuilder.Append(" SELECT");
      stringBuilder.Append("   COUNT(F_PLANT_CD)");
      stringBuilder.Append(" FROM T_MONITOR_SCHEDULE");
      stringBuilder.Append(" WHERE F_MONITOR_NAME = 'DataLackMonitor'");
      stringBuilder.AppendFormat(" AND   F_PLANT_CD ='\0'", (object) plant_cd);
      stringBuilder.Append(" AND   F_SCHEDULE < sysdate");
      ((DbConnection) this.Conn).Open();
      OracleCommand oracleCommand = new OracleCommand(stringBuilder.ToString(), this.Conn);
      try
      {
        return (int) ((DbCommand) oracleCommand).ExecuteScalar() == 1;
      }
      finally
      {
        ((DbConnection) this.Conn).Close();
      }
    }

    public List<int> GetExecutablePlants()
    {
      StringBuilder stringBuilder = new StringBuilder(1024);
      stringBuilder.Append(" SELECT");
      stringBuilder.Append("   F_PLANT_CD");
      stringBuilder.Append(" FROM T_MONITOR_SCHEDULE");
      stringBuilder.Append(" WHERE F_MONITOR_NAME = 'DataLackMonitor'");
      stringBuilder.Append(" AND   F_SCHEDULE < sysdate");
      stringBuilder.Append(" ORDER BY F_PLANT_CD");
      ((DbConnection) this.Conn).Open();
      List<int> executablePlants = new List<int>();
      OracleCommand oracleCommand = new OracleCommand(stringBuilder.ToString(), this.Conn);
      try
      {
        OracleDataReader oracleDataReader = oracleCommand.ExecuteReader();
        if (((DbDataReader) oracleDataReader).HasRows)
        {
          while (((DbDataReader) oracleDataReader).Read())
            executablePlants.Add((int) ((DbDataReader) oracleDataReader).GetInt16(0));
        }
        ((DbDataReader) oracleDataReader).Close();
        oracleDataReader.Dispose();
      }
      finally
      {
        ((DbConnection) this.Conn).Close();
      }
      return executablePlants;
    }

    public abstract int Execute();

    public SanupsNetMonitor.DataSet.Monitor.T_MONITOR_HISTORYDataTable GetHistory()
    {
      StringBuilder stringBuilder = new StringBuilder(1024);
      stringBuilder.Append("  SELECT");
      stringBuilder.Append("    T_MONITOR_HISTORY.F_MONITOR_NAME F_MONITOR_NAME,");
      stringBuilder.Append("    T_MONITOR_HISTORY.F_EXECUTE_DATE F_EXECUTE_DATE,");
      stringBuilder.Append("    T_MONITOR_HISTORY.F_PLANT_CD F_PLANT_CD,");
      stringBuilder.Append("    T_MONITOR_HISTORY.F_PLANT_NM F_PLANT_NM,");
      stringBuilder.Append("    T_MONITOR_HISTORY.F_UNITNO F_UNITNO,");
      stringBuilder.Append("    T_MONITOR_HISTORY.F_MEASURE_DATE F_MEASURE_DATE,");
      stringBuilder.Append("    T_MONITOR_HISTORY.F_SUBSCRIBER_ACT F_SUBSCRIBER_ACT,");
      stringBuilder.Append("    T_MONITOR_HISTORY.F_SUBSCRIBER_NM F_SUBSCRIBER_NM,");
      stringBuilder.Append("    T_MONITOR_HISTORY.F_SCHEDULE F_SCHEDULE,");
      stringBuilder.Append("    T_MONITOR_HISTORY.F_LEVEL F_LEVEL,");
      stringBuilder.Append("    T_MONITOR_HISTORY.F_RESULT F_RESULT,");
      stringBuilder.Append("    T_MONITOR_HISTORY.F_DETAIL F_DETAIL,");
      stringBuilder.Append("    T_MONITOR_HISTORY.F_SEND_DONE F_SEND_DONE,");
      stringBuilder.Append("    T_MONITOR_HISTORY.CREATED CREATED,");
      stringBuilder.Append("    T_MONITOR_HISTORY.MODIFIED MODIFIED");
      stringBuilder.Append("  FROM T_MONITOR_HISTORY");
      stringBuilder.Append("      INNER JOIN (SELECT F_MONITOR_NAME,F_EXECUTE_DATE FROM T_MONITOR_HISTORY WHERE F_SEND_DONE = 0 and rownum=1 ORDER BY F_MONITOR_NAME,F_EXECUTE_DATE) TOP_HIST");
      stringBuilder.Append("        ON TOP_HIST.F_MONITOR_NAME = T_MONITOR_HISTORY.F_MONITOR_NAME AND TOP_HIST.F_EXECUTE_DATE = T_MONITOR_HISTORY.F_EXECUTE_DATE ");
      stringBuilder.Append("  ORDER BY F_MONITOR_NAME,F_EXECUTE_DATE,F_PLANT_NM,F_UNITNO,F_MEASURE_DATE ");
      OracleDataAdapter oracleDataAdapter = new OracleDataAdapter(new OracleCommand(stringBuilder.ToString(), this.Conn));
      SanupsNetMonitor.DataSet.Monitor.T_MONITOR_HISTORYDataTable history = new SanupsNetMonitor.DataSet.Monitor.T_MONITOR_HISTORYDataTable();
      ((DbConnection) this.Conn).Open();
      try
      {
        ((DbDataAdapter) oracleDataAdapter).Fill((DataTable) history);
      }
      finally
      {
        ((DbConnection) this.Conn).Close();
      }
      return history;
    }

    public void SetSendHistory(SanupsNetMonitor.DataSet.Monitor.T_MONITOR_HISTORYDataTable dt)
    {
      OracleCommand oracleCommand = new OracleCommand("SNET_PKG_MONITOR.SET_SEND_HISTORY", this.Conn);
      ((DbCommand) oracleCommand).CommandType = CommandType.StoredProcedure;
      OracleParameter oracleParameter1 = new OracleParameter("plant_cd", (OracleDbType) 126);
      OracleParameter oracleParameter2 = new OracleParameter("execute_date", (OracleDbType) 106);
      oracleCommand.Parameters.Add(oracleParameter1);
      oracleCommand.Parameters.Add(oracleParameter2);
      ((DbConnection) this.Conn).Open();
      try
      {
        foreach (SanupsNetMonitor.DataSet.Monitor.T_MONITOR_HISTORYRow monitorHistoryRow in (TypedTableBase<SanupsNetMonitor.DataSet.Monitor.T_MONITOR_HISTORYRow>) dt)
        {
          ((DbParameter) oracleParameter1).Value = (object) monitorHistoryRow.F_MONITOR_NAME;
          ((DbParameter) oracleParameter2).Value = (object) monitorHistoryRow.F_EXECUTE_DATE;
          ((DbCommand) oracleCommand).ExecuteNonQuery();
        }
      }
      finally
      {
        ((DbConnection) this.Conn).Close();
      }
    }

    public void Dispose()
    {
      if (this.Conn == null)
        return;
      if (((DbConnection) this.Conn).State == ConnectionState.Open)
        ((DbConnection) this.Conn).Close();
      ((Component) this.Conn).Dispose();
      this.Conn = (OracleConnection) null;
    }

    public enum Level
    {
      All,
      Critical,
      Error,
      Worning,
      Information,
      PeriodicReport,
    }
  }
}
