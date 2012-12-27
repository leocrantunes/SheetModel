using System.Collections.Generic;
using OclLibrary.iface;

namespace OclLibrary.impl.util
{
    public class AstOclConstraintFactoryManager {
        private static Dictionary<Ocl20Package, AstOclConstraintFactory> astFactoryMap = new Dictionary<Ocl20Package, AstOclConstraintFactory>();

        public static AstOclConstraintFactory getInstance(Ocl20Package oclPackage) {
            AstOclConstraintFactory factory;
            astFactoryMap.TryGetValue(oclPackage, out factory);
            if (factory == null) {
                factory = new AstOclConstraintFactoryImpl(oclPackage);
                astFactoryMap.Add(oclPackage, factory);
            }
            return	factory;
        }
    }
}
