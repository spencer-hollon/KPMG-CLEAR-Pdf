using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


using System.Collections.ObjectModel;
using System.Xml;
using System.Xml.Serialization;


/*  change  */

namespace PDFMeta
{
    class Program
    {
        static void Main(string[] args)
        {

            /*  SAMPLE subGrid PDF MetaObj  */
            /*
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
            */
            /*  SAMPLE PDFFill MetaObj    */
            
            PDFFillMeta templateXML = new PDFFillMeta();
            templateXML.CRMEntityName = "clear_campaignstatement";
            templateXML.PDFFileName = "casos460";

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

            PDFFillMeta.textMapField sampleTextFielda = new PDFFillMeta.textMapField();
            sampleTextFielda.crmAttributeName = "clear_committeename";
            sampleTextFielda.acroFieldName = "pg1-3 comm name";
            sampleTextFielda.isEntityRef = true;
            SampleTextFieldList.Add(sampleTextFielda);


            PDFFillMeta.textMapField sampleTextFieldb = new PDFFillMeta.textMapField();
            sampleTextFieldb.crmAttributeName = "clear_listidnumber";
            sampleTextFieldb.acroFieldName = "pg1 - 3 Comm ID#";
            SampleTextFieldList.Add(sampleTextFieldb);
            

            PDFFillMeta.textMapField sampleTextField1 = new PDFFillMeta.textMapField();
            sampleTextField1.crmAttributeName = "clear_statementperiodfrom";
            sampleTextField1.acroFieldName = "pg1-stmt cvrs - from";
            sampleTextField1.isDate = true;
            SampleTextFieldList.Add(sampleTextField1);

            PDFFillMeta.textMapField sampleTextField2 = new PDFFillMeta.textMapField();
            sampleTextField2.crmAttributeName = "clear_statementperiodthrough";
            sampleTextField2.acroFieldName = "pg1-stmt cvrs - through";
            sampleTextField2.isDate = true;
            SampleTextFieldList.Add(sampleTextField2);

            PDFFillMeta.textMapField sampleTextField3 = new PDFFillMeta.textMapField();
            sampleTextField3.crmAttributeName = "clear_streetaddress";
            sampleTextField3.acroFieldName = "pg1-3 comm street address";
            SampleTextFieldList.Add(sampleTextField3);

            PDFFillMeta.textMapField sampleTextField4 = new PDFFillMeta.textMapField();
            sampleTextField4.crmAttributeName = "clear_city";
            sampleTextField4.acroFieldName = "pg1-3 comm city";
            SampleTextFieldList.Add(sampleTextField4);

            PDFFillMeta.textMapField sampleTextField5 = new PDFFillMeta.textMapField();
            sampleTextField5.crmAttributeName = "clear_state";
            sampleTextField5.acroFieldName = "pg1-3 comm state";
            SampleTextFieldList.Add(sampleTextField5);

            PDFFillMeta.textMapField sampleTextField6 = new PDFFillMeta.textMapField();
            sampleTextField6.crmAttributeName = "clear_zipcode";
            sampleTextField6.acroFieldName = "pg1-3 comm zip code";
            SampleTextFieldList.Add(sampleTextField6);

            PDFFillMeta.textMapField sampleTextField7 = new PDFFillMeta.textMapField();
            sampleTextField7.crmAttributeName = "clear_phone";
            sampleTextField7.acroFieldName = "pg1-3 comm ph#";
            SampleTextFieldList.Add(sampleTextField7);

            PDFFillMeta.textMapField sampleTextField8 = new PDFFillMeta.textMapField();
            sampleTextField8.crmAttributeName = "clear_treasurername";
            sampleTextField8.acroFieldName = "pg1-3 Treas/name of treas";
            SampleTextFieldList.Add(sampleTextField8);

            PDFFillMeta.textMapField sampleTextField9 = new PDFFillMeta.textMapField();
            sampleTextField9.crmAttributeName = "clear_treasureraddress";
            sampleTextField9.acroFieldName = "pg1-3 Treas/address";
            SampleTextFieldList.Add(sampleTextField9);

            PDFFillMeta.textMapField sampleTextField10 = new PDFFillMeta.textMapField();
            sampleTextField10.crmAttributeName = "clear_treasurercity";
            sampleTextField10.acroFieldName = "pg1-3 Teas/city";
            SampleTextFieldList.Add(sampleTextField10);

            PDFFillMeta.textMapField sampleTextField11 = new PDFFillMeta.textMapField();
            sampleTextField11.crmAttributeName = "clear_treasurerstate";
            sampleTextField11.acroFieldName = "pg1-3 Treas/state";
            SampleTextFieldList.Add(sampleTextField11);

            PDFFillMeta.textMapField sampleTextField12 = new PDFFillMeta.textMapField();
            sampleTextField12.crmAttributeName = "clear_treasurerzipcode";
            sampleTextField12.acroFieldName = "pg1-3 Treas/zip code";
            SampleTextFieldList.Add(sampleTextField12);

            PDFFillMeta.textMapField sampleTextField13 = new PDFFillMeta.textMapField();
            sampleTextField13.crmAttributeName = "clear_treasurerphone";
            sampleTextField13.acroFieldName = "pg1-3 Treas/ph#";
            SampleTextFieldList.Add(sampleTextField13);

            PDFFillMeta.textMapField sampleTextField14= new PDFFillMeta.textMapField();
            sampleTextField14.crmAttributeName = "clear_committeename";
            sampleTextField14.isEntityRef = true;
            sampleTextField14.acroFieldName = "pg3 / name of filer";
            SampleTextFieldList.Add(sampleTextField14);

            PDFFillMeta.textMapField sampleTextField15 = new PDFFillMeta.textMapField();
            sampleTextField15.crmAttributeName = "clear_listidnumber";
            sampleTextField15.acroFieldName = "pg1-3 Comm ID#";
            SampleTextFieldList.Add(sampleTextField15);

            PDFFillMeta.textMapField sampleTextField16 = new PDFFillMeta.textMapField();
            sampleTextField16.crmAttributeName = "clear_totalmonetarycontributions";
            sampleTextField16.acroFieldName = "pg3-Col A1";
            SampleTextFieldList.Add(sampleTextField16);

            PDFFillMeta.textMapField sampleTextField17 = new PDFFillMeta.textMapField();
            sampleTextField17.crmAttributeName = "clear_statementperiodfrom";
            sampleTextField17.acroFieldName = "pg3-stmt cvrs - from";
            sampleTextField17.isDate = true;
            SampleTextFieldList.Add(sampleTextField17);

            PDFFillMeta.textMapField sampleTextField18 = new PDFFillMeta.textMapField();
            sampleTextField18.crmAttributeName = "clear_statementperiodthrough";
            sampleTextField18.acroFieldName = "pg3-stmt cvrs - through";
            sampleTextField18.isDate = true;
            SampleTextFieldList.Add(sampleTextField18);



            templateXML.textFields = SampleTextFieldList;
            templateXML.conditionalCheckboxes.Add(sampleCondition1);

            PDFFillMeta.subGridPDF scheduleA = new PDFFillMeta.subGridPDF();
            scheduleA.relatedEntityName = "460MoneyContrib";
            scheduleA.relatedPDF = "460SchedA";

            templateXML.subGrids.Add(scheduleA);


            XmlSerializer XMLer = new XmlSerializer(templateXML.GetType());
            XMLer.Serialize(Console.Out, templateXML);
            
        }
    }




}