using Ocl20.library.iface.common;
using Ocl20.uml13.iface.foundation.datatypes;

namespace Ocl20.library.impl.common
{
    public class ParameterImpl : CoreModelElementImpl, Parameter
    {
        private Expression defaultValue;
        private ParameterDirectionKind kind;
        private CoreBehavioralFeature behavioralFeature;
        private CoreClassifier type;

        public Expression getDefaultValue()
        {
            return defaultValue;
        }

        public void setDefaultValue(Expression newValue)
        {
            defaultValue = newValue;
        }

        public ParameterDirectionKind getKind()
        {
            return kind;
        }

        public void setKind(ParameterDirectionKind newValue)
        {
            kind = newValue;
        }

        public CoreBehavioralFeature getBehavioralFeature()
        {
            return behavioralFeature;
        }

        public void setBehavioralFeature(CoreBehavioralFeature newValue)
        {
            behavioralFeature = newValue;
        }

        public CoreClassifier getType()
        {
            return type;
        }

        public void setType(CoreClassifier newValue)
        {
            type = newValue;
        }
    }
}
