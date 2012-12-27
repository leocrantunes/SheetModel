using System;
using System.Collections.Generic;
using OclLibrary.iface.common;

namespace OclLibrary.iface.evaluation
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
