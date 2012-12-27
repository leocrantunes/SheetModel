using System;
using System.Windows;
using SheetModel.ViewModel;
using MessageBox = System.Windows.MessageBox;
using System.Windows.Input;
using System.Windows.Controls;
using SheetModel.Model;

namespace SheetModel.View
{
	/// <summary>
	/// Interaction logic for SheetModelWiz.xaml
	/// </summary>
	public partial class SheetModelWiz : Window
	{
        private SheetModelWizViewModel _viewModel;
        
		public SheetModelWiz()
		{
			this.InitializeComponent();
		}

        public SheetModelWiz(SheetModelWizViewModel viewModel) : this()
        {
            _viewModel = viewModel;
            DataContext = _viewModel;

            wizStep0.ViewModel = _viewModel;
            wizStep0.DataContext = _viewModel;

            wizStep1.ViewModel = _viewModel;
            wizStep1.DataContext = _viewModel;

            wizStep2.ViewModel = _viewModel;
            wizStep2.DataContext = _viewModel;
        }

		private void BtnCancelClick(object sender, EventArgs e)
		{
            Close();
		}

		private void BtnCloseClick(object sender, RoutedEventArgs e)
		{
		    this.DialogResult = true;
		}

		private void BtnNextClick(object sender, RoutedEventArgs e)
		{
		    _viewModel.OnBtnNextClick();
		}

		private void BtnPreviousClick(object sender, RoutedEventArgs e)
		{
		    _viewModel.OnBtnPreviousClick();
		}

		private void WindowClosing(object sender, System.ComponentModel.CancelEventArgs e)
		{
            var resultado = MessageBox.Show("Você tem certeza que deseja sair do assistente?", "Aviso", MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (resultado == MessageBoxResult.No)
                e.Cancel = true;
		}
	}
}