// TODO : Rever CoreClassifier

using System.Collections.Generic;

namespace OclParser.cst.type
{
    public abstract class CSTTypeCS : CSTNode {
        //private CoreClassifier ast;

        public abstract string getName();

        public abstract List<object> getAllSimpleTypesNodesCS();

        /**
     * @return
     */
        //public CoreClassifier getAst() {
        //    return ast;
        //}

        /**
     * @param classifier
     */
        //public void setAst(CoreClassifier classifier) {
        //    ast = classifier;
        //}
    }
}
