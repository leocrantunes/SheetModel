using System;
using Ocl20.library.iface.expressions;

namespace Ocl20.library.impl.expressions
{
    public class StringLiteralExpImpl : PrimitiveLiteralExpImpl, StringLiteralExp {

        private	String 	stringSymbol;
	
        public StringLiteralExpImpl() {
        }

        public override void accept(IASTOclVisitor visitor) {
            visitor.visitStringLiteralExp(this);
        }

        public override 	String	ToString() {
            return	"\'" +   this.getStringSymbol() + "\'";
        }
	
	

        /**
	 * @return Returns the stringSymbol.
	 */
        public String getStringSymbol() {
            return stringSymbol;
        }
        /**
	 * @param stringSymbol The stringSymbol to set.
	 */
        public void setStringSymbol(String stringSymbol) {
            this.stringSymbol = stringSymbol;
        }
	
        public override Object Clone() {
            StringLiteralExpImpl theClone = (StringLiteralExpImpl) base.Clone();
            theClone.stringSymbol = stringSymbol;
            return	theClone;
        }

    }
}
