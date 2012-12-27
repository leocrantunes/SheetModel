/*
 * Created on 18/02/2005
 *
 * TODO To change the template for this generated file go to
 * Window - Preferences - Java - Code Style - Code Templates
 */
package br.ufrj.cos.lens.odyssey.MDRRepository;

import impl.ocl20.common.CoreModelElementImpl;

import java.io.File;
import java.io.FileInputStream;
import java.io.InputStream;
import java.lang.reflect.Method;
import java.util.ArrayList;
import java.util.Collection;
import java.util.Iterator;
import java.util.List;

import javax.jmi.model.EnumerationType;
import javax.jmi.model.ModelElement;
import javax.jmi.model.ModelPackage;
import javax.jmi.model.MofClass;
import javax.jmi.model.MofPackage;
import javax.jmi.model.Namespace;
import javax.jmi.reflect.RefPackage;


import ocl20.Ocl20Package;
import ocl20.common.CoreAttribute;
import ocl20.common.CoreClassifier;
import ocl20.common.CoreModelElement;

import org.netbeans.api.mdr.MDRManager;
import org.netbeans.api.mdr.MDRepository;
import org.omg.uml13.foundation.datatypes.Expression;
import org.omg.uml13.foundation.datatypes.Multiplicity;
import org.omg.uml13.foundation.datatypes.MultiplicityRange;
import org.openide.ErrorManager;


import uml.UmlPackage;

/**
 * @author Administrator
 *
 * TODO To change the template for this generated type comment go to
 * Window - Preferences - Java - Code Style - Code Templates
 */
public class MofExample  {
    // name of MOF extent
    private static final String MOF_EXTENT_NAME = "MOF";
    
    //  name of UML metamodel extent
    private static String UML13_METAMODEL_EXTENT_NAME = "UML13-METAMODEL";
    // name of xml file containing the UML metamodel definition
    private static final String uml13MetamodelFileName = "resource/metamodels/01-12-02_Diff.xml";

    // name of UML model extent
    private static String UML13_USERMODEL_EXTENT_NAME;
    // input UML model that will be imported to the repository
    private static String uml13ModelFileName;


    //  name of UML metamodel extent
    private static String UML14_METAMODEL_EXTENT_NAME = "UML14-METAMODEL";
    // name of xml file containing the UML metamodel definition
    private static final String uml14MetamodelFileName = "resource/metamodels/01-02-15_Diff.xml";

    // name of UML model extent
    private static String UML14_USERMODEL_EXTENT_NAME;
    // input UML model that will be imported to the repository
    private static String uml14ModelFileName;

    
    //  name of UML metamodel extent
    private static String MY_METAMODEL_EXTENT_NAME = "MY-METAMODEL";
    // name of xml file containing the UML metamodel definition
    private static final String myMetamodelFileName = "resource/metamodels/MOFOCL20.xml";

    
    private MDRManager manager;
    private MDRepository repository;
    private ModelPackage mofMetaModel;
    private RefPackage uml13UserModel;
    private RefPackage uml14UserModel;
    private	ModelPackage myMetaModel;
    private RefPackage  myOtherModel;
    
    private int totalM2;
    private int totalM1;
    
    private List showedObjects;

    /**
     * DOCUMENT ME!
     *
     * @param args DOCUMENT ME!
     */
    public static void main(String[] args) {
        MofExample example = new MofExample();
        UML13_USERMODEL_EXTENT_NAME = args[0];
        uml13ModelFileName = args[1];
        UML14_USERMODEL_EXTENT_NAME = args[2];
        uml14ModelFileName = args[3];

        example.run();
    }


    /**
     * DOCUMENT ME!
     */
    public void run() {
        String uri;
        InputStream stream;

        try {
            MOFMetamodelRepository repository = MOFMetamodelRepositoryFactory
                .getRepository();

            File uml13MetaModel = new File(uml13MetamodelFileName);
            uri = uml13MetaModel.toURL()
                                .toString();
            stream = new FileInputStream(uml13MetaModel);

            if (repository.getExtent(UML13_METAMODEL_EXTENT_NAME) == null) {
                repository.importM2Model(UML13_METAMODEL_EXTENT_NAME, stream, uri);
            }


            
            File uml14MetaModel = new File(uml14MetamodelFileName);
            uri = uml14MetaModel.toURL()
                                .toString();
            stream = new FileInputStream(uml14MetaModel);

            if (repository.getExtent(UML14_METAMODEL_EXTENT_NAME) == null) {
                repository.importM2Model(UML14_METAMODEL_EXTENT_NAME, stream, uri);
            }

            
//            File myMetaModel = new File(myMetamodelFileName);
//            uri = myMetaModel.toURL()
//                                .toString();
//            stream = new FileInputStream(myMetaModel);
//
//            if (repository.getExtent(MY_METAMODEL_EXTENT_NAME) == null) {
//                repository.importM2Model(MY_METAMODEL_EXTENT_NAME, stream, uri);
//            }


            this.mofMetaModel = (ModelPackage) repository.getExtent(UML13_METAMODEL_EXTENT_NAME);
//            this.myMetaModel = (ModelPackage) repository.getExtent(MY_METAMODEL_EXTENT_NAME);

          showAllM1Elements(new Mof14GenericObjectPrinter(), this.mofMetaModel);

//            showAllM2Elements(this.mofMetaModel);

            
            File uml13UserModel = new File(uml13ModelFileName);
            uri = uml13UserModel.toURL()
                              .toString();
            stream = new FileInputStream(uml13UserModel);
            repository.importM1Model(UML13_USERMODEL_EXTENT_NAME, stream, uri,
                UML13_METAMODEL_EXTENT_NAME, "UML");


            File uml14UserModel = new File(uml14ModelFileName);
            uri = uml14UserModel.toURL()
                              .toString();
            stream = new FileInputStream(uml14UserModel);
            repository.importM1Model(UML14_USERMODEL_EXTENT_NAME, stream, uri,
                UML14_METAMODEL_EXTENT_NAME, "UML");

            
            
            this.uml13UserModel = (RefPackage) repository.getExtent(UML13_USERMODEL_EXTENT_NAME);
            this.uml14UserModel = (RefPackage) repository.getExtent(UML14_USERMODEL_EXTENT_NAME);
            
//            showAllM1Elements(new Mof14GenericObjectPrinter(), this.mofMetaModel);
            
            System.out.printf("\n\n\n\n\n\n ELEMENTS FROM UML 13 MODEL:\n");
            showAllM1Elements(new  Uml13GenericObjectPrinter(), this.uml13UserModel);
            
            System.out.printf("\n\n\n\n\n\n ELEMENTS FROM UML 14 MODEL:\n");
            showAllM1Elements(new  Uml14GenericObjectPrinter(), this.uml14UserModel);

            System.out.printf("\n\ntotal m1 = %d - total m2 = %d", totalM1, totalM2);

//            testMyMetamodel();
            
            MOFMetamodelRepositoryFactory.releaseRepository();
        } catch (Exception e) {
            ErrorManager.getDefault()
                        .notify(ErrorManager.ERROR, e);
        }
    }

    public void testMyMetamodel() {
//      this.myMetaModel = (ModelPackage) repository.getExtent(MY_METAMODEL_EXTENT_NAME);

    	MofPackage metaPack = null;
        for (Iterator it = this.myMetaModel.getMofPackage().refAllOfClass().iterator(); it.hasNext();) {
        		MofPackage pkg = (MofPackage) it.next();
        		
        		System.out.println("pkg name = " + pkg.getName());
        		if ("OCL20".equals(pkg.getName())) {
        			metaPack = pkg;
        			break;
        		}
        }

        if (metaPack == null) {
        	System.out.println("meta pack = null");
        	return;
        }
        try {
        	MOFMetamodelRepository repository = MOFMetamodelRepositoryFactory
            .getRepository();

        	RefPackage pack = repository.createExtent("OCL20", metaPack);
    	
        	ocl20.Ocl20Package myPack = (Ocl20Package) pack;
    	
        } catch (Exception e) {
        	e.printStackTrace();
        }
        
        
      UmlPackage model = (uml.UmlPackage) this.uml13UserModel;
      Collection allClassifier = model.getFoundation().getCore().getClassifier().refAllOfType();
      CoreModelElementImpl aClassifier = null, bClassifier = null;
      
      for (Iterator iter = allClassifier.iterator(); iter.hasNext();) {
      	aClassifier = (CoreModelElementImpl) iter.next();
      	bClassifier = (CoreModelElementImpl) iter.next();
      	break;
      }
      
      System.out.println(aClassifier.compareTo(aClassifier));
      System.out.println(aClassifier.compareTo(bClassifier));
    }
    
    
    /**
     * DOCUMENT ME!
     *
     * @param umlModel DOCUMENT ME!
     */
    public void showAllM2Elements(ModelPackage umlMetaModel) {
        // iterate over all packages of the model
        // if you open the MDRBrowser you will see inside the Model_Management package
        // a metaclass Package. Expanding this metaclass, you will see all the packages
        // that were instantiated in the user model.
        // getUmlPackage().refAllOfType() will return all instances of metaclass Package
        
    	System.out.println("METAMODEL ELEMENTS \n");
        System.out.println("object = " + umlMetaModel.getClass().getName());
        showAllM2ModelElements(umlMetaModel);
    }

    
    public void showAllM2ModelElements(ModelPackage umlMetaModel) {
        for (Iterator iter = umlMetaModel.getModelElement().refAllOfType().iterator(); iter.hasNext();) {
            ModelElement aElement= (ModelElement) iter.next(); 
            showM2ModelElement(aElement);
            System.out.println(" ");
        }
    }

    
    private void showM2ModelElement(ModelElement element) {
        System.out.printf("  element = %-30.30s - class = %s\n", element.getName(), element.getClass().getName());

        if (element instanceof MofClass) 
        	System.out.println("mof class:  " + element.getName());

        if (element instanceof CoreClassifier) 
        	System.out.println("classifier:  " + element.getName());
        
        if (element instanceof Namespace) {
            showM2Contents((Namespace) element);
        }
        if (element instanceof EnumerationType) {
            EnumerationType enumeration = (EnumerationType) element;
            Collection x = enumeration.getLabels();
            System.out.println("   *** Enumeration Details = " + element.getName());
            for (Iterator iter = x.iterator(); iter.hasNext();) {
                Object o = iter.next();
                System.out.println("      o = " + o.getClass() + " - " + o);
            }
        }
        

        totalM2++;
    }
    
    
    private void showM2Contents(Namespace ns) {
    	System.out.println("  ****  begin contents  ****");
        for (Iterator iter = ns.getContents().iterator(); iter.hasNext();) {
            ModelElement elem = (ModelElement) iter.next();
            showM2ModelElement(elem);
        }
        System.out.println("  ****  end contents ****");
    }
    
    
    public void showAllM1Elements(GenericObjectPrinter objectPrinter, RefPackage m1Model) {
        
        System.out.printf("\n\n\n\n\n\n M1 MODEL ELEMENTS:\n");
        
//        UmlPackage model = (uml.UmlPackage) m1Model;
    
        Method[] methods = m1Model.getClass().getDeclaredMethods();
        for (int i = 0; i < methods.length; i++) {
            try {
                if (methods[i].getReturnType().getName().endsWith("Class") && ! methods[i].getName().endsWith("getClass")) {
                	objectPrinter.showAllInstances(m1Model, methods[i]);
                } else {
                    objectPrinter.showAllDetails(m1Model);	
                }
            } catch (Exception e) {
                e.printStackTrace();
            }
        }
        
        System.out.printf("\n\n\n\n\n\n end m1 model elements\n");
    }

}    

abstract class GenericObjectPrinter {
    
    protected List showedObjects = new ArrayList(100);
    
    public GenericObjectPrinter() {
        showedObjects = new ArrayList(100);
    }
    
    abstract public void    show(Object o);
    
    protected void    showAllDetails(Object o) {
        Method[] methods = o.getClass().getDeclaredMethods();
        for (int i = 0; i < methods.length; i++) {
            try {
                if (methods[i].getReturnType() == null || methods[i].getName() == null)
                    continue;
                
                if (methods[i].getParameterTypes().length > 0)
                    continue;
                
                if (methods[i].getReturnType().getName().endsWith("Class") && ! methods[i].getName().endsWith("getClass")) {
                    showAllInstances(o, methods[i]);
                } else if (methods[i].getName().startsWith("get") || methods[i].getName().startsWith("is")) {
                    Object abc = (Object) methods[i].invoke(o, (Object[]) null);
                    if (abc != null && ( (!(abc instanceof Collection)) || abc instanceof Collection && ((Collection)abc).size() > 0)) {
                        System.out.printf("      \n        #### " + methods[i].getName() + ": ");
                        show(abc);
                    }   
                }
            } catch (Exception e) {
                e.printStackTrace();
            }
        }
    }

    protected void    showAllInstances(Object object, Method method) throws Exception {
        System.out.printf("\n\n***** " + object.getClass().getName() + "." + method.getName() + "****\n");
        Object abc = (Object) method.invoke(object, (Object[]) null);
        
        Method refAllOfType = abc.getClass().getMethod("refAllOfType", (Class[]) null);
        Collection result = (Collection) refAllOfType.invoke(abc, (Object[]) null);

        for (Iterator iter = result.iterator(); iter.hasNext();) {
            try {
                Object o = (Object) iter.next();
           
                Method getName = o.getClass().getMethod("getName", (Class[]) null);
                if (getName != null) {
                    System.out.println(" ");
                    System.out.println(" ");
                    System.out.println("    element name =  " + ((String) getName.invoke(o, (Object[]) null)) + "      -  class : " + o.getClass().getName());
                    if (! showedObjects.contains(o))
                    	showAllDetails(o);
                    showedObjects.add(o);
                }
                else
                    System.out.println("    element class : " + o.getClass().getName());
            } catch (Exception e) {
            } 
        }
        System.out.printf("\n***** number of elements in %s = %d", object.getClass().getName() + "."+ method.getName(),  result.size());
    }   
}


