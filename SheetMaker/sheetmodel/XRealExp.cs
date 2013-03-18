using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;

namespace SheetMaker.sheetmodel
{
    public class XRealExp : XLiteralExp
    {
        private double realSymbol;

        public XRealExp()
        {}

        public double getRealSymbol()
        {
            return realSymbol;
        }

        public void setRealSymbol(double value)
        {
            realSymbol = value;
        }

        public override string getContent()
        {
            return getRealSymbol().ToString(CultureInfo.InvariantCulture);
        }
    }
}
