// Decompiled with JetBrains decompiler
// Type: SanupsNetMonitor.util.DataLackMonitor
// Assembly: SanupsNetMonitor, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CAE8435A-29A0-4A51-923B-367788891E7C
// Assembly location: C:\work\SanupsNetMonitor\bin\SanupsNetMonitor.exe

using Oracle.ManagedDataAccess.Client;
using SanupsNetMonitor.lib;
using System;
using System.ComponentModel;
using System.Data;
using System.Data.Common;

namespace SanupsNetMonitor.util
{
  internal class DataLackMonitor : Monitor
  {
    public override int Execute()
    {
      OracleCommand oracleCommand = new OracleCommand("SNET_PKG_MONITOR.DATA_LACK_MONITOR_0001", this.Conn);
      ((DbCommand) oracleCommand).CommandType = CommandType.StoredProcedure;
      OracleParameter oracleParameter = new OracleParameter("retVal", (OracleDbType) 112);
      ((DbParameter) oracleParameter).Direction = ParameterDirection.ReturnValue;
      oracleCommand.Parameters.Add(oracleParameter);
      ((DbConnection) this.Conn).Open();
      try
      {
        ((DbCommand) oracleCommand).ExecuteNonQuery();
      }
      catch (Exception ex)
      {
        throw ex;
      }
      finally
      {
        ((Component) oracleCommand).Dispose();
        ((DbConnection) this.Conn).Close();
      }
      return int.Parse(((DbParameter) oracleParameter).Value.ToString());
    }
  }
}
