using System;
using System.Collections.Generic;
using Ocl20.library.iface.common;

namespace Ocl20.library.iface.evaluation
{
    public interface OclValue : ICloneable {
        CoreClassifier getType();
        void setCoreType(CoreClassifier type);
        OclValue	executeOperation(CoreOperation operation, List<object> args);
        OclValue	executeOperation(String	operationName, List<object> args);
        bool equals(object obj);
        List<object> getOwnedElements();

        bool oclIsUndefined();
        bool oclIsNull();
        bool oclIsInvalid();
        bool isTrue();
        bool isFalse();
    }
}
