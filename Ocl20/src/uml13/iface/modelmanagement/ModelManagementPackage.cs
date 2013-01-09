/**
 * Model_Management package interface.
 */

using Ocl20.uml13.iface.modelmanagement;
using UmlPackage = Ocl20.uml13.iface.common.UmlPackage;

public interface ModelManagementPackage {
    /**
     * Returns UmlPackage class proxy object.
     * @return UmlPackage class proxy object.
     */
    UmlPackage getUmlPackage();
    /**
     * Returns Model class proxy object.
     * @return Model class proxy object.
     */
    Model getModel();
    /**
     * Returns Subsystem class proxy object.
     * @return Subsystem class proxy object.
     */
    //Subsystem getSubsystem();
    /**
     * Returns ElementImport class proxy object.
     * @return ElementImport class proxy object.
     */
    //ElementImport getElementImport();
    /**
     * Returns AModelElementElementImport association proxy object.
     * @return AModelElementElementImport association proxy object.
     */
    //AModelElementElementImport getAModelElementElementImport();
    /**
     * Returns APackageElementImport association proxy object.
     * @return APackageElementImport association proxy object.
     */
    //APackageElementImport getAPackageElementImport();
}
