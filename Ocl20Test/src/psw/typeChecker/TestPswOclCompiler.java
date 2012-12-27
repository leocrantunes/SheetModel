/*
 * Created on Oct 30, 2004
 *
 * To change the template for this generated file go to
 * Window&gt;Preferences&gt;Java&gt;Code Generation&gt;Code and Comments
 */
package br.ufrj.cos.lens.odyssey.tools.psw.typeChecker;

import java.io.PrintWriter;
import java.io.Reader;
import java.io.StringReader;
import java.util.ArrayList;
import java.util.Collection;
import java.util.HashMap;
import java.util.Iterator;
import java.util.List;
import java.util.Map;
import java.util.Set;

import ocl20.common.CoreClassifier;
import ocl20.common.CoreModel;
import ocl20.common.CoreOperation;
import ocl20.environment.Environment;

import br.ufrj.cos.lens.odyssey.MDRRepository.MOFMetaModelRepositoryException;
import br.ufrj.cos.lens.odyssey.MDRRepository.MOFMetamodelRepositoryFactory;
import br.ufrj.cos.lens.odyssey.MDRRepository.MOFRepositoryReader;
import br.ufrj.cos.lens.odyssey.MDRRepository.models.Uml13ModelsRepository;
import br.ufrj.cos.lens.odyssey.tools.psw.parser.OCLCompilerException;
import br.ufrj.cos.lens.odyssey.tools.psw.semantics.types.OclTypesFactory;
import junit.framework.TestCase;

/**
 * @author Administrator
 *
 * To change the template for this generated type comment go to
 * Window&gt;Preferences&gt;Java&gt;Code Generation&gt;Code and Comments
 */
public class TestPswOclCompiler extends TestCase {
	private	Environment	environment;
	private	CoreModel 	model;
	
	public	void	setUp() throws Exception {
		super.setUp();
		assertNotNull(environment = getEnvironment());
	}

	public	void	testInvariantRecompilation() {
		String	newline = "\r\n";
		
		String	oclFile = 
		"context Film inv : name.size() <= 10" 
		+	newline +     
		"context Film inv: name.size() < 10" 
		+ newline +     
		"context Tape inv: number > 30 \n" 
		+ newline +     
		"context SpecialFilm inv: name.size() < 50"      
		+ newline +     
    	"context Film inv  names01: name.size() < 10 " 
		+ newline +
		"context Tape inv: number > 30 " 
		+ newline + 
		"context SpecialFilm inv names01: name.size() < 50"
		+ newline +     
		"context Film inv  names02: name.size() < 10 " 
		+ newline +
		"context Tape inv: number > 30 " 
		+ newline + 
		"context Film inv names03: name.size() < 50"
		+ newline +     
		"context Film inv  names04: name.size() < 10 " 
		+ newline +
		"context Film inv : name.size() > 20 " 
		+ newline + 
		"context Film" 
		+ newline +     
		"inv : name.size() < 50"
		+ newline +
		"inv  names05: name.size() < 10 "
		+ newline +
		"context Tape inv: number > 30 " 
		+ newline + 
		"context Film inv names06: name.size() < 50"     
		;
		
		String	oclFile2 = 
		"context Film inv : name.size() <= 10"
		; 
		
		try {
			PSWOclCompiler oclCompiler = new PSWOclCompiler(environment, new ConstraintSourceTrackerImpl());
			List rootNode = oclCompiler.compileOclStream(oclFile, getCurrentMethodName(), new PrintWriter(System.out));
			rootNode = oclCompiler.compileOclStream(oclFile2, getCurrentMethodName() + "-a", new PrintWriter(System.out));
			rootNode = oclCompiler.compileOclStream(oclFile, getCurrentMethodName(), new PrintWriter(System.out));
			assertNotNull(rootNode);
			assertTrue(rootNode.size() >= 1);
			checkInvariantsQty("Film", 11);
			checkInvariantsQty("Tape", 4);
			checkInvariantsQty("SpecialFilm", 2);
			
			rootNode = oclCompiler.compileOclStream(oclFile2, getCurrentMethodName(), new PrintWriter(System.out));
			assertNotNull(rootNode);
			assertTrue(rootNode.size() >= 1);
			checkInvariantsQty("Film", 2);

			System.out.println("compilation errors: " + oclCompiler.getErrorsCount());
		} catch (Exception e) {
			System.out.println(e.getMessage());
			fail();
		}

	}


	public	void	testStringStreamNotOK() {
		String	newline = "\r\n";
		
		String	oclFile1 = 
		"context Film inv : name.size() <= 10" 
		+	newline +     
		"context Film inv: name.size() >  10" 
		+ newline +     
		"context Tapex inv: number > 30" 
		+ newline +     
		"context SpecialFilm inv: name.size() < 50 and name.size() >> 10"      
		+ newline +     
		"context Film inv  names01: name.size3() < 10 " 
		+ newline +
		"context Tape inv: number > 30 and number2 > 10 " 
		+ newline + 
		"context SpecialFilm inv names01: name.size() < 50"
		;
		
		String	oclFile2 = 
		"context Film inv : name.size() <= 10" 
		+	newline +     
		"context Film inv: name.size() >  10" 
		+ newline +     
		"context Tape inv: number > 30" 
		+ newline +     
		"context SpecialFilm inv: name.size() < 50 and name.size() >> 10"      
		+ newline +     
		"context Film inv  names01: name.size3() < 10 " 
		+ newline +
		"context Tape inv: number > 30 and number2 > 10 " 
		+ newline + 
		"context SpecialFilm inv names01: name.size() < 50"
		;

		
		String	oclFile3 = 
		"context Film inv : name.size() <= 10" 
		+	newline +     
		"context Film inv: name.size() >  10" 
		+ newline +     
		"context Tape inv: number > 30" 
		+ newline +     
		"context SpecialFilm inv: name.size() < 50 and name.size() > 10"      
		+ newline +     
		"context Film inv  names01: name.size() < 10 " 
		+ newline +
		"context Tape inv: number > 30 and number2 > 10 " 
		+ newline + 
		"context SpecialFilm inv names01: name.size() < 50"
		;

		String	oclFile4 = 
		"context Film inv : name.size() <= 10" 
		+	newline +     
		"context Film inv: name.size() >  10" 
		+ newline +     
		"context Tape inv: number > 30" 
		+ newline +     
		"context SpecialFilm inv: name.size() < 50 and name.size() > 10"      
		+ newline +     
		"context Film inv  names01: name.size() < 10 " 
		+ newline +
		"context Tape inv: number > 30 and number > 10 " 
		+ newline + 
		"context SpecialFilm inv names01: name.size() < 50"
		;

		ConstraintSourceTracker sourceTracker = new ConstraintSourceTrackerImpl();
		doCompilation(oclFile1, getCurrentMethodName(), false, 2, 0, 1, sourceTracker);
		doCompilation(oclFile2, getCurrentMethodName(), false, 2, 1, 1, sourceTracker);
		doCompilation(oclFile3, getCurrentMethodName(), false, 3, 1, 2, sourceTracker);
		doCompilation(oclFile4, getCurrentMethodName(), true, 3, 2, 2, sourceTracker);
	}

	protected void	doCompilation(String oclFile, String sourceName, boolean rootNodeNotNull, int filmInv, int tapeInv, int specialFilmInv, ConstraintSourceTracker sourceTracker) {
		
		try {
			PSWOclCompiler oclCompiler = new PSWOclCompiler(environment, sourceTracker);
			List rootNode = oclCompiler.compileOclStream(oclFile, sourceName, null);
			if (rootNodeNotNull)
				assertNotNull(rootNode);
			else
				assertNull(rootNode);
			
			checkInvariantsQty("Film", filmInv);
			checkInvariantsQty("Tape", tapeInv);
			checkInvariantsQty("SpecialFilm", specialFilmInv);
			
			Set errors = oclCompiler.getErrors();
			
			System.out.println("\ncompilation errors: " + errors.size());
			for (Iterator iter = errors.iterator(); iter.hasNext();) {
				OCLCompilerException exception = (OCLCompilerException) iter.next();
				System.out.println(exception);
			}
		} catch (Exception e) {
			System.out.println(e.getMessage());
			fail();
		}
		
	}


