using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SheetMaker.sheetmodel
{
    public class XBooleanExp : XLiteralExp
    {
        private bool booleanSymbol;

        public XBooleanExp()
        {}

        public bool getBooleanSymbol()
        {
            return booleanSymbol;
        }

        public void setBooleanSymbol(bool value)
        {
            booleanSymbol = value;
        }

        public override string getContent()
        {
            return getBooleanSymbol().ToString();
        }
    }
}
