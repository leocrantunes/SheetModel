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
using SheetModel.ViewModel;

namespace SheetModel
{
	/// <summary>
	/// Interaction logic for AddWorksheet.xaml
	/// </summary>
	public partial class AddWorksheet : Window
	{
        public AddWorksheetViewModel ViewModel { get; set; }

		public AddWorksheet()
		{
			this.InitializeComponent();

            ViewModel = new AddWorksheetViewModel();
            DataContext = ViewModel;
		}

		private void btnOk_Click(object sender, System.Windows.RoutedEventArgs e)
		{
            DialogResult = true;
		}

		private void btnCancel_Click(object sender, System.Windows.RoutedEventArgs e)
		{
            DialogResult = false;
		}
	}
}