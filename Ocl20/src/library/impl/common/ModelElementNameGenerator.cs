using System;
using System.Collections.Generic;
using Ocl20.library.iface.common;

namespace Ocl20.library.impl.common
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