	public	void	testStringStreamInitRecompilation() {
		String	newline = "\r\n";
		
		String	oclFile1 = 
		"context Film::name init: \"John\" " 
		+	newline +     
		"context Tape::number init: 30" 
		+ newline +     
		"context SpecialFilm::name init: \"Peter\" "      
		;

		String	oclFile2 = 
		"context SpecialFilm::days init: 20 " 
		;

		String	oclFile3 =
		"context Film inv : name.size() > 10";
		
		try {
			PSWOclCompiler oclCompiler = new PSWOclCompiler(environment, new ConstraintSourceTrackerImpl());
			for (int i = 0; i < 2; i++) {
				List rootNode1 = oclCompiler.compileOclStream(oclFile1, getCurrentMethodName(), null);
				List rootNode2 = oclCompiler.compileOclStream(oclFile2, getCurrentMethodName()+"-a", null);
				assertNotNull(rootNode1);
				assertNotNull(rootNode2);
			
				CoreClassifier filmClassifier = (CoreClassifier) environment.lookup("Film");
				assertNotNull(filmClassifier.getInitConstraint("name"));
				
				CoreClassifier tapeClassifier = (CoreClassifier) environment.lookup("Tape");
				assertNotNull(tapeClassifier.getInitConstraint("number"));
				
				CoreClassifier specialfilmClassifier = (CoreClassifier) environment.lookup("SpecialFilm");
				assertNotNull(specialfilmClassifier.getInitConstraint("name"));
				assertNotNull(specialfilmClassifier.getInitConstraint("days"));
			}

			List rootNode = oclCompiler.compileOclStream(oclFile3, getCurrentMethodName(), null);
			assertNotNull(rootNode);
			
			CoreClassifier filmClassifier = (CoreClassifier) environment.lookup("Film");
			assertNull(filmClassifier.getInitConstraint("name"));
				
			CoreClassifier tapeClassifier = (CoreClassifier) environment.lookup("Tape");
			assertNull(tapeClassifier.getInitConstraint("number"));
				
			CoreClassifier specialfilmClassifier = (CoreClassifier) environment.lookup("SpecialFilm");
			assertNull(specialfilmClassifier.getInitConstraint("name"));
			assertNotNull(specialfilmClassifier.getInitConstraint("days"));
			
			Set errors = oclCompiler.getErrors();
			System.out.println("\ncompilation errors: " + errors.size());
			
			for (Iterator iter = errors.iterator(); iter.hasNext();) {
				OCLCompilerException exception = (OCLCompilerException) iter.next();
				System.out.println(exception);
			}
		} catch (Exception e) {
			System.out.println(e.getMessage());
			fail();
		}
	}


