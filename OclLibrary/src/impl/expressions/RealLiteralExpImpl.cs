using System;
using OclLibrary.iface.expressions;

namespace OclLibrary.impl.expressions
{
    public class RealLiteralExpImpl : NumericLiteralExpImpl, RealLiteralExp {

        private String realSymbol;
	
        public RealLiteralExpImpl() {
        }
        public override void accept(IASTOclVisitor visitor) {
            visitor.visitRealLiteralExp(this);
        }

        public	String	toString() {
            return	this.getRealSymbol();
        }
	
	
        /**
	 * @return Returns the realSymbol.
	 */
        public String getRealSymbol() {
            return realSymbol;
        }
        /**
	 * @param realSymbol The realSymbol to set.
	 */
        public void setRealSymbol(String realSymbol) {
            this.realSymbol = realSymbol;
        }
	
        public override Object Clone() {
            RealLiteralExpImpl theClone = (RealLiteralExpImpl) base.Clone();
            theClone.realSymbol = realSymbol;
            return	theClone;
        }

    }
}
