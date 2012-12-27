using System;
using Ocl20.library.iface.common;
using Ocl20.library.iface.expressions;

namespace Ocl20.library.impl.expressions
{
    public class EnumLiteralExpImpl : LiteralExpImpl, EnumLiteralExp {

        private	CoreEnumLiteral	referredEnumLiteral;
	
        /**
	 * @param object
	 */
        public EnumLiteralExpImpl() {
        }

        public override void accept(IASTOclVisitor visitor) {
            visitor.visitEnumLiteralExp(this);
        }

        public override String	ToString() {
            return this.getType().getName() + "::" + this.getReferredEnumLiteral().getName();
        }

	
        /**
	 * @return Returns the referredEnumLiteral.
	 */
        public CoreEnumLiteral getReferredEnumLiteral() {
            return referredEnumLiteral;
        }
        /**
	 * @param referredEnumLiteral The referredEnumLiteral to set.
	 */
        public void setReferredEnumLiteral(CoreEnumLiteral referredEnumLiteral) {
            this.referredEnumLiteral = referredEnumLiteral;
        }
	
        public override Object Clone() {
            EnumLiteralExpImpl theClone = (EnumLiteralExpImpl) base.Clone();
            theClone.referredEnumLiteral = referredEnumLiteral;
            return	theClone;
        }
    }
}
