/*
 * Created on Apr 16, 2005
 *
 * TODO To change the template for this generated file go to
 * Window - Preferences - Java - Code Style - Code Templates
 */
package impl.ocl20.oclScript;

import br.ufrj.cos.lens.odyssey.tools.psw.oclScript.metamodel.base.Action;

/**
 * @author alexcorr
 *
 * TODO To change the template for this generated type comment go to
 * Window - Preferences - Java - Code Style - Code Templates
 */
public class OclActionBodyDefinition  {

	private	Action	action;
	
	/**
	 * @param action
	 */
	public OclActionBodyDefinition(Action action) {
		this.action = action;
	}
	
	public Action getAction() {
		return	this.action;
	}
	
}
