using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Xml;
using System.Xml.Serialization;

namespace PDFMeta
{
    public class PDFFillMeta
    {
        [XmlAttribute]
        public string CRMEntityName;

        [XmlAttribute]
        public string PrimaryEntityPrimaryKey;

        [XmlAttribute]
        public string PDFFileName;

        [XmlAttribute]
        public bool isSubPDF =false;


        public List<textMapField> textFields = new List<textMapField>();
        public List<conditionalCheckbox> conditionalCheckboxes = new List<conditionalCheckbox>();
        public List<subGridPDF> subGrids = new List<subGridPDF>();
        


        public class textMapField
        {
            [XmlAttribute]
            public bool isConcat = false;

            [XmlAttribute]
            public bool isEntityRef = false;

            [XmlAttribute]
            public bool isDate = false;

            [XmlAttribute]
            public bool isMoney = false;

            [XmlAttribute]
            public bool isParentAttr = false;

            public string crmAttributeName;
            public string acroFieldName;

        }

        public class conditionalCheckbox
        {
            public string crmAttributeName;
            public List<conditionalMap> conditionalsList = new List<conditionalMap>();
        }

        public class conditionalMap
        {

            public int keyVal;
            public List<string> fieldList = new List<string>();

        }

        public class subGridPDF
        {
            [XmlAttribute]
            public string relatedEntityName;

            [XmlAttribute]
            public string relatedEntityKey;

            [XmlAttribute]
            public string relatedPDF;

            public List<subGridRow> subGridRows = new List<subGridRow>();
        }


        public class subGridRow
        {
            public List<subGridColumn> rowMap = new List<subGridColumn>();
        }

        public class subGridColumn
        {
            public conditionalCheckbox checkField = null;
            public textMapField textField = null;

        }

    }//Close PDFFillMeta
}
