using System;
using OclLibrary.iface.expressions;

namespace OclLibrary.impl.expressions
{
    public class BooleanLiteralExpImpl : PrimitiveLiteralExpImpl, BooleanLiteralExp {

        private bool booleanSymbol;
	
        public BooleanLiteralExpImpl() {
        }
	
        public override void accept(IASTOclVisitor visitor) {
            visitor.visitBooleanLiteralExp(this);
        }

        public override String ToString() {
            return	this.isBooleanSymbol().ToString();
        }

        /**
	 * @return Returns the booleanSymbol.
	 */
        public bool isBooleanSymbol() {
            return booleanSymbol;
        }
        /**
	 * @param booleanSymbol The booleanSymbol to set.
	 */
        public void setBooleanSymbol(bool booleanSymbol) {
            this.booleanSymbol = booleanSymbol;
        }
	
        public override Object Clone() {
            BooleanLiteralExpImpl theClone = (BooleanLiteralExpImpl) base.Clone();
            theClone.booleanSymbol = booleanSymbol;
            return	theClone;
        }
    }
}
