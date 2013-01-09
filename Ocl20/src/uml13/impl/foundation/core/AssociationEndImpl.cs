using System.Collections.Generic;
using Ocl20.library.iface.common;
using Ocl20.library.impl.common;
using Ocl20.uml13.iface.foundation.core;
using Ocl20.uml13.iface.foundation.datatypes;

namespace Ocl20.uml13.impl.foundation.core
{
    public abstract class AssociationEndImpl : CoreAssociationEndImpl, AssociationEnd {

        protected override CoreAssociation getSpecificAssociation() {
            return	(CoreAssociation) getAssociation();
        }
        protected override List<object> getSpecificQualifiers() {
            return	getQualifier();
        }

        protected override CoreClassifier getSpecificParticipant() {
            if (getType() == typeof(CoreClassifier))
                return (CoreClassifier) getType();
            else
                return	null;
		
        }
        protected override bool getSpecificIsMandatory() {
            Multiplicity multiplicity = getMultiplicity();
            List<object> rangeCollection = multiplicity.getRange();
            foreach (MultiplicityRange range in rangeCollection) {
                if (range.getLower() == 0)
                    return	false;
            }
            return	true;
        }
	
        protected override bool getSpecificIsOneMultiplicity() {
            Multiplicity multiplicity = getMultiplicity();
            List<object> rangeCollection = multiplicity.getRange();
            int index = 0;
            foreach (MultiplicityRange range in rangeCollection) {
                if (range.getUpper() == 1 &&  index != rangeCollection.Count - 1)
                    return	true;
                index++;
            }
            return	false;
        }
        protected override bool getSpecificIsOrdered() {
            return (getOrdering() == OrderingKindEnum.OK_ORDERED);
        }

        public abstract Namespace getNamespace();
        public abstract void setNamespace(Namespace newValue);
        public abstract List<object> getClientDependency();
        public abstract List<object> getSupplierDependency();
        public abstract bool isNavigable();
        public abstract void setNavigable(bool newValue);
        public abstract OrderingKind getOrdering();
        public abstract void setOrdering(OrderingKind newValue);
        public abstract AggregationKind getAggregation();
        public abstract void setAggregation(AggregationKind newValue);
        public abstract ScopeKind getTargetScope();
        public abstract void setTargetScope(ScopeKind newValue);
        public abstract Multiplicity getMultiplicity();
        public abstract void setMultiplicity(Multiplicity newValue);
        public abstract ChangeableKind getChangeability();
        public abstract void setChangeability(ChangeableKind newValue);
        public abstract UmlAssociation getAssociation();
        public abstract void setAssociation(UmlAssociation newValue);
        public abstract List<object> getQualifier();
        public abstract Classifier getType();
        public abstract void setType(Classifier newValue);
        public abstract List<object> getSpecification();
    }
}
