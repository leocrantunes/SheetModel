using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Collections.ObjectModel;
using SheetModel.Model;

namespace SheetModel.ViewModel
{
    public class ShowModelViewModel : INotifyPropertyChanged
    {
        public ShowModelViewModel()
        {
            ModelClasses = new ObservableCollection<ModelClass>();
            ModelAssociations = new ObservableCollection<ModelAssociation>();
        }

        private string _classDescription;
        public string ClassDescription
        {
            get { return _classDescription; }
            set
            {
                _classDescription = value;
                OnPropertyChanged("ClassDescription");
            }
        }
        
        private ModelClass _selectedClass;
        public ModelClass SelectedClass
        {
            get { return _selectedClass; }
            set
            {
                _selectedClass = value;
                OnPropertyChanged("SelectedClass");
            }
        }

        private ModelAssociation _selectedAssociation;
        public ModelAssociation SelectedAssociation
        {
            get { return _selectedAssociation; }
            set
            {
                _selectedAssociation = value;
                OnPropertyChanged("SelectedAssociation");
            }
        }

        private ObservableCollection<ModelClass> _modelClasses;
        public ObservableCollection<ModelClass> ModelClasses
        {
            get { return _modelClasses; }
            set { _modelClasses = value; }
        }

        private ObservableCollection<ModelAssociation> _modelAssociations;
        public ObservableCollection<ModelAssociation> ModelAssociations
        {
            get { return _modelAssociations; }
            set { _modelAssociations = value; }
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
