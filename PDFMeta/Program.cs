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
            
            PDFFillMeta templateXML = new PDFFillMeta();
            templateXML.CRMEntityName = "clear_monetarycontributions";
            templateXML.PDFFileName = "460SchedA";


            templateXML.isSubPDF = true;
            templateXML.numSubGridRow = 2;

            PDFFillMeta.textMapField sampleText1 = new PDFFillMeta.textMapField();
            sampleText1.crmAttributeName = "clear_listidnumber";
            sampleText1.isParentAttr = true;
            sampleText1.acroFieldName = "pg1-3 Comm ID#";
            templateXML.textFields.Add(sampleText1);

            PDFFillMeta.textMapField sampleText2 = new PDFFillMeta.textMapField();
            sampleText2.crmAttributeName = "parent.clear_listidnumber";
            sampleText2.acroFieldName = "pg3/name of filer";
            templateXML.textFields.Add(sampleText2);

            PDFFillMeta.textMapField sampleTextFielda = new PDFFillMeta.textMapField();
            sampleTextFielda.crmAttributeName = "clear_committeename";
            sampleTextFielda.acroFieldName = "pg1-3 comm name";
            sampleTextFielda.isParentAttr = true;
            sampleTextFielda.isEntityRef = true;
            templateXML.textFields.Add(sampleTextFielda);

            PDFFillMeta.textMapField sampleTextFieldb = new PDFFillMeta.textMapField();
            sampleTextFieldb.crmAttributeName = "clear_statementperiodfrom";
            sampleTextFieldb.acroFieldName = "pg1-stmt cvrs - from";
            sampleTextFieldb.isParentAttr = true;
            sampleTextFieldb.isDate = true;
            templateXML.textFields.Add(sampleTextFieldb);

            PDFFillMeta.textMapField sampleTextFieldc = new PDFFillMeta.textMapField();
            sampleTextFieldc.crmAttributeName = "clear_statementperiodthrough";
            sampleTextFieldc.acroFieldName = "pg1-stmt cvrs - through";
            sampleTextFieldc.isParentAttr = true;
            sampleTextFieldc.isDate = true;
            templateXML.textFields.Add(sampleTextFieldc);


            PDFFillMeta.subGridRow row1 = new PDFFillMeta.subGridRow();

            PDFFillMeta.subGridColumn row1Column1 = new PDFFillMeta.subGridColumn();
            row1Column1.textField = new PDFFillMeta.textMapField();
            row1Column1.textField.crmAttributeName = "clear_datereceived";
            row1Column1.textField.acroFieldName = "pg4-Sch A/date1";
            row1.rowMap.Add(row1Column1);

            PDFFillMeta.subGridColumn row1Column2 = new PDFFillMeta.subGridColumn();
            row1Column2.textField = new PDFFillMeta.textMapField();
            row1Column2.textField.isConcat = true;
            row1Column2.textField.crmAttributeName = "clear_name+clear_contributoraddress+clear_city_text+clear_state_text+clear_zipcode";
            row1Column2.textField.acroFieldName = "pg4-Sch A/name & address1";
            row1.rowMap.Add(row1Column2);

            PDFFillMeta.subGridColumn row1Column3 = new PDFFillMeta.subGridColumn();
            row1Column3.checkField = new PDFFillMeta.conditionalCheckbox();
            row1Column3.checkField.crmAttributeName = "clear_contributorcode";

                PDFFillMeta.conditionalMap row1Column3Map1 = new PDFFillMeta.conditionalMap();
                row1Column3Map1.keyVal = 409830000;
                List<string> sampleCheckFieldList = new List<string>();
                sampleCheckFieldList.Add("pg4-Sch A/cc/ind1");
                row1Column3Map1.fieldList = sampleCheckFieldList;
                row1Column3.checkField.conditionalsList.Add(row1Column3Map1);

                PDFFillMeta.conditionalMap row1Column3Map2 = new PDFFillMeta.conditionalMap();
                row1Column3Map1.keyVal = 409830001;
                List<string> sampleCheckFieldList2 = new List<string>();
                sampleCheckFieldList2.Add("pg4-Sch A/cc/comm1");
                row1Column3Map2.fieldList = sampleCheckFieldList2;
                row1Column3.checkField.conditionalsList.Add(row1Column3Map1);

                PDFFillMeta.conditionalMap row1Column3Map3 = new PDFFillMeta.conditionalMap();
                row1Column3Map3.keyVal = 409830002;
                List<string> sampleCheckFieldList3 = new List<string>();
                sampleCheckFieldList3.Add("pg4-Sch A/cc/oth1");
                row1Column3Map3.fieldList = sampleCheckFieldList3;
                row1Column3.checkField.conditionalsList.Add(row1Column3Map3);

                PDFFillMeta.conditionalMap row1Column3Map4 = new PDFFillMeta.conditionalMap();
                row1Column3Map4.keyVal = 409830003;
                List<string> sampleCheckFieldList4 = new List<string>();
                sampleCheckFieldList4.Add("pg4-Sch A/cc/pty1");
                row1Column3Map4.fieldList = sampleCheckFieldList4;
                row1Column3.checkField.conditionalsList.Add(row1Column3Map4);

                PDFFillMeta.conditionalMap row1Column3Map5 = new PDFFillMeta.conditionalMap();
                row1Column3Map5.keyVal = 409830004;
                List<string> sampleCheckFieldList5 = new List<string>();
                sampleCheckFieldList5.Add("pg4-Sch A/cc/scc1");
                row1Column3Map5.fieldList = sampleCheckFieldList5;
                row1Column3.checkField.conditionalsList.Add(row1Column3Map5);

            row1.rowMap.Add(row1Column3);


            PDFFillMeta.subGridColumn row1Column4 = new PDFFillMeta.subGridColumn();
            row1Column4.textField = new PDFFillMeta.textMapField();
            row1Column4.textField.isConcat = true;
            row1Column4.textField.crmAttributeName = "clear_occupation+clear_employer";
            row1Column4.textField.acroFieldName = "ppg4-Sch A/occup & emplyr1";
            row1.rowMap.Add(row1Column4);

            PDFFillMeta.subGridColumn row1Column5 = new PDFFillMeta.subGridColumn();
            row1Column5.textField = new PDFFillMeta.textMapField();
            row1Column5.textField.isMoney = true;
            row1Column5.textField.crmAttributeName = "clear_amountreceived";
            row1Column5.textField.acroFieldName = "pg4-Sch A/amt rec'd1";
            row1.rowMap.Add(row1Column5);

            PDFFillMeta.subGridColumn row1Column6 = new PDFFillMeta.subGridColumn();
            row1Column6.textField = new PDFFillMeta.textMapField();
            row1Column6.textField.isMoney= true;
            row1Column6.textField.crmAttributeName = "clear_cumulativetodate";
            row1Column6.textField.acroFieldName = "pg4-Sch A/cum cal yr1";
            row1.rowMap.Add(row1Column6);

            PDFFillMeta.subGridColumn row1Column7 = new PDFFillMeta.subGridColumn();
            row1Column7.textField = new PDFFillMeta.textMapField();
            row1Column7.textField.isMoney = true;
            row1Column7.textField.crmAttributeName = "clear_perelectiontodate";
            row1Column7.textField.acroFieldName = "pg4-Sch A/per elect1";
            row1.rowMap.Add(row1Column7);
            templateXML.subGridRows.Add(row1);



            PDFFillMeta.subGridRow row2 = new PDFFillMeta.subGridRow();

            PDFFillMeta.subGridColumn row2Column1 = new PDFFillMeta.subGridColumn();
            row2Column1.textField = new PDFFillMeta.textMapField();
            row2Column1.textField.crmAttributeName = "clear_datereceived";
            row2Column1.textField.acroFieldName = "pg4-Sch A/date2";
            row2.rowMap.Add(row2Column1);

            PDFFillMeta.subGridColumn row2Column2 = new PDFFillMeta.subGridColumn();
            row2Column2.textField = new PDFFillMeta.textMapField();
            row2Column2.textField.isConcat = true;
            row2Column2.textField.crmAttributeName = "clear_name+clear_contributoraddress+clear_city_text+clear_state_text+clear_zipcode";
            row2Column2.textField.acroFieldName = "pg4-Sch A/name & address2";
            row2.rowMap.Add(row2Column2);

            PDFFillMeta.subGridColumn row2Column3 = new PDFFillMeta.subGridColumn();
            row2Column3.checkField = new PDFFillMeta.conditionalCheckbox();
            row2Column3.checkField.crmAttributeName = "clear_contributorcode";

            PDFFillMeta.conditionalMap row2Column3Map1 = new PDFFillMeta.conditionalMap();
            row2Column3Map1.keyVal = 409830000;
            List<string> sampleCheckFieldListaa = new List<string>();
            sampleCheckFieldListaa.Add("pg4-Sch A/cc/ind2");
            row2Column3Map1.fieldList = sampleCheckFieldListaa;
            row2Column3.checkField.conditionalsList.Add(row2Column3Map1);

            PDFFillMeta.conditionalMap row2Column3Map2 = new PDFFillMeta.conditionalMap();
            row2Column3Map1.keyVal = 409830001;
            List<string> sampleCheckFieldListab = new List<string>();
            sampleCheckFieldListab.Add("pg4-Sch A/cc/comm2");
            row2Column3Map2.fieldList = sampleCheckFieldListab;
            row2Column3.checkField.conditionalsList.Add(row2Column3Map1);

            PDFFillMeta.conditionalMap row2Column3Map3 = new PDFFillMeta.conditionalMap();
            row2Column3Map3.keyVal = 409830002;
            List<string> sampleCheckFieldListac = new List<string>();
            sampleCheckFieldListac.Add("pg4-Sch A/cc/oth2");
            row2Column3Map3.fieldList = sampleCheckFieldListac;
            row2Column3.checkField.conditionalsList.Add(row2Column3Map3);

            PDFFillMeta.conditionalMap row2Column3Map4 = new PDFFillMeta.conditionalMap();
            row2Column3Map4.keyVal = 409830003;
            List<string> sampleCheckFieldListad = new List<string>();
            sampleCheckFieldListad.Add("pg4-Sch A/cc/pty2");
            row2Column3Map4.fieldList = sampleCheckFieldListad;
            row2Column3.checkField.conditionalsList.Add(row2Column3Map4);

            PDFFillMeta.conditionalMap row2Column3Map5 = new PDFFillMeta.conditionalMap();
            row2Column3Map5.keyVal = 409830004;
            List<string> sampleCheckFieldListae = new List<string>();
            sampleCheckFieldListae.Add("pg4-Sch A/cc/scc2");
            row2Column3Map5.fieldList = sampleCheckFieldListae;
            row2Column3.checkField.conditionalsList.Add(row2Column3Map5);

            row2.rowMap.Add(row2Column3);


            PDFFillMeta.subGridColumn row2Column4 = new PDFFillMeta.subGridColumn();
            row2Column4.textField = new PDFFillMeta.textMapField();
            row2Column4.textField.isConcat = true;
            row2Column4.textField.crmAttributeName = "clear_occupation+clear_employer";
            row2Column4.textField.acroFieldName = "pg4-Sch A/occup & emplyr1";
            row2.rowMap.Add(row2Column4);

            PDFFillMeta.subGridColumn row2Column5 = new PDFFillMeta.subGridColumn();
            row2Column5.textField = new PDFFillMeta.textMapField();
            row2Column5.textField.isMoney = true;
            row2Column5.textField.crmAttributeName = "clear_amountreceived";
            row2Column5.textField.acroFieldName = "pg4-Sch A/amt rec'd1";
            row2.rowMap.Add(row2Column5);

            PDFFillMeta.subGridColumn row2Column6 = new PDFFillMeta.subGridColumn();
            row2Column6.textField = new PDFFillMeta.textMapField();
            row2Column6.textField.isMoney = true;
            row2Column6.textField.crmAttributeName = "clear_cumulativetodate";
            row2Column6.textField.acroFieldName = "pg4-Sch A/cum cal yr1";
            row2.rowMap.Add(row2Column6);

            PDFFillMeta.subGridColumn row2Column7 = new PDFFillMeta.subGridColumn();
            row2Column7.textField = new PDFFillMeta.textMapField();
            row2Column7.textField.isMoney = true;
            row2Column7.textField.crmAttributeName = "clear_perelectiontodate";
            row2Column7.textField.acroFieldName = "pg4-Sch A/per elect1";
            row2.rowMap.Add(row2Column7);
            templateXML.subGridRows.Add(row2);


            XmlSerializer XMLer = new XmlSerializer(templateXML.GetType());
            XMLer.Serialize(Console.Out, templateXML);

            /*  SAMPLE PDFFill MetaObj    */
            /*
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
            scheduleA.relatedEntityName = "clear_monetarycontributions";
            scheduleA.relatedPDF = "460SchedA";

            templateXML.subGrids.Add(scheduleA);


            XmlSerializer XMLer = new XmlSerializer(templateXML.GetType());
            XMLer.Serialize(Console.Out, templateXML);
            */

        }
    }




}