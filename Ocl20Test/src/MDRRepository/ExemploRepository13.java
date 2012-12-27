package br.ufrj.cos.lens.odyssey.MDRRepository;

import org.netbeans.api.mdr.*;

import org.omg.uml13.foundation.core.AssociationClass;
import org.omg.uml13.foundation.core.AssociationEnd;
import org.omg.uml13.foundation.core.Attribute;
import org.omg.uml13.foundation.core.Classifier;
import org.omg.uml13.foundation.core.Dependency;
import org.omg.uml13.foundation.core.Generalization;
import org.omg.uml13.foundation.core.Interface;
import org.omg.uml13.foundation.core.ModelElement;
import org.omg.uml13.foundation.core.Operation;
import org.omg.uml13.foundation.core.Parameter;
import org.omg.uml13.foundation.core.UmlAssociation;
import org.omg.uml13.foundation.core.UmlClass;
import org.omg.uml13.foundation.datatypes.MultiplicityRange;
import org.omg.uml13.foundation.datatypes.ScopeKindEnum;
import org.omg.uml13.foundation.extensionmechanisms.Stereotype;
import org.omg.uml13.modelmanagement.Model;
import org.omg.uml13.modelmanagement.Subsystem;
import org.omg.uml13.modelmanagement.UmlPackage;

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

// parameters for activation: 

public class ExemploRepository13 {
    // name of MOF extent
    private static final String MOF_EXTENT_NAME = "MOF";
    private static String UML_METAMODEL_EXTENT;

    // name of UML model extent
    private static String UML_USERMODEL_EXTENT_NAME;

    // input UML model that will be imported to the repository
    private static String umlModelFileName;

	// directory - MDR repository 
	private static	String	mdrRepositoryLocation;

    // name of xml file containing the UML metamodel definition
    private static final String uml13MetamodelFileName = "resource/metamodels/01-12-02_Diff.xml";
    private MDRManager manager;
    private MDRepository repository;
    private ModelPackage mofMetaModel;
    private uml.UmlPackage umlUserModel;

    private  int total;
    /**
     * DOCUMENT ME!
     *
     * @param args DOCUMENT ME!
     */
    public static void main(String[] args) {
        ExemploRepository13 example = new ExemploRepository13();
        UML_USERMODEL_EXTENT_NAME = args[0];
        umlModelFileName = args[1];
        mdrRepositoryLocation = args[2];

        example.run();
    }

    /**
     * DOCUMENT ME!
     *
     * @param element DOCUMENT ME!
     * @param enumerationStereotype DOCUMENT ME!
     *
     * @return DOCUMENT ME!
     */
    public boolean isEnumeration(
        ModelElement element,
        Stereotype   enumerationStereotype) {
        if (element instanceof UmlClass && (enumerationStereotype != null)) {
            UmlClass cls = (UmlClass) element;

            for (Iterator it = enumerationStereotype.getExtendedElement()
                                                    .iterator(); it.hasNext();) {
                if (it.next() == element) {
                    return true;
                }
            }
        }

        return false;
    }

    /**
     * DOCUMENT ME!
     *
     * @param element DOCUMENT ME!
     *
     * @return DOCUMENT ME!
     */
    public boolean isEnumerationStereotype(ModelElement element) {
        if (element instanceof Stereotype) {
            Stereotype stereotype = (Stereotype) element;

            for (Iterator it = stereotype.getExtendedElement()
                                         .iterator(); it.hasNext();) {
                System.out.println("Stereotyped element "
                    + stereotype.getName() + " - "
                    + ((ModelElement) it.next()).getName());
            }

            return stereotype.getName()
                             .equals("enumeration");
        }

        return false;
    }

