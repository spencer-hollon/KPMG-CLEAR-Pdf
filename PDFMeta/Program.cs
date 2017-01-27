using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


using System.Collections.ObjectModel;
using System.Xml;
using System.Xml.Serialization;




namespace PDFMeta
{
    class Program
    {
        static void Main(string[] args)
        {

            /*  SAMPLE subGrid PDF MetaObj  */
            PDFFillMeta templateXML = new PDFFillMeta();

            templateXML.isSubPDF = true;
            templateXML.numSubGridRow = 2;

            PDFFillMeta.textMapField sampleText1 = new PDFFillMeta.textMapField();
            sampleText1.crmAttributeName = "parent.clear_listidnumber";
            sampleText1.acroFieldName = "pg1-3 Comm ID#";

            templateXML.textFields.Add(sampleText1);

            PDFFillMeta.subGridColumn row1Column1 = new PDFFillMeta.subGridColumn();
            row1Column1.isConditional = false;
            row1Column1.textField.crmAttributeName = "clear_datereceived";
            row1Column1.textField.acroFieldName = "pg4-Sch A/date1";

            PDFFillMeta.subGridColumn row1Column2 = new PDFFillMeta.subGridColumn();
            row1Column2.isConditional = true;
            row1Column2.checkField.crmAttributeName = "clear_contributorcode";
            PDFFillMeta.conditionalMap column2Map1 = new PDFFillMeta.conditionalMap();
            column2Map1.keyVal = 409830000;
            List<string> sampleCheckFieldList = new List<string>();
            sampleCheckFieldList.Add("pg4-Sch A/cc/ind1");
            column2Map1.fieldList = sampleCheckFieldList;
            row1Column2.checkField.conditionalsList.Add(column2Map1);


            PDFFillMeta.conditionalMap column2Map2 = new PDFFillMeta.conditionalMap();
            column2Map2.keyVal = 40983000;
            List<string> sampleCheckFieldList2 = new List<string>();
            sampleCheckFieldList2.Add("pg4-Sch A/cc/comm1");
            column2Map2.fieldList = sampleCheckFieldList2;
            row1Column2.checkField.conditionalsList.Add(column2Map2);

            PDFFillMeta.subGridRow row1 = new PDFFillMeta.subGridRow();
            row1.rowMap.Add(row1Column1);
            row1.rowMap.Add(row1Column2);

            templateXML.subGridRows.Add(row1);

            XmlSerializer XMLer = new XmlSerializer(templateXML.GetType());
            XMLer.Serialize(Console.Out, templateXML);
            /*  SAMPLE PDFFill MetaObj    */
            /*
            PDFFillMeta templateXML = new PDFFillMeta();
            List<string> sampleList1 = new List<string>();
            sampleList1.Add("pg1-1 type of comm/oh-can");
            sampleList1.Add("pg1-1 type of comm/oh-can/state");

            List<string> sampleList2 = new List<string>();
            sampleList2.Add("pg1-1 type of comm/oh-can");
            sampleList2.Add("pg1-1 type of comm/oh-can/Recall");

            PDFFillMeta.conditionalCheckbox sampleCondition1 = new PDFFillMeta.conditionalCheckbox();
            sampleCondition1.crmAttributeName = "clear_controlledcommitteetype";
            PDFFillMeta.conditionalMap conField1 = new PDFFillMeta.conditionalMap();
            conField1.keyVal = 409830000;
            conField1.fieldList = sampleList1;
            sampleCondition1.conditionalsList.Add(conField1);

            PDFFillMeta.conditionalMap conField2 = new PDFFillMeta.conditionalMap();
            conField2.keyVal = 409830001;
            conField2.fieldList = sampleList2;
            sampleCondition1.conditionalsList.Add(conField2);


            List<PDFFillMeta.textMapField> SampleTextFieldList = new List<PDFFillMeta.textMapField>();
            PDFFillMeta.textMapField sampleTextField1 = new PDFFillMeta.textMapField();
            sampleTextField1.crmAttributeName = "clear_statementperiodfrom";
            sampleTextField1.acroFieldName = "pg1-stmt cvrs - from";
            
            PDFFillMeta.textMapField sampleTextField2 = new PDFFillMeta.textMapField();
            sampleTextField2.crmAttributeName = "clear_statementperiodthrough";
            sampleTextField2.acroFieldName = "pg1-stmt cvrs - through";

            SampleTextFieldList.Add(sampleTextField1);
            SampleTextFieldList.Add(sampleTextField2);

            templateXML.textFields = SampleTextFieldList;
            templateXML.conditionalCheckboxes.Add(sampleCondition1);

            PDFFillMeta.subGridPDF scheduleA = new PDFFillMeta.subGridPDF();
            scheduleA.relatedEntityName = "460MoneyContrib";
            scheduleA.relatedPDF = "460SchedA";

            templateXML.subGrids.Add(scheduleA);


            XmlSerializer XMLer = new XmlSerializer(templateXML.GetType());
            XMLer.Serialize(Console.Out, templateXML);
            */
        }
    }




}