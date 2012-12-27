using System.Collections.Generic;
using Microsoft.Office.Interop.Excel;

namespace SheetModel.Model
{
    public class SheetBlock
    {
        public SheetBlock()
        {
            _sheetColumns = new List<SheetColumn>();
        }

        private string _name;
        public string Name
        {
            get { return _name; }
            set { _name = value; }
        }
        
        private string _type;
        public string Type
        {
            get { return _type; }
            set { _type = value; }
        }

        private Range _begin;
        public Range Begin
        {
            get { return _begin; }
            set { _begin = value; }
        }

        private List<SheetColumn> _sheetColumns;
        public List<SheetColumn> SheetColumns
        {
            get { return _sheetColumns; }
            set { _sheetColumns = value; }
        }
    }
}
