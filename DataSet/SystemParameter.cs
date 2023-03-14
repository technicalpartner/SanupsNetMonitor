// Decompiled with JetBrains decompiler
// Type: SanupsNetMonitor.DataSet.SystemParameter
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

namespace SanupsNetMonitor.DataSet
{
  internal class SystemParameter : IDisposable
  {
    private OracleConnection Conn;

    public SystemParameter() => this.Conn = new OracleConnection(Settings.Default.ConnectionString);

    public List<string> GetByGroupId(SystemParameter.GroupId _groupId)
    {
      List<string> byGroupId = new List<string>();
      ((DbConnection) this.Conn).Open();
      string format = "SELECT F_PARAMETER FROM T_SYSTEM_PARAMETERS WHERE F_GROUP_ID={0} ORDER BY F_PARAMETER_ID ASC";
      try
      {
        OracleDataReader oracleDataReader = new OracleCommand(string.Format(format, (object) (int) _groupId), this.Conn).ExecuteReader();
        if (((DbDataReader) oracleDataReader).HasRows)
        {
          while (((DbDataReader) oracleDataReader).Read())
            byGroupId.Add(((DbDataReader) oracleDataReader)[0].ToString());
        }
      }
      finally
      {
        ((DbConnection) this.Conn).Close();
        ((Component) this.Conn).Dispose();
      }
      return byGroupId;
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

    public enum GroupId
    {
      MailSendSetting = 11, // 0x0000000B
    }
  }
}
