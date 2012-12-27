using System.Collections.Generic;
using Ocl20.library.iface.common;

namespace Ocl20.parser.cst.type
{
    public abstract class CSTTypeCS : CSTNode {
        private CoreClassifier ast;

        public abstract string getName();

        public abstract List<object> getAllSimpleTypesNodesCS();

        /**
     * @return
     */
        public CoreClassifier getAst()
        {
            return ast;
        }

        /**
     * @param classifier
     */
        public void setAst(CoreClassifier classifier)
        {
            ast = classifier;
        }
    }
}
