using System.Collections.Generic;
using Ocl20.library.iface;

namespace Ocl20.library.impl.environment
{
    public class EnvironmentFactory {
        private	Ocl20Package oclPackage;
        private List<object> releasedEnv = new List<object>();
        private List<object> nestedEnvironments = new List<object>();
	
        public static int n = 0;
        public EnvironmentFactory(Ocl20Package oclPackage) {
            this.oclPackage = oclPackage;
        }

        public EnvironmentImpl getNestedEnvironmentInstance() {
    	
    	
//    	EnvironmentImpl  result;
//    	if (releasedEnv.size() > 0) {
//    		result = (EnvironmentImpl ) releasedEnv.get(0);
//    		releasedEnv.remove(result);
//    	}	
//    	else {
//        	n++;
//        	result = oclPackage.getEnvironment().getEnvironment().createEnvironment();
//        	((EnvironmentImpl) result).setOclPackage(oclPackage);
//        	nestedEnvironments.add(result);
//    	} 
    	
            EnvironmentImpl result = getEnvironmentInstance();
    	
            return	result;
        }

        public EnvironmentImpl  getEnvironmentInstance() {
    	
            n++;
//       	EnvironmentImpl  result = oclPackage.getEnvironment().getEnvironment().createEnvironment();
            EnvironmentImpl  result = new EnvironmentImpl();
            ((EnvironmentImpl) result).setOclPackage(oclPackage);
    	
    	
            return	result;
        }

        public void releaseEnvironment(EnvironmentImpl  env) {
            if (nestedEnvironments.Contains(env)) {
                ((EnvironmentImpl) env).clear();
                releasedEnv.Add(env);
            }	
        }
    
//    public NamedElement createNamedElement(String name, CoreModelElement element, bool mayBeImplicit) {
//    	NamedElement namedElement = oclPackage.getEnvironment().getNamedElement().createNamedElement();
//    	
//    	namedElement.setName(name);
//    	namedElement.setReferredElement(element);
//    	namedElement.setMayBeImplicit(mayBeImplicit);
//    	
//    	return	namedElement;
//    }
    }
}
