using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SheetMaker.sheetmodel
{
    public class XDataTableColumn : XNamedElement
    {
        private XDataTable xDataTable;
        private XDataType xDataType;
        private XDataContent xDataContent;

        public XDataTableColumn()
        {}

        public XDataTable getDataTable()
        {
            return xDataTable;
        }

        public void setDataTable(XDataTable value)
        {
            xDataTable = value;
        }

        public XDataType getDataType()
        {
            return xDataType;
        }

        public void setDataType(XDataType value)
        {
            xDataType = value;
        }

        public XDataContent getDataContent()
        {
            return xDataContent;
        }

        public void setDataContent(XDataContent value)
        {
            xDataContent = value;
        }
    }
}
