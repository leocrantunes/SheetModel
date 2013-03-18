using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SheetMaker.sheetmodel
{
    #region arrangement

    public enum ColumnsArrangement
    {
        Horizontal,
        Vertical
    }

    #endregion

    public class XDataTable: XNamedElement
    {
        private XWorksheet xWorksheet;
        private ColumnsArrangement arrangement;
        private List<XDataTableColumn> xDataTableColumns;

        public XDataTable()
        {
            arrangement = ColumnsArrangement.Horizontal;
            xDataTableColumns = new List<XDataTableColumn>();
        }

        public XWorksheet getWorksheet()
        {
            return xWorksheet;
        }

        public void setWorksheet(XWorksheet value)
        {
            xWorksheet = value;
        }
 
        public ColumnsArrangement getArrangement()
        {
            return arrangement;
        }

        public void setArrangement(ColumnsArrangement value)
        {
            arrangement = value;
        }
 
        public List<XDataTableColumn> getDataTableColumns()
        {
            return xDataTableColumns;
        }

        public void setDataTableColumns(List<XDataTableColumn> value)
        {
            xDataTableColumns = value;
        }
    }
}
