using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SheetMaker.sheetmodel
{
    public class XFunctionExp
    {
        private List<XParameter> xParameters;

        public XFunctionExp()
        {}

        public List<XParameter> getParameters()
        {
            return xParameters;
        }

        public void setParameters(List<XParameter> value)
        {
            xParameters = value;
        }
    }
}
