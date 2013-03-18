using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SheetMaker.sheetmodel
{
    public class XNamedElement
    {
        private string name;

        public string getName()
        {
            return name;
        }

        public void setName(string value)
        {
            name = value;
        }
    }
}
