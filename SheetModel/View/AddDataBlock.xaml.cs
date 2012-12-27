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
using SheetModel.Model;

namespace SheetModel
{
	/// <summary>
	/// Interaction logic for AddDataBlock.xaml
	/// </summary>
	public partial class AddDataBlock : Window
	{
        public AddDataBlockViewModel ViewModel { get; set; }
        
		public AddDataBlock()
		{
			this.InitializeComponent();			
		}

        public AddDataBlock(AddDataBlockViewModel viewModel) : this()
        {
            ViewModel = viewModel;
            DataContext = ViewModel;
        }

        private void btnOk_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            foreach (ModelAttribute m in ViewModel.TargetClass.ModelAttributes)
            {
                ViewModel.SheetBlock.SheetColumns.Add(new SheetColumn() { ModelAttribute = m, HasTotal = m.HasTotal, TotalType = "SUM" });
            }

            DialogResult = true;
        }

        private void btnCancel_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            DialogResult = false;
        }
	}
}