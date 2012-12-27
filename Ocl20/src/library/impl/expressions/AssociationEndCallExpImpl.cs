using System;
using Ocl20.library.iface.common;
using Ocl20.library.iface.expressions;
using CoreAssociationEnd = Ocl20.library.iface.common.CoreAssociationEnd;

namespace Ocl20.library.impl.expressions
{
    public class AssociationEndCallExpImpl : NavigationCallExpImpl, AssociationEndCallExp {

        private	CoreAssociationEnd	referredAssociationEnd;
        /**
	 * @param object
	 */
        public AssociationEndCallExpImpl() {
        }

        public CoreClassifier getExpressionType(OclExpression source, CoreAssociationEnd associationEnd) {
            return	base.getExpressionType(source.getType(), associationEnd, associationEnd.getTheParticipant());
        }

        protected override String getSpecificString() {
            return	this.getReferredAssociationEnd().getName();
        }

        /* (non-Javadoc)
	 * @see br.ufrj.cos.lens.odyssey.tools.psw.metamodels.ocl20.expressions.propertyCall.ASTModelPropertyCallExp#accept(br.ufrj.cos.lens.odyssey.tools.psw.metamodels.ocl20.IASTOclVisitor)
	 */
        public override void accept(IASTOclVisitor visitor) {
            base.accept(visitor);
            visitor.visitAssociationEndCallExp(this);
        }

	
        /**
	 * @return Returns the referredAssociationEnd.
	 */
        public CoreAssociationEnd getReferredAssociationEnd() {
            return referredAssociationEnd;
        }
        /**
	 * @param referredAssociationEnd The referredAssociationEnd to set.
	 */
        public void setReferredAssociationEnd(
            CoreAssociationEnd referredAssociationEnd) {
            this.referredAssociationEnd = referredAssociationEnd;
            }
	
        public override Object Clone() {
            AssociationEndCallExpImpl theClone = (AssociationEndCallExpImpl) base.Clone();
            theClone.referredAssociationEnd = referredAssociationEnd;
            return	theClone;
        }

    }
}
