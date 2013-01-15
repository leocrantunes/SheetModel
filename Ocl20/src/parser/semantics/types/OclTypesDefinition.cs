using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;
using Ocl20.library.iface;
using Ocl20.library.iface.common;
using Ocl20.library.iface.types;
using Ocl20.library.impl;
using Ocl20.library.impl.common;
using Ocl20.library.impl.environment;
using Ocl20.xmireader;
using CorePackage = Ocl20.library.iface.common.CorePackage;
using Environment = Ocl20.library.iface.environment.Environment;

namespace Ocl20.parser.semantics.types
{
    public class OclTypesDefinition {

        private static String[] collectionNames = {
            "Set", "Bag", "Sequence", "OrderedSet", "Collection"
        };
        private static String[] genericCollectionNames = {
            "Set<", "Bag<", "Sequence<", "OrderedSet<", "Collection<"
        };
	    
        private static Dictionary<String, String> basicTypes;
	    
        static OclTypesDefinition() {
            basicTypes = new Dictionary<string, string>();
            basicTypes.Add("REAL", "Real");
            basicTypes.Add("DOUBLE", "Real");
            basicTypes.Add("FLOAT", "Real");
            basicTypes.Add("INTEGER", "Integer");
            basicTypes.Add("BYTE", "Integer");
            basicTypes.Add("SHORT", "Integer");
            basicTypes.Add("INT", "Integer");
            basicTypes.Add("LONG", "Integer");
            basicTypes.Add("STRING", "String");
            basicTypes.Add("BOOLEAN", "Boolean");
            basicTypes.Add("BOOL", "Boolean");
            basicTypes.Add("DATE", "Date");
            basicTypes.Add("DATETIME", "DateTime");
            basicTypes.Add("VOID", "oclVoid");
            basicTypes.Add("OCLANY", "OclAny");
        }
	    
        private static String UMLMODEL_DEFINING_OCLPRIMITIVETYPES = "OCLTypes";
        private static Environment oclTypesEnvironment = null;
        private static Ocl20Package oclPackage = null;
        private static CoreModel oclTypesModel = null;

        public static Environment getEnvironment() {
            try {
                loadTypesDefinitions();
                return	oclTypesEnvironment;
            } catch (Exception e) {
                Console.WriteLine(e.StackTrace);
            }
	    	
            return	oclTypesEnvironment;
        }
	    
        public static void resetEnvironment() {
            oclTypesEnvironment = null;
        }
	    
        private static void loadTypesDefinitions() {
            if (oclTypesEnvironment == null)
            {
                //Uml14ModelsRepository modelRepository = new Uml14ModelsRepository(MOFMetamodelRepositoryFactory.getRepository());
                //String extentName = "UMLMODEL_DEFINING_OCLPRIMITIVETYPES";
                //modelRepository.importModel(extentName, "resource/metamodels/oclPrimitiveTypes.xml");

                XmiReader reader = new XmiReader(@"C:\Users\Leo\Documents\visual studio 2010\Projects\SheetModel_20121206\SheetModel\Ocl20\resource\metamodels\oclPrimitiveTypes.xml");
                oclTypesModel = reader.getMetamodel();
                //oclTypesModel = modelRepository.getModelWithoutOCL(extentName);

                if (oclTypesModel != null)
                {
                    if (oclPackage == null)
                        oclPackage = new Ocl20PackageImpl();
                    
                    oclTypesEnvironment = EnvironmentFactoryManager.getInstance(oclPackage).getEnvironmentInstance();
                    oclTypesModel.setOclPackage(oclPackage);
                    oclTypesModel.populateEnvironment(oclTypesEnvironment);
                }
            }
        }
	    
        public static CoreClassifier getType(String name) {
            if (name.IndexOf("::", StringComparison.Ordinal) >= 0) {
                return (CoreClassifier) oclTypesEnvironment.lookupPathName(name);
            } else {
                return (CoreClassifier) oclTypesEnvironment.lookup(name);
            }
        }

        public static bool isOclPrimitiveType(String name) {
            String type;
            basicTypes.TryGetValue(name.ToUpper(), out type);
            return ((oclTypesEnvironment != null) && (oclTypesEnvironment.lookup(name) != null)) || type != null;
        }

        public static CoreClassifier getOclPrimitiveType(String name) {
            if (oclTypesEnvironment != null) {
                String	oclTypeName;
                basicTypes.TryGetValue(name.ToUpper(), out oclTypeName);
                CoreClassifier oclClassifier = (CoreClassifier) oclTypesEnvironment.lookup(oclTypeName);
    			
                if (oclTypeName != null && oclClassifier  != null)
                    return	oclClassifier;
                else if (oclTypesEnvironment.lookup(name) != null)
                    return	(CoreClassifier) oclTypesEnvironment.lookup(name); 
            } 
            return	null;
        }
	    
        public static bool isOclGenericCollectionType(String name) {
            for (int i = 0; i < genericCollectionNames.Length; i++)
                if (name.StartsWith(genericCollectionNames[i])) {
                    return true;
                }

            return false;
        }

        public static bool isOclCollectionType(String name) {
            for (int i = 0; i < collectionNames.Length; i++)
                if (name.StartsWith(collectionNames[i]))
                    return true;
            
            return false;
        }

        public static bool isOclTupleType(String name) {
            return (name.StartsWith("Tuple("));
        }
	    
        public static bool isOclVoidType(String name) {
            return name.Equals("OclVoid");
        }

        public static bool isOclType(String name) {
            return isOclPrimitiveType(name) || isOclCollectionType(name) || isOclTupleType(name) || isOclVoidType(name);
        }
	    
        public static bool isOclTypesModel(CoreModel model) {
            return model.getEnvironmentWithoutParents()
                        .lookup("Set<T>") != null;
        }

        public static bool typeNeedsToBeParsed(String name) {
            for (int i = 0; i < collectionNames.Length; i++)
                if (name.StartsWith(collectionNames[i]) && (name.IndexOf('<') < 0)) {
                    return true;
                }
            if (name.StartsWith("Tuple") && (name.IndexOf('<') < 0)) {
                return	true;
            }

            return false;
        }

        public static bool typeNeedsToBeParsed(CoreClassifier classifier) {
            if  (! (classifier.GetType() == typeof(CollectionType))) {
                return	typeNeedsToBeParsed(classifier.getName());
            }

            return false;
        }

        public void populateEnvironment(Environment environment, CoreModel model) {
            const bool isFirstLevel = true;

            addAllClassifiersFromInnerPackages(isFirstLevel, model, environment);
        }

        protected void addAllClassifiersFromInnerPackages(
            bool firstLevel,
            CorePackage aPackage,
            Environment environment) {
            foreach (CoreModelElement element in aPackage.getElemOwnedElements()) {
                if (element.getName() == null) {
                    continue;
                }

                if (isClassifierToBeAdded(element)) {
                    addElementToEnvironment(element.getName(),
                                            element,
                                            environment);
                } else if (element.GetType() == typeof(CorePackage)) {
                    if (firstLevel) {
                        addElementToEnvironment(element.getName(),
                                                element,
                                                environment);
                    }

                    const bool notFirstLevel = true;
                    addAllClassifiersFromInnerPackages(notFirstLevel, (CorePackage) element, environment);
                }
            }
            }

        protected void addElementToEnvironment(
            String name,
            CoreModelElement wrapper,
            Environment environment) {
            try {
                environment.addElement(name, wrapper, false);
            } catch (NameClashException e) {
                environment.removeElement(name);
            }
            }

        protected bool isClassifierToBeAdded(CoreModelElement element) {
            return	element.GetType() == typeof(CoreClassifier);
        }

//	    public static CoreClassifier parseType(
//	        Environment environment,
//	        String name) {
//	        CSTNode node = null;
//
//	        try {
//	            PSWOclCompiler oclCompiler = new PSWOclCompiler(environment, new ConstraintSourceTrackerImpl());
//	            List nodes = oclCompiler.compileOclStream(name, "",
//	                    new PrintWriter(System.out), CSTTypeCS.class);
//	            node = (CSTNode) nodes.get(0);
//	        } catch (Exception e) {
//	            System.out.println(e.getMessage());
//	        }
//
//	        if (node != null) {
//	            CSTTypeCS typeNode = (CSTTypeCS) node;
//
//	            return typeNode.getAst();
//	        } else {
//	            return null;
//	        }
//	    }
//	}

    }
}
