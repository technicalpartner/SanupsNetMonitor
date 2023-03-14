// Decompiled with JetBrains decompiler
// Type: SanupsNetMonitor.util.MessageMakerFuctory
// Assembly: SanupsNetMonitor, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CAE8435A-29A0-4A51-923B-367788891E7C
// Assembly location: C:\work\SanupsNetMonitor\bin\SanupsNetMonitor.exe

using SanupsNetMonitor.lib;

namespace SanupsNetMonitor.util
{
  internal class MessageMakerFuctory
  {
    public IMessageMaker Create(string message_code) => (IMessageMaker) new DataLackMessageMaker();
  }
}
