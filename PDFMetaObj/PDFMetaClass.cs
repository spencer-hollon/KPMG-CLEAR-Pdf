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
        public bool isSubPDF = new bool();
        [XmlAttribute]
        public int numSubGridRow;



        public List<textMapField> textFields = new List<textMapField>();
        public List<conditionalCheckbox> conditionalCheckboxes = new List<conditionalCheckbox>();
        public List<subGridPDF> subGrids = new List<subGridPDF>();
        public List<subGridRow> subGridRows = new List<subGridRow>();


        public class textMapField
        {
            [XmlAttribute]
            public bool isConcat = false;

            [XmlAttribute]
            public bool isEntityRef = false;

            [XmlAttribute]
            public bool isDate;

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
            public string relatedEntityName;
            public string relatedPDF;
        }

        public class subGridRow
        {
            public List<subGridColumn> rowMap = new List<subGridColumn>();
        }

        public class subGridColumn
        {
            public bool isConditional;

            public conditionalCheckbox checkField = new conditionalCheckbox();
            public textMapField textField = new textMapField();

        }

    }//Close PDFFillMeta
}
