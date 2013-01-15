using System;
using System.Collections.Generic;
using System.Xml.Linq;
using Ocl20.library.iface.common;
using Ocl20.library.impl.common;
using Ocl20.uml13.iface.foundation.core;
using Ocl20.uml13.iface.foundation.datatypes;
using Attribute = Ocl20.uml13.iface.foundation.core.Attribute;

namespace Ocl20.uml13.impl.foundation.core
{
    public class AttributeImpl : CoreAttributeImpl, Attribute {

        #region members

        private Classifier type;

        #endregion

        protected override CoreModelElement getSpecificOwnerElement() {
            return	(CoreModelElement) getOwner();
        }
	
        public override List<object> getSpecificOwnedElements() {
            return	new List<object>();
        }

        public override void setFeatureOwner(CoreClassifier newValue)
        {
            throw new NotImplementedException();
        }

        public override bool getSpecificIsInstanceScope() {
            return (getOwnerScope() == ScopeKindEnum.SK_INSTANCE);
        }

        public override void setFeatureType(CoreClassifier newValue)
        {
            throw new NotImplementedException();
        }

        public override CoreClassifier	getSpecificType() {
            return	(CoreClassifier) getType();
        }

        public override void setTheEnumeration(CoreEnumeration newValue)
        {
            throw new NotImplementedException();
        }

        protected override CoreAssociationEnd getSpecificAssociationEnd() {
            return	(CoreAssociationEnd) getAssociationEnd();
        }
	
        public override bool getSpecificIsDerived() {
            String		name = super_getName();
		
            return (name[0] == '/');
        }

        protected override string super_getName()
        {
            throw new NotImplementedException();
        }

        public override String getName() {
            String		name = super_getName();
		
            if (name == null)
                return	name;
            else
                return (name[0] == '/') ? 
                           name.Substring(1, name.Length) :
                           name;
        }

        public override void setName(string newValue)
        {
            throw new NotImplementedException();
        }

        public Namespace getNamespace()
        {
            throw new NotImplementedException();
        }

        public void setNamespace(Namespace newValue)
        {
            throw new NotImplementedException();
        }

        public List<object> getClientDependency()
        {
            throw new NotImplementedException();
        }

        public List<object> getSupplierDependency()
        {
            throw new NotImplementedException();
        }

        public override void setElemOwner(CoreModelElement newValue)
        {
            throw new NotImplementedException();
        }

        public override List<object> getTheStereotypes()
        {
            throw new NotImplementedException();
        }

        public ScopeKind getOwnerScope()
        {
            throw new NotImplementedException();
        }

        public Classifier getOwner()
        {
            throw new NotImplementedException();
        }

        public Multiplicity getMultiplicity()
        {
            throw new NotImplementedException();
        }

        public void setMultiplicity(Multiplicity newValue)
        {
            throw new NotImplementedException();
        }

        public ChangeableKind getChangeability()
        {
            throw new NotImplementedException();
        }

        public void setChangeability(ChangeableKind newValue)
        {
            throw new NotImplementedException();
        }

        public ScopeKind getTargetScope()
        {
            throw new NotImplementedException();
        }

        public void setTargetScope(ScopeKind newValue)
        {
            throw new NotImplementedException();
        }

        public Classifier getType()
        {
            return type;
        }

        public void setType(Classifier newValue)
        {
            type = newValue;
        }

        public AssociationEnd getAssociationEnd()
        {
            throw new NotImplementedException();
        }
    }
}
