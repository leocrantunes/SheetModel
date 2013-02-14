using System.Windows;
using System.IO;
using Excel = Microsoft.Office.Interop.Excel;

namespace SheetMaker
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            string filePath = "C:\\spreadsheet\\Teste.xlsx";

            if (File.Exists(filePath))
                File.Delete(filePath);

            Excel.Application eApp;
            Excel.Workbook eBook;

            eApp = new Excel.Application();
            eBook = eApp.Workbooks.Add();

            eBook.SaveAs(filePath);
            eBook.Close();

            eApp.Quit();
        }
    }
}
