/**
 * CoreAssociationEnd object instance interface.
 */

using System.Collections.Generic;

namespace Ocl20.library.iface.common
{
    public interface CoreAssociationEnd : CoreModelElement {
        /**
     * @return 
     */
        bool isOneMultiplicity();
        /**
     * @return 
     */
        bool isMandatory();
        /**
     * @return 
     */
        bool isOrdered();
        /**
     * @return 
     */
        string getFullPathName();
        /**
     * Returns the value of reference theAssociation.
     * @return Value of reference theAssociation.
     */
        CoreAssociation getTheAssociation();
        /**
     * Sets the value of reference theAssociation. See {@link #getTheAssociation} 
     * for description on the reference.
     * @param newValue New value to be set.
     */
        void setTheAssociation(CoreAssociation newValue);
        /**
     * Returns the value of reference initConstraint.
     * @return Value of reference initConstraint.
     */
        List<object> getInitConstraint();
        /**
     * Returns the value of reference theParticipant.
     * @return Value of reference theParticipant.
     */
        CoreClassifier getTheParticipant();
        /**
     * Sets the value of reference theParticipant. See {@link #getTheParticipant} 
     * for description on the reference.
     * @param newValue New value to be set.
     */
        void setTheParticipant(CoreClassifier newValue);
        /**
     * Returns the value of reference theQualifiers.
     * @return Value of reference theQualifiers.
     */
        List<object> getTheQualifiers();
        /**
     * Returns the value of reference deriveConstraint.
     * @return Value of reference deriveConstraint.
     */
        List<object> getDeriveConstraint();
    }
}
