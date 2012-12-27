using Ocl20.parser.controller;

namespace Ocl20.parser.cst.literalExp
{
    public abstract class CSTPrimitiveLiteralExpCS : CSTLiteralExpCS {
        protected CSTPrimitiveLiteralExpCS(OCLWorkbenchToken token) : base(token) {
        }
    }
}
