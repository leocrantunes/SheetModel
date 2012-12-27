/*
 * Created on Nov 28, 2003
 *
 * To change the template for this generated file go to
 * Window>Preferences>Java>Code Generation>Code and Comments
 */
package br.ufrj.cos.lens.odyssey.tools.psw.parser.semantic;

import br.ufrj.cos.lens.odyssey.tools.psw.parser.cst.literalExp.CSTLiteralExpCS;

/**
 * @author Administrator
 *
 * To change the template for this generated type comment go to
 * Window>Preferences>Java>Code Generation>Code and Comments
 */
public abstract class TestLiteralExp extends TestNodeCS {
	
	
	/* (non-Javadoc)
	 * @see br.ufrj.cos.lens.odyssey.tools.psw.parser.semantic.TestNodeCS#getSpecificNodeClass()
	 */
	protected Class getSpecificNodeClass() throws Exception {
		return CSTLiteralExpCS.class;
	}

}
