using System;
using System.Windows.Forms;
using SheetModel.ViewModel;
using UserControl = System.Windows.Controls.UserControl;
using System.Windows;
using SheetModel.Model;

namespace SheetModel.View
{
	/// <summary>
	/// Interaction logic for WizStep1.xaml
	/// </summary>
	public partial class WizStep1 : UserControl
	{
	    public WizStep1()
		{
			this.InitializeComponent();
		}

        public SheetModelWizViewModel ViewModel { get; set; }

        private void BtnOpenFileClick(object sender, RoutedEventArgs args)
        {
            ViewModel.OnOpenFileClicked();
        }

        private void TreeView_SelectedItemChanged(object sender, System.Windows.RoutedPropertyChangedEventArgs<object> e)
        {
            if (e.NewValue.GetType() == typeof(ModelClass))
                

            if (e.NewValue != null && e.NewValue.GetType() == typeof(ModelClass))
            {
                ViewModel.SelectedItem = (ModelClass)e.NewValue;
                ViewModel.ClassDescription = ((ModelClass)e.NewValue).Description;
            }
            else if (e.NewValue != null && e.NewValue.GetType() == typeof(ModelAttribute))
            {
                ViewModel.ClassDescription = ((ModelAttribute)e.NewValue).Description;
            }
        }

        private void BtnSetToBaseClassClick(object sender, RoutedEventArgs e)
        {
            ViewModel.SelectedBaseClass = ViewModel.SelectedItem;
            ViewModel.LoadTargetAssociations();
        }
	}
}