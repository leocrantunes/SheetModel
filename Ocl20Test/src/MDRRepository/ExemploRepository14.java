package br.ufrj.cos.lens.odyssey.MDRRepository;

import org.netbeans.api.mdr.*;

import org.omg.uml14.foundation.core.*;
import org.omg.uml14.foundation.datatypes.*;
import org.omg.uml14.modelmanagement.Model;
import org.omg.uml14.modelmanagement.UmlPackage;

import org.openide.ErrorManager;

/*
 * Created on 26/09/2003
 *
 * To change the template for this generated file go to
 * Window>Preferences>Java>Code Generation>Code and Comments
 */

/**
 * @author Alexandre Correa
 *
 * To change the template for this generated type comment go to
 * Window>Preferences>Java>Code Generation>Code and Comments
 */
import java.io.File;
import java.io.FileInputStream;
import java.io.InputStream;

import java.util.Collection;
import java.util.Iterator;

import javax.jmi.model.ModelPackage;


/**
 * DOCUMENT ME!
 *
 * @author $author$
 * @version $Revision: 1.2 $
 */
public class ExemploRepository14 {
    // name of MOF extent
    private static final String MOF_EXTENT_NAME = "MOF";
    private static String UML_METAMODEL_EXTENT;

    // name of UML model extent
    private static String UML_USERMODEL_EXTENT_NAME;

    // input UML model that will be imported to the repository
    private static String umlModelFileName;

    // name of xml file containing the UML metamodel definition
    private static final String uml14MetamodelFileName = "resource/metamodels/01-02-15_Diff.xml";

	// directory - MDR repository 
	private static	String	mdrRepositoryLocation;

    //	private static	String	umlMetamodelFileName;
    // output UML model that will be exported from the repository
    private static final String outputFileName = "output.xml";
    private MDRManager manager;
    private MDRepository repository;
    private ModelPackage mofMetaModel;
    private org.omg.uml.UmlPackage umlUserModel;

    /**
     * DOCUMENT ME!
     *
     * @param args DOCUMENT ME!
     */
    public static void main(String[] args) {
        ExemploRepository14 example = new ExemploRepository14();
        UML_USERMODEL_EXTENT_NAME = args[0];
        umlModelFileName = args[1];
		mdrRepositoryLocation = args[2];

        example.run();
    }

    /**
     * DOCUMENT ME!
     *
     * @param element DOCUMENT ME!
     *
     * @return DOCUMENT ME!
     */
    public boolean isEnumeration(ModelElement element) {
        if (element instanceof Enumeration) {
            return true;
        }

        if (element instanceof UmlClass) {
            UmlClass cls = (UmlClass) element;

            Collection stereotypes = cls.getStereotype();

            for (Iterator it = stereotypes.iterator(); it.hasNext();) {
                Stereotype s = (Stereotype) it.next();

                if (s.getName()
                         .toUpperCase()
                         .equals("ENUMERATION")) {
                    return true;
                }
            }
        }

        return false;
    }

    /**
     * DOCUMENT ME!
     */
    public void run() {
        String uri;
        InputStream stream;

        final String UML_METAMODEL_EXTENT = "UML14";
        final String UML_USER_MODEL_EXTENT = UML_USERMODEL_EXTENT_NAME;

        try {
			MOFMetamodelRepositoryFactory.setRepositoryLocation(mdrRepositoryLocation);
            MOFMetamodelRepository repository = MOFMetamodelRepositoryFactory
                .getRepository();

            File uml14MetaModel = new File(uml14MetamodelFileName);
            uri = uml14MetaModel.toURL()
                                .toString();
            stream = new FileInputStream(uml14MetaModel);

            if (repository.getExtent(UML_METAMODEL_EXTENT) == null) {
                repository.importM2Model(UML_METAMODEL_EXTENT, stream, uri);
            }

            File umlUserModel = new File(umlModelFileName);
            uri = umlUserModel.toURL()
                              .toString();
            stream = new FileInputStream(umlUserModel);
            repository.importM1Model(UML_USER_MODEL_EXTENT, stream, uri,
                UML_METAMODEL_EXTENT, "UML");

            this.umlUserModel = (org.omg.uml.UmlPackage) repository.getExtent(UML_USER_MODEL_EXTENT);

            showAllElements(this.umlUserModel);

            MOFMetamodelRepositoryFactory.releaseRepository();
        } catch (Exception e) {
            ErrorManager.getDefault()
                        .notify(ErrorManager.ERROR, e);
        }
    }

    /**
     * DOCUMENT ME!
     *
     * @param umlModel DOCUMENT ME!
     */
    public void showAllElements(org.omg.uml.UmlPackage umlModel) {
        // iterate over all packages of the model
        // if you open the MDRBrowser you will see inside the Model_Management package
        // a metaclass Package. Expanding this metaclass, you will see all the packages
        // that were instantiated in the user model.
        // getUmlPackage().refAllOfType() will return all instances of metaclass Package
        for (Iterator it = umlModel.getModelManagement()
                                   .getModel()
                                   .refAllOfType()
                                   .iterator(); it.hasNext();) {
            showModel((Model) it.next());
        }
    }

    /**
     * DOCUMENT ME!
     *
     * @param features DOCUMENT ME!
     */
    public void showAllFeatures(Collection features) {
        for (Iterator it = features.iterator(); it.hasNext();) {
            ModelElement element = (ModelElement) it.next();

            if (element instanceof Attribute) {
                showAttribute((Attribute) element);
            }

            if (element instanceof Operation) {
                showOperation((Operation) element);
            }
        }
    }

