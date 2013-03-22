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
        private int keyIndex;

        public XDataTable()
        {
            arrangement = ColumnsArrangement.Horizontal;
            xDataTableColumns = new List<XDataTableColumn>();
            keyIndex = 1;
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

        public int getKeyIndex()
        {
            return keyIndex;
        }

        public void setKeyIndex(int value)
        {
            keyIndex = value;
        }
 
    }
}
