// Decompiled with JetBrains decompiler
// Type: SanupsNetMonitor.Program
// Assembly: SanupsNetMonitor, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CAE8435A-29A0-4A51-923B-367788891E7C
// Assembly location: C:\work\SanupsNetMonitor\bin\SanupsNetMonitor.exe

using log4net;
using SanupsNetMonitor.util;
using System;
using System.Reflection;
using System.Threading;

namespace SanupsNetMonitor
{
  internal class Program
  {
    private static Mutex _mutex;

    private static void Main(string[] args)
    {
      ILog logger = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);
      logger.InfoFormat("{0:yyyy/MM/dd HH:mm:ss} : SANUPS NETのデータ欠落監視を実行します", (object) DateTime.Now);
      Program._mutex = new Mutex(false, "SanupsNetMonitor");
      if (!Program._mutex.WaitOne(0, false))
        return;
      try
      {
        using (DataLackMonitor dataLackMonitor = new DataLackMonitor())
        {
          dataLackMonitor.Execute();
          while (true)
          {
            SanupsNetMonitor.DataSet.Monitor.T_MONITOR_HISTORYDataTable history = dataLackMonitor.GetHistory();
            if (history.Count != 0)
            {
              new SanupsNetMonitor.util.Notice().Send(history);
              dataLackMonitor.SetSendHistory(history);
            }
            else
              break;
          }
        }
      }
      catch (Exception ex)
      {
        logger.Error((object) "SANUPS NET監視機能は異常を検出しました", ex);
      }
    }
  }
}
