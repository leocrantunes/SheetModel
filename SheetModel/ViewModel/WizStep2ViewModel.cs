using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Xml.Linq;
using SheetModel.Model;

namespace SheetModel.ViewModel
{
    public class WizStep2ViewModel
    {
        public WizStep2ViewModel()
        {
            //LoadSheetTabs(model.Element("SheetModel"));
        }

        private ObservableCollection<SheetTab> _sheetTabs;
        public ObservableCollection<SheetTab> SheetTabs
        {
            get { return _sheetTabs; }
            set { _sheetTabs = value; }
        }
        
        //private void LoadSheetTabs(XElement sheetModel)
        //{
        //    IEnumerable<XElement> sheetTabs = sheetModel.Elements("Worksheet");
        //    int numTabs = 0;
        //    foreach (XElement sheetTab in sheetTabs)
        //    {
        //        string sheetTabName = sheetTab.Attribute("name").Value;
        //        List<SheetBlock> tSheetBlocks = new List<SheetBlock>();
        //        IEnumerable<XElement> sheetBlocks = sheetTab.Elements("Block");

        //        foreach (XElement sheetBlock in sheetBlocks)
        //        {
        //            string sheetBlockType = sheetBlock.Attribute("type").Value;
        //            SheetBlock tSheetBlock = new SheetBlock();
        //            tSheetBlock.Type = sheetBlockType;
        //            if (tSheetBlock.Type == "input")
        //            {
        //                string sheetBlockSource = sheetBlock.Attribute("source").Value;
        //                List<SheetColumn> tSheetColumns = new List<SheetColumn>();
        //                IEnumerable<XElement> sheetColumns = sheetBlock.Elements("Column");
        //                tSheetBlock.ModelClass = ModelClasses.FirstOrDefault(m => m.Name.Equals(sheetBlockSource));

        //                foreach (XElement sheetColumn in sheetColumns)
        //                {
        //                    string source = sheetColumn.Attribute("source").Value;
        //                    SheetColumn tSheetColumn = new SheetColumn();
        //                    tSheetColumn.ModelAttribute = tSheetBlock.ModelClass.ModelAttributes.FirstOrDefault(a => a.Name.Equals(source));
        //                    string sheetColumnName = tSheetColumn.ModelAttribute.Name;
        //                    XAttribute hasTotal = sheetColumn.Attribute("hasTotal");
        //                    if (hasTotal != null)
        //                    {
        //                        tSheetColumn.HasTotal = bool.Parse(hasTotal.Value);
        //                        tSheetColumn.TotalType = sheetColumn.Attribute("totalType").Value;
        //                    }

        //                    tSheetColumns.Add(tSheetColumn);
        //                }

        //                tSheetBlock.SheetColumns = tSheetColumns;
        //            }

        //            tSheetBlocks.Add(tSheetBlock);
        //        }

        //        SheetTabs.Add(new SheetTab { Name = sheetTabName, SheetBlocks = tSheetBlocks });
        //    }
        //}
    }
}
