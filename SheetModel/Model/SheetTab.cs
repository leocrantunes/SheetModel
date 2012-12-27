using System.Collections.Generic;
using Microsoft.Office.Interop.Excel;
using System.Collections.ObjectModel;

namespace SheetModel.Model
{
    public class SheetTab
    {
        public SheetTab()
        {
            _sheetBlocks = new ObservableCollection<SheetBlock>();
        }

        private string _name;
        public string Name
        {
            get { return _name; }
            set { _name = value; }
        }

        private Worksheet _worksheet;
        public Worksheet Worksheet
        {
            get { return _worksheet; }
            set { _worksheet = value; }
        }

        private ObservableCollection<SheetBlock> _sheetBlocks;
        public ObservableCollection<SheetBlock> SheetBlocks
        {
            get { return _sheetBlocks; }
            set { _sheetBlocks = value; }
        }
    }
}
