// Decompiled with JetBrains decompiler
// Type: SanupsNetMonitor.util.DataLackMessageMaker
// Assembly: SanupsNetMonitor, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CAE8435A-29A0-4A51-923B-367788891E7C
// Assembly location: C:\work\SanupsNetMonitor\bin\SanupsNetMonitor.exe

using SanupsNetMonitor.lib;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Text;

namespace SanupsNetMonitor.util
{
  internal class DataLackMessageMaker : IMessageMaker, IDisposable
  {
    private DateTime mExecuteDate;

    public DataLackMessageMaker() => this.mExecuteDate = DateTime.Now;

    public string CreateSubject(SanupsNetMonitor.DataSet.Monitor.T_MONITOR_HISTORYDataTable dt_history)
    {
      this.mExecuteDate = dt_history[0].F_EXECUTE_DATE;
      return Encoding.UTF8.GetString(Encoding.UTF8.GetBytes(("【SANUPS状態監視】" + string.Format("{0:[yyyy-MM-dd]_[HH-mm]}", (object) this.mExecuteDate)).ToCharArray()));
    }

    public string CreateBody(SanupsNetMonitor.DataSet.Monitor.T_MONITOR_HISTORYDataTable dt_history)
    {
      int index = 0;
      if (dt_history.Rows.Count <= index)
        return string.Empty;
      StringBuilder stringBuilder = new StringBuilder();
      stringBuilder.AppendFormat("     プラント名     \n");
      stringBuilder.AppendFormat("--------------------\n");
      do
      {
        stringBuilder.AppendFormat("{0}\n", (object) dt_history[index].F_PLANT_NM);
        index = this.GetNextIndex(dt_history, index);
      }
      while (dt_history.Rows.Count > index);
      stringBuilder.Append("\n");
      return stringBuilder.ToString();
    }

    public List<string> CreateAttachFiles(SanupsNetMonitor.DataSet.Monitor.T_MONITOR_HISTORYDataTable dt_history)
    {
      int num = 0;
      if (dt_history.Rows.Count <= num)
        return (List<string>) null;
      string temporaryPath = this.GetTemporaryPath();
      List<string> attachFiles = new List<string>();
      Encoding encoding = Encoding.GetEncoding("Shift-JIS");
      using (StreamWriter streamWriter = new StreamWriter(temporaryPath, false, encoding))
      {
        StringBuilder stringBuilder = new StringBuilder();
        stringBuilder.AppendFormat("プラント名,測定日時\n");
        foreach (SanupsNetMonitor.DataSet.Monitor.T_MONITOR_HISTORYRow monitorHistoryRow in (TypedTableBase<SanupsNetMonitor.DataSet.Monitor.T_MONITOR_HISTORYRow>) dt_history)
        {
          stringBuilder.AppendFormat("{0},{1:yyyy/MM/dd HH:mm}\n", (object) monitorHistoryRow.F_PLANT_NM, (object) monitorHistoryRow.F_MEASURE_DATE);
          ++num;
        }
        streamWriter.Write((object) stringBuilder);
        streamWriter.Close();
        attachFiles.Add(temporaryPath);
      }
      return attachFiles;
    }

    private string GetTemporaryPath()
    {
      string path = AppDomain.CurrentDomain.BaseDirectory + "temp";
      if (!Directory.Exists(path))
        Directory.CreateDirectory(path);
      string str = string.Format("teiji_{0:[yyyyMMdd]_[HHmm]}.csv", (object) DateTime.Now);
      return path + "\\" + str;
    }

    public int GetNextIndex(SanupsNetMonitor.DataSet.Monitor.T_MONITOR_HISTORYDataTable dt_history, int index)
    {
      if (dt_history.Rows.Count <= index)
        return index;
      SanupsNetMonitor.DataSet.Monitor.T_MONITOR_HISTORYRow monitorHistoryRow = dt_history[index];
      while (monitorHistoryRow.F_PLANT_CD == dt_history[index].F_PLANT_CD)
      {
        ++index;
        if (dt_history.Rows.Count <= index)
          break;
      }
      return index;
    }

    public void Dispose()
    {
      string path = AppDomain.CurrentDomain.BaseDirectory + "temp";
      if (!Directory.Exists(path))
        return;
      foreach (string file in Directory.GetFiles(path))
        File.Delete(file);
    }
  }
}
