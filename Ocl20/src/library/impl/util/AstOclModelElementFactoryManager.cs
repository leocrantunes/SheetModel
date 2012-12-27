using System.Collections.Generic;
using Ocl20.library.iface;
using Ocl20.library.iface.util;

namespace Ocl20.library.impl.util
{
    public class AstOclModelElementFactoryManager  {
        private static Dictionary<Ocl20Package, AstOclModelElementFactory> astFactoryMap = new Dictionary<Ocl20Package, AstOclModelElementFactory>();

        public static AstOclModelElementFactory getInstance(Ocl20Package oclPackage) {
            AstOclModelElementFactory factory;
            astFactoryMap.TryGetValue(oclPackage, out factory);
            if (factory == null) {
                factory = oclPackage.getUtil().getAstOclModelElementFactory().createAstOclModelElementFactory();
                ((AstOclModelElementFactoryImpl) factory).setOclPackage(oclPackage);
                astFactoryMap.Add(oclPackage, factory);
            }
            return	factory;
        }
    }
}