	public	void	testStringStreamDeriveRecompilation() {
		String	newline = "\r\n";
		
		String	oclFile1 = 
		"context Film::name derive: \"John\" " 
		+	newline +     
		"context Tape::number derive: 30" 
		+ newline +     
		"context SpecialFilm::name derive: \"Peter\" "      
		;

		String	oclFile2 = 
		"context SpecialFilm::days derive: 20 " 
		;

		String	oclFile3 =
		"context Film inv : name.size() > 10";
		
		try {
			PSWOclCompiler oclCompiler = new PSWOclCompiler(environment, new ConstraintSourceTrackerImpl());
			for (int i = 0; i < 2; i++) {
				List rootNode1 = oclCompiler.compileOclStream(oclFile1, getCurrentMethodName(), null);
				List rootNode2 = oclCompiler.compileOclStream(oclFile2, getCurrentMethodName()+"-a", null);
				assertNotNull(rootNode1);
				assertNotNull(rootNode2);
			
				CoreClassifier filmClassifier = (CoreClassifier) environment.lookup("Film");
				assertNotNull(filmClassifier.getDeriveConstraint("name"));
				
				CoreClassifier tapeClassifier = (CoreClassifier) environment.lookup("Tape");
				assertNotNull(tapeClassifier.getDeriveConstraint("number"));
				
				CoreClassifier specialfilmClassifier = (CoreClassifier) environment.lookup("SpecialFilm");
				assertNotNull(specialfilmClassifier.getDeriveConstraint("name"));
				assertNotNull(specialfilmClassifier.getDeriveConstraint("days"));
			}

			List rootNode = oclCompiler.compileOclStream(oclFile3, getCurrentMethodName(), null);
			assertNotNull(rootNode);
			
			CoreClassifier filmClassifier = (CoreClassifier) environment.lookup("Film");
			assertNull(filmClassifier.getDeriveConstraint("name"));
				
			CoreClassifier tapeClassifier = (CoreClassifier) environment.lookup("Tape");
			assertNull(tapeClassifier.getDeriveConstraint("number"));
				
			CoreClassifier specialfilmClassifier = (CoreClassifier) environment.lookup("SpecialFilm");
			assertNull(specialfilmClassifier.getDeriveConstraint("name"));
			assertNotNull(specialfilmClassifier.getDeriveConstraint("days"));
			
			Set errors = oclCompiler.getErrors();
			System.out.println("\ncompilation errors: " + errors.size());
			
			for (Iterator iter = errors.iterator(); iter.hasNext();) {
				OCLCompilerException exception = (OCLCompilerException) iter.next();
				System.out.println(exception);
			}
		} catch (Exception e) {
			System.out.println(e.getMessage());
			fail();
		}
	}


	public	void	testStringStreamDefAttributeRecompilation() {
		String	newline = "\r\n";
		
		String	oclFile1 = 
		"context Film def: newAttr : Integer = 10"  
		+	newline +     
		"context Tape def: number9 : Integer = 30" 
		+ newline +     
		"context SpecialFilm def: ghj : Integer = 20"
		+ newline +
		"context Tape def: number5 : Integer = 30"
		+ newline
		;

		String	oclFile2 = 
		"context SpecialFilm::days init: 20 " 
		;

		String	oclFile3 = 
		"context Tape::number init: 20 " 
		;

		try {
			PSWOclCompiler oclCompiler = new PSWOclCompiler(environment, new ConstraintSourceTrackerImpl());
			for (int i = 0; i < 2; i++) {
				List rootNode1 = oclCompiler.compileOclStream(oclFile1, getCurrentMethodName(), null);
				showCompilerErrors(oclCompiler);
				List rootNode2 = oclCompiler.compileOclStream(oclFile2, getCurrentMethodName()+"-a", null);
				showCompilerErrors(oclCompiler);
				
				assertNotNull(rootNode1);
				assertNotNull(rootNode2);
				
				CoreClassifier filmClassifier = (CoreClassifier) environment.lookup("Film");
				assertNotNull(filmClassifier.lookupAttribute("newAttr"));
				
				CoreClassifier tapeClassifier = (CoreClassifier) environment.lookup("Tape");
				assertNotNull(tapeClassifier.lookupAttribute("number9"));
				
				CoreClassifier specialfilmClassifier = (CoreClassifier) environment.lookup("SpecialFilm");
				assertNotNull(specialfilmClassifier.lookupAttribute("ghj"));
				
				assertNotNull(tapeClassifier.lookupAttribute("number5"));

			}

			List rootNode = oclCompiler.compileOclStream(oclFile3, getCurrentMethodName(), null);
			assertNotNull(rootNode);

			CoreClassifier filmClassifier = (CoreClassifier) environment.lookup("Film");
			assertNull(filmClassifier.lookupAttribute("newAttr"));
				
			CoreClassifier tapeClassifier = (CoreClassifier) environment.lookup("Tape");
			assertNull(tapeClassifier.lookupAttribute("number9"));
				
			CoreClassifier specialfilmClassifier = (CoreClassifier) environment.lookup("SpecialFilm");
			assertNull(specialfilmClassifier.lookupAttribute("ghj"));
			
		} catch (Exception e) {
			System.out.println(e.getMessage());
			fail();
		}
	}	
		
		public	void	testStringStreamMultiDef() {
			String	newline = "\r\n";
			
			String	oclFile1 = 
			"context Film inv: newAttr > 10"  
			+	newline +     
			"context Tape inv: number9 = 30" 
			+ newline +     
			"context Film def: newAttr : Integer = 20 - getABC()"
			+ newline +
			"context Tape def: number9 : Integer = 30 + number10"
			+ newline +
			"context Tape inv: number10 = number8 + getXYZ()"
			+ newline +
			"context Tape def: number15 : Integer = 30"
			+ newline +
			"context Tape def: col : Set(Integer) = Set{1,2,3}"
			+ newline +
			"context Tape def: getXYZ() : Integer = 50"
			;

			String	oclFile2 = 
			"context SpecialFilm::days init: 20 " 
			+ newline +
			"context Tape def: number7 : Integer = 30"
			;

			String	oclFile3 = 
			"context Tape def: number10 : Integer = 20"
			+ newline + 
			"context Tape def: number11 : Integer = number10 + 40"
			+ newline +
			"context Tape def: number8 : Integer = number7 + getXYZ()"
			+ newline +
			"context Film def: getABC() : Integer = 40"
			+ newline +
			"context Tape def: number16 : Integer = number15 + 20" 
			+ newline +
			"context Tape def: myTuple : Tuple(a : Integer, b : Real) = Tuple{a : Integer = 10, b: Real = 20.50}" 
			;

			try {
				PSWOclCompiler oclCompiler = new PSWOclCompiler(environment, new ConstraintSourceTrackerImpl());
				
				Map<String, Reader> map = new HashMap<String, Reader>();
				map.put(getCurrentMethodName(), new StringReader(oclFile1));
				map.put(getCurrentMethodName() + "-a", new StringReader(oclFile2));
				map.put(getCurrentMethodName() + "-b", new StringReader(oclFile3));
				
				Map<String, List> rootNode1 = oclCompiler.compileListOclStreams(map, null, null);
				showCompilerErrors(oclCompiler);
				assertNotNull(rootNode1);
					
				CoreClassifier filmClassifier = (CoreClassifier) environment.lookup("Film");
				assertNotNull(filmClassifier.lookupAttribute("newAttr"));
					
				CoreClassifier tapeClassifier = (CoreClassifier) environment.lookup("Tape");
				assertNotNull(tapeClassifier.lookupAttribute("number9"));
					
				CoreClassifier specialfilmClassifier = (CoreClassifier) environment.lookup("Tape");
				assertNotNull(specialfilmClassifier.lookupAttribute("number10"));

				
			} catch (Exception e) {
				System.out.println(e.getMessage());
				fail();
			}
	}


