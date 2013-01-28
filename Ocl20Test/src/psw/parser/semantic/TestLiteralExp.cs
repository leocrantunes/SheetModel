using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Ocl20.parser.cst.literalExp;

namespace Ocl20Test.psw.parser.semantic
{
    public abstract class TestLiteralExp : TestNodeCS {

        protected override Type getSpecificNodeClass()
        {
            return typeof (CSTLiteralExpCS);
        }
    }

}
