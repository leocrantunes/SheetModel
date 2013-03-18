using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SheetMaker.sheetmodel
{
    public class XDataTableReferenceExp : XReferenceExp
    {
        private XDataTable xDataTable;

        public XDataTableReferenceExp()
        {}

        public XDataTable getDataTable()
        {
            return xDataTable;
        }

        public void setDataTable(XDataTable value)
        {
            xDataTable = value;
        }
    }
}
