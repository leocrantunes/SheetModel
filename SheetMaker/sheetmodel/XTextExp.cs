using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SheetMaker.sheetmodel
{
    public class XTextExp : XLiteralExp
    {
        private string textSymbol;

        public XTextExp()
        {}

        public string getTextSymbol()
        {
            return textSymbol;
        }

        public void setTextSymbol(string value)
        {
            textSymbol = value;
        }

        public override string getContent()
        {
            return getTextSymbol();
        }
    }
}
