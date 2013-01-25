using System;
using Ocl20.library.iface.constraints;

namespace Ocl20.library.impl.constraints
{
    public class ConstraintsPackageImpl : ConstraintsPackage
    {
        public OclDefinedAttribute getOclDefinedAttribute()
        {
            return new OclDefinedAttributeImpl();
        }

        public OclDefinedOperation getOclDefinedOperation()
        {
            return new OclDefinedOperationImpl();
        }

        public OclPrePostConstraint getOclPrePostConstraint()
        {
            return new OclPrePostConstraintImpl();
        }

        public OclPostConstraint getOclPostConstraint()
        {
            return new OclPostConstraintImpl();
        }

        public OclPreConstraint getOclPreConstraint()
        {
            return new OclPreConstraintImpl();
        }

        public OclBodyConstraint getOclBodyConstraint()
        {
            return new OclBodyConstraintImpl();
        }

        public OclAssocEndInitConstraint getOclAssocEndInitConstraint()
        {
            return new OclAssocEndInitConstraintImpl();
        }

        public OclAttributeInitConstraint getOclAttributeInitConstraint()
        {
            return new OclAttributeInitConstraintImpl();
        }

        public OclAssocEndDeriveConstraint getOclAssocEndDeriveConstraint()
        {
            return new OclAssocEndDeriveConstraintImpl();
        }

        public OclAttributeDeriveConstraint getOclAttributeDeriveConstraint()
        {
            return new OclAttributeDeriveConstraintImpl();
        }

        public OclAttributeDefConstraint getOclAttributeDefConstraint()
        {
            throw new NotImplementedException();
            //return new OclAttributeDefConstraint();
        }

        public OclOperationDefConstraint getOclOperationDefConstraint()
        {
            throw new NotImplementedException();
            //return new OclOperationDefConstraintImpl();
        }

        public OclInvariantConstraint getOclInvariantConstraint()
        {
            return new OclInvariantConstraintImpl();
        }

        public OclDefConstraint getOclDefConstraint()
        {
            return new OclDefConstraintImpl();
        }

        public OclInitConstraint getOclInitConstraint()
        {
            throw new NotImplementedException();
            //return new OclInitConstraintImpl();
        }

        public OclDeriveConstraint getOclDeriveConstraint()
        {
            throw new NotImplementedException();
        }

        public ExpressionInOcl getExpressionInOcl()
        {
            return new ExpressionInOclImpl();
        }

        public OclClassifierConstraint getOclClassifierConstraint()
        {
            throw new NotImplementedException();
        }

        public OclOperationConstraint getOclOperationConstraint()
        {
            throw new NotImplementedException();
        }

        public OclConstraint getOclConstraint()
        {
            throw new NotImplementedException();
        }

        public ModelElementExpressionInOcl getModelElementExpressionInOcl()
        {
            throw new NotImplementedException();
        }

        public BodyExpressionOwnership getBodyExpressionOwnership()
        {
            throw new NotImplementedException();
        }

        public Preownership getPreownership()
        {
            throw new NotImplementedException();
        }

        public Postownership getPostownership()
        {
            throw new NotImplementedException();
        }

        public OclconstraintExpressionInOcl getOclconstraintExpressionInOcl()
        {
            throw new NotImplementedException();
        }

        public OclassocEndInitConstraintCoreAssociationEnd getOclassocEndInitConstraintCoreAssociationEnd()
        {
            throw new NotImplementedException();
        }

        public OclattributeInitConstraintCoreAttribute getOclattributeInitConstraintCoreAttribute()
        {
            throw new NotImplementedException();
        }

        public OclassocEndDeriveConstraintCoreAssociationEnd getOclassocEndDeriveConstraintCoreAssociationEnd()
        {
            throw new NotImplementedException();
        }

        public OclattributeDeriveConstraintCoreAttribute getOclattributeDeriveConstraintCoreAttribute()
        {
            throw new NotImplementedException();
        }

        public IsContextFor getIsContextFor()
        {
            throw new NotImplementedException();
        }

        public OclOperationConstraintCoreOperation getOclOperationConstraintCoreOperation()
        {
            throw new NotImplementedException();
        }
    }
}
