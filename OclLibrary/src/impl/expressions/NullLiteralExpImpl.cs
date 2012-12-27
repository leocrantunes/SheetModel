using System;
using OclLibrary.iface.expressions;

namespace OclLibrary.impl.expressions
{
    public class NullLiteralExpImpl : LiteralExpImpl, NullLiteralExp {

        public override void accept(IASTOclVisitor visitor) {
            visitor.visitNullLiteralExp(this);
        }

        public	String	toString() {
            return "null";
        }
	
        public override Object Clone() {
            NullLiteralExpImpl theClone = (NullLiteralExpImpl) base.Clone();
            return	theClone;
        }

    }
}
