using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SheetMaker.sheetmodel
{
    public class XWorkbook : XNamedElement
    {
        private List<XWorksheet> xWorksheets;

        public XWorkbook()
        {
            xWorksheets = new List<XWorksheet>();
        }

        public List<XWorksheet> getWorksheets()
        {
            return xWorksheets;
        }

        public void setWorksheets(List<XWorksheet> value)
        {
            xWorksheets = value;
        }
    }
}
