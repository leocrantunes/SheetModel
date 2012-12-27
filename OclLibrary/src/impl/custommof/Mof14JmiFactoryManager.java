/*
 * Created on 16/03/2005
 *
 * TODO To change the template for this generated file go to
 * Window - Preferences - Java - Code Style - Code Templates
 */
package impl.ocl20.custommof;


import java.util.HashMap;
import java.util.Map;

import ocl20.Ocl20Package;

/**
 * @author Administrator
 *
 * TODO To change the template for this generated type comment go to
 * Window - Preferences - Java - Code Style - Code Templates
 */
public class Mof14JmiFactoryManager {

    private static Map<Ocl20Package, Mof14JmiFactory> astFactoryMap = new HashMap<Ocl20Package, Mof14JmiFactory>();

    public static Mof14JmiFactory getInstance(Ocl20Package oclPackage) {
    	Mof14JmiFactory factory = astFactoryMap.get(oclPackage);
    	if (factory == null) {
    		factory = new Mof14JmiFactory(oclPackage);
    		astFactoryMap.put(oclPackage, factory);
    	}
    	return	factory;
    }
}
