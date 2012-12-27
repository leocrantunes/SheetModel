using System;
using System.Collections.Generic;
using OclLibrary.iface.common;

namespace OclLibrary.impl.common
{
    public interface ModelElementNameGenerator {
        String generateName(CoreModelElement element);

        String generateNameForOperation(
            String name,
            List<object> paramTypes);

        bool operationNameMatches(
            String operationMangledName,
            String name);

    }
}
