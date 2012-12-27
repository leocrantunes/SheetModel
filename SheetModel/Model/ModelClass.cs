using System.Collections.Generic;
using System.ComponentModel;

namespace SheetModel.Model
{
    public class ModelClass : INotifyPropertyChanged
    {
        public ModelClass()
        {
            _modelAttributes = new List<ModelAttribute>();
            IsSelected = true;
        }

        private string _name;
        public string Name
        {
            get { return _name; }
            set
            {
                _name = value;
                OnPropertyChanged("Name");
            }
        }

        private List<ModelAttribute> _modelAttributes;
        public List<ModelAttribute> ModelAttributes
        {
            get { return _modelAttributes; }
            set { _modelAttributes = value; }
        }

        private bool _isSelected;
        public bool IsSelected
        {
            get { return _isSelected; }
            set
            {
                if (value != _isSelected)
                {
                    _isSelected = value;
                    OnPropertyChanged("IsSelected");
                }
            }
        }

        private bool _bestBaseClass;
        public bool BestBaseClass
        {
            get { return _bestBaseClass; }
            set
            {
                if (value != _bestBaseClass)
                {
                    _bestBaseClass = value;
                    OnPropertyChanged("BestBaseClass");
                }
            }
        }

        private string _description;
        public string Description
        {
            get { return _description; }
            set
            {
                _description = value;
                OnPropertyChanged("Description");
            }
        }

        private string _bcDescription;
        public string BcDescription
        {
            get { return _bcDescription; }
            set
            {
                _bcDescription = value;
                OnPropertyChanged("BcDescription");
            }
        }


        #region INotifyPropertyChanged Members

        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged(string name)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null)
            {
                handler(this, new PropertyChangedEventArgs(name));
            }
        }

        #endregion
    }
}
