/**
 * AssociationEnd object instance interface.
 */

using System.Collections.Generic;
using Ocl20.uml13.iface.foundation.datatypes;

namespace Ocl20.uml13.iface.foundation.core
{
    public interface AssociationEnd : ModelElement {
        /**
     * Returns the value of attribute isNavigable.
     * @return Value of attribute isNavigable.
     */
        bool isNavigable();
        /**
     * Sets the value of isNavigable attribute. See {@link #isNavigable} for description 
     * on the attribute.
     * @param newValue New value to be set.
     */
        void setNavigable(bool newValue);
        /**
     * Returns the value of attribute ordering.
     * @return Value of attribute ordering.
     */
        OrderingKind getOrdering();
        /**
     * Sets the value of ordering attribute. See {@link #getOrdering} for description 
     * on the attribute.
     * @param newValue New value to be set.
     */
       void setOrdering(OrderingKind newValue);
        /**
     * Returns the value of attribute aggregation.
     * @return Value of attribute aggregation.
     */
        AggregationKind getAggregation();
        /**
     * Sets the value of aggregation attribute. See {@link #getAggregation} for 
     * description on the attribute.
     * @param newValue New value to be set.
     */
        void setAggregation(AggregationKind newValue);
        /**
     * Returns the value of attribute targetScope.
     * @return Value of attribute targetScope.
     */
        ScopeKind getTargetScope();
        /**
     * Sets the value of targetScope attribute. See {@link #getTargetScope} for 
     * description on the attribute.
     * @param newValue New value to be set.
     */
        void setTargetScope(ScopeKind newValue);
        /**
     * Returns the value of attribute multiplicity.
     * @return Value of attribute multiplicity.
     */
        Multiplicity getMultiplicity();
        /**
     * Sets the value of multiplicity attribute. See {@link #getMultiplicity} 
     * for description on the attribute.
     * @param newValue New value to be set.
     */
        void setMultiplicity(Multiplicity newValue);
        /**
     * Returns the value of attribute changeability.
     * @return Value of attribute changeability.
     */
        ChangeableKind getChangeability();
        /**
     * Sets the value of changeability attribute. See {@link #getChangeability} 
     * for description on the attribute.
     * @param newValue New value to be set.
     */
        void setChangeability(ChangeableKind newValue);
        /**
     * Returns the value of reference association.
     * @return Value of reference association.
     */
        UmlAssociation getAssociation();
        /**
     * Sets the value of reference association. See {@link #getAssociation} for 
     * description on the reference.
     * @param newValue New value to be set.
     */
        void setAssociation(UmlAssociation newValue);
        /**
     * Returns the value of reference qualifier.
     * @return Value of reference qualifier.
     */
        List<object> getQualifier();
        /**
     * Returns the value of reference type.
     * @return Value of reference type.
     */
        Classifier getType();
        /**
     * Sets the value of reference type. See {@link #getType} for description 
     * on the reference.
     * @param newValue New value to be set.
     */
        void setType(Classifier newValue);
        /**
     * Returns the value of reference specification.
     * @return Value of reference specification.
     */
        List<object> getSpecification();
    }
}
