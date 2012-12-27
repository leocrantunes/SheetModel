/*
 * Created on Oct 27, 2004
 *
 * To change the template for this generated file go to
 * Window&gt;Preferences&gt;Java&gt;Code Generation&gt;Code and Comments
 */
package br.ufrj.cos.lens.odyssey.tools.psw.project;


import java.util.ArrayList;
import java.util.Iterator;
import java.util.List;
import java.util.Set;

import ocl20.common.CoreClassifier;
import ocl20.common.CoreModel;
import ocl20.environment.Environment;
//import ocl20.environment.NamedElement;

import br.ufrj.cos.lens.odyssey.tools.psw.parser.OCLCompilerException;
import br.ufrj.cos.lens.odyssey.tools.psw.semantics.types.OclTypesFactory;

import junit.framework.TestCase;

/**
 * @author Administrator
 *
 * To change the template for this generated type comment go to
 * Window&gt;Preferences&gt;Java&gt;Code Generation&gt;Code and Comments
 */
public class TestPswProject extends TestCase {

	public	void	setup() {
	}

	public	void	testCreateNewProject() {
		String	name = "alex";
		int	metamodel = PswProjectConstants.UML13_METAMODEL;
		String	xmiFile = "tests/resource/examples/myExampleRose.xml";
		List	oclFiles = null;

		try {		
			PswProjectSession	session = new PswProjectSession();
			PswProject project = session.createProject(name, metamodel, xmiFile, oclFiles);
			CoreModel model = project.getModel();
			Environment env = model.getEnvironmentWithoutParents();
			OclTypesFactory.setEnvironment(model);
			CoreClassifier classifier = (CoreClassifier) env.lookup("Film");
			assertNotNull(classifier);
		}
		catch (PswProjectSessionException e) {
			fail();		
		}
	}

	public	void	testOpenProject() {
		String	name = "john.psw";
		int	metamodel = PswProjectConstants.UML13_METAMODEL;
		String	xmiFile = "tests/resource/examples/myExampleRose.xml";
		List	oclFiles = null;

		try {		
			PswProjectSession	session = new PswProjectSession();
			PswProject project = session.createProject(name, metamodel, xmiFile, oclFiles);
			session.saveProject(name);
			PswProject otherProject = session.openProject(name);
			
			CoreModel model = otherProject.getModel();
			assertNotNull(model);
			CoreClassifier classifier = (CoreClassifier) session.getCurrentProject().getEnvironment().lookup("Film");
			assertNotNull(classifier);
			OclTypesFactory.setEnvironment(model);
		}
		catch (PswProjectSessionException e) {
			fail();		
		}
	}


	public	void	testCompileFile() {
		String	name = "alex";
		int	metamodel = PswProjectConstants.UML13_METAMODEL;
		String	xmiFile = "tests/resource/examples/myExampleRose.xml";
		List	oclFiles = null;

		try {		
			PswProjectSession	session = new PswProjectSession();
			PswProject project = session.createProject(name, metamodel, xmiFile, oclFiles);
			Set	errors = session.compileOclFile("tests/resource/examples/oclFile01.ocl");
			assertEquals(0, errors.size());

			errors = session.compileOclFile("tests/resource/examples/oclFile02.ocl");
			assertTrue(errors.size() > 0);
			System.out.println("\nerrors - " + "testCompileFile" + ": ");
			for (Iterator iter = errors.iterator(); iter.hasNext();) {
				OCLCompilerException element = (OCLCompilerException) iter.next();
				System.out.println(element);
			}
		}
		catch (PswProjectSessionException e) {
			e.printStackTrace();		
			fail();
		}
	}


	public	void	testCompileProject() {
		String	name = "alex";
		int	metamodel = PswProjectConstants.UML13_METAMODEL;
		String	xmiFile = "tests/resource/examples/myExampleRose.xml";
		List	oclFiles = new ArrayList();
		oclFiles.add("tests/resource/examples/oclFile03.ocl");
		oclFiles.add("tests/resource/examples/oclFile01.ocl");
		oclFiles.add("tests/resource/examples/oclFile02.ocl");

		try {		
			PswProjectSession	session = new PswProjectSession();
			PswProject project = session.createProject(name, metamodel, xmiFile, oclFiles);
			Set	errors = session.compileProject();
			assertTrue(errors.size() > 0);
			System.out.println("\nerrors - " + "testCompileProject" + ": ");
			for (Iterator iter = errors.iterator(); iter.hasNext();) {
				OCLCompilerException element = (OCLCompilerException) iter.next();
				System.out.println(element);
			}
		}
		catch (PswProjectSessionException e) {
			e.printStackTrace();		
			fail();
		}
	}


	public	void	testRemoveOclFile() {
		String	name = "alex";
		int	metamodel = PswProjectConstants.UML13_METAMODEL;
		String	xmiFile = "tests/resource/examples/myExampleRose.xml";
		List	oclFiles = new ArrayList();
		oclFiles.add("tests/resource/examples/oclFile01.ocl");
		oclFiles.add("tests/resource/examples/oclFile03.ocl");
		oclFiles.add("tests/resource/examples/oclFile02.ocl");

		PswProjectSession	session = null;

		try {		
			session = new PswProjectSession();
			PswProject project = session.createProject(name, metamodel, xmiFile, oclFiles);
			Set	errors = session.compileProject();
			assertTrue(errors.size() > 0);
			System.out.println("\nerrors - " + "testRemoveOclFile" + ": ");
			for (Iterator iter = errors.iterator(); iter.hasNext();) {
				OCLCompilerException element = (OCLCompilerException) iter.next();
				System.out.println(element);
			}
		}
		catch (PswProjectSessionException e) {
			e.printStackTrace();		
			fail();
		}
		
		oclFiles = new ArrayList();
		oclFiles.add("tests/resource/examples/oclFile02.ocl");
		oclFiles.add("tests/resource/examples/oclFile03.ocl");
		
		try {		
			PswProject project = session.updateProject(name, metamodel, xmiFile, oclFiles);
			Set	errors = session.compileProject();
			assertTrue(errors.size() > 0);
			System.out.println("\nerrors: ");
			for (Iterator iter = errors.iterator(); iter.hasNext();) {
				OCLCompilerException element = (OCLCompilerException) iter.next();
				System.out.println(element);
			}
		}
		catch (PswProjectSessionException e) {
			e.printStackTrace();		
			fail();
		}
	}

	public	void	testLoyaltyProject() {
		String	name = "loyalty";
		int	metamodel = PswProjectConstants.UML13_METAMODEL;
		String	xmiFile = "tests/resource/examples/loyalty/loyalty.xml";
		List	oclFiles = new ArrayList();
		oclFiles.add("tests/resource/examples/loyalty/LoyaltyProgram.ocl");
		oclFiles.add("tests/resource/examples/loyalty/Burning.ocl");
		oclFiles.add("tests/resource/examples/loyalty/Customer.ocl");
		oclFiles.add("tests/resource/examples/loyalty/CustomerCard.ocl");
		oclFiles.add("tests/resource/examples/loyalty/LoyaltyAccount.ocl");
		oclFiles.add("tests/resource/examples/loyalty/Membership.ocl");
		oclFiles.add("tests/resource/examples/loyalty/ProgramPartner.ocl");
		oclFiles.add("tests/resource/examples/loyalty/Service.ocl");
		oclFiles.add("tests/resource/examples/loyalty/ServiceLevel.ocl");
		oclFiles.add("tests/resource/examples/loyalty/Transaction.ocl");
		oclFiles.add("tests/resource/examples/loyalty/TransactionReport.ocl");
		oclFiles.add("tests/resource/examples/loyalty/TransactionReportLine.ocl");
		
		try {		
			PswProjectSession	session = new PswProjectSession();
			PswProject project = session.createProject(name, metamodel, xmiFile, oclFiles);
			
//			CoreClassifier bool = (CoreClassifier) project.getEnvironment().lookup("Date");
//			assertNotNull(bool);
//			Environment env = bool.getEnvironmentWithoutParents();
//			Collection elements = env.getNamedElements();
//			for (Iterator iter = elements.iterator(); iter.hasNext();) {
//				NamedElement n = (NamedElement) iter.next();
//				System.out.println(n.getName() +  " : " + n.getReferredElement().getClass().getName());
//			}
			
			Set	errors = session.compileProject();
			System.out.println("\nerrors - " + name + ": ");
			for (Iterator iter = errors.iterator(); iter.hasNext();) {
				OCLCompilerException element = (OCLCompilerException) iter.next();
				System.out.println(element);
			}
			assertEquals(0, errors.size());

			for (Iterator iter = oclFiles.iterator(); iter.hasNext();) {
				String element = (String) iter.next();
				System.out.println(element + ": " + session.getCurrentProject().getDependantFilesTransitiveClosure(element));
			}
		}
		catch (PswProjectSessionException e) {
			e.printStackTrace();		
			fail();
		}
	}

	public	void	testBookProject() {
		String	name = "book";
		int	metamodel = PswProjectConstants.UML13_METAMODEL;
		String	xmiFile = "tests/resource/examples/book/book.xml";
		List	oclFiles = new ArrayList();
		oclFiles.add("tests/resource/examples/book/Appendix.ocl");
		oclFiles.add("tests/resource/examples/book/Bookpart.ocl");
		oclFiles.add("tests/resource/examples/book/Chapter.ocl");
		oclFiles.add("tests/resource/examples/book/ChapterDependency.ocl");
		oclFiles.add("tests/resource/examples/book/Period.ocl");
		oclFiles.add("tests/resource/examples/book/Prependix.ocl");
		
		try {		
			PswProjectSession	session = new PswProjectSession();
			PswProject project = session.createProject(name, metamodel, xmiFile, oclFiles);
			Set	errors = session.compileProject();
			System.out.println("\nerrors - " + name + ": ");
			for (Iterator iter = errors.iterator(); iter.hasNext();) {
				OCLCompilerException element = (OCLCompilerException) iter.next();
				System.out.println(element);
			}
			assertEquals(0, errors.size());
		}
		catch (PswProjectSessionException e) {
			e.printStackTrace();		
			fail();
		}
	}
}