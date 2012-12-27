using System;
using OclLibrary.iface.expressions;

namespace OclLibrary.impl.expressions
{
    public class InvalidLiteralExpImpl : LiteralExpImpl, InvalidLiteralExp {

        public override void accept(IASTOclVisitor visitor) {
            visitor.visitInvalidLiteralExp(this);
        }

        public	String	toString() {
            return "invalid";
        }
	
        public override Object Clone() {
            NullLiteralExpImpl theClone = (NullLiteralExpImpl) base.Clone();
            return	theClone;
        }
    }
}
