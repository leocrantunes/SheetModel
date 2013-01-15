using System;
using System.Collections.Generic;
using Ocl20.library.iface.common;

namespace Ocl20.library.impl.common
{
    public class CoreModelElementImpl : CoreModelElement, IComparable
    {
        private string name;
        private CoreModelElement elemOwner;
        private List<object> elemOwnedElements;
        private List<CoreStereotype> theStereotypes;
        private CoreNamespace coreNamespace;
        private List<object> connection;
        List<object> clientDependency;

        public CoreModelElementImpl()
        {
            name = "";
            elemOwner = null;
            elemOwnedElements = new List<object>();
            theStereotypes = new List<CoreStereotype>();
            coreNamespace = null;
            connection = new List<object>();
            clientDependency = new List<object>();
        }

        public virtual String getName()
        {
            return name;
        }

        public virtual void setName(string newValue)
        {
            name = newValue;
        }

        public virtual int CompareTo(Object arg0)
        {
            int compareClassNames = this.GetType().FullName.CompareTo(arg0.GetType().FullName);

            if (compareClassNames == 0)
                if (this.getName() != null && ((CoreModelElement)arg0).getName() != null)
                    return this.getName().ToUpper().CompareTo(((CoreModelElement)arg0).getName().ToUpper());
                else
                    return -1;
            else
                return compareClassNames;
        }

        public virtual CoreModelElement getElemOwner()
        {
            return elemOwner;
            //return getSpecificOwnerElement();
        }

        public virtual void setElemOwner(CoreModelElement newValue)
        {
            elemOwner = newValue;
        }

        public virtual ICollection<object> getElemOwnedElements()
        {
            return elemOwnedElements;
        }

        public void setElemOwnedElements(List<object> newValue)
        {
            elemOwnedElements = newValue;
        }

        public CoreModel getModel()
        {
            if (this.GetType() == typeof (CoreModelImpl))
                return (CoreModel) this;
            else {
                CoreModelElement ns = this.getElemOwner();
                while ((ns != null) && !(ns.GetType() == typeof (CoreModelImpl))) {
                    ns = ns.getElemOwner();
                }
                return (CoreModel) ns;
            }
        }

        public bool hasStereotype(String stereotypeName)
        {
            if (getSpecificHasDirectStereotype())
            {
                foreach (CoreStereotype stereotype in getSpecificStereotypes()) {
                    if (stereotypeName != null && stereotypeName.ToUpper().Equals(stereotype.getName().ToUpper()))
                        return true;
                }
            }
            else
            {
                foreach (CoreStereotype stereotype in getModel().getAllStereotypes())
                {
                    if (stereotype.getExtendedElement().Contains(this) && stereotypeName != null && stereotypeName.ToUpper().Equals(stereotype.getName().ToUpper()))
                        return true;
                }
            }

            return false;
        }

        protected bool getSpecificHasDirectStereotype()
        {
            return false;
        }

        protected List<object> getSpecificStereotypes()
        {
            return new List<object>();
        }

        protected List<object> adjustCollectionResult(List<object> aCollection)
        {
            if (aCollection != null)
                return aCollection;
            else
                return new List<object>();
        }

        protected List<object> adjustListResult(List<object> aCollection)
        {
            if (aCollection != null)
                return new List<object>(aCollection);
            else
                return new List<object>();
        }

        public List<object> getConstraintExpressionInOcl()
        {
            return new List<object>();
        }

        public virtual List<CoreStereotype> getTheStereotypes()
        {
            return theStereotypes;
        }

        public void setTheStereotypes(List<CoreStereotype> newValue)
        {
            theStereotypes = newValue;
        }

        public CoreNamespace getNamespace()
        {
            return coreNamespace;
        }

        public void setNamespace(CoreNamespace newValue)
        {
            coreNamespace = newValue;
        }

        #region from uml13

        protected virtual CoreModelElement getSpecificOwnerElement()
        {
            //return getNamespace();
            return getElemOwner();
        }

        public virtual ICollection<object> getSpecificOwnedElements()
        {
            return getElemOwnedElements();
        }

        public List<object> getConnection()
        {
            return connection;
        }

        public void setConnection(List<object> newValue)
        {
            connection = newValue;
        }

        public List<object> getClientDependency()
        {
            return clientDependency;
        }

        public void setClientDependency(List<object> newValue)
        {
            clientDependency = newValue;
        }

        #endregion

    }
}
