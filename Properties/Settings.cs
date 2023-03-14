// Decompiled with JetBrains decompiler
// Type: SanupsNetMonitor.Properties.Settings
// Assembly: SanupsNetMonitor, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CAE8435A-29A0-4A51-923B-367788891E7C
// Assembly location: C:\work\SanupsNetMonitor\bin\SanupsNetMonitor.exe

using System.CodeDom.Compiler;
using System.Configuration;
using System.Diagnostics;
using System.Runtime.CompilerServices;

namespace SanupsNetMonitor.Properties
{
  [GeneratedCode("Microsoft.VisualStudio.Editors.SettingsDesigner.SettingsSingleFileGenerator", "11.0.0.0")]
  [CompilerGenerated]
  internal sealed class Settings : ApplicationSettingsBase
  {
    private static Settings defaultInstance = (Settings) SettingsBase.Synchronized((SettingsBase) new Settings());

    public static Settings Default => Settings.defaultInstance;

    [DefaultSettingValue("Provider=OraOLEDB.Oracle.1;Data Source=san02K1;Persist Security Info=True;Password=egelivirp;User ID=sanuser_01")]
    [ApplicationScopedSetting]
    [DebuggerNonUserCode]
    [SpecialSetting(SpecialSetting.ConnectionString)]
    public string ConnectionString1 => (string) this[nameof (ConnectionString1)];

    [DebuggerNonUserCode]
    [ApplicationScopedSetting]
    [DefaultSettingValue("Data Source=san02K1;Persist Security Info=True;Password=egelivirp;User ID=sanuser_01")]
    [SpecialSetting(SpecialSetting.ConnectionString)]
    public string ConnectionString => (string) this[nameof (ConnectionString)];
  }
}
