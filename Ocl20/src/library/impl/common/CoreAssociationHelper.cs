using System.Collections.Generic;
using System.Text;
using Ocl20.library.iface.common;
using Ocl20.library.utils;

namespace Ocl20.library.impl.common
{
    public class CoreAssociationHelper {

        /* (non-Javadoc)
	 * @see ocl20.CoreAssociation#getAssociationEnd(java.lang.String)
	 */
        public static CoreAssociationEnd getAssociationEnd(CoreAssociation association, string roleName) {
            // first try to match role name
            foreach (CoreAssociationEnd assocEnd in association.getTheAssociationEnds()) {
                StringBuilder upperCaseName = new StringBuilder(roleName);
                char firstChar = roleName.MySubstring(0, 1).ToUpper()[0];
                upperCaseName.Insert(0, firstChar);
                string adjustedRoleName = upperCaseName.ToString();
            
                if (assocEnd.getName().Equals(roleName) ||
                    assocEnd.getName().Equals(adjustedRoleName)) {
                        return assocEnd;
                    }
            }

            return null;
        }
	
	
        public static string buildName(CoreAssociation association) {
            StringBuilder name = new StringBuilder();
        
            foreach (CoreAssociationEnd assocEnd in association.getTheAssociationEnds()) {
                if (name.Length > 0) {
                    name.Append("-");
                }
                name.Append(assocEnd.getTheParticipant().getFullPathName());
                name.Append("::");
                name.Append(assocEnd.getName());
            }
        
            return name.ToString();
        }
    
    
        /* (non-Javadoc)
	 * @see ocl20.CoreAssociation#getTheAssociationEnds(ocl20.CoreClassifier)
	 */
        public static List<object> getTheAssociationEnds(CoreAssociation association, CoreClassifier classifier) {

            List<object> connection = association.getTheAssociationEnds();
		
            if (isSelfAssociation(connection, classifier)) {
                return	connection;
            } else {
                List<object> result = new List<object>();
			
                foreach (CoreAssociationEnd associationEnd in connection) {
                    if (classifier != associationEnd.getTheParticipant()) {
                        result.Add(associationEnd);
                    }
                }
                return	result;
            }
        }
    
        public static bool isSelfAssociation(List<object> connection, CoreClassifier classifier) {
            if (connection.Count == 2) {
                CoreAssociationEnd assocEnd0 = (CoreAssociationEnd) connection[0];
                CoreAssociationEnd assocEnd1 = (CoreAssociationEnd) connection[1];
                if ( ( (assocEnd0.getTheParticipant() == assocEnd1.getTheParticipant()) &&
                       (classifier == assocEnd0.getTheParticipant()) ) ) {
                           return	true;
                       }
            }
            return	false;
        }

	
        /* (non-Javadoc)
	 * @see ocl20.CoreAssociation#isClassifierInAssociation(ocl20.CoreClassifier[])
	 */
        public static bool isClassifierInAssociation(CoreAssociation association, CoreClassifier classifier) {
            foreach (CoreAssociationEnd associationEnd in association.getTheAssociationEnds()) {
                if (classifier == associationEnd.getTheParticipant())
                    return true;
            }
		
            return	false;						
        }
    }
}
