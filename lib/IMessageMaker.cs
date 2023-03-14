// Decompiled with JetBrains decompiler
// Type: SanupsNetMonitor.lib.IMessageMaker
// Assembly: SanupsNetMonitor, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CAE8435A-29A0-4A51-923B-367788891E7C
// Assembly location: C:\work\SanupsNetMonitor\bin\SanupsNetMonitor.exe

using System;
using System.Collections.Generic;

namespace SanupsNetMonitor.lib
{
  internal interface IMessageMaker : IDisposable
  {
    string CreateSubject(SanupsNetMonitor.DataSet.Monitor.T_MONITOR_HISTORYDataTable dt_history);

    string CreateBody(SanupsNetMonitor.DataSet.Monitor.T_MONITOR_HISTORYDataTable dt_history);

    List<string> CreateAttachFiles(SanupsNetMonitor.DataSet.Monitor.T_MONITOR_HISTORYDataTable dt_history);

    int GetNextIndex(SanupsNetMonitor.DataSet.Monitor.T_MONITOR_HISTORYDataTable dt_history, int index);
  }
}
