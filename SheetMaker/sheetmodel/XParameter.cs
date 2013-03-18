using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SheetMaker.sheetmodel
{
    public class XParameter
    {
        private XFunctionExp xFunctionExp;

        public XParameter()
        {}

        public XFunctionExp getFunctionExp()
        {
            return xFunctionExp;
        }

        public void setFunctionExp(XFunctionExp value)
        {
            xFunctionExp = value;
        }
    }
}
