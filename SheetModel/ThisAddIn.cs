using System;
using System.Globalization;
using SheetModel.Model;
using SheetModel.View;
using SheetModel.ViewModel;
using Excel = Microsoft.Office.Interop.Excel;
using Microsoft.Office.Core;
using System.Collections.ObjectModel;

namespace SheetModel
{
    public partial class ThisAddIn
    {
        private CommandBarButton _createSpreadsheet;

        private int NumTabs = 1;
        private int i = 1;
        private int j = 1;

        private void ThisAddInStartup(object sender, EventArgs e)
        {
            _createSpreadsheet = DefineShortcutMenu("[SheetModel] Create Spreadsheet...", 100);
            _createSpreadsheet.Click += CreateSpreadsheetClick;
        }

        private void ThisAddIn_Shutdown(object sender, EventArgs e)
        {
            Application.ActiveWorkbook.SaveAs();
        }

        public void AddWorsheet(SheetTab sheetTab)
        {
            sheetTab.Worksheet = NumTabs == 1 ? (Excel.Worksheet)Application.ActiveWorkbook.Worksheets.Add(missing, missing, missing, missing)
                : (Excel.Worksheet)Application.ActiveWorkbook.Worksheets.Add(missing, Application.ActiveWorkbook.Worksheets[NumTabs-1], missing, missing);
            
            sheetTab.Worksheet.Name = sheetTab.Name;

            NumTabs++;
        }

        public void RemoveWorsheet(SheetTab sheetTab)
        {
            Excel.Worksheet worksheet = (Excel.Worksheet) Application.ActiveWorkbook.Worksheets[sheetTab.Worksheet.Index];
            worksheet.Delete();

            NumTabs--;
        }

        public void ChangeSelection(SheetTab sheetTab)
        {
            sheetTab.Worksheet.Select();
        }

        public void AddDataBlock(SheetTab sheetTab, SheetBlock sheetBlock)
        {
            try
            {
                if (sheetBlock.Type == "input" || sheetBlock.Type == "single")
                {
                    int nColumns = sheetBlock.SheetColumns.Count;
                    if (nColumns <= 0) return;
                    sheetTab.Worksheet.Cells[i, j] = sheetBlock.Name;
                    i++;
                    Excel.Range bBegin = (Excel.Range)sheetTab.Worksheet.Cells[i, j];
                    Excel.Range bEnd = (Excel.Range)sheetTab.Worksheet.Cells[i, nColumns];

                    Excel.ListObject interopList = sheetTab.Worksheet.ListObjects.Add(
                                             Excel.XlListObjectSourceType.xlSrcRange, sheetTab.Worksheet.Range[bBegin, bEnd]);
                    interopList.Name = sheetBlock.Name != null ? sheetBlock.Name : "";
                    interopList.ShowTotals = true;

                    foreach (SheetColumn sheetColumn in sheetBlock.SheetColumns)
                    {
                        interopList.ListColumns[j].Name = sheetColumn.ModelAttribute.Name;
                        interopList.ListColumns[j].TotalsCalculation = sheetColumn.HasTotal
                            ? GetTotalsCalculation(sheetColumn.TotalType)
                            : Excel.XlTotalsCalculation.xlTotalsCalculationNone;
                        j++;
                    }
                }
                //else if (sheetBlock.Type == "single") { }
                else // sheetBlock.Type == "total"
                { }

                i += 4;
                j = 1;
            }
            catch (Exception ex) { }
        }

        public void CreateWorksheets(ObservableCollection<SheetTab> sheetTabs)
        {
            int numTabs = 1;
            foreach (SheetTab sheetTab in sheetTabs)
            {
                int numWorksheets = Application.ActiveWorkbook.Worksheets.Count;
                Application.ActiveWorkbook.Worksheets.Add(missing, Application.ActiveWorkbook.Worksheets[numWorksheets],
                                                          missing, missing);
                sheetTab.Worksheet = (Excel.Worksheet)Application.ActiveWorkbook.Worksheets[numTabs];
                sheetTab.Worksheet.Name = sheetTab.Name;
                
                int i = 1;
                int j = 1;

                foreach (SheetBlock sheetBlock in sheetTab.SheetBlocks)
                {
                    if (sheetBlock.Type == "input")
                    {
                        int nColumns = sheetBlock.SheetColumns.Count;
                        if (nColumns <= 0) continue;
                        sheetTab.Worksheet.Cells[i, j] = sheetBlock.Name;
                        i++;
                        Excel.Range bBegin = (Excel.Range)sheetTab.Worksheet.Cells[i, j];
                        Excel.Range bEnd = (Excel.Range)sheetTab.Worksheet.Cells[i, nColumns];

                        Excel.ListObject interopList = sheetTab.Worksheet.ListObjects.Add(
                                                 Excel.XlListObjectSourceType.xlSrcRange, sheetTab.Worksheet.Range[bBegin, bEnd]);
                        interopList.Name = sheetBlock.Name != null ? sheetBlock.Name : "";
                        interopList.ShowTotals = true;

                        foreach (SheetColumn sheetColumn in sheetBlock.SheetColumns)
                        {
                            interopList.ListColumns[j].Name = sheetColumn.ModelAttribute.Name;
                            interopList.ListColumns[j].TotalsCalculation = sheetColumn.HasTotal
                                ? GetTotalsCalculation(sheetColumn.TotalType)
                                : Excel.XlTotalsCalculation.xlTotalsCalculationNone;
                            j++;
                        }
                    }
                    else if (sheetBlock.Type == "single") { }
                    else // sheetBlock.Type == "total"
                    { }

                    i += 4;
                    j = 1;
                }

                numTabs++;
            }

            sheetTabs[0].Worksheet.Select();
        }

        private void CreateSpreadsheetClick(CommandBarButton ctrl, ref bool cancelDefault)
        {
            try
            {
                PrepareWorkbook();

                var s = new SheetModelWiz(new SheetModelWizViewModel(this));
                s.Show();
            }
            catch (Exception ex)
            {
                System.Windows.Forms.MessageBox.Show(ex.Message);
            }
        }

        private void PrepareWorkbook()
        {
            foreach (Excel.Worksheet worksheet in Application.ActiveWorkbook.Worksheets)
            {
                if (worksheet.Index == 1) continue;
                worksheet.Delete();
            }
        }

        private Excel.XlTotalsCalculation GetTotalsCalculation(string totalsType)
        {
            switch (totalsType)
            {
                case "SUM":
                    return Excel.XlTotalsCalculation.xlTotalsCalculationSum;
                default:
                    return Excel.XlTotalsCalculation.xlTotalsCalculationNone;
            }
        }
        
        private CommandBarButton DefineShortcutMenu(string menuItemName, int menuItemId)
        {
            MsoControlType menuItem = MsoControlType.msoControlButton;
            CommandBarButton command =
                (CommandBarButton)Application.CommandBars["Cell"].Controls.Add(menuItem, missing, missing, 1, true);

            command.Style = MsoButtonStyle.msoButtonCaption;
            command.Caption = menuItemName;
            command.Tag = menuItemId.ToString(CultureInfo.InvariantCulture);

            return command;
        }

        #region VSTO generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InternalStartup()
        {
            this.Startup += new System.EventHandler(ThisAddInStartup);
            this.Shutdown += new System.EventHandler(ThisAddIn_Shutdown);
        }
        
        #endregion
    }
}
