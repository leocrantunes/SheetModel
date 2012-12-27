using System;
using Ocl20.library.iface.expressions;

namespace Ocl20.library.impl.expressions
{
    public class RealLiteralExpImpl : NumericLiteralExpImpl, RealLiteralExp {

        private String realSymbol;
	
        public RealLiteralExpImpl() {
        }
        public override void accept(IASTOclVisitor visitor) {
            visitor.visitRealLiteralExp(this);
        }

        public	override String	ToString() {
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
