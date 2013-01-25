/**
 * constraints package interface.
 */

namespace Ocl20.library.iface.constraints
{
    public interface ConstraintsPackage {
        /**
     * Returns OclDefinedAttribute class proxy object.
     * @return OclDefinedAttribute class proxy object.
     */
        OclDefinedAttribute getOclDefinedAttribute();
        /**
     * Returns OclDefinedOperation class proxy object.
     * @return OclDefinedOperation class proxy object.
     */
        OclDefinedOperation getOclDefinedOperation();
        /**
     * Returns OclPrePostConstraint class proxy object.
     * @return OclPrePostConstraint class proxy object.
     */
        OclPrePostConstraint getOclPrePostConstraint();
        /**
     * Returns OclPostConstraint class proxy object.
     * @return OclPostConstraint class proxy object.
     */
        OclPostConstraint getOclPostConstraint();
        /**
     * Returns OclPreConstraint class proxy object.
     * @return OclPreConstraint class proxy object.
     */
        OclPreConstraint getOclPreConstraint();
        /**
     * Returns OclBodyConstraint class proxy object.
     * @return OclBodyConstraint class proxy object.
     */
        OclBodyConstraint getOclBodyConstraint();
        /**
     * Returns OclAssocEndInitConstraint class proxy object.
     * @return OclAssocEndInitConstraint class proxy object.
     */
        OclAssocEndInitConstraint getOclAssocEndInitConstraint();
        /**
     * Returns OclAttributeInitConstraint class proxy object.
     * @return OclAttributeInitConstraint class proxy object.
     */
        OclAttributeInitConstraint getOclAttributeInitConstraint();
        /**
     * Returns OclAssocEndDeriveConstraint class proxy object.
     * @return OclAssocEndDeriveConstraint class proxy object.
     */
        OclAssocEndDeriveConstraint getOclAssocEndDeriveConstraint();
        /**
     * Returns OclAttributeDeriveConstraint class proxy object.
     * @return OclAttributeDeriveConstraint class proxy object.
     */
        OclAttributeDeriveConstraint getOclAttributeDeriveConstraint();
        /**
     * Returns OclAttributeDefConstraint class proxy object.
     * @return OclAttributeDefConstraint class proxy object.
     */
        OclAttributeDefConstraint getOclAttributeDefConstraint();
        /**
     * Returns OclOperationDefConstraint class proxy object.
     * @return OclOperationDefConstraint class proxy object.
     */
        OclOperationDefConstraint getOclOperationDefConstraint();
        /**
     * Returns OclInvariantConstraint class proxy object.
     * @return OclInvariantConstraint class proxy object.
     */
        OclInvariantConstraint getOclInvariantConstraint();
        /**
     * Returns OclDefConstraint class proxy object.
     * @return OclDefConstraint class proxy object.
     */
        OclDefConstraint getOclDefConstraint();
        /**
     * Returns OclInitConstraint class proxy object.
     * @return OclInitConstraint class proxy object.
     */
        OclInitConstraint getOclInitConstraint();
        /**
     * Returns OclDeriveConstraint class proxy object.
     * @return OclDeriveConstraint class proxy object.
     */
        OclDeriveConstraint getOclDeriveConstraint();
        /**
     * Returns ExpressionInOcl class proxy object.
     * @return ExpressionInOcl class proxy object.
     */
        ExpressionInOcl getExpressionInOcl();
        /**
     * Returns OclClassifierConstraint class proxy object.
     * @return OclClassifierConstraint class proxy object.
     */
        OclClassifierConstraint getOclClassifierConstraint();
        /**
     * Returns OclOperationConstraint class proxy object.
     * @return OclOperationConstraint class proxy object.
     */
        OclOperationConstraint getOclOperationConstraint();
        /**
     * Returns OclConstraint class proxy object.
     * @return OclConstraint class proxy object.
     */
        OclConstraint getOclConstraint();
        /**
     * Returns ModelElementExpressionInOcl association proxy object.
     * @return ModelElementExpressionInOcl association proxy object.
     */
        ModelElementExpressionInOcl getModelElementExpressionInOcl();
        /**
     * Returns BodyExpressionOwnership association proxy object.
     * @return BodyExpressionOwnership association proxy object.
     */
        BodyExpressionOwnership getBodyExpressionOwnership();
        /**
     * Returns Preownership association proxy object.
     * @return Preownership association proxy object.
     */
        Preownership getPreownership();
        /**
     * Returns Postownership association proxy object.
     * @return Postownership association proxy object.
     */
        Postownership getPostownership();
        /**
     * Returns OclconstraintExpressionInOcl association proxy object.
     * @return OclconstraintExpressionInOcl association proxy object.
     */
        OclconstraintExpressionInOcl getOclconstraintExpressionInOcl();
        /**
     * Returns OclassocEndInitConstraintCoreAssociationEnd association proxy object.
     * @return OclassocEndInitConstraintCoreAssociationEnd association proxy object.
     */
        OclassocEndInitConstraintCoreAssociationEnd getOclassocEndInitConstraintCoreAssociationEnd();
        /**
     * Returns OclattributeInitConstraintCoreAttribute association proxy object.
     * @return OclattributeInitConstraintCoreAttribute association proxy object.
     */
        OclattributeInitConstraintCoreAttribute getOclattributeInitConstraintCoreAttribute();
        /**
     * Returns OclassocEndDeriveConstraintCoreAssociationEnd association proxy 
     * object.
     * @return OclassocEndDeriveConstraintCoreAssociationEnd association proxy 
     * object.
     */
        OclassocEndDeriveConstraintCoreAssociationEnd getOclassocEndDeriveConstraintCoreAssociationEnd();
        /**
     * Returns OclattributeDeriveConstraintCoreAttribute association proxy object.
     * @return OclattributeDeriveConstraintCoreAttribute association proxy object.
     */
        OclattributeDeriveConstraintCoreAttribute getOclattributeDeriveConstraintCoreAttribute();
        /**
     * Returns IsContextFor association proxy object.
     * @return IsContextFor association proxy object.
     */
        IsContextFor getIsContextFor();
        /**
     * Returns OclOperationConstraintCoreOperation association proxy object.
     * @return OclOperationConstraintCoreOperation association proxy object.
     */
        OclOperationConstraintCoreOperation getOclOperationConstraintCoreOperation();
    }
}
