using System.Collections.Generic;
using Ocl20.library.iface.constraints;
using Ocl20.library.impl.constraints;

namespace Ocl20.parser.typeChecker
{
    public class ConstraintSourceTrackerImpl : ConstraintSourceTracker {
        private Dictionary<string, HashSet<object>> sourceToOwners; // maps a source file name to a set of owners of OCL constraints
        private Dictionary<string, HashSet<object>> sourceToDependants;
    

        public ConstraintSourceTrackerImpl() {
            sourceToOwners = new Dictionary<string, HashSet<object>>();
            sourceToDependants = new Dictionary<string, HashSet<object>>();
        }
    
    
        public void addOwnerToSource(string source, OclConstraintOwner owner) {
            HashSet<object> owners;
            sourceToOwners.TryGetValue(source, out owners);
    	
            if (owners == null) {
                owners = new HashSet<object>();
                sourceToOwners.Add(source, owners);
            }

            owners.Add(owner);
        }

        public void addOwnerToSource(
            OclConstraint constraint,
            OclConstraintOwner owner) {
            addOwnerToSource(constraint.getSource(), owner);
            }

        public HashSet<object> getOwnersForSource(string source)
        {
            HashSet<object> owners;
            sourceToOwners.TryGetValue(source, out owners);
            return owners;
        }
    
        public void addDependantToSource (string sourceFile, string dependantSourceFile)		 {
            if (!sourceFile.Equals(dependantSourceFile)) {
                HashSet<object> dependants;
                sourceToDependants.TryGetValue(sourceFile, out dependants);

                if (dependants == null) {
                    dependants = new HashSet<object>();
                    sourceToDependants.Add(sourceFile, dependants);
                }

                dependants.Add(dependantSourceFile);
            }
        }

        public HashSet<object> getDependantsForSource(string source) {
            HashSet<object> dependants;
            sourceToDependants.TryGetValue(source, out dependants);
            return dependants;
        }
    
        public void clearAll() {
            sourceToOwners.Clear();
            sourceToDependants.Clear();
        }    
    }
}