    /**
     * DOCUMENT ME!
     *
     * @param association DOCUMENT ME!
     */
    public void showAssociation(UmlAssociation association) {
        System.out.println("\nAssociation: " + association.getName());

        for (Iterator it = association.getConnection()
                                      .iterator(); it.hasNext();) {
            showAssociationEnd((AssociationEnd) it.next());
        }
    }

    /**
     * DOCUMENT ME!
     *
     * @param associationEnd DOCUMENT ME!
     */
    public void showAssociationEnd(AssociationEnd associationEnd) {
        MultiplicityRange range = (MultiplicityRange) associationEnd.getMultiplicity()
                                                                    .getRange()
                                                                    .iterator()
                                                                    .next();

        System.out.println("  association end: " + associationEnd.getName()
            + "  multiplicity: " + range.getLower() + ".." + range.getUpper()
            + "  associated classifier: "
            + associationEnd.getParticipant().getName());
    }

    /**
     * DOCUMENT ME!
     *
     * @param attribute DOCUMENT ME!
     */
    public void showAttribute(Attribute attribute) {
        System.out.println("   attribute " + attribute.getName() + ": "
            + attribute.getType().getName());
    }

    /**
     * DOCUMENT ME!
     *
     * @param umlClass DOCUMENT ME!
     */
    public void showClass(UmlClass umlClass) {
        System.out.println("\nclass: " + umlClass.getName());

        showClassifier(umlClass);

        for (Iterator it = umlClass.getClientDependency()
                                   .iterator(); it.hasNext();) {
            showRealizations((Dependency) it.next());
        }
    }

    /**
     * DOCUMENT ME!
     *
     * @param umlClassifier DOCUMENT ME!
     */
    public void showClassifier(Classifier umlClassifier) {
        for (Iterator it = umlClassifier.getGeneralization()
                                        .iterator(); it.hasNext();) {
            showSuperClass((Generalization) it.next());
        }

        showAllFeatures(umlClassifier.getFeature());
    }

    /**
     * DOCUMENT ME!
     *
     * @param enum DOCUMENT ME!
     */
    public void showEnumeration(Classifier enumeration) {
        System.out.println("Enumeration: " + enumeration.getName());

        for (Iterator it = enumeration.getFeature()
                               .iterator(); it.hasNext();) {
            ModelElement element = (ModelElement) it.next();

            if (element instanceof Attribute) {
                System.out.println("   enumValue: " + element.getName());
            }
        }
    }

    /**
     * DOCUMENT ME!
     *
     * @param umlInterface DOCUMENT ME!
     */
    public void showInterface(Interface umlInterface) {
        System.out.println("\ninterface: " + umlInterface.getName());

        showClassifier(umlInterface);
    }

    /**
     * DOCUMENT ME!
     *
     * @param umlModel DOCUMENT ME!
     */
    public void showModel(Model umlModel) {
        System.out.println("Model: " + umlModel.getName());

        for (Iterator it = umlModel.getOwnedElement()
                                   .iterator(); it.hasNext();) {
            ModelElement element = (ModelElement) it.next();

            System.out.println("model element " + element.getName() + " - "
                + element.getClass().getName());

            if (element instanceof UmlPackage) {
                showUmlPackage((UmlPackage) element);
            }

            if (element instanceof UmlAssociation) {
                showAssociation((UmlAssociation) element);
            }

            if (element instanceof UmlClass) {
                showClass((UmlClass) element);
            }
        }
    }

    /**
     * DOCUMENT ME!
     *
     * @param operation DOCUMENT ME!
     */
    public void showOperation(Operation operation) {
        System.out.println("   operation " + operation.getName());

        for (Iterator it = operation.getParameter()
                                    .iterator(); it.hasNext();) {
            showParameter((Parameter) it.next());
        }
    }

    /**
     * DOCUMENT ME!
     *
     * @param parameter DOCUMENT ME!
     */
    public void showParameter(Parameter parameter) {
        System.out.println("       parameter " + parameter.getName() + ": "
            + parameter.getType().getName() + "  kind: " + parameter.getKind());
    }

    /**
     * DOCUMENT ME!
     *
     * @param realization DOCUMENT ME!
     */
    public void showRealizations(Dependency realization) {
        for (Iterator it = realization.getSupplier()
                                      .iterator(); it.hasNext();) {
            Classifier c = (Classifier) it.next();
            System.out.println("   realizes: " + c.getName());
        }
    }

    /**
     * DOCUMENT ME!
     *
     * @param generalization DOCUMENT ME!
     */
    public void showSuperClass(Generalization generalization) {
        System.out.println("   inherits from: "
            + generalization.getParent().getName());
    }

    /**
     * DOCUMENT ME!
     *
     * @param umlPackage DOCUMENT ME!
     */
    public void showUmlPackage(UmlPackage umlPackage) {
        System.out.println("\n***********************************");
        System.out.println("Package: " + umlPackage.getName());
        System.out.println("***********************************");

        for (Iterator it = umlPackage.getOwnedElement()
                                     .iterator(); it.hasNext();) {
            ModelElement element = (ModelElement) it.next();

            System.out.println("package element " + element.getName() + " - "
                + element.getClass().getName());

            if (isEnumeration(element)) {
                showEnumeration((Classifier) element);
            }
            // show only classes and interfaces defined in this package
            else if (element instanceof UmlClass) {
                showClass((UmlClass) element);
            } else if (element instanceof Interface) {
                showInterface((Interface) element);
            } else if (element instanceof Classifier) {
                showClassifier((Classifier) element);
            } else if (element instanceof UmlPackage) {
                showUmlPackage((UmlPackage) element);
            }
        }

        System.out.println("\n***********************************");
        System.out.println("End of Package: " + umlPackage.getName());
        System.out.println("***********************************");
    }
}
