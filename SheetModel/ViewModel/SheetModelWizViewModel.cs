using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Windows.Forms;
using System.Xml.Linq;
using SheetModel.Model;
using System.Windows.Input;
using System.Diagnostics;
using Excel = Microsoft.Office.Interop.Excel;

namespace SheetModel.ViewModel
{
    public class SheetModelWizViewModel : INotifyPropertyChanged
    {
        private ThisAddIn _thisAddIn;

        public SheetModelWizViewModel(ThisAddIn thisAddIn)
        {
            _thisAddIn = thisAddIn;
            Steps = new ObservableCollection<bool>();
            for (int i=0; i<5; i++)
                Steps.Add(false);
            Steps[0] = true;

            ModelClasses = new ObservableCollection<ModelClass>();
            ModelAssociations = new ObservableCollection<ModelAssociation>();
            TargetAssociations = new ObservableCollection<ModelAssociation>();
            SheetTabs = new ObservableCollection<SheetTab>();
            FilePath = _filePath;
        }

        #region databind

        private string _filePath = @"C:\Users\Leo\Desktop\IRPF_Final.xml";
        public string FilePath
        {
            get { return _filePath; }
            set
            {
                _filePath = value;
                OnPropertyChanged("FilePath");
                LoadXml();
            }
        }

        private int _isSelectedIndex;
        public int IsSelectedIndex
        {
            get { return _isSelectedIndex; }
            set
            {
                _isSelectedIndex = value;
                OnPropertyChanged("IsSelectedIndex");
            }
        }