    /**
     * DOCUMENT ME!
     */
    public void run() {
        String uri;
        InputStream stream;

        final String UML_METAMODEL_EXTENT = "UML13";
        final String UML_USER_MODEL_EXTENT = UML_USERMODEL_EXTENT_NAME;

        try {
        	MOFMetamodelRepositoryFactory.setRepositoryLocation(mdrRepositoryLocation);
            MOFMetamodelRepository repository = MOFMetamodelRepositoryFactory
                .getRepository();

            File uml13MetaModel = new File(uml13MetamodelFileName);
            uri = uml13MetaModel.toURL()
                                .toString();
            stream = new FileInputStream(uml13MetaModel);

            if (repository.getExtent(UML_METAMODEL_EXTENT) == null) {
                repository.importM2Model(UML_METAMODEL_EXTENT, stream, uri);
            }

            File umlUserModel = new File(umlModelFileName);
            uri = umlUserModel.toURL()
                              .toString();
            stream = new FileInputStream(umlUserModel);
            repository.importM1Model(UML_USER_MODEL_EXTENT, stream, uri,
                UML_METAMODEL_EXTENT, "UML");

            this.umlUserModel = (uml.UmlPackage) repository.getExtent(UML_USER_MODEL_EXTENT);

            showAllElements(this.umlUserModel);
            System.out.println("total = " + total);

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
    public void showAllElements(uml.UmlPackage umlModel) {
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
     * @param associationClass DOCUMENT ME!
     */
    public void showAssociationClass(AssociationClass associationClass) {
        System.out.println("\nAssociation Class: " + associationClass.getName());

        for (Iterator it = associationClass.getConnection()
                                           .iterator(); it.hasNext();) {
            showAssociationEnd((AssociationEnd) it.next());
        }

        showClass(associationClass);
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
            + "  associated classifier: " + associationEnd.getType().getName());

        if (associationEnd.getQualifier()
                              .size() > 0) {
            System.out.println("	qualifiers: ");
        }

        for (Iterator it = associationEnd.getQualifier()
                                         .iterator(); it.hasNext();) {
            Attribute qualifier = (Attribute) it.next();
            showAttribute(qualifier);
        }
    }

    /**
     * DOCUMENT ME!
     *
     * @param attribute DOCUMENT ME!
     */
    public void showAttribute(Attribute attribute) {
        String scope;

        if (attribute.getOwnerScope() == ScopeKindEnum.SK_INSTANCE) {
            scope = "  -  instance scope";
        } else {
            scope = "  -  classifier scope";
        }

        System.out.println("   attribute " + attribute.getName() + ": "
            + attribute.getType().getName() + scope);
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
        System.out.println("\nClassifier: " + umlClassifier.getName());

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
        System.out.println("\nInterface: " + umlInterface.getName());

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

            if (element instanceof UmlPackage
                    && !(element instanceof Subsystem)) {
                showUmlPackage((UmlPackage) element);
            } else if (element instanceof UmlAssociation) {
                showAssociation((UmlAssociation) element);
            } else if (element instanceof UmlClass) {
                showClass((UmlClass) element);
            } else if (element instanceof Classifier) {
                showClassifier((Classifier) element);
            } else {
                System.out.println("\nModel Element:" + element.getName()
                    + " - " + element.getClass().getName());
            }
//            total++;
        }
    }

    /**
     * DOCUMENT ME!
     *
     * @param operation DOCUMENT ME!
     */
    public void showOperation(Operation operation) {
        if (operation.getName()
                         .charAt(0) != '$') {
            System.out.println("   operation :" + operation.getName()
                + "  -  instance scope" + " - isQuery: " + operation.isQuery() + " - isAbstract: " + operation.isAbstract());
        } else {
            System.out.println("   operation :" + operation.getName()
                + "  -  classifier scope" + " - isQuery: " + operation.isQuery() + " - isAbstract: " + operation.isAbstract());
        }

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
        System.out.println("       parameter :" + parameter.getName() + " :"
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

        Stereotype enumerationStereotype = null;

        for (Iterator it = umlPackage.getOwnedElement()
                                     .iterator(); it.hasNext();) {
            ModelElement element = (ModelElement) it.next();

            // show only classes and interfaces defined in this package
            System.out.println("Package element " + element.getName() + " - "
                + element.getClass().getName());

            if (isEnumerationStereotype(element)) {
                enumerationStereotype = (Stereotype) element;
            }

            if (isEnumeration(element, enumerationStereotype)) {
                showEnumeration((Classifier) element);
            }
            // show only classes and interfaces defined in this package
            else if (element instanceof AssociationClass) {
                showAssociationClass((AssociationClass) element);
            } else if (element instanceof UmlClass) {
                showClass((UmlClass) element);
            }
            //			else if (element instanceof Interface)
            //				showInterface((Interface) element);
            else if (element instanceof Classifier) {
                showClassifier((Classifier) element);
            } else if (element instanceof UmlPackage) {
                showUmlPackage((UmlPackage) element);
            } else if (element instanceof UmlAssociation) {
                showAssociation((UmlAssociation) element);
            }
            total++;
        }
    }
}
