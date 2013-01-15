using System;
using System.Collections.Generic;
using Ocl20.library.iface.common;

namespace Ocl20.library.impl.common
{
    public class CoreElementFactory {
        public CoreAttribute createSpecificAttribute(CoreClassifier classifier, String name, CoreClassifier type) {
            CorePackage mainPackage = (CorePackage) classifier.getModel().getMainPackage();

            CoreAttribute attribute = null;
            //CoreAttribute attribute = mainPackage.getFoundation().getCore().getAttribute();
            //attribute.setName(name);
            //attribute.setNamespace((CoreClassifier) classifier);
            //attribute.setType((CoreClassifier) type);
    	
            return (CoreAttribute) attribute;
        }

        public CoreOperation createSpecificOperation(CoreClassifier classifier, String name, List<object> paramNames, List<object> paramTypes, CoreClassifier returnType) {
            CorePackage mainPackage = (CorePackage) classifier.getModel().getMainPackage();

            CoreOperation operation = null;
            //CoreOperation operation = mainPackage.getFoundation().getCore().getOperation();
            //operation.setName(name);
            //operation.setNamespace((CoreClassifier) this);
    	
            //for (int i = 0; i < paramNames.Count; i++) {
            //    String	paramName = (String) paramNames[i];
            //    Parameter newParameter = mainPackage.getFoundation().getCore().getParameter();

            //    newParameter.setBehavioralFeature(operation);
            //    newParameter.setName(paramName);
            //    newParameter.setType((CoreClassifier) paramTypes[i]);
            //    newParameter.setKind(ParameterDirectionKindEnum.PDK_IN);
            //}

            //Parameter returnParameter = mainPackage.getFoundation().getCore().getParameter();

            //returnParameter.setBehavioralFeature(operation);
            //returnParameter.setName("return");
            //returnParameter.setType((CoreClassifier) returnType);
            //returnParameter.setKind(ParameterDirectionKindEnum.PDK_RETURN);
    	
            return (CoreOperation) operation;
        }

        public void createSpecificStereotype(CoreClassifier classifier, CoreFeature feature, String stereotypeName)  {
            CorePackage mainPackage = (CorePackage) classifier.getModel().getMainPackage();

            CoreStereotype stereotype = null;
            //CoreStereotype stereotype = new CoreStereotypeImpl();
    	
            //stereotype.setName(stereotypeName);
            //mainPackage.getFoundation().getExtensionMechanisms().getAStereotypeExtendedElement().add(stereotype, (CoreFeature) feature);
        }
    }
}
