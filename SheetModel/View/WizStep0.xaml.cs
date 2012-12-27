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
using UserControl = System.Windows.Controls.UserControl;
using SheetModel.ViewModel;

namespace SheetModel.View
{
	/// <summary>
	/// Interaction logic for WizStep0.xaml
	/// </summary>
	public partial class WizStep0 : UserControl
	{
		public WizStep0()
		{
			this.InitializeComponent();
		}

        public SheetModelWizViewModel ViewModel { get; set; }

        private void BtnOpenFileClick(object sender, RoutedEventArgs args)
        {
            ViewModel.OnOpenFileClicked();
        }

        private void BtnShowModelClick(object sender, System.Windows.RoutedEventArgs e)
        {
            ViewModel.OnBtnShowModelClick();
        }
	}
}