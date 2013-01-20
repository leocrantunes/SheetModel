using System.Collections.Generic;
using Ocl20.library.iface.common;

namespace Ocl20.library.impl.common
{
    public class GeneralizationImpl : CoreModelElementImpl, Generalization
    {
        private string discriminator;
        private CoreClassifier child;
        private CoreClassifier parent;
        private CoreClassifier powertype;
        
        public string getDiscriminator()
        {
            return discriminator;
        }

        public void setDiscriminator(string newValue)
        {
            discriminator = newValue;
        }

        public CoreClassifier getChild()
        {
            return child;
        }

        public void setChild(CoreClassifier newValue)
        {
            child = newValue;
        }

        public CoreClassifier getParent()
        {
            return parent;
        }

        public void setParent(CoreClassifier newValue)
        {
            parent = newValue;
        }

        public CoreClassifier getPowertype()
        {
            return powertype;
        }

        public void setPowertype(CoreClassifier newValue)
        {
            powertype = newValue;
        }
    }
}
