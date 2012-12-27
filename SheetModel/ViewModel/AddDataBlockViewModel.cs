using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SheetModel.Model;
using System.ComponentModel;

namespace SheetModel.ViewModel
{
    public class AddDataBlockViewModel : INotifyPropertyChanged
    {
        public ModelAssociation TargetAssociation { get; set; }
        public ModelClass TargetClass { get; set; }
        public string AssociationString { get; set; }
        public string TypeString { get; set; }

        private string _name;
        public string Name
        {
            get { return _name; }
            set
            {
                _name = value;
                if (SheetBlock != null) SheetBlock.Name = Name;
                OnPropertyChanged("Name");
            }
        }

        private SheetBlock _sheetBlock;
        public SheetBlock SheetBlock
        {
            get { return _sheetBlock; }
            set
            {
                _sheetBlock = value;
                OnPropertyChanged("SheetBlock");
            }
        }

        public AddDataBlockViewModel(ModelAssociation targetAssociation, ModelClass targetClass)
        {
            TargetAssociation = targetAssociation;
            TargetClass = targetClass;
            string multiplicity = targetAssociation.Members.FirstOrDefault(m => m.ClassName.Equals(targetClass.Name)).Multiplicity;
            SheetBlock = new Model.SheetBlock() { Type = multiplicity.Equals("1") ? "single" : "input" };
            TypeString = SheetBlock.Type;
            AssociationString = string.Format("{0} {1} {2} {3}", TargetAssociation.Members[0].ClassName,
                TargetAssociation.Members[0].Multiplicity, TargetAssociation.Members[1].Multiplicity, TargetAssociation.Members[1].ClassName);
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
