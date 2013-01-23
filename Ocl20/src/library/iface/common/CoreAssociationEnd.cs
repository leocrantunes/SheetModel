/**
 * CoreAssociationEnd object instance interface.
 */

using System.Collections.Generic;
using Ocl20.uml13.iface.foundation.datatypes;

namespace Ocl20.library.iface.common
{
    public interface CoreAssociationEnd : CoreModelElement {

        bool isOneMultiplicity();
        bool isMandatory();
        bool isOrdered();
        string getFullPathName();
        CoreAssociation getTheAssociation();
        void setTheAssociation(CoreAssociation newValue);
        List<object> getInitConstraint();
        CoreClassifier getTheParticipant();
        void setTheParticipant(CoreClassifier newValue);
        List<object> getTheQualifiers();
        List<object> getDeriveConstraint();
        OrderingKind getOrdering();
        void setOrdering(OrderingKind newValue);
        Multiplicity getMultiplicity();
        void setMultiplicity(Multiplicity newValue);
        CoreAssociation getAssociation();
        void setAssociation(CoreAssociation newValue);
        List<object> getQualifier();
        void setQualifier(List<object> newValue);
        CoreClassifier getType();
        void setType(CoreClassifier newValue);
    }
}
