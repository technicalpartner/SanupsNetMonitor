// Decompiled with JetBrains decompiler
// Type: SanupsNetMonitor.util.Notice
// Assembly: SanupsNetMonitor, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CAE8435A-29A0-4A51-923B-367788891E7C
// Assembly location: C:\work\SanupsNetMonitor\bin\SanupsNetMonitor.exe

using Oracle.ManagedDataAccess.Client;
using SanupsNetMonitor.DataSet;
using SanupsNetMonitor.lib;
using SanupsNetMonitor.Properties;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.Common;
using System.Net;
using System.Net.Mail;
using System.Text;
using TKMP.Net;
using TKMP.Writer;

namespace SanupsNetMonitor.util
{
  internal class Notice
  {
    private Notice.SMTP MySMTP;
    protected OracleConnection Conn;

    public Notice()
    {
      this.MySMTP = new Notice.SMTP();
      this.Conn = new OracleConnection(Settings.Default.ConnectionString);
    }

    public void Send(SanupsNetMonitor.DataSet.Monitor.T_MONITOR_HISTORYDataTable dt_history)
    {
      int index = 0;
      IMessageMaker messageMaker = new MessageMakerFuctory().Create(dt_history[index].F_RESULT.ToString());
      using (messageMaker)
      {
        string subject = messageMaker.CreateSubject(dt_history);
        string body = messageMaker.CreateBody(dt_history);
        List<string> attachFiles = messageMaker.CreateAttachFiles(dt_history);
        StringBuilder stringBuilder = new StringBuilder(1024);
        stringBuilder.Append(" SELECT");
        stringBuilder.Append("   T_MONITOR_NOTICE.F_ACCOUNT");
        stringBuilder.Append("   ,T_MONITOR_NOTICE.F_PLANT_CD");
        stringBuilder.Append("   ,T_MONITOR_NOTICE.F_LEVEL");
        stringBuilder.Append("   ,T_ADDRESSES.F_MAIL");
        stringBuilder.Append(" FROM T_ADDRESSES, T_MONITOR_NOTICE");
        stringBuilder.Append(" WHERE T_ADDRESSES.F_ACCOUNT = T_MONITOR_NOTICE.F_ACCOUNT");
        SanupsNetMonitor.DataSet.Notice.MailToDataTable mailToDataTable = new SanupsNetMonitor.DataSet.Notice.MailToDataTable();
        OracleDataAdapter oracleDataAdapter = new OracleDataAdapter(stringBuilder.ToString(), this.Conn);
        ((DbConnection) this.Conn).Open();
        ((DbDataAdapter) oracleDataAdapter).Fill((DataTable) mailToDataTable);
        ((DbConnection) this.Conn).Close();
        List<string> to = new List<string>();
        foreach (SanupsNetMonitor.DataSet.Notice.MailToRow mailToRow in (TypedTableBase<SanupsNetMonitor.DataSet.Notice.MailToRow>) mailToDataTable)
          to.Add(mailToRow.F_MAIL);
        new Notice.SMTP().Send(to, subject, body, attachFiles);
      }
    }

    protected class SMTP
    {
      private string HostName;
      private int Port;

      public SMTP()
      {
        SystemParameter systemParameter = new SystemParameter();
        try
        {
          systemParameter.GetByGroupId(SystemParameter.GroupId.MailSendSetting);
          this.HostName = ConfigurationManager.AppSettings["SMTP_HOST"];
          this.Port = int.Parse(ConfigurationManager.AppSettings["SMTP_PORT"]);
        }
        finally
        {
          systemParameter.Dispose();
        }
      }

      public void Send(List<string> to, string subject, string body, List<string> attachfiles)
      {
        string[] array = to.ToArray();
        string[] strArray = new string[0];
        this.Send(array, strArray, strArray, subject, body, attachfiles);
      }

      public void Send(
        string[] to,
        string[] cc,
        string[] bcc,
        string subject,
        string body,
        List<string> attachfiles)
      {
        using (new MailMessage())
        {
          TKMP.Writer.MailWriter mailWriter = new TKMP.Writer.MailWriter();
          mailWriter.FromAddress = "sanups.net.monitor@sanups.net";
          mailWriter.Headers.Add("From", "SANUPS.NET MONITOR <sanups.net.monitor@sanups.net>");
          foreach (string str in to)
          {
            mailWriter.ToAddressList.Add(str);
            mailWriter.Headers.Add("To", str);
          }
          foreach (string str in cc)
          {
            mailWriter.ToAddressList.Add(str);
            mailWriter.Headers.Add("CC", str);
          }
          foreach (string str in bcc)
          {
            mailWriter.ToAddressList.Add(str);
            mailWriter.Headers.Add("BCC", str);
          }
          mailWriter.Headers.Add("Subject", subject);
          MultiPart multiPart = new MultiPart();
          multiPart.AddPart((IPart) new TextPart(body));
          if (attachfiles != null)
          {
            foreach (string attachfile in attachfiles)
              multiPart.AddPart((IPart) new FilePart(attachfile));
          }
          mailWriter.MainPart = (IPart) multiPart;
          TKMP.Net.SmtpClient smtpClient = new TKMP.Net.SmtpClient(this.GetIpAddress(ConfigurationManager.AppSettings["SMTP_HOST"]), int.Parse(ConfigurationManager.AppSettings["SMTP_PORT"]), (ISmtpLogon) new AuthCramMd5(ConfigurationManager.AppSettings["SMTP_UID"], ConfigurationManager.AppSettings["SMTP_PASSWORD"]));
          if (!smtpClient.Connect())
            throw new ApplicationException("SMTPサーバーへの接続に失敗しました。");
          smtpClient.SendMail((IMailWriter) mailWriter);
          smtpClient.Close();
        }
      }

      private IPAddress GetIpAddress(string host)
      {
        IPAddress[] hostAddresses = Dns.GetHostAddresses(host);
        return hostAddresses.Length != 0 ? hostAddresses[0] : throw new ApplicationException("ホスト名の解決に失敗しました。");
      }
    }
  }
}
