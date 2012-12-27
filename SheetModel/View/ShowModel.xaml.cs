using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using SheetModel.Model;
using SheetModel.ViewModel;

namespace SheetModel
{
	/// <summary>
	/// Interaction logic for ShowModel.xaml
	/// </summary>
	public partial class ShowModel : Window
	{
		public ShowModel()
		{
			this.InitializeComponent();

            ViewModel = new ShowModelViewModel();
            DataContext = ViewModel;
		}

        public ShowModelViewModel ViewModel { get; set; }

        private void TreeView_SelectedClassChanged(object sender, System.Windows.RoutedPropertyChangedEventArgs<object> e)
        {
            if (e.NewValue != null && e.NewValue.GetType() == typeof(ModelClass))
            {
                ViewModel.ClassDescription = ((ModelClass)e.NewValue).Description;
            }
            else if (e.NewValue != null && e.NewValue.GetType() == typeof(ModelAttribute))
            {
                ViewModel.ClassDescription = ((ModelAttribute)e.NewValue).Description;
            }
        }

        private void TreeView_SelectedAssociationChanged(object sender, System.Windows.RoutedPropertyChangedEventArgs<object> e)
        {
            if (e.NewValue != null && e.NewValue.GetType() == typeof(ModelAssociation))
            {
                ViewModel.SelectedAssociation = (ModelAssociation)e.NewValue;
            }
            else if (e.NewValue != null && e.NewValue.GetType() == typeof(AssociationMember))
            {
                // ((AssociationMember)e.NewValue).
            }
        }
	}
}