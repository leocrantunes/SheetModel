/*
 * Created on May 10, 2004
 *
 * To change the template for this generated file go to
 * Window>Preferences>Java>Code Generation>Code and Comments
 */
package br.ufrj.cos.lens.odyssey.tools.psw.objectSpace;

import java.util.Collection;
import java.util.Iterator;

import ocl20.common.CoreAttribute;
import ocl20.common.CoreClassifier;

import br.ufrj.cos.lens.odyssey.tools.psw.semantics.values.OclBooleanValue;
import br.ufrj.cos.lens.odyssey.tools.psw.semantics.values.OclIntegerValue;
import br.ufrj.cos.lens.odyssey.tools.psw.semantics.values.OclObjectValue;
import br.ufrj.cos.lens.odyssey.tools.psw.semantics.values.OclRealValue;
import br.ufrj.cos.lens.odyssey.tools.psw.semantics.values.OclStringValue;

/**
 * @author Administrator
 *
 * To change the template for this generated type comment go to
 * Window>Preferences>Java>Code Generation>Code and Comments
 */
public class TestPSWObjectValue extends TestPSWOclEvaluator {


	public void testGetAllAttributes() {
		CoreClassifier cls = (CoreClassifier) environment.lookup("SpecialFilm");
		
		Collection atts = cls.getAllAttributesTransitiveClosure();
		
		for (Iterator iter = atts.iterator(); iter.hasNext(); ) {
			CoreAttribute att = (CoreAttribute) iter.next();
			System.out.println("att name = " + att.getName());
		}
		
		assertTrue(true);
	}
	
	public	void	testCreateSpecialFilmObject() {
		
		try {
			IObjectSpace objectSpace = new PSWObjectSpace();
			CoreClassifier cls = (CoreClassifier) environment.lookup("SpecialFilm");
			OclObjectValue obj1 = new PSWOclObjectValue(GUIDGenerator.generateGUID(), cls, objectSpace);
			assertNotNull(obj1);
			assertTrue(((OclBooleanValue) obj1.getValueOf(cls.lookupAttribute("lateReturnFee")).executeOperation("oclIsUndefined", null)).booleanValue());
			assertTrue(((OclBooleanValue) obj1.getValueOf(cls.lookupAttribute("name")).executeOperation("oclIsUndefined", null)).booleanValue());
			assertTrue(((OclBooleanValue) obj1.getValueOf(cls.lookupAttribute("rentalFee")).executeOperation("oclIsUndefined", null)).booleanValue());
			assertTrue(((OclBooleanValue) obj1.getValueOf(cls.lookupAttribute("days")).executeOperation("oclIsUndefined", null)).booleanValue());
			assertTrue(((OclBooleanValue) obj1.getValueOf(cls.lookupAttribute("code")).executeOperation("oclIsUndefined", null)).booleanValue());
		} catch (IllegalArgumentException e) {
			fail();			
		} catch (Exception e) {
			e.printStackTrace();
			fail();			
		}
	}

	public	void	testNonExistentAttribute() {
		
		try {
			CoreClassifier cls = (CoreClassifier) environment.lookup("SpecialFilm");
			OclObjectValue obj1 = new PSWOclObjectValue(GUIDGenerator.generateGUID(), cls, null);
			assertNotNull(obj1);
			assertNotNull(obj1.getValueOf(cls.lookupAttribute("fooAttribute")));
			fail();
		} catch (IllegalArgumentException e) {
		} catch (Exception e) {
			e.printStackTrace();
			fail();			
		}
	}

	public	void	testSetValueOfAttribute() {
		
		try {
			IObjectSpace objectSpace = new PSWObjectSpace();
			CoreClassifier cls = (CoreClassifier) environment.lookup("SpecialFilm");
			OclObjectValue obj1 = new PSWOclObjectValue(GUIDGenerator.generateGUID(), cls, objectSpace);
			assertNotNull(obj1);
			obj1.setValueOf(cls.lookupAttribute("lateReturnFee"), new OclRealValue("10.45"));
			obj1.setValueOf(cls.lookupAttribute("name"), new OclStringValue("Titanic"));
			obj1.setValueOf(cls.lookupAttribute("rentalFee"), new OclIntegerValue(10));
			obj1.setValueOf(cls.lookupAttribute("days"), new OclIntegerValue(23));
			obj1.setValueOf(cls.lookupAttribute("code"), new OclStringValue("ABC123"));

			assertEquals(10.45, ( (OclRealValue) obj1.getValueOf(cls.lookupAttribute("lateReturnFee"))).doubleValue().doubleValue(), 0.00001);
			assertEquals("Titanic", ( (OclStringValue) obj1.getValueOf(cls.lookupAttribute("name"))).stringValue());
			assertEquals(10, ( (OclIntegerValue) obj1.getValueOf(cls.lookupAttribute("rentalFee"))).intValue());
			assertEquals(23, ( (OclIntegerValue) obj1.getValueOf(cls.lookupAttribute("days"))).intValue());
			assertEquals("ABC123", ( (OclStringValue) obj1.getValueOf(cls.lookupAttribute("code"))).stringValue());
			
			obj1.setValueOf(cls.lookupAttribute("lateReturnFee"), new OclIntegerValue(10));
			assertEquals(10.0, ( (OclRealValue) obj1.getValueOf(cls.lookupAttribute("lateReturnFee"))).doubleValue().doubleValue(), 0.00001);
			
		} catch (IllegalArgumentException e) {
			fail();			
		} catch (Exception e) {
			e.printStackTrace();
			fail();			
		}
	}

	public	void	testIncorrectSetValueOfAttribute() {
		try {
			CoreClassifier cls = (CoreClassifier) environment.lookup("SpecialFilm");
			OclObjectValue obj1 = new PSWOclObjectValue(GUIDGenerator.generateGUID(), cls, null);
			assertNotNull(obj1);
			obj1.setValueOf(cls.lookupAttribute("lateReturnFee"), new OclStringValue("Titanic"));
			fail();
		} catch (IllegalArgumentException e) {
		} catch (Exception e) {
			e.printStackTrace();
			fail();			
		}
	}
}
