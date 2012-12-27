/*
 * Created on Feb 28, 2005
 *
 * TODO To change the template for this generated file go to
 * Window - Preferences - Java - Code Style - Code Templates
 */
package br.ufrj.cos.lens.odyssey.MDRRepository.models.uml13;

import ocl20.environment.Environment;

import java.util.ArrayList;
import java.util.Collection;
import java.util.Iterator;
import java.util.List;

import ocl20.common.CoreClassifier;
import ocl20.common.CoreModel;
import ocl20.common.CoreModelElement;
import ocl20.common.CorePackage;



import junit.framework.TestCase;
import br.ufrj.cos.lens.odyssey.MDRRepository.MOFMetaModelRepositoryException;
import br.ufrj.cos.lens.odyssey.MDRRepository.MOFMetamodelRepositoryFactory;
import br.ufrj.cos.lens.odyssey.MDRRepository.MOFRepositoryReader;
import br.ufrj.cos.lens.odyssey.MDRRepository.models.Uml13ModelsRepository;

/**
 * @author alexcorr
 *
 * TODO To change the template for this generated type comment go to
 * Window - Preferences - Java - Code Style - Code Templates
 */
public class TestUml13CoreModelElement extends TestCase {
		protected	static MOFRepositoryReader repository;
		protected	static CoreModel	model = null; 
		
		public TestUml13CoreModelElement(String arg0) throws Exception {
			super(arg0);
			
			if (model == null) {
				setUmlModelsRepository();
			}
		}

		protected CoreModelElement getModelElement(Environment elements, String name, Class clazz) {
			for (Iterator iter = elements.getAllOfType(clazz).iterator(); iter.hasNext();) {
				CoreModelElement element = (CoreModelElement) iter.next();

				if (element.getName() != null && element.getName().equals(name) &&
					clazz.isAssignableFrom(element.getClass()))
					return	element;
			}
			return	null;
		}

		protected CoreModelElement getModelElement(Collection elements, String name, Class clazz) {
			for (Iterator iter = elements.iterator(); iter.hasNext();) {
				CoreModelElement element = (CoreModelElement) iter.next();

				if (element.getName().equals(name) &&
					clazz.isAssignableFrom(element.getClass()))
					return	element;
			}
			return	null;
		}

		protected List	getModelElements(Collection elements, String name, Class clazz) {
			List	result = new ArrayList();
			for (Iterator iter = elements.iterator(); iter.hasNext();) {
				CoreModelElement element = (CoreModelElement) iter.next();
				if (element.getName().equals(name) &&
					clazz.isAssignableFrom(element.getClass()))
					result.add(element);
			}
			return	result;
		}
		
		protected CoreClassifier getClassifier(String packageName, String name) {
			CoreClassifier aClassifier;

			if (packageName != null) {
				CorePackage aPackage = (CorePackage) getModelElement(model.getElemOwnedElements(), packageName, CorePackage.class);
				assertNotNull(aPackage);
				aClassifier = (CoreClassifier) getModelElement(aPackage.getElemOwnedElements(), name, CoreClassifier.class);
			}
			else {
				aClassifier = (CoreClassifier) getModelElement(model.getElemOwnedElements(), name, CoreClassifier.class);
			}
			assertNotNull(aClassifier);		

			return	aClassifier;
		}

		public static void setUmlModelsRepository() throws MOFMetaModelRepositoryException {
			Uml13ModelsRepository  modelRepository = new Uml13ModelsRepository(MOFMetamodelRepositoryFactory.getRepository());
			String extentName = "RoseExample";
			modelRepository.importModel(extentName, "tests/resource/examples/myExampleRose.xml");
			repository = modelRepository;
			assertNotNull(model = repository.getModel(extentName));
		}

}
