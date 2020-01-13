using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SheetMaker.sheetmodel
{
    public class XDataArray : XDataContent
    {
        private List<string> arraySymbol;

        public XDataArray()
        {
            arraySymbol = new List<string>();
        }

        public List<string> getArray()
        {
            return arraySymbol;
        } 

        public void setArray(List<string> value)
        {
            arraySymbol = value;
        }

        public override string getContent()
        {
            string content = "";
            foreach (string s in arraySymbol)
            {
                content += s + ";";
            }
            return content;
        }
    }
}
