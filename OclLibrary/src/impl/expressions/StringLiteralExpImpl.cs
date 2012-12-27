using System;
using OclLibrary.iface.expressions;

namespace OclLibrary.impl.expressions
{
    public class StringLiteralExpImpl : PrimitiveLiteralExpImpl, StringLiteralExp {

        private	String 	stringSymbol;
	
        public StringLiteralExpImpl() {
        }

        public override void accept(IASTOclVisitor visitor) {
            visitor.visitStringLiteralExp(this);
        }

        public	String	toString() {
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