	public	void	testStringStreamDefOperationRecompilation() {
		String	newline = "\r\n";
		
		String	oclFile1 = 
		"context Film def: isNewFilm() : Boolean = true"  
		+	newline +     
		"context Tape def: getNumber(x : Integer) : Integer = x + 10" 
		+ newline +
		"context Tape def: getNumber() : Integer = 10" 
		+ newline +     
		"context SpecialFilm def: isSpecialFilm() : Boolean = name = \"Special\" "      
		;

		String	oclFile2 = 
		"context SpecialFilm::days init: 20 " 
		;

		String	oclFile3 = 
		"context Tape::number init: 20 " 
		;

		try {
			PSWOclCompiler oclCompiler = new PSWOclCompiler(environment, new ConstraintSourceTrackerImpl());
			List	noParametersList = new ArrayList();
			CoreClassifier integerType = (CoreClassifier) environment.lookup("Integer");
			List paramList = new ArrayList();
			paramList.add(integerType);

			for (int i = 0; i < 2; i++) {
				List rootNode1 = oclCompiler.compileOclStream(oclFile1, getCurrentMethodName(), null);
				showCompilerErrors(oclCompiler);
				List rootNode2 = oclCompiler.compileOclStream(oclFile2, getCurrentMethodName()+"-a", null);
				showCompilerErrors(oclCompiler);

				assertNotNull(rootNode1);
				assertNotNull(rootNode2);
			
				CoreClassifier filmClassifier = (CoreClassifier) environment.lookup("Film");
				assertNotNull(filmClassifier.lookupOperation("isNewFilm", noParametersList));
				
				CoreClassifier tapeClassifier = (CoreClassifier) environment.lookup("Tape");
				assertNotNull(tapeClassifier.lookupOperation("getNumber", noParametersList));
				assertNotNull(tapeClassifier.lookupOperation("getNumber", paramList));
				
				CoreClassifier specialfilmClassifier = (CoreClassifier) environment.lookup("SpecialFilm");
				assertNotNull(specialfilmClassifier.lookupOperation("isSpecialFilm", noParametersList));
			}

			List rootNode = oclCompiler.compileOclStream(oclFile3, getCurrentMethodName(), null);
			assertNotNull(rootNode);

			CoreClassifier filmClassifier = (CoreClassifier) environment.lookup("Film");
			assertNull(filmClassifier.lookupOperation("isNewFilm", noParametersList));
				
			CoreClassifier tapeClassifier = (CoreClassifier) environment.lookup("Tape");
			assertNull(tapeClassifier.lookupOperation("getNumber", noParametersList));
			assertNull(tapeClassifier.lookupOperation("getNumber", paramList));
				
			CoreClassifier specialfilmClassifier = (CoreClassifier) environment.lookup("SpecialFilm");
			assertNull(specialfilmClassifier.lookupOperation("isSpecialFilm", noParametersList));
			
		} catch (Exception e) {
			System.out.println(e.getMessage());
			fail();
		}
	}


	public	void	testStringStreamBodyRecompilation() {
		String	newline = "\r\n";
		
		String	oclFile1 = 
		"context Film::getRentalFee(dayOfWeek : Integer) : Real  body: self.tapes->size() * 10.3 + dayOfWeek " 
		;

		String	oclFile2 = 
		"context SpecialFilm::days init: 20 " 
		;

		String	oclFile3 = 
		"context Tape::number init: 20 " 
		;

		try {
			PSWOclCompiler oclCompiler = new PSWOclCompiler(environment, new ConstraintSourceTrackerImpl());
			List	noParametersList = new ArrayList();
			CoreClassifier integerType = (CoreClassifier) environment.lookup("Integer");
			List paramList = new ArrayList();
			paramList.add(integerType);

			for (int i = 0; i < 2; i++) {
				System.out.println("i = " + i);
				List rootNode1 = oclCompiler.compileOclStream(oclFile1, getCurrentMethodName(), null);
				showCompilerErrors(oclCompiler);
				List rootNode2 = oclCompiler.compileOclStream(oclFile2, getCurrentMethodName()+"-a", null);
				showCompilerErrors(oclCompiler);

				assertNotNull(rootNode1);
				assertNotNull(rootNode2);
			
				CoreClassifier filmClassifier = (CoreClassifier) environment.lookup("Film");
				CoreOperation oper;
				assertNotNull(oper = filmClassifier.lookupOperation("getRentalFee", paramList));
				assertNotNull(oper.getBodyDefinition());
			}

			List rootNode = oclCompiler.compileOclStream(oclFile3, getCurrentMethodName(), null);
			assertNotNull(rootNode);

			CoreClassifier filmClassifier = (CoreClassifier) environment.lookup("Film");
			CoreOperation oper;
			assertNotNull(oper = filmClassifier.lookupOperation("getRentalFee", paramList));
			assertNull(oper.getBodyDefinition());
		} catch (Exception e) {
			System.out.println(e.getMessage());
			fail();
		}
	}


