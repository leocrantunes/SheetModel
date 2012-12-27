using System.Collections.Generic;
using OclLibrary.iface.constraints;
using OclLibrary.impl.constraints;

namespace OclParser.typeChecker
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
