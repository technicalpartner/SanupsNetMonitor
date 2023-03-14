// Decompiled with JetBrains decompiler
// Type: SanupsNetMonitor.DataSet.Monitor
// Assembly: SanupsNetMonitor, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CAE8435A-29A0-4A51-923B-367788891E7C
// Assembly location: C:\work\SanupsNetMonitor\bin\SanupsNetMonitor.exe

using System;
using System.CodeDom.Compiler;
using System.Collections;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Data;
using System.Diagnostics;
using System.IO;
using System.Runtime.Serialization;
using System.Text;
using System.Xml;
using System.Xml.Schema;
using System.Xml.Serialization;

namespace SanupsNetMonitor.DataSet
{
  [HelpKeyword("vs.data.DataSet")]
  [XmlSchemaProvider("GetTypedDataSetSchema")]
  [XmlRoot("Monitor")]
  [ToolboxItem(true)]
  [DesignerCategory("code")]
  [Serializable]
  public class Monitor : System.Data.DataSet
  {
    private Monitor.T_MONITOR_HISTORYDataTable tableT_MONITOR_HISTORY;
    private SchemaSerializationMode _schemaSerializationMode = SchemaSerializationMode.IncludeSchema;

    [DebuggerNonUserCode]
    [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
    public Monitor()
    {
      this.BeginInit();
      this.InitClass();
      CollectionChangeEventHandler changeEventHandler = new CollectionChangeEventHandler(this.SchemaChanged);
      base.Tables.CollectionChanged += changeEventHandler;
      base.Relations.CollectionChanged += changeEventHandler;
      this.EndInit();
    }

    [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
    [DebuggerNonUserCode]
    protected Monitor(SerializationInfo info, StreamingContext context)
      : base(info, context, false)
    {
      if (this.IsBinarySerialized(info, context))
      {
        this.InitVars(false);
        CollectionChangeEventHandler changeEventHandler = new CollectionChangeEventHandler(this.SchemaChanged);
        this.Tables.CollectionChanged += changeEventHandler;
        this.Relations.CollectionChanged += changeEventHandler;
      }
      else
      {
        string s = (string) info.GetValue("XmlSchema", typeof (string));
        if (this.DetermineSchemaSerializationMode(info, context) == SchemaSerializationMode.IncludeSchema)
        {
          System.Data.DataSet dataSet = new System.Data.DataSet();
          dataSet.ReadXmlSchema((XmlReader) new XmlTextReader((TextReader) new StringReader(s)));
          if (dataSet.Tables[nameof (T_MONITOR_HISTORY)] != null)
            base.Tables.Add((DataTable) new Monitor.T_MONITOR_HISTORYDataTable(dataSet.Tables[nameof (T_MONITOR_HISTORY)]));
          this.DataSetName = dataSet.DataSetName;
          this.Prefix = dataSet.Prefix;
          this.Namespace = dataSet.Namespace;
          this.Locale = dataSet.Locale;
          this.CaseSensitive = dataSet.CaseSensitive;
          this.EnforceConstraints = dataSet.EnforceConstraints;
          this.Merge(dataSet, false, MissingSchemaAction.Add);
          this.InitVars();
        }
        else
          this.ReadXmlSchema((XmlReader) new XmlTextReader((TextReader) new StringReader(s)));
        this.GetSerializationData(info, context);
        CollectionChangeEventHandler changeEventHandler = new CollectionChangeEventHandler(this.SchemaChanged);
        base.Tables.CollectionChanged += changeEventHandler;
        this.Relations.CollectionChanged += changeEventHandler;
      }
    }

    [DebuggerNonUserCode]
    [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    public Monitor.T_MONITOR_HISTORYDataTable T_MONITOR_HISTORY => this.tableT_MONITOR_HISTORY;

    [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
    [DebuggerNonUserCode]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
    [Browsable(true)]
    public override SchemaSerializationMode SchemaSerializationMode
    {
      get => this._schemaSerializationMode;
      set => this._schemaSerializationMode = value;
    }

    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    [DebuggerNonUserCode]
    [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
    public new DataTableCollection Tables => base.Tables;

    [DebuggerNonUserCode]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
    public new DataRelationCollection Relations => base.Relations;

    [DebuggerNonUserCode]
    [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
    protected override void InitializeDerivedDataSet()
    {
      this.BeginInit();
      this.InitClass();
      this.EndInit();
    }

    [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
    [DebuggerNonUserCode]
    public override System.Data.DataSet Clone()
    {
      Monitor monitor = (Monitor) base.Clone();
      monitor.InitVars();
      monitor.SchemaSerializationMode = this.SchemaSerializationMode;
      return (System.Data.DataSet) monitor;
    }

    [DebuggerNonUserCode]
    [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
    protected override bool ShouldSerializeTables() => false;

    [DebuggerNonUserCode]
    [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
    protected override bool ShouldSerializeRelations() => false;

    [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
    [DebuggerNonUserCode]
    protected override void ReadXmlSerializable(XmlReader reader)
    {
      if (this.DetermineSchemaSerializationMode(reader) == SchemaSerializationMode.IncludeSchema)
      {
        this.Reset();
        System.Data.DataSet dataSet = new System.Data.DataSet();
        int num = (int) dataSet.ReadXml(reader);
        if (dataSet.Tables["T_MONITOR_HISTORY"] != null)
          base.Tables.Add((DataTable) new Monitor.T_MONITOR_HISTORYDataTable(dataSet.Tables["T_MONITOR_HISTORY"]));
        this.DataSetName = dataSet.DataSetName;
        this.Prefix = dataSet.Prefix;
        this.Namespace = dataSet.Namespace;
        this.Locale = dataSet.Locale;
        this.CaseSensitive = dataSet.CaseSensitive;
        this.EnforceConstraints = dataSet.EnforceConstraints;
        this.Merge(dataSet, false, MissingSchemaAction.Add);
        this.InitVars();
      }
      else
      {
        int num = (int) this.ReadXml(reader);
        this.InitVars();
      }
    }

    [DebuggerNonUserCode]
    [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
    protected override XmlSchema GetSchemaSerializable()
    {
      MemoryStream memoryStream = new MemoryStream();
      this.WriteXmlSchema((XmlWriter) new XmlTextWriter((Stream) memoryStream, (Encoding) null));
      memoryStream.Position = 0L;
      return XmlSchema.Read((XmlReader) new XmlTextReader((Stream) memoryStream), (ValidationEventHandler) null);
    }

    [DebuggerNonUserCode]
    [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
    internal void InitVars() => this.InitVars(true);

    [DebuggerNonUserCode]
    [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
    internal void InitVars(bool initTable)
    {
      this.tableT_MONITOR_HISTORY = (Monitor.T_MONITOR_HISTORYDataTable) base.Tables["T_MONITOR_HISTORY"];
      if (!initTable || this.tableT_MONITOR_HISTORY == null)
        return;
      this.tableT_MONITOR_HISTORY.InitVars();
    }

    [DebuggerNonUserCode]
    [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
    private void InitClass()
    {
      this.DataSetName = nameof (Monitor);
      this.Prefix = "";
      this.Namespace = "http://tempuri.org/Monitor.xsd";
      this.EnforceConstraints = true;
      this.SchemaSerializationMode = SchemaSerializationMode.IncludeSchema;
      this.tableT_MONITOR_HISTORY = new Monitor.T_MONITOR_HISTORYDataTable();
      base.Tables.Add((DataTable) this.tableT_MONITOR_HISTORY);
    }

    [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
    [DebuggerNonUserCode]
    private bool ShouldSerializeT_MONITOR_HISTORY() => false;

    [DebuggerNonUserCode]
    [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
    private void SchemaChanged(object sender, CollectionChangeEventArgs e)
    {
      if (e.Action != CollectionChangeAction.Remove)
        return;
      this.InitVars();
    }

    [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
    [DebuggerNonUserCode]
    public static XmlSchemaComplexType GetTypedDataSetSchema(XmlSchemaSet xs)
    {
      Monitor monitor = new Monitor();
      XmlSchemaComplexType typedDataSetSchema = new XmlSchemaComplexType();
      XmlSchemaSequence xmlSchemaSequence = new XmlSchemaSequence();
      xmlSchemaSequence.Items.Add((XmlSchemaObject) new XmlSchemaAny()
      {
        Namespace = monitor.Namespace
      });
      typedDataSetSchema.Particle = (XmlSchemaParticle) xmlSchemaSequence;
      XmlSchema schemaSerializable = monitor.GetSchemaSerializable();
      if (xs.Contains(schemaSerializable.TargetNamespace))
      {
        MemoryStream memoryStream1 = new MemoryStream();
        MemoryStream memoryStream2 = new MemoryStream();
        try
        {
          schemaSerializable.Write((Stream) memoryStream1);
          IEnumerator enumerator = xs.Schemas(schemaSerializable.TargetNamespace).GetEnumerator();
          while (enumerator.MoveNext())
          {
            XmlSchema current = (XmlSchema) enumerator.Current;
            memoryStream2.SetLength(0L);
            current.Write((Stream) memoryStream2);
            if (memoryStream1.Length == memoryStream2.Length)
            {
              memoryStream1.Position = 0L;
              memoryStream2.Position = 0L;
              do
                ;
              while (memoryStream1.Position != memoryStream1.Length && memoryStream1.ReadByte() == memoryStream2.ReadByte());
              if (memoryStream1.Position == memoryStream1.Length)
                return typedDataSetSchema;
            }
          }
        }
        finally
        {
          memoryStream1?.Close();
          memoryStream2?.Close();
        }
      }
      xs.Add(schemaSerializable);
      return typedDataSetSchema;
    }

    [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
    public delegate void T_MONITOR_HISTORYRowChangeEventHandler(
      object sender,
      Monitor.T_MONITOR_HISTORYRowChangeEvent e);

    [XmlSchemaProvider("GetTypedTableSchema")]
    [Serializable]
    public class T_MONITOR_HISTORYDataTable : TypedTableBase<Monitor.T_MONITOR_HISTORYRow>
    {
      private DataColumn columnF_MONITOR_NAME;
      private DataColumn columnF_PLANT_CD;
      private DataColumn columnF_PLANT_NM;
      private DataColumn columnF_UNITNO;
      private DataColumn columnF_MEASURE_DATE;
      private DataColumn columnF_SUBSCRIBER_ACT;
      private DataColumn columnF_SUBSCRIBER_NM;
      private DataColumn columnF_SCHEDULE;
      private DataColumn columnF_EXECUTE_DATE;
      private DataColumn columnF_LEVEL;
      private DataColumn columnF_RESULT;
      private DataColumn columnF_DETAIL;
      private DataColumn columnF_SEND_DONE;
      private DataColumn columnCREATED;
      private DataColumn columnMODIFIED;

      [DebuggerNonUserCode]
      [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
      public T_MONITOR_HISTORYDataTable()
      {
        this.TableName = "T_MONITOR_HISTORY";
        this.BeginInit();
        this.InitClass();
        this.EndInit();
      }

      [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
      [DebuggerNonUserCode]
      internal T_MONITOR_HISTORYDataTable(DataTable table)
      {
        this.TableName = table.TableName;
        if (table.CaseSensitive != table.DataSet.CaseSensitive)
          this.CaseSensitive = table.CaseSensitive;
        if (table.Locale.ToString() != table.DataSet.Locale.ToString())
          this.Locale = table.Locale;
        if (table.Namespace != table.DataSet.Namespace)
          this.Namespace = table.Namespace;
        this.Prefix = table.Prefix;
        this.MinimumCapacity = table.MinimumCapacity;
      }

      [DebuggerNonUserCode]
      [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
      protected T_MONITOR_HISTORYDataTable(SerializationInfo info, StreamingContext context)
        : base(info, context)
      {
        this.InitVars();
      }

      [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
      [DebuggerNonUserCode]
      public DataColumn F_MONITOR_NAMEColumn => this.columnF_MONITOR_NAME;

      [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
      [DebuggerNonUserCode]
      public DataColumn F_PLANT_CDColumn => this.columnF_PLANT_CD;

      [DebuggerNonUserCode]
      [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
      public DataColumn F_PLANT_NMColumn => this.columnF_PLANT_NM;

      [DebuggerNonUserCode]
      [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
      public DataColumn F_UNITNOColumn => this.columnF_UNITNO;

      [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
      [DebuggerNonUserCode]
      public DataColumn F_MEASURE_DATEColumn => this.columnF_MEASURE_DATE;

      [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
      [DebuggerNonUserCode]
      public DataColumn F_SUBSCRIBER_ACTColumn => this.columnF_SUBSCRIBER_ACT;

      [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
      [DebuggerNonUserCode]
      public DataColumn F_SUBSCRIBER_NMColumn => this.columnF_SUBSCRIBER_NM;

      [DebuggerNonUserCode]
      [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
      public DataColumn F_SCHEDULEColumn => this.columnF_SCHEDULE;

      [DebuggerNonUserCode]
      [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
      public DataColumn F_EXECUTE_DATEColumn => this.columnF_EXECUTE_DATE;

      [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
      [DebuggerNonUserCode]
      public DataColumn F_LEVELColumn => this.columnF_LEVEL;

      [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
      [DebuggerNonUserCode]
      public DataColumn F_RESULTColumn => this.columnF_RESULT;

      [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
      [DebuggerNonUserCode]
      public DataColumn F_DETAILColumn => this.columnF_DETAIL;

      [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
      [DebuggerNonUserCode]
      public DataColumn F_SEND_DONEColumn => this.columnF_SEND_DONE;

      [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
      [DebuggerNonUserCode]
      public DataColumn CREATEDColumn => this.columnCREATED;

      [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
      [DebuggerNonUserCode]
      public DataColumn MODIFIEDColumn => this.columnMODIFIED;

      [DebuggerNonUserCode]
      [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
      [Browsable(false)]
      public int Count => this.Rows.Count;

      [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
      [DebuggerNonUserCode]
      public Monitor.T_MONITOR_HISTORYRow this[int index] => (Monitor.T_MONITOR_HISTORYRow) this.Rows[index];

      [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
      public event Monitor.T_MONITOR_HISTORYRowChangeEventHandler T_MONITOR_HISTORYRowChanging;

      [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
      public event Monitor.T_MONITOR_HISTORYRowChangeEventHandler T_MONITOR_HISTORYRowChanged;

      [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
      public event Monitor.T_MONITOR_HISTORYRowChangeEventHandler T_MONITOR_HISTORYRowDeleting;

      [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
      public event Monitor.T_MONITOR_HISTORYRowChangeEventHandler T_MONITOR_HISTORYRowDeleted;

      [DebuggerNonUserCode]
      [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
      public void AddT_MONITOR_HISTORYRow(Monitor.T_MONITOR_HISTORYRow row) => this.Rows.Add((DataRow) row);

      [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
      [DebuggerNonUserCode]
      public Monitor.T_MONITOR_HISTORYRow AddT_MONITOR_HISTORYRow(
        string F_MONITOR_NAME,
        Decimal F_PLANT_CD,
        string F_PLANT_NM,
        Decimal F_UNITNO,
        DateTime F_MEASURE_DATE,
        string F_SUBSCRIBER_ACT,
        string F_SUBSCRIBER_NM,
        DateTime F_SCHEDULE,
        DateTime F_EXECUTE_DATE,
        Decimal F_LEVEL,
        Decimal F_RESULT,
        string F_DETAIL,
        Decimal F_SEND_DONE,
        DateTime CREATED,
        DateTime MODIFIED)
      {
        Monitor.T_MONITOR_HISTORYRow row = (Monitor.T_MONITOR_HISTORYRow) this.NewRow();
        object[] objArray = new object[15]
        {
          (object) F_MONITOR_NAME,
          (object) F_PLANT_CD,
          (object) F_PLANT_NM,
          (object) F_UNITNO,
          (object) F_MEASURE_DATE,
          (object) F_SUBSCRIBER_ACT,
          (object) F_SUBSCRIBER_NM,
          (object) F_SCHEDULE,
          (object) F_EXECUTE_DATE,
          (object) F_LEVEL,
          (object) F_RESULT,
          (object) F_DETAIL,
          (object) F_SEND_DONE,
          (object) CREATED,
          (object) MODIFIED
        };
        row.ItemArray = objArray;
        this.Rows.Add((DataRow) row);
        return row;
      }

      [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
      [DebuggerNonUserCode]
      public override DataTable Clone()
      {
        Monitor.T_MONITOR_HISTORYDataTable historyDataTable = (Monitor.T_MONITOR_HISTORYDataTable) base.Clone();
        historyDataTable.InitVars();
        return (DataTable) historyDataTable;
      }

      [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
      [DebuggerNonUserCode]
      protected override DataTable CreateInstance() => (DataTable) new Monitor.T_MONITOR_HISTORYDataTable();

      [DebuggerNonUserCode]
      [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
      internal void InitVars()
      {
        this.columnF_MONITOR_NAME = this.Columns["F_MONITOR_NAME"];
        this.columnF_PLANT_CD = this.Columns["F_PLANT_CD"];
        this.columnF_PLANT_NM = this.Columns["F_PLANT_NM"];
        this.columnF_UNITNO = this.Columns["F_UNITNO"];
        this.columnF_MEASURE_DATE = this.Columns["F_MEASURE_DATE"];
        this.columnF_SUBSCRIBER_ACT = this.Columns["F_SUBSCRIBER_ACT"];
        this.columnF_SUBSCRIBER_NM = this.Columns["F_SUBSCRIBER_NM"];
        this.columnF_SCHEDULE = this.Columns["F_SCHEDULE"];
        this.columnF_EXECUTE_DATE = this.Columns["F_EXECUTE_DATE"];
        this.columnF_LEVEL = this.Columns["F_LEVEL"];
        this.columnF_RESULT = this.Columns["F_RESULT"];
        this.columnF_DETAIL = this.Columns["F_DETAIL"];
        this.columnF_SEND_DONE = this.Columns["F_SEND_DONE"];
        this.columnCREATED = this.Columns["CREATED"];
        this.columnMODIFIED = this.Columns["MODIFIED"];
      }

      [DebuggerNonUserCode]
      [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
      private void InitClass()
      {
        this.columnF_MONITOR_NAME = new DataColumn("F_MONITOR_NAME", typeof (string), (string) null, MappingType.Element);
        this.Columns.Add(this.columnF_MONITOR_NAME);
        this.columnF_PLANT_CD = new DataColumn("F_PLANT_CD", typeof (Decimal), (string) null, MappingType.Element);
        this.Columns.Add(this.columnF_PLANT_CD);
        this.columnF_PLANT_NM = new DataColumn("F_PLANT_NM", typeof (string), (string) null, MappingType.Element);
        this.Columns.Add(this.columnF_PLANT_NM);
        this.columnF_UNITNO = new DataColumn("F_UNITNO", typeof (Decimal), (string) null, MappingType.Element);
        this.Columns.Add(this.columnF_UNITNO);
        this.columnF_MEASURE_DATE = new DataColumn("F_MEASURE_DATE", typeof (DateTime), (string) null, MappingType.Element);
        this.Columns.Add(this.columnF_MEASURE_DATE);
        this.columnF_SUBSCRIBER_ACT = new DataColumn("F_SUBSCRIBER_ACT", typeof (string), (string) null, MappingType.Element);
        this.Columns.Add(this.columnF_SUBSCRIBER_ACT);
        this.columnF_SUBSCRIBER_NM = new DataColumn("F_SUBSCRIBER_NM", typeof (string), (string) null, MappingType.Element);
        this.Columns.Add(this.columnF_SUBSCRIBER_NM);
        this.columnF_SCHEDULE = new DataColumn("F_SCHEDULE", typeof (DateTime), (string) null, MappingType.Element);
        this.Columns.Add(this.columnF_SCHEDULE);
        this.columnF_EXECUTE_DATE = new DataColumn("F_EXECUTE_DATE", typeof (DateTime), (string) null, MappingType.Element);
        this.Columns.Add(this.columnF_EXECUTE_DATE);
        this.columnF_LEVEL = new DataColumn("F_LEVEL", typeof (Decimal), (string) null, MappingType.Element);
        this.Columns.Add(this.columnF_LEVEL);
        this.columnF_RESULT = new DataColumn("F_RESULT", typeof (Decimal), (string) null, MappingType.Element);
        this.Columns.Add(this.columnF_RESULT);
        this.columnF_DETAIL = new DataColumn("F_DETAIL", typeof (string), (string) null, MappingType.Element);
        this.Columns.Add(this.columnF_DETAIL);
        this.columnF_SEND_DONE = new DataColumn("F_SEND_DONE", typeof (Decimal), (string) null, MappingType.Element);
        this.Columns.Add(this.columnF_SEND_DONE);
        this.columnCREATED = new DataColumn("CREATED", typeof (DateTime), (string) null, MappingType.Element);
        this.Columns.Add(this.columnCREATED);
        this.columnMODIFIED = new DataColumn("MODIFIED", typeof (DateTime), (string) null, MappingType.Element);
        this.Columns.Add(this.columnMODIFIED);
        this.columnF_MONITOR_NAME.AllowDBNull = false;
        this.columnF_MONITOR_NAME.MaxLength = 20;
        this.columnF_PLANT_CD.AllowDBNull = false;
        this.columnF_PLANT_NM.AllowDBNull = false;
        this.columnF_PLANT_NM.MaxLength = 64;
        this.columnF_SUBSCRIBER_ACT.MaxLength = 20;
        this.columnF_SUBSCRIBER_NM.MaxLength = 40;
        this.columnF_EXECUTE_DATE.AllowDBNull = false;
        this.columnF_LEVEL.AllowDBNull = false;
        this.columnF_RESULT.AllowDBNull = false;
        this.columnF_DETAIL.AllowDBNull = false;
        this.columnF_DETAIL.MaxLength = 200;
        this.columnF_SEND_DONE.AllowDBNull = false;
      }

      [DebuggerNonUserCode]
      [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
      public Monitor.T_MONITOR_HISTORYRow NewT_MONITOR_HISTORYRow() => (Monitor.T_MONITOR_HISTORYRow) this.NewRow();

      [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
      [DebuggerNonUserCode]
      protected override DataRow NewRowFromBuilder(DataRowBuilder builder) => (DataRow) new Monitor.T_MONITOR_HISTORYRow(builder);

      [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
      [DebuggerNonUserCode]
      protected override Type GetRowType() => typeof (Monitor.T_MONITOR_HISTORYRow);

      [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
      [DebuggerNonUserCode]
      protected override void OnRowChanged(DataRowChangeEventArgs e)
      {
        base.OnRowChanged(e);
        if (this.T_MONITOR_HISTORYRowChanged == null)
          return;
        this.T_MONITOR_HISTORYRowChanged((object) this, new Monitor.T_MONITOR_HISTORYRowChangeEvent((Monitor.T_MONITOR_HISTORYRow) e.Row, e.Action));
      }

      [DebuggerNonUserCode]
      [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
      protected override void OnRowChanging(DataRowChangeEventArgs e)
      {
        base.OnRowChanging(e);
        if (this.T_MONITOR_HISTORYRowChanging == null)
          return;
        this.T_MONITOR_HISTORYRowChanging((object) this, new Monitor.T_MONITOR_HISTORYRowChangeEvent((Monitor.T_MONITOR_HISTORYRow) e.Row, e.Action));
      }

      [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
      [DebuggerNonUserCode]
      protected override void OnRowDeleted(DataRowChangeEventArgs e)
      {
        base.OnRowDeleted(e);
        if (this.T_MONITOR_HISTORYRowDeleted == null)
          return;
        this.T_MONITOR_HISTORYRowDeleted((object) this, new Monitor.T_MONITOR_HISTORYRowChangeEvent((Monitor.T_MONITOR_HISTORYRow) e.Row, e.Action));
      }

      [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
      [DebuggerNonUserCode]
      protected override void OnRowDeleting(DataRowChangeEventArgs e)
      {
        base.OnRowDeleting(e);
        if (this.T_MONITOR_HISTORYRowDeleting == null)
          return;
        this.T_MONITOR_HISTORYRowDeleting((object) this, new Monitor.T_MONITOR_HISTORYRowChangeEvent((Monitor.T_MONITOR_HISTORYRow) e.Row, e.Action));
      }

      [DebuggerNonUserCode]
      [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
      public void RemoveT_MONITOR_HISTORYRow(Monitor.T_MONITOR_HISTORYRow row) => this.Rows.Remove((DataRow) row);

      [DebuggerNonUserCode]
      [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
      public static XmlSchemaComplexType GetTypedTableSchema(XmlSchemaSet xs)
      {
        XmlSchemaComplexType typedTableSchema = new XmlSchemaComplexType();
        XmlSchemaSequence xmlSchemaSequence = new XmlSchemaSequence();
        Monitor monitor = new Monitor();
        XmlSchemaAny xmlSchemaAny1 = new XmlSchemaAny();
        xmlSchemaAny1.Namespace = "http://www.w3.org/2001/XMLSchema";
        xmlSchemaAny1.MinOccurs = 0M;
        xmlSchemaAny1.MaxOccurs = Decimal.MaxValue;
        xmlSchemaAny1.ProcessContents = XmlSchemaContentProcessing.Lax;
        xmlSchemaSequence.Items.Add((XmlSchemaObject) xmlSchemaAny1);
        XmlSchemaAny xmlSchemaAny2 = new XmlSchemaAny();
        xmlSchemaAny2.Namespace = "urn:schemas-microsoft-com:xml-diffgram-v1";
        xmlSchemaAny2.MinOccurs = 1M;
        xmlSchemaAny2.ProcessContents = XmlSchemaContentProcessing.Lax;
        xmlSchemaSequence.Items.Add((XmlSchemaObject) xmlSchemaAny2);
        typedTableSchema.Attributes.Add((XmlSchemaObject) new XmlSchemaAttribute()
        {
          Name = "namespace",
          FixedValue = monitor.Namespace
        });
        typedTableSchema.Attributes.Add((XmlSchemaObject) new XmlSchemaAttribute()
        {
          Name = "tableTypeName",
          FixedValue = nameof (T_MONITOR_HISTORYDataTable)
        });
        typedTableSchema.Particle = (XmlSchemaParticle) xmlSchemaSequence;
        XmlSchema schemaSerializable = monitor.GetSchemaSerializable();
        if (xs.Contains(schemaSerializable.TargetNamespace))
        {
          MemoryStream memoryStream1 = new MemoryStream();
          MemoryStream memoryStream2 = new MemoryStream();
          try
          {
            schemaSerializable.Write((Stream) memoryStream1);
            IEnumerator enumerator = xs.Schemas(schemaSerializable.TargetNamespace).GetEnumerator();
            while (enumerator.MoveNext())
            {
              XmlSchema current = (XmlSchema) enumerator.Current;
              memoryStream2.SetLength(0L);
              current.Write((Stream) memoryStream2);
              if (memoryStream1.Length == memoryStream2.Length)
              {
                memoryStream1.Position = 0L;
                memoryStream2.Position = 0L;
                do
                  ;
                while (memoryStream1.Position != memoryStream1.Length && memoryStream1.ReadByte() == memoryStream2.ReadByte());
                if (memoryStream1.Position == memoryStream1.Length)
                  return typedTableSchema;
              }
            }
          }
          finally
          {
            memoryStream1?.Close();
            memoryStream2?.Close();
          }
        }
        xs.Add(schemaSerializable);
        return typedTableSchema;
      }
    }

    public class T_MONITOR_HISTORYRow : DataRow
    {
      private Monitor.T_MONITOR_HISTORYDataTable tableT_MONITOR_HISTORY;

      [DebuggerNonUserCode]
      [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
      internal T_MONITOR_HISTORYRow(DataRowBuilder rb)
        : base(rb)
      {
        this.tableT_MONITOR_HISTORY = (Monitor.T_MONITOR_HISTORYDataTable) this.Table;
      }

      [DebuggerNonUserCode]
      [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
      public string F_MONITOR_NAME
      {
        get => (string) this[this.tableT_MONITOR_HISTORY.F_MONITOR_NAMEColumn];
        set => this[this.tableT_MONITOR_HISTORY.F_MONITOR_NAMEColumn] = (object) value;
      }

      [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
      [DebuggerNonUserCode]
      public Decimal F_PLANT_CD
      {
        get => (Decimal) this[this.tableT_MONITOR_HISTORY.F_PLANT_CDColumn];
        set => this[this.tableT_MONITOR_HISTORY.F_PLANT_CDColumn] = (object) value;
      }

      [DebuggerNonUserCode]
      [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
      public string F_PLANT_NM
      {
        get => (string) this[this.tableT_MONITOR_HISTORY.F_PLANT_NMColumn];
        set => this[this.tableT_MONITOR_HISTORY.F_PLANT_NMColumn] = (object) value;
      }

      [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
      [DebuggerNonUserCode]
      public Decimal F_UNITNO
      {
        get
        {
          try
          {
            return (Decimal) this[this.tableT_MONITOR_HISTORY.F_UNITNOColumn];
          }
          catch (InvalidCastException ex)
          {
            throw new StrongTypingException("テーブル 'T_MONITOR_HISTORY' にある列 'F_UNITNO' の値は DBNull です。", (Exception) ex);
          }
        }
        set => this[this.tableT_MONITOR_HISTORY.F_UNITNOColumn] = (object) value;
      }

      [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
      [DebuggerNonUserCode]
      public DateTime F_MEASURE_DATE
      {
        get
        {
          try
          {
            return (DateTime) this[this.tableT_MONITOR_HISTORY.F_MEASURE_DATEColumn];
          }
          catch (InvalidCastException ex)
          {
            throw new StrongTypingException("テーブル 'T_MONITOR_HISTORY' にある列 'F_MEASURE_DATE' の値は DBNull です。", (Exception) ex);
          }
        }
        set => this[this.tableT_MONITOR_HISTORY.F_MEASURE_DATEColumn] = (object) value;
      }

      [DebuggerNonUserCode]
      [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
      public string F_SUBSCRIBER_ACT
      {
        get
        {
          try
          {
            return (string) this[this.tableT_MONITOR_HISTORY.F_SUBSCRIBER_ACTColumn];
          }
          catch (InvalidCastException ex)
          {
            throw new StrongTypingException("テーブル 'T_MONITOR_HISTORY' にある列 'F_SUBSCRIBER_ACT' の値は DBNull です。", (Exception) ex);
          }
        }
        set => this[this.tableT_MONITOR_HISTORY.F_SUBSCRIBER_ACTColumn] = (object) value;
      }

      [DebuggerNonUserCode]
      [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
      public string F_SUBSCRIBER_NM
      {
        get
        {
          try
          {
            return (string) this[this.tableT_MONITOR_HISTORY.F_SUBSCRIBER_NMColumn];
          }
          catch (InvalidCastException ex)
          {
            throw new StrongTypingException("テーブル 'T_MONITOR_HISTORY' にある列 'F_SUBSCRIBER_NM' の値は DBNull です。", (Exception) ex);
          }
        }
        set => this[this.tableT_MONITOR_HISTORY.F_SUBSCRIBER_NMColumn] = (object) value;
      }

      [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
      [DebuggerNonUserCode]
      public DateTime F_SCHEDULE
      {
        get
        {
          try
          {
            return (DateTime) this[this.tableT_MONITOR_HISTORY.F_SCHEDULEColumn];
          }
          catch (InvalidCastException ex)
          {
            throw new StrongTypingException("テーブル 'T_MONITOR_HISTORY' にある列 'F_SCHEDULE' の値は DBNull です。", (Exception) ex);
          }
        }
        set => this[this.tableT_MONITOR_HISTORY.F_SCHEDULEColumn] = (object) value;
      }

      [DebuggerNonUserCode]
      [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
      public DateTime F_EXECUTE_DATE
      {
        get => (DateTime) this[this.tableT_MONITOR_HISTORY.F_EXECUTE_DATEColumn];
        set => this[this.tableT_MONITOR_HISTORY.F_EXECUTE_DATEColumn] = (object) value;
      }

      [DebuggerNonUserCode]
      [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
      public Decimal F_LEVEL
      {
        get => (Decimal) this[this.tableT_MONITOR_HISTORY.F_LEVELColumn];
        set => this[this.tableT_MONITOR_HISTORY.F_LEVELColumn] = (object) value;
      }

      [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
      [DebuggerNonUserCode]
      public Decimal F_RESULT
      {
        get => (Decimal) this[this.tableT_MONITOR_HISTORY.F_RESULTColumn];
        set => this[this.tableT_MONITOR_HISTORY.F_RESULTColumn] = (object) value;
      }

      [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
      [DebuggerNonUserCode]
      public string F_DETAIL
      {
        get => (string) this[this.tableT_MONITOR_HISTORY.F_DETAILColumn];
        set => this[this.tableT_MONITOR_HISTORY.F_DETAILColumn] = (object) value;
      }

      [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
      [DebuggerNonUserCode]
      public Decimal F_SEND_DONE
      {
        get => (Decimal) this[this.tableT_MONITOR_HISTORY.F_SEND_DONEColumn];
        set => this[this.tableT_MONITOR_HISTORY.F_SEND_DONEColumn] = (object) value;
      }

      [DebuggerNonUserCode]
      [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
      public DateTime CREATED
      {
        get
        {
          try
          {
            return (DateTime) this[this.tableT_MONITOR_HISTORY.CREATEDColumn];
          }
          catch (InvalidCastException ex)
          {
            throw new StrongTypingException("テーブル 'T_MONITOR_HISTORY' にある列 'CREATED' の値は DBNull です。", (Exception) ex);
          }
        }
        set => this[this.tableT_MONITOR_HISTORY.CREATEDColumn] = (object) value;
      }

      [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
      [DebuggerNonUserCode]
      public DateTime MODIFIED
      {
        get
        {
          try
          {
            return (DateTime) this[this.tableT_MONITOR_HISTORY.MODIFIEDColumn];
          }
          catch (InvalidCastException ex)
          {
            throw new StrongTypingException("テーブル 'T_MONITOR_HISTORY' にある列 'MODIFIED' の値は DBNull です。", (Exception) ex);
          }
        }
        set => this[this.tableT_MONITOR_HISTORY.MODIFIEDColumn] = (object) value;
      }

      [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
      [DebuggerNonUserCode]
      public bool IsF_UNITNONull() => this.IsNull(this.tableT_MONITOR_HISTORY.F_UNITNOColumn);

      [DebuggerNonUserCode]
      [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
      public void SetF_UNITNONull() => this[this.tableT_MONITOR_HISTORY.F_UNITNOColumn] = Convert.DBNull;

      [DebuggerNonUserCode]
      [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
      public bool IsF_MEASURE_DATENull() => this.IsNull(this.tableT_MONITOR_HISTORY.F_MEASURE_DATEColumn);

      [DebuggerNonUserCode]
      [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
      public void SetF_MEASURE_DATENull() => this[this.tableT_MONITOR_HISTORY.F_MEASURE_DATEColumn] = Convert.DBNull;

      [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
      [DebuggerNonUserCode]
      public bool IsF_SUBSCRIBER_ACTNull() => this.IsNull(this.tableT_MONITOR_HISTORY.F_SUBSCRIBER_ACTColumn);

      [DebuggerNonUserCode]
      [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
      public void SetF_SUBSCRIBER_ACTNull() => this[this.tableT_MONITOR_HISTORY.F_SUBSCRIBER_ACTColumn] = Convert.DBNull;

      [DebuggerNonUserCode]
      [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
      public bool IsF_SUBSCRIBER_NMNull() => this.IsNull(this.tableT_MONITOR_HISTORY.F_SUBSCRIBER_NMColumn);

      [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
      [DebuggerNonUserCode]
      public void SetF_SUBSCRIBER_NMNull() => this[this.tableT_MONITOR_HISTORY.F_SUBSCRIBER_NMColumn] = Convert.DBNull;

      [DebuggerNonUserCode]
      [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
      public bool IsF_SCHEDULENull() => this.IsNull(this.tableT_MONITOR_HISTORY.F_SCHEDULEColumn);

      [DebuggerNonUserCode]
      [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
      public void SetF_SCHEDULENull() => this[this.tableT_MONITOR_HISTORY.F_SCHEDULEColumn] = Convert.DBNull;

      [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
      [DebuggerNonUserCode]
      public bool IsCREATEDNull() => this.IsNull(this.tableT_MONITOR_HISTORY.CREATEDColumn);

      [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
      [DebuggerNonUserCode]
      public void SetCREATEDNull() => this[this.tableT_MONITOR_HISTORY.CREATEDColumn] = Convert.DBNull;

      [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
      [DebuggerNonUserCode]
      public bool IsMODIFIEDNull() => this.IsNull(this.tableT_MONITOR_HISTORY.MODIFIEDColumn);

      [DebuggerNonUserCode]
      [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
      public void SetMODIFIEDNull() => this[this.tableT_MONITOR_HISTORY.MODIFIEDColumn] = Convert.DBNull;
    }

    [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
    public class T_MONITOR_HISTORYRowChangeEvent : EventArgs
    {
      private Monitor.T_MONITOR_HISTORYRow eventRow;
      private DataRowAction eventAction;

      [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
      [DebuggerNonUserCode]
      public T_MONITOR_HISTORYRowChangeEvent(Monitor.T_MONITOR_HISTORYRow row, DataRowAction action)
      {
        this.eventRow = row;
        this.eventAction = action;
      }

      [DebuggerNonUserCode]
      [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
      public Monitor.T_MONITOR_HISTORYRow Row => this.eventRow;

      [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
      [DebuggerNonUserCode]
      public DataRowAction Action => this.eventAction;
    }
  }
}