	public	void	testStringStreamPrePostRecompilation() {
		String	newline = "\r\n";
		
		String	oclFile1 = 
		"context Film::getRentalFee(dayOfWeek : Integer) : Real  pre : 10 < 20 post : 10 > 20 " 
		;

		String	oclFile2 = 
		"context Film::getRentalFee(dayOfWeek : Integer) : Real  pre : 10 < 20 post : 10 > 20 " 
		;

		String	oclFile3 = 
		"context Tape::number init: 20 " 
		;

		try {
			PSWOclCompiler oclCompiler = new PSWOclCompiler(environment, new ConstraintSourceTrackerImpl());
			List	noParametersList = new ArrayList();
			CoreClassifier integerType = (CoreClassifier) environment.lookup("Integer");
			List paramList = new ArrayList();
			paramList.add(integerType);

			for (int i = 0; i < 2; i++) {
				List rootNode1 = oclCompiler.compileOclStream(oclFile1, getCurrentMethodName(), null);
				showCompilerErrors(oclCompiler);
				List rootNode2 = oclCompiler.compileOclStream(oclFile2, getCurrentMethodName()+"-a", null);
				showCompilerErrors(oclCompiler);

				assertNotNull(rootNode1);
				assertNotNull(rootNode2);
			
				CoreClassifier filmClassifier = (CoreClassifier) environment.lookup("Film");
				CoreOperation oper;
				assertNotNull(oper = filmClassifier.lookupOperation("getRentalFee", paramList));
				assertEquals(2, oper.getSpecifications().size());
			}

			List rootNode = oclCompiler.compileOclStream(oclFile3, getCurrentMethodName(), null);
			assertNotNull(rootNode);

			CoreClassifier filmClassifier = (CoreClassifier) environment.lookup("Film");
			CoreOperation oper;
			assertNotNull(oper = filmClassifier.lookupOperation("getRentalFee", paramList));
			assertEquals(1, oper.getSpecifications().size());
		} catch (Exception e) {
			System.out.println(e.getMessage());
			fail();
		}
	}



	protected	void	checkInvariantsQty(String classifierName, int qty) {
		CoreClassifier classifier = (CoreClassifier) environment.lookup(classifierName);
		Collection invariants = classifier.getAllInvariants();
		assertEquals(qty, invariants.size());
	}



	protected Environment getEnvironment() throws MOFMetaModelRepositoryException {
		Uml13ModelsRepository  modelRepository = new Uml13ModelsRepository(MOFMetamodelRepositoryFactory.getRepository());
		String extentName = "RoseExample";
		modelRepository.importModel(extentName, "tests/resource/examples/myExampleRose.xml");
		MOFRepositoryReader repository = modelRepository;
		assertNotNull(model = repository.getModel(extentName));
		OclTypesFactory.setEnvironment(model);
		return	model.getEnvironmentWithoutParents();
	}

	protected String getCurrentMethodName() {
		return new Exception().getStackTrace()[1].getMethodName();
	}
	
	private	void	showCompilerErrors(PSWOclCompiler oclCompiler) {
		Set errors = oclCompiler.getErrors();
		System.out.println("\ncompilation errors: " + errors.size());
			
		for (Iterator iter = errors.iterator(); iter.hasNext();) {
			OCLCompilerException exception = (OCLCompilerException) iter.next();
			System.out.println(exception);
		}

	}
}
