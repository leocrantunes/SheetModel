using System;
using OclLibrary.iface.expressions;

namespace OclLibrary.impl.expressions
{
    public class OclTypeLiteralExpImpl : LiteralExpImpl, OclTypeLiteralExp {

        /**
	 * 
	 */
        public OclTypeLiteralExpImpl() {
        }

        public override void accept(IASTOclVisitor visitor) {
            visitor.visitOclTypeLiteralExp(this);
        }

        public override String ToString() {
            return	this.getType().ToString();
        }
    }
}
