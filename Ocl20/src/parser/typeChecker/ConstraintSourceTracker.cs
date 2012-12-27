using System.Collections.Generic;
using Ocl20.library.iface.constraints;
using Ocl20.library.impl.constraints;

namespace Ocl20.parser.typeChecker
{
    public interface ConstraintSourceTracker {
        void addOwnerToSource(string source, OclConstraintOwner owner);
        void addOwnerToSource(OclConstraint constraint, OclConstraintOwner owner);
        HashSet<object> getOwnersForSource(string source);
        void addDependantToSource (string sourceFile, string dependantSourceFile);
        HashSet<object> getDependantsForSource(string source);
        void clearAll();
    }
}
