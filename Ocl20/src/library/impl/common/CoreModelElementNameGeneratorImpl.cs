using System;
using System.Collections.Generic;
using System.Text;
using Ocl20.library.iface.common;

namespace Ocl20.library.impl.common
{
    public class CoreModelElementNameGeneratorImpl : ModelElementNameGenerator {
	
        private static ModelElementNameGenerator instance;
        private static String OP_PARAM_TYPES_SEPARATOR = "$$";

        public static ModelElementNameGenerator getInstance() {
            if (instance == null) {
                instance = new CoreModelElementNameGeneratorImpl();
            }	

            return instance;
        }

        /* (non-Javadoc)
	 * 	@see br.cos.ufrj.lens.odyssey.tools.psw.metamodels.core.uml14Bridge.ModelElementNameGenerator#generateName()
	 */
        public String generateName(CoreModelElement element) {
            if (element.GetType() == typeof(CoreOperation))
                return generateNameForOperation((CoreOperation) element);
            else	
                return element.getName();
        }

        public String generateNameForOperation(
            String name,
            List<object> paramTypes) {
            List<object> paramTypesNames = new List<object>();

            foreach (CoreClassifier param in paramTypes) {
                paramTypesNames.Add(param.getName());
            }
		
            return generateOperationName(name, paramTypesNames);
            }


        private String generateNameForOperation(CoreOperation operation) {
            List<object> parametersNames = operation.getParametersTypesNamesExceptReturn();

            return generateOperationName(operation.getName(), parametersNames);
        }

        public String generateOperationName(String name, List<object> paramTypesNames) {
            StringBuilder mangledName = new StringBuilder(name);

            foreach (var paramTypesName in paramTypesNames)
            {
                mangledName.Append(OP_PARAM_TYPES_SEPARATOR + paramTypesName);
            }

            return mangledName.ToString();
        }

        public bool operationNameMatches(String operationMangledName, String name) {
            string[] names = operationMangledName.Split(new[] { OP_PARAM_TYPES_SEPARATOR }, StringSplitOptions.None);
            String opName = names.Length > 0 ? names[0] : null;
            return ((opName != null) && opName.Equals(name));
        }
    }
}