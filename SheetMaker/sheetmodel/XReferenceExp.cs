using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SheetMaker.sheetmodel
{
    public class XReferenceExp
    {
        private XWorksheet xWorksheet;

        public XReferenceExp()
        {}

        public XWorksheet getWorksheet()
        {
            return xWorksheet;
        }

        public void setWorksheet(XWorksheet value)
        {
            xWorksheet = value;
        }
    }
}
