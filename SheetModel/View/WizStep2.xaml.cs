using System.Windows.Controls;
using SheetModel.ViewModel;
using SheetModel.Model;

namespace SheetModel.View
{
	/// <summary>
	/// Interaction logic for WizStep2.xaml
	/// </summary>
	public partial class WizStep2 : UserControl
	{
        public SheetModelWizViewModel ViewModel { get; set; }

		public WizStep2()
		{
			this.InitializeComponent();
		}
        
		private void BtnAddWorksheetClick(object sender, System.Windows.RoutedEventArgs e)
		{
            ViewModel.OnBtnAddWorksheetClick();
		}

		private void BtnAddBlocoClick(object sender, System.Windows.RoutedEventArgs e)
		{
            ViewModel.OnBtnAddDataBlockClick();
		}

		private void BtnAddAreaClick(object sender, System.Windows.RoutedEventArgs e)
		{
            ViewModel.OnBtnAddAreaClick();
		}

        private void TreeView_SelectedSheetTabChanged(object sender, System.Windows.RoutedPropertyChangedEventArgs<object> e)
        {
            if (e.NewValue != null && e.NewValue.GetType() == typeof(SheetTab))
            {
                ViewModel.SelectedSheetTab = (SheetTab)e.NewValue;
                ViewModel.OnChangeSelection((SheetTab)e.NewValue);
            }
        }

        private void TreeView_SelectedTargetAssociationChanged(object sender, System.Windows.RoutedPropertyChangedEventArgs<object> e)
        {
            if (e.NewValue != null && e.NewValue.GetType() == typeof(ModelAssociation))
                ViewModel.SelectedTargetAssociation = (ModelAssociation)e.NewValue;
        }

        private void btnPreview_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            ViewModel.OnBtnPreviewClick();
        }

        private void BtnRemoveWorksheetClick(object sender, System.Windows.RoutedEventArgs e)
        {
            ViewModel.OnBtnRemoveWorksheetClick();
        }

	}
}