using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;

namespace SheetMaker.sheetmodel
{
    public class XIntegerExp : XLiteralExp
    {
        private int integerSymbol;

        public XIntegerExp()
        {}

        public int getIntegerSymbol()
        {
            return integerSymbol;
        }

        public void setIntegerSymbol(int value)
        {
            integerSymbol = value;
        }

        public override string getContent()
        {
            return getIntegerSymbol().ToString(CultureInfo.InvariantCulture);
        }
    }
}
