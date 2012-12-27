/*
 * Created on 19/07/2004
 *
 * To change the template for this generated file go to
 * Window>Preferences>Java>Code Generation>Code and Comments
 */
package br.ufrj.cos.lens.odyssey.tools.psw.parser.semantic;

import ocl20.common.CoreClassifier;


/**
 * @author Administrador
 *
 * To change the template for this generated type comment go to
 * Window>Preferences>Java>Code Generation>Code and Comments
 */

public class TestInvConstraints extends TestPropertyCallExp {
	public void testInv_01() {
		doTestContextOK("context Film inv : name.size() <= 10",     
				getCurrentMethodName());
	}

	public void testInv_02() {
		doTestManyContextOK(
										"context Film inv: name.size() < 10 " +
										"context Tape inv: number > 30 " + 
										"context SpecialFilm inv: name.size() < 50",     
				getCurrentMethodName());
	}

	public void testInv_03() {
		doTestManyContextOK(
										"context Film inv  names01: name.size() < 10 " +
										"context Tape inv: number > 30 " + 
										"context SpecialFilm inv names01: name.size() < 50",     
				getCurrentMethodName());
	}

	public void testInv_04() {
		doTestManyContextOK(
										"context Film inv  names01: name.size() < 10 " +
										"context Tape inv: number > 30 " + 
										"context Film inv names02: name.size() < 50",     
				getCurrentMethodName());
	}

	public void testInv_05() {
		doTestManyContextOK(
										"context Film inv  names01: name.size() < 10 " +
										"context Film inv : name.size() > 20 " + 
										"context Film inv : name.size() < 50",     
				getCurrentMethodName());
				
		CoreClassifier	film = (CoreClassifier) environment.lookup("Film");
		assertEquals(3, film.getAllInvariants().size());
		assertNotNull(film.getInvariant("names01"));				
	}

	public void testInvNotOK_01() {
		doTestContextNotOK("context Film inv : name.size() ",     
				getCurrentMethodName());
	}

	public void testInvNotOK_02() {
		doTestManyContextNotOK(
										"context Film inv  names01: name.size() < 10 " +
										"context Tape inv: number > 30 " + 
										"context Film inv names01: name.size() < 50",     
				getCurrentMethodName());
	}

}

