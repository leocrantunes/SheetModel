using System.Collections.Generic;
using Ocl20.library.iface.common;
using Ocl20.uml13.iface.foundation.datatypes;

namespace Ocl20.library.impl.common
{
    public class CoreAssociationEndImpl : CoreModelElementImpl, CoreAssociationEnd
    {
        private OrderingKind ordering;
        private Multiplicity multiplicity;
        private CoreAssociation association;
        private List<object> qualifier;
        private CoreClassifier type;

        public CoreAssociationEndImpl()
        {
            qualifier = new List<object>();
        }

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

        public void setTheParticipant(CoreClassifier newValue)
        {
            setType(newValue);
        }

        public List<object> getTheQualifiers() {
            return adjustListResult(getSpecificQualifiers());
        }

        public CoreAssociation getTheAssociation() {
            return	getSpecificAssociation();
        }

        public void setTheAssociation(CoreAssociation newValue)
        {
            setAssociation(newValue);
        }

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
            if (getType() is CoreClassifierImpl)
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
            foreach (MultiplicityRange range in rangeCollection)
            {
                if (range.getUpper() == 1 && rangeCollection.Count == 1)
                    return true;
            }
            return false;
        }
        
        protected bool getSpecificIsOrdered()
        {
            return (getOrdering() == OrderingKindEnum.OK_ORDERED);
        }

        public OrderingKind getOrdering()
        {
            return ordering;
        }

        public void setOrdering(OrderingKind newValue)
        {
            ordering = newValue;
        }

        public Multiplicity getMultiplicity()
        {
            return multiplicity;
        }

        public void setMultiplicity(Multiplicity newValue)
        {
            multiplicity = newValue;
        }

        public CoreAssociation getAssociation()
        {
            return association;
        }

        public void setAssociation(CoreAssociation newValue)
        {
            association = newValue;
        }

        public List<object> getQualifier()
        {
            return qualifier;
        }

        public void setQualifier(List<object> newValue)
        {
            qualifier = newValue;
        }

        public CoreClassifier getType()
        {
            return type;
        }

        public void setType(CoreClassifier newValue)
        {
            type = newValue;
        }

        #endregion

    }
}