        private ModelClass _selectedItem;
        public ModelClass SelectedItem
        {
            get { return _selectedItem; }
            set
            {
                _selectedItem = value;
                OnPropertyChanged("SelectedItem");
            }
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

        private ModelClass _selectedBaseClass;
        public ModelClass SelectedBaseClass
        {
            get { return _selectedBaseClass; }
            set
            {
                _selectedBaseClass = value;
                OnPropertyChanged("SelectedBaseClass");
            }
        }

        private SheetTab _selectedSheetTab;
        public SheetTab SelectedSheetTab
        {
            get { return _selectedSheetTab; }
            set
            {
                _selectedSheetTab = value;
                OnPropertyChanged("SelectedSheetTab");
            }
        }

        private ModelAssociation _selectedTargetAssociation;
        public ModelAssociation SelectedTargetAssociation
        {
            get { return _selectedTargetAssociation; }
            set
            {
                _selectedTargetAssociation = value;
                OnPropertyChanged("SelectedTargetAssociation");
            }
        }

        private ObservableCollection<bool> _steps;
        public ObservableCollection<bool> Steps
        {
            get { return _steps; }
            set { _steps = value; }
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

        private ObservableCollection<ModelAssociation> _targetAssociations;
        public ObservableCollection<ModelAssociation> TargetAssociations
        {
            get { return _targetAssociations; }
            set { _targetAssociations = value; }
        }

        private ObservableCollection<SheetTab> _sheetTabs;
        public ObservableCollection<SheetTab> SheetTabs
        {
            get { return _sheetTabs; }
            set { _sheetTabs = value; }
        }

        #endregion

        public void OnBtnNextClick()
        {
            Steps[IsSelectedIndex++] = false;
            Steps[IsSelectedIndex] = true;
        }

        public void OnBtnPreviousClick()
        {
            Steps[IsSelectedIndex--] = false;
            Steps[IsSelectedIndex] = true;
        }

        public void OnOpenFileClicked()
        {
            var openFileDialog = new OpenFileDialog();
            var result = openFileDialog.ShowDialog();
            if (result == DialogResult.OK)
            {
                FilePath = openFileDialog.FileName;
            }
        }

        private void LoadXml()
        {
            try
            {
                XDocument docModel = XDocument.Load(FilePath);
                XElement model = docModel.Element("Model");
                if (model == null) throw new Exception("Documento fora do formato.");
                LoadModelClasses(model.Element("ClassModel"));
                LoadModelAssociations(model.Element("ClassModel"));
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }    
        }
        
        private void LoadModelClasses(XElement classModel)
        {
            ModelClasses.Clear();
            IEnumerable<XElement> modelClasses = classModel.Elements("Class");
            foreach (XElement modelClass in modelClasses)
            {
                var name = modelClass.Attribute("name");
                string modelClassName = name != null ? name.Value : "";
                var desc = modelClass.Attribute("description");
                string description = desc != null ? desc.Value : "";
                var bbc = modelClass.Attribute("bestbaseclass");
                bool bestBaseClass = bbc != null ? Boolean.Parse(bbc.Value) : false;
                var bcdesc = modelClass.Attribute("bcdescription");
                string bcDescription = bcdesc != null ? bcdesc.Value : "";
                IEnumerable<XElement> modelAttributes = modelClass.Elements("Attribute");

                var tModelAttributes = (from modelAttribute in modelAttributes
                                        let aName = modelAttribute.Attribute("name")
                                        let modelAttributeName = aName != null ? aName.Value : ""
                                        let aType = modelAttribute.Attribute("type")
                                        let modelAttributeType = aType != null ? aType.Value : ""
                                        let adesc = modelAttribute.Attribute("description")
                                        let adescription = adesc != null ? adesc.Value : ""
                                        select new ModelAttribute { Name = modelAttributeName, Type = modelAttributeType, Description = adescription }).ToList();

                ModelClasses.Add(new ModelClass { Name = modelClassName, Description = description, BestBaseClass = bestBaseClass, BcDescription = bcDescription, ModelAttributes = tModelAttributes });
            }
        }

        private void LoadModelAssociations(XElement classModel)
        {
            ModelAssociations.Clear();
            IEnumerable<XElement> modelAssociations = classModel.Elements("Association");
            foreach (XElement modelAssociation in modelAssociations)
            {
                var ma = new ModelAssociation();

                var name = modelAssociation.Attribute("name");
                string modelAssociationName = name != null ? name.Value : "";
                ma.Name = modelAssociationName;

                var desc = modelAssociation.Attribute("description");
                string description = desc != null ? desc.Value : "";
                ma.Description = description;

                XElement endA = modelAssociation.Element("EndA");
                if (endA != null)
                {
                    XAttribute xEndATarget = endA.Attribute("target");
                    string endATarget = xEndATarget != null ? xEndATarget.Value : "";
                    XAttribute xEndAMultiplicity = endA.Attribute("multiplicity");
                    string endAMultiplicity = xEndAMultiplicity != null ? xEndAMultiplicity.Value : "";
                    XAttribute xEndARole = endA.Attribute("role");
                    string endARole = xEndARole != null ? xEndARole.Value : "";

                    ma.Members.Add(new AssociationMember { ClassName = endATarget, Multiplicity = endAMultiplicity, Role = endARole });
                }
                
                XElement endB = modelAssociation.Element("EndB");
                if (endB != null)
                {
                    XAttribute xEndBTarget = endB.Attribute("target");
                    string endBTarget = xEndBTarget != null ? xEndBTarget.Value : "";
                    XAttribute xEndBMultiplicity = endB.Attribute("multiplicity");
                    string endBMultiplicity = xEndBMultiplicity != null ? xEndBMultiplicity.Value : "";
                    XAttribute xEndBRole = endB.Attribute("role");
                    string endBRole = xEndBRole != null ? xEndBRole.Value : "";

                    ma.Members.Add(new AssociationMember { ClassName = endBTarget, Multiplicity = endBMultiplicity, Role = endBRole });
                }

                ModelAssociations.Add(ma);
            }
        }

        public void LoadTargetAssociations()
        {
            if (SelectedBaseClass != null)
            {
                TargetAssociations.Clear();
                foreach (ModelAssociation ma in ModelAssociations.
                    Where(a => a.Members.FirstOrDefault(m => m.ClassName.Equals(SelectedBaseClass.Name)) != null))
                {
                    TargetAssociations.Add(ma);
                }
            }
        }

        public void OnBtnPreviewClick()
        {
            _thisAddIn.CreateWorksheets(SheetTabs);
        }

        public void OnBtnAddWorksheetClick()
        {
            AddWorksheet addWorsheet = new AddWorksheet();
            addWorsheet.ShowDialog();

            if (addWorsheet.DialogResult.Value)
            {
                SheetTab sheetTab = new SheetTab() { Name = addWorsheet.ViewModel.Name };
                SheetTabs.Add(sheetTab);
                _thisAddIn.AddWorsheet(sheetTab);
            }
        }

        public void OnBtnRemoveWorksheetClick()
        {
            if (SelectedSheetTab != null)
            {
                var result = MessageBox.Show("Deseja apagar a Worksheet e todos os seus componentes?",
                    "Apagar Worsheet", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2);
                if (result == DialogResult.Yes)
                {
                    _thisAddIn.RemoveWorsheet(SelectedSheetTab);
                    SheetTabs.Remove(SelectedSheetTab);
                }
            }
        }

        public void OnChangeSelection(SheetTab sheetTab)
        {
            _thisAddIn.ChangeSelection(sheetTab);
        }

        public void OnBtnAddDataBlockClick()
        {
            if (SelectedSheetTab != null && SelectedTargetAssociation != null)
            {
                AssociationMember targetMember = SelectedTargetAssociation.Members.FirstOrDefault(m => !m.ClassName.Equals(SelectedBaseClass.Name));
                ModelClass targetClass = ModelClasses.FirstOrDefault(m => m.Name.Equals(targetMember.ClassName));
                AddDataBlock addDataBlock = new AddDataBlock(new AddDataBlockViewModel(SelectedTargetAssociation, targetClass));
                addDataBlock.ShowDialog();

                if (addDataBlock.DialogResult.Value)
                {
                    SelectedSheetTab.SheetBlocks.Add(addDataBlock.ViewModel.SheetBlock);
                    _thisAddIn.AddDataBlock(SelectedSheetTab, addDataBlock.ViewModel.SheetBlock);
                }
            }
        }

        public void OnBtnAddAreaClick()
        {
        }

        public void OnBtnShowModelClick()
        {
            ShowModel showModel = new ShowModel();
            showModel.ViewModel.ModelAssociations = ModelAssociations;
            showModel.ViewModel.ModelClasses = ModelClasses;
            
            showModel.ShowDialog();
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

    public class RelayCommand : ICommand
    {
        #region Fields

        readonly Action<object> _execute;
        readonly Predicate<object> _canExecute;

        #endregion // Fields

        #region Constructors

        public RelayCommand(Action<object> execute)
            : this(execute, null)
        {
        }

        public RelayCommand(Action<object> execute, Predicate<object> canExecute)
        {
            if (execute == null)
                throw new ArgumentNullException("execute");

            _execute = execute;
            _canExecute = canExecute;
        }
        #endregion // Constructors

        #region ICommand Members

        [DebuggerStepThrough]
        public bool CanExecute(object parameter)
        {
            return _canExecute == null ? true : _canExecute(parameter);
        }

        public event EventHandler CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }

        public void Execute(object parameter)
        {
            _execute(parameter);
        }

        #endregion // ICommand Members
    }
}
