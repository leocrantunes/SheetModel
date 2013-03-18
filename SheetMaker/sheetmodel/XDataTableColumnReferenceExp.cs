using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SheetMaker.sheetmodel
{
    public class XDataTableColumnReferenceExp : XReferenceExp
    {
        private XDataTable xDataTableColumn;

        public XDataTableColumnReferenceExp()
        {}

        public XDataTable getDataTable()
        {
            return xDataTableColumn;
        }

        public void setDataTable(XDataTable value)
        {
            xDataTableColumn = value;
        }
    }
}
