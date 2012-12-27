using System.Collections.Generic;
using Ocl20.library.iface;

namespace Ocl20.library.impl.util
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
