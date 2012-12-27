using Microsoft.Office.Interop.Excel;

namespace SheetModel.Model
{
    public class SheetColumn
    {
        public SheetColumn()
        {
            _hasTotal = false;
            _totalType = "";
        }

        private ModelAttribute _modelAttribute;
        public ModelAttribute ModelAttribute
        {
            get { return _modelAttribute; }
            set { _modelAttribute = value; }
        }

        private Range _begin;
        public Range Begin
        {
            get { return _begin; }
            set { _begin = value; }
        }

        private bool _hasTotal;
        public bool HasTotal
        {
            get { return _hasTotal; }
            set { _hasTotal = value; }
        }

        private string _totalType;
        public string TotalType
        {
            get { return _totalType; }
            set { _totalType = value; }
        }
    }
}
