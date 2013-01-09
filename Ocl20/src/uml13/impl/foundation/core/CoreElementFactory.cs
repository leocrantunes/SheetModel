using System;
using System.Collections.Generic;
using Ocl20.library.iface.common;
using Ocl20.uml13.iface.common;
using Ocl20.uml13.iface.foundation.core;
using Ocl20.uml13.iface.foundation.datatypes;
using Ocl20.uml13.iface.foundation.extensionmechanisms;
using Attribute = Ocl20.uml13.iface.foundation.core.Attribute;

namespace Ocl20.uml13.impl.foundation.core
{
    public class CoreElementFactory {
        public CoreAttribute createSpecificAttribute(CoreClassifier classifier, String name, CoreClassifier type) {
            UmlPackage mainPackage = (UmlPackage) classifier.getModel().getMainPackage();
    	
            Attribute attribute = mainPackage.getFoundation().getCore().getAttribute();
            attribute.setName(name);
            attribute.setNamespace((Classifier) classifier);
            attribute.setType((Classifier) type);
    	
            return (CoreAttribute) attribute;
        }

        public CoreOperation createSpecificOperation(CoreClassifier classifier, String name, List<object> paramNames, List<object> paramTypes, CoreClassifier returnType) {
            UmlPackage mainPackage = (UmlPackage) classifier.getModel().getMainPackage();
    	
            Operation operation = mainPackage.getFoundation().getCore().getOperation();
            operation.setName(name);
            operation.setNamespace((Classifier) this);
    	
            for (int i = 0; i < paramNames.Count; i++) {
                String	paramName = (String) paramNames[i];
                Parameter newParameter = mainPackage.getFoundation().getCore().getParameter();

                newParameter.setBehavioralFeature(operation);
                newParameter.setName(paramName);
                newParameter.setType((Classifier) paramTypes[i]);
                newParameter.setKind(ParameterDirectionKindEnum.PDK_IN);
            }

            Parameter returnParameter = mainPackage.getFoundation().getCore().getParameter();

            returnParameter.setBehavioralFeature(operation);
            returnParameter.setName("return");
            returnParameter.setType((Classifier) returnType);
            returnParameter.setKind(ParameterDirectionKindEnum.PDK_RETURN);
    	
            return (CoreOperation) operation;
        }

        public void createSpecificStereotype(CoreClassifier classifier, CoreFeature feature, String stereotypeName)  {
            UmlPackage mainPackage = (UmlPackage) classifier.getModel().getMainPackage();
    	
            //verificar
            Stereotype stereotype = mainPackage.getFoundation().getExtensionMechanisms().getStereotype();
    	
            stereotype.setName(stereotypeName);
            mainPackage.getFoundation().getExtensionMechanisms().getAStereotypeExtendedElement().add(stereotype, (Feature) feature);
        }
    }
}