class   Uml13GenericObjectPrinter extends GenericObjectPrinter {
    public void show(Object o) {
        if (showedObjects.contains(o))
            return;
        
        if (o instanceof Collection) {
            Collection x = (Collection) o;
            for (Iterator iter = x.iterator(); iter.hasNext();) {
                show(iter.next());
            }
        } else if (o instanceof RefPackage) {
            showAllDetails(o);
            showedObjects.add(o);
        } else if (o instanceof Multiplicity || o instanceof Expression || o instanceof MultiplicityRange) {
            showedObjects.add(o);
            showAllDetails(o);
        } else if (o instanceof org.omg.uml13.foundation.core.ModelElement) {
            String name  = ((org.omg.uml13.foundation.core.ModelElement)o).getName();
            if (name != null && name.length() > 0)
                System.out.print(((org.omg.uml13.foundation.core.ModelElement)o).getName() + " ");
            else
                System.out.print(o + ";  ");
            
            if (o instanceof CoreModelElement) {
            	CoreModelElement elem = (CoreModelElement) o;
            	if (elem.getModel() != null) {
            		System.out.print("   - modelName = " + elem.getModel().getName());
            		if (elem.getName().equals("package_1_1")) {
            			Collection x = elem.getElemOwnedElements();
            			for (Iterator iter = x.iterator(); iter.hasNext();) {
            				CoreModelElement elem1 = (CoreModelElement) iter.next();
            				System.out.print(" -- " + elem1.getName());
            			}
            		}
            	} else {
            		System.out.print("   - modelName = null");
            	}
            }
            
            if (o instanceof CoreAttribute) {
            	CoreAttribute attr = (CoreAttribute) o;
            	System.out.print("  - owner: " + attr.getFeatureOwner().getName() + " - instancescope? " + attr.isInstanceScope() + " - type: " + attr.getFeatureType().getName() + ";  ");
            }
            
        } else {
            System.out.print(o + ";  ");
        }
    }
}


class   Uml14GenericObjectPrinter extends GenericObjectPrinter {
    public void show(Object o) {
        if (showedObjects.contains(o)) {
            if (o instanceof org.omg.uml14.foundation.core.ModelElement) {
                String name  = ((org.omg.uml14.foundation.core.ModelElement)o).getName();
                if (name != null && name.length() > 0)
                    System.out.print(((org.omg.uml14.foundation.core.ModelElement)o).getName() + " ");
                else
                    System.out.print(o + ";  ");
            }
            return;
        } 
        
        if (o instanceof Collection) {
            Collection x = (Collection) o;
            for (Iterator iter = x.iterator(); iter.hasNext();) {
                showedObjects.add(o);
                show(iter.next());
            }
        } else if (o instanceof RefPackage) {
            showAllDetails(o);
            showedObjects.add(o);
        } else if (o instanceof org.omg.uml14.foundation.datatypes.Multiplicity || o instanceof org.omg.uml14.foundation.datatypes.Expression || o instanceof org.omg.uml14.foundation.datatypes.MultiplicityRange) {
            showedObjects.add(o);
            showAllDetails(o);
        } else if (o instanceof org.omg.uml14.foundation.core.ModelElement) {
            String name  = ((org.omg.uml14.foundation.core.ModelElement)o).getName();
            if (name != null && name.length() > 0)
                System.out.print(((org.omg.uml14.foundation.core.ModelElement)o).getName() + " ");
            else
                System.out.print(o + ";  ");
        } else {
            System.out.print(o + ";  ");
        }
    }
}


class   Mof14GenericObjectPrinter extends GenericObjectPrinter {
    public void show(Object o) {
        if (showedObjects.contains(o)) {
        	if (o instanceof ModelElement) {
                String name  = ((ModelElement)o).getName();
                if (name != null && name.length() > 0)
                    System.out.print(((ModelElement)o).getName() + " ");
                else
                    System.out.print(o + ";  ");
            } 
            return;
        }
        
        if (o instanceof Collection) {
            Collection x = (Collection) o;
            for (Iterator iter = x.iterator(); iter.hasNext();) {
            	showedObjects.add(o);
                show(iter.next());
            }
        } else if (o instanceof RefPackage) {
        	showedObjects.add(o);
            showAllDetails(o);
        } else if (o instanceof ModelElement) {
            String name  = ((ModelElement)o).getName();
            if (name != null && name.length() > 0)
                System.out.print(((ModelElement)o).getName() + " ");
            else
                System.out.print(o + ";  ");
        } else {
            System.out.print(o + ";  ");
        }
    }
}
