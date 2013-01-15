using System.Collections.Generic;
using Ocl20.library.iface.common;
using Ocl20.uml13.iface.foundation.datatypes;

namespace Ocl20.library.impl.common
{
    public abstract class CoreAssociationEndImpl : CoreModelElementImpl, CoreAssociationEnd {
	
        public string getFullPathName() {
            return	getTheAssociation().getName() + "::" + this.getName();
        }

        public override string getName() {
            string name = base.getName();
		
            if (!string.IsNullOrEmpty(name)) {
                return	name;
            }
            else {
                return	getTheParticipant().getName();
            }
        }

        public abstract void setTheParticipant(CoreClassifier newValue);

        public List<object> getTheQualifiers() {
            return adjustListResult(getSpecificQualifiers());
        }

        public CoreAssociation getTheAssociation() {
            return	getSpecificAssociation();
        }

        public abstract void setTheAssociation(CoreAssociation newValue);

        public CoreClassifier getTheParticipant() {
            return	getSpecificParticipant();
        }

        public bool isMandatory() {
            return	getSpecificIsMandatory();
        }

        public bool isOneMultiplicity() {
            return getSpecificIsOneMultiplicity();
        }

        public bool isOrdered() {
            return getSpecificIsOrdered();
        }

        public List<object> getDeriveConstraint() {
            // TODO Auto-generated method stub
            return null;
        }

        public List<object> getInitConstraint() {
            // TODO Auto-generated method stub
            return null;
        }

        #region from uml13

        protected CoreAssociation getSpecificAssociation()
        {
            return (CoreAssociation) getAssociation();
        }
        protected List<object> getSpecificQualifiers()
        {
            return getQualifier();
        }

        protected CoreClassifier getSpecificParticipant()
        {
            if (GetType() == typeof(CoreClassifier))
                return (CoreClassifier) getType();
            else
                return null;
        }

        protected bool getSpecificIsMandatory()
        {
            Multiplicity multiplicity = getMultiplicity();
            List<object> rangeCollection = multiplicity.getRange();
            foreach (MultiplicityRange range in rangeCollection)
            {
                if (range.getLower() == 0)
                    return false;
            }
            return true;
        }

        protected bool getSpecificIsOneMultiplicity()
        {
            Multiplicity multiplicity = getMultiplicity();
            List<object> rangeCollection = multiplicity.getRange();
            int index = 0;
            foreach (MultiplicityRange range in rangeCollection)
            {
                if (range.getUpper() == 1 && index != rangeCollection.Count - 1)
                    return true;
                index++;
            }
            return false;
        }
        
        protected bool getSpecificIsOrdered()
        {
            return (getOrdering() == OrderingKindEnum.OK_ORDERED);
        }

        public abstract OrderingKind getOrdering();
        public abstract Multiplicity getMultiplicity();
        public abstract CoreAssociation getAssociation();
        public abstract List<object> getQualifier();
        public abstract CoreClassifier getType();

        #endregion

    }
}
