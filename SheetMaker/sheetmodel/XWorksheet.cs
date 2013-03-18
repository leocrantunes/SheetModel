using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SheetMaker.sheetmodel
{
    public class XWorksheet : XNamedElement
    {
        private XWorkbook xWorkbook;
        private List<XDataTable> xDataTables;

        public XWorksheet()
        {
            xDataTables = new List<XDataTable>();
        }
        
        public XWorkbook getWorkbook()
        {
            return xWorkbook;
        }

        public void setWorkbook(XWorkbook value)
        {
            xWorkbook = value;
        }

        public List<XDataTable> getDataTables()
        {
            return xDataTables;
        }

        public void setDataTables(List<XDataTable> value)
        {
            xDataTables = value;
        }
    }
}
