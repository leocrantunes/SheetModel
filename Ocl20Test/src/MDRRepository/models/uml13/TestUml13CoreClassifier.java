/*
 * Created on Feb 28, 2005
 *
 * TODO To change the template for this generated file go to
 * Window - Preferences - Java - Code Style - Code Templates
 */
package br.ufrj.cos.lens.odyssey.MDRRepository.models.uml13;


import java.util.ArrayList;
import java.util.Collection;
import java.util.List;

import ocl20.common.CoreAssociation;
import ocl20.common.CoreAssociationClass;
import ocl20.common.CoreAssociationEnd;
import ocl20.common.CoreAttribute;
import ocl20.common.CoreClassifier;
import ocl20.common.CoreEnumLiteral;
import ocl20.common.CoreEnumeration;
import ocl20.common.CoreInterface;
import ocl20.common.CoreOperation;
import ocl20.common.CorePackage;
import ocl20.environment.Environment;





/**
 * @author alexcorr
 *
 * TODO To change the template for this generated type comment go to
 * Window - Preferences - Java - Code Style - Code Templates
 */
public class TestUml13CoreClassifier extends TestUml13CoreModelElement {
	public TestUml13CoreClassifier(String arg0) throws Exception {
		super(arg0);
	}

	public void testFeatures() {
		CoreClassifier aClassifier = getClassifier("MyExample", "Film");
		
		assertNotNull(getModelElement(aClassifier.getClassifierFeatures(), "name", CoreAttribute.class));
		assertNotNull(getModelElement(aClassifier.getClassifierFeatures(), "rentalFee", CoreAttribute.class));
		assertNotNull(getModelElement(aClassifier.getClassifierFeatures(), "getRentalFee", CoreOperation.class));
		assertNotNull(getModelElement(aClassifier.getClassifierFeatures(), "getTapes", CoreOperation.class));
		assertNull(getModelElement(aClassifier.getClassifierFeatures(), "code", CoreAttribute.class));
	}

	public void testAllAttributes() {
		CoreClassifier aClassifier = getClassifier("MyExample", "Film");
		
		assertNotNull(getModelElement(aClassifier.getAllAttributes(), "name", CoreAttribute.class));
		assertNotNull(getModelElement(aClassifier.getAllAttributes(), "rentalFee", CoreAttribute.class));
		assertNull(getModelElement(aClassifier.getAllAttributes(), "code", CoreAttribute.class));
	}

	public void testGetElementsForEnv() {
		CoreClassifier aClassifier = getClassifier(null, "SpecialFilm");
		
		assertNotNull(getModelElement(aClassifier.getEnvironmentWithoutParents(), "name", CoreAttribute.class));
		assertNotNull(getModelElement(aClassifier.getEnvironmentWithoutParents(),  "rentalFee", CoreAttribute.class));
		assertNotNull(getModelElement(aClassifier.getEnvironmentWithoutParents(), "code", CoreAttribute.class));
		assertNotNull(getModelElement(aClassifier.getEnvironmentWithoutParents(), "lateReturnFee", CoreAttribute.class));
		assertNotNull(getModelElement(aClassifier.getEnvironmentWithoutParents(), "days", CoreAttribute.class));
		assertNull(getModelElement(aClassifier.getEnvironmentWithoutParents(), "anAttribute", CoreAttribute.class));
		
		CoreClassifier aFilm = getClassifier("MyExample", "Film");
		assertNotNull(getModelElement(aFilm.getAllClassifierScopeAttributes(), "days", CoreAttribute.class));
		assertNull(getModelElement(aFilm.getAllClassifierScopeAttributes(), "lateReturnFee", CoreAttribute.class));
	}

	public void testInterfaces() {
		CorePackage myExample = (CorePackage) getModelElement(model.getEnvironmentWithoutParents(), "MyExample", CorePackage.class);
		CoreClassifier clsProduct = (CoreClassifier) getModelElement(myExample.getEnvironmentWithoutParents(), "Product", CoreClassifier.class);
		CoreClassifier clsFilm = (CoreClassifier) getModelElement(model.getEnvironmentWithoutParents(), "Film", CoreClassifier.class);
		CoreClassifier clsGame = (CoreClassifier) getModelElement(model.getEnvironmentWithoutParents(), "Game", CoreClassifier.class);
		CoreClassifier clsSpecialFilm = (CoreClassifier) getModelElement(model.getEnvironmentWithoutParents(), "SpecialFilm", CoreClassifier.class);
		CoreClassifier clsDistributor = (CoreClassifier) getModelElement(model.getEnvironmentWithoutParents(), "Distributor", CoreClassifier.class);
		CoreInterface iproduct = (CoreInterface) getModelElement(model.getEnvironmentWithoutParents(), "IProduct", CoreInterface.class);
		CoreInterface iThing= (CoreInterface) getModelElement(model.getEnvironmentWithoutParents(), "IThing", CoreInterface.class);

		assertTrue(clsProduct.getAllInterfaces().contains(iproduct));
		assertFalse(clsProduct.getAllInterfaces().contains(iThing));
		assertFalse(clsFilm.getAllInterfaces().contains(iproduct));
		assertFalse(clsSpecialFilm.getAllInterfaces().contains(iproduct));
		
		assertTrue(clsProduct.getAllImplementedInterfaces().contains(iproduct));
		assertTrue(clsFilm.getAllImplementedInterfaces().contains(iproduct));
		assertTrue(clsSpecialFilm.getAllImplementedInterfaces().contains(iproduct));
		
		assertTrue(clsProduct.getAllImplementedInterfaces().contains(iThing));
		assertTrue(clsFilm.getAllImplementedInterfaces().contains(iThing));
		assertTrue(clsSpecialFilm.getAllImplementedInterfaces().contains(iThing));
	}

	public void testAncestors() {
		CoreClassifier aClassifier = getClassifier("MyExample", "Film");
		assertNotNull(getModelElement(aClassifier.getAllAncestors(), "Product", CoreClassifier.class));
		assertEquals("MyExample", aClassifier.getElemOwner().getName());
		
		CorePackage myExample = (CorePackage) getModelElement(model.getEnvironmentWithoutParents(), "MyExample", CorePackage.class);
		CoreClassifier product = (CoreClassifier) getModelElement(myExample.getEnvironmentWithoutParents(), "Product", CoreClassifier.class);
		CoreClassifier specialFilm = (CoreClassifier) getModelElement(model.getEnvironmentWithoutParents(), "SpecialFilm", CoreClassifier.class);
		assertTrue(specialFilm.getAllAncestors().contains(product));
	}		

	public void testEnumeration() {
		CoreClassifier aClassifier = getClassifier("MyExample", "Situation");
		assertTrue(aClassifier.hasStereotype("enumeration"));
		assertFalse(aClassifier.hasStereotype("MyStereotype"));
		assertTrue(aClassifier.isEnumeration());
		
		CoreEnumLiteral enumLiteral = ((CoreEnumeration) aClassifier).lookupEnumLiteral("married");
		assertNotNull(enumLiteral);
	}
	
	public void testModel() {
		CoreClassifier aClassifier = getClassifier("MyExample", "Film");
		assertEquals(model, aClassifier.getModel());
		assertFalse(aClassifier.isEnumeration());
	}

	public void testSubClasses() {
		CoreClassifier aClassifier = getClassifier("MyExample", "Product");
		assertNotNull(getModelElement(aClassifier.getAllSubClasses(), "Film", CoreClassifier.class));
		assertNotNull(getModelElement(aClassifier.getAllSubClasses(), "SpecialFilm", CoreClassifier.class));
		assertNotNull(getModelElement(getClassifier("MyExample", "Product").getAllSubClasses(), "Game", CoreClassifier.class));
		assertNotNull(getModelElement(getClassifier("MyExample", "Film").getAllSubClasses(), "SpecialFilm", CoreClassifier.class));
		assertNotNull(getModelElement(getClassifier("MyExample", "IThing").getAllSubClasses(), "IProduct", CoreInterface.class));
		assertNull(getModelElement(getClassifier("MyExample", "IThing").getAllSubClasses(), "SpecialFilm", CoreClassifier.class));
		assertEquals("MyExample", aClassifier.getElemOwner().getName());
	}		

	public void testImplementors() {
		assertNotNull(getModelElement(((CoreInterface)getClassifier("MyExample", "IThing")).getAllImplementors(), "SpecialFilm", CoreClassifier.class));
		assertNotNull(getModelElement(((CoreInterface)getClassifier("MyExample", "IProduct")).getAllImplementors(), "Product", CoreClassifier.class));
		assertNotNull(getModelElement(((CoreInterface)getClassifier("MyExample", "IProduct")).getAllImplementors(), "Film", CoreClassifier.class));
	}

	public void testConformsTo() {
		
		CoreClassifier filme = (CoreClassifier) getModelElement(model.getEnvironmentWithoutParents(), "Film", CoreClassifier.class);
		CorePackage myExample = (CorePackage) getModelElement(model.getEnvironmentWithoutParents(), "MyExample", CorePackage.class);
		CoreClassifier produto = (CoreClassifier) getModelElement(myExample.getEnvironmentWithoutParents(), "Product", CoreClassifier.class);
		CoreClassifier iproduto = (CoreClassifier) getModelElement(model.getEnvironmentWithoutParents(), "IProduct", CoreInterface.class);
		CoreClassifier ithing= (CoreClassifier) getModelElement(model.getEnvironmentWithoutParents(), "IThing", CoreInterface.class);
		CoreClassifier fita = (CoreClassifier) getModelElement(model.getEnvironmentWithoutParents(), "Tape", CoreClassifier.class);
		CoreClassifier specialFilm = (CoreClassifier) getModelElement(model.getEnvironmentWithoutParents(), "SpecialFilm", CoreClassifier.class);
		
		CoreClassifier intType= (CoreClassifier) getModelElement(model.getEnvironmentWithoutParents(), "Integer", CoreClassifier.class);
		CoreClassifier doubleType = (CoreClassifier) getModelElement(model.getEnvironmentWithoutParents(), "Real", CoreClassifier.class);
		
		assertNotNull(filme);
		assertNotNull(produto);
		assertNotNull(iproduto);
		assertNotNull(fita);
		assertNotNull(specialFilm);
		assertNotNull(ithing);

		assertTrue(intType.conformsTo(doubleType));
		
		assertTrue(specialFilm.conformsTo(filme));
		assertTrue(specialFilm.conformsTo(produto));
		
		assertTrue(filme.conformsTo(produto));
		
		assertFalse(filme.conformsTo(fita));
		assertFalse(produto.conformsTo(filme));
		assertFalse(produto.conformsTo(fita));
		
		assertTrue(produto.conformsTo(iproduto));
		assertTrue(produto.conformsTo(produto));
		assertTrue(specialFilm.conformsTo(iproduto));
		assertTrue(filme.conformsTo(iproduto));

		assertTrue(produto.conformsTo(ithing));
		assertTrue(specialFilm.conformsTo(ithing));
		assertTrue(filme.conformsTo(ithing));

	}

	
	public void testFindAttribute() {
		CoreClassifier specialFilm = (CoreClassifier) getModelElement(model.getEnvironmentWithoutParents(), "SpecialFilm", CoreClassifier.class);
		assertNotNull(specialFilm);
		
		doPositiveTestLookupAttribute(specialFilm, "lateReturnFee", "Real");
		doPositiveTestLookupAttribute(specialFilm, "name", "String");
		doPositiveTestLookupAttribute(specialFilm, "rentalFee", "Integer");
		doPositiveTestLookupAttribute(specialFilm, "code", "String");
		doPositiveTestLookupAttribute(specialFilm, "days", "Integer");
		doNegativeTestLookupAttribute(specialFilm, "number");
	}

	private void doPositiveTestLookupAttribute(CoreClassifier cls, String attName, String typeName) {
		CoreAttribute att = cls.lookupAttribute(attName);
		assertNotNull(att);
		assertEquals(attName, att.getName());
		assertEquals(typeName, att.getFeatureType().getName());
	}

	private void doNegativeTestLookupAttribute(CoreClassifier cls, String name) {
		CoreAttribute att = cls.lookupAttribute(name);
		assertNull(att);
	}

	
	public void testFindAssociationEnd() {
		CoreClassifier cls = (CoreClassifier) getModelElement(model.getEnvironmentWithoutParents(), "SpecialFilm", CoreClassifier.class);
		assertNotNull(cls);
		
		doPositiveTestLookupAssociationEnd(cls, "tapes", "Tape");
		doNegativeTestLookupAssociationEnd(cls, "theFilm");
		doNegativeTestLookupAssociationEnd(cls, "name");
		
		cls = (CoreClassifier) getModelElement(model.getEnvironmentWithoutParents(), "Tape", CoreClassifier.class);
		assertNotNull(cls);
		doPositiveTestLookupAssociationEnd(cls, "theFilm", "Film");
		doNegativeTestLookupAssociationEnd(cls, "tapes");
		doNegativeTestLookupAssociationEnd(cls, "number");
		
		cls = (CoreClassifier) getModelElement(model.getEnvironmentWithoutParents(), "RentalItem", CoreClassifier.class);
		assertNotNull(cls);
		doPositiveTestLookupAssociationEnd(cls, "Rental", "Rental");
	}

	
	private void doPositiveTestLookupAssociationEnd(CoreClassifier cls, String name, String assocTypeName) {
		CoreAssociationEnd assocEnd = cls.lookupAssociationEnd(name);
		assertNotNull(name);
		assertNotNull(assocEnd);
		assertEquals(name, assocEnd.getName());
		assertEquals(assocTypeName, assocEnd.getTheParticipant().getName());
	}

	private void doNegativeTestLookupAssociationEnd(CoreClassifier cls, String name) {
		CoreAssociationEnd assocEnd = cls.lookupAssociationEnd(name);
		assertNull(assocEnd);
	}

	
	public void testAssociation() {
		CoreClassifier cls = (CoreClassifier) getModelElement(model.getEnvironmentWithoutParents(), "SpecialFilm", CoreClassifier.class);
		assertNotNull(cls);
		
		CoreAssociationEnd assocEnd = cls.lookupAssociationEnd("tapes");
		assertNotNull(assocEnd);
		CoreAssociation assoc = assocEnd.getTheAssociation();
		assertNotNull(assoc);
		
		CoreClassifier tapeCls = (CoreClassifier) getModelElement(model.getEnvironmentWithoutParents(), "Tape", CoreClassifier.class);
		assertNotNull(tapeCls);
		CoreAssociationEnd	otherAssocEnd = tapeCls.lookupAssociationEnd("theFilm");
		assertNotNull(otherAssocEnd);
		assertEquals(assoc, otherAssocEnd.getTheAssociation());
		
		List assocEnds = assoc.getTheAssociationEnds();
		assertTrue(assocEnds.contains(assocEnd));
		assertEquals(2, assocEnds.size());
		for (int i = 0; i < assocEnds.size(); i++) {
			CoreAssociationEnd theAssocEnd = (CoreAssociationEnd) assocEnds.get(i);
			assertTrue(theAssocEnd.getTheParticipant().getName().equals("Tape") ||
					theAssocEnd.getTheParticipant().getName().equals("Film") );
		}
	}

	public void testAssociationEnds() {
		CoreClassifier cls = (CoreClassifier) getModelElement(model.getEnvironmentWithoutParents(), "Allocation", CoreClassifier.class);

		List assocEnds = new ArrayList(cls.getAllAssociationEnds());
		assertEquals(2, assocEnds.size());
		for (int i = 0; i < assocEnds.size(); i++) {
			CoreAssociationEnd theAssocEnd = (CoreAssociationEnd) assocEnds.get(i);
			assertTrue(theAssocEnd.getTheParticipant().getName().equals("SpecialFilm") ||
					theAssocEnd.getTheParticipant().getName().equals("Distributor") );
		}
	}

	public void testAssociationClass() {
		CoreClassifier cls = (CoreClassifier) getModelElement(model.getEnvironmentWithoutParents(), "SpecialFilm", CoreClassifier.class);
		CoreClassifier allocation = (CoreClassifier) getModelElement(model.getEnvironmentWithoutParents(), "Allocation", CoreClassifier.class);
		
		List assocClasses = new ArrayList(cls.getAllAssociationClasses());
		assertEquals(1, assocClasses.size());
		assertEquals(allocation, assocClasses.get(0));

		assertEquals(allocation, cls.lookupAssociationClass("Allocation"));
	}

	public void testSelfAssociation() {
		CoreClassifier cls = (CoreClassifier) getModelElement(model.getEnvironmentWithoutParents(), "Person", CoreClassifier.class);
		assertNotNull(cls);
		
		CoreAssociationEnd assocEnd = cls.lookupAssociationEnd("bosses");
		assertNotNull(assocEnd);
		CoreAssociation assoc = assocEnd.getTheAssociation();
		assertNotNull(assoc);
		assertTrue(assoc instanceof CoreAssociationClass);
		List assocEnds = assoc.getTheAssociationEnds();
		assertTrue(assocEnds.contains(assocEnd));
		assertEquals(2, assocEnds.size());
		for (int i = 0; i < assocEnds.size(); i++) {
			CoreAssociationEnd theAssocEnd = (CoreAssociationEnd) assocEnds.get(i);
			assertTrue(theAssocEnd.getTheParticipant().getName().equals("Person"));
		}
	}

	public void testLookupOperation_02() {
		CoreClassifier filme = (CoreClassifier) getModelElement(model.getEnvironmentWithoutParents(), "Film", CoreClassifier.class);
		assertNotNull(filme);
		
		List paramTypes = new ArrayList();
		
		CoreOperation op = filme.lookupOperation("getTapes", paramTypes);
		assertNotNull(op);
		assertEquals("Set(Tape)", op.getReturnType().getName());
	}

	
	public void testLookupExactOperationSignature() {
		CoreClassifier filme = (CoreClassifier) getModelElement(model.getEnvironmentWithoutParents(), "Film", CoreClassifier.class);
		CoreClassifier intType= (CoreClassifier) getModelElement(model.getElemOwnedElements(), "Integer", CoreClassifier.class);
		CoreClassifier doubleType = (CoreClassifier) getModelElement(model.getEnvironmentWithoutParents(), "Real", CoreClassifier.class);

		
		assertNotNull(filme);
		assertNotNull(intType);
		assertNotNull(doubleType);
		
		List paramTypes = new ArrayList();
		paramTypes.add(intType);
		
		CoreOperation op = filme.lookupSameSignatureOperation("getRentalFee", paramTypes, doubleType);
		assertNotNull(op);
	}

	public void testLookupOperationWithoutReturn() {
		CoreClassifier filme = (CoreClassifier) getModelElement(model.getEnvironmentWithoutParents(), "Film", CoreClassifier.class);
		CoreClassifier intType= (CoreClassifier) getModelElement(model.getElemOwnedElements(), "Integer", CoreClassifier.class);
		
		assertNotNull(filme);
		assertNotNull(intType);
		
		List paramTypes = new ArrayList();
		paramTypes.add(intType);
		
		CoreOperation op = filme.lookupOperation("setDaysForReturn", paramTypes);
		assertNotNull(op);
		assertNull(op.getReturnType());
	}

	public void testLookupOverloadedOperation() {
		CoreClassifier specialFilm = (CoreClassifier) getModelElement(model.getEnvironmentWithoutParents(), "SpecialFilm", CoreClassifier.class);
		CoreClassifier intType= (CoreClassifier) getModelElement(model.getElemOwnedElements(), "Integer", CoreClassifier.class);
		CoreClassifier realType= (CoreClassifier) getModelElement(model.getEnvironmentWithoutParents(), "Real", CoreClassifier.class);
		
		assertNotNull(specialFilm);
		assertNotNull(intType);
		assertNotNull(realType);
		
		List paramTypes = new ArrayList();
		paramTypes.add(intType);
		paramTypes.add(intType);
		paramTypes.add(realType);
		
		CoreOperation op = specialFilm.lookupOperation("doSomething", paramTypes);
		assertNotNull(op);
		assertEquals(intType, op.getReturnType());
		
		paramTypes.clear();
		op = specialFilm.lookupOperation("doSomething", paramTypes);
		assertNotNull(op);
		assertNull(op.getReturnType());;
	}

	
	
	public void testAllDirectSubclasses() {
		CorePackage myExample = (CorePackage) getModelElement(model.getEnvironmentWithoutParents(), "MyExample", CorePackage.class);
		CoreClassifier produto = (CoreClassifier) getModelElement(myExample.getEnvironmentWithoutParents(), "Product", CoreClassifier.class);
		CoreClassifier filme = (CoreClassifier) getModelElement(model.getEnvironmentWithoutParents(), "Film", CoreClassifier.class);
		CoreClassifier game = (CoreClassifier) getModelElement(model.getEnvironmentWithoutParents(), "Game", CoreClassifier.class);
		CoreClassifier specialFilm = (CoreClassifier) getModelElement(model.getEnvironmentWithoutParents(), "SpecialFilm", CoreClassifier.class);
		CoreClassifier iproduto = (CoreClassifier) getModelElement(model.getEnvironmentWithoutParents(), "IProduct", CoreInterface.class);
		CoreClassifier ithing= (CoreClassifier) getModelElement(model.getEnvironmentWithoutParents(), "IThing", CoreInterface.class);

		assertTrue(ithing.getAllDirectSubClasses().contains(iproduto));
		assertTrue(produto.getAllDirectSubClasses().contains(filme));
		assertTrue(produto.getAllDirectSubClasses().contains(game));
		assertFalse(produto.getAllDirectSubClasses().contains(specialFilm));
		assertFalse(game.getAllDirectSubClasses().contains(produto));
	}

	public void testAllSubclasses() {
		CorePackage myExample = (CorePackage) getModelElement(model.getEnvironmentWithoutParents(), "MyExample", CorePackage.class);
		CoreClassifier produto = (CoreClassifier) getModelElement(myExample.getEnvironmentWithoutParents(), "Product", CoreClassifier.class);
		CoreClassifier filme = (CoreClassifier) getModelElement(model.getEnvironmentWithoutParents(), "Film", CoreClassifier.class);
		CoreClassifier game = (CoreClassifier) getModelElement(model.getEnvironmentWithoutParents(), "Game", CoreClassifier.class);
		CoreClassifier specialFilm = (CoreClassifier) getModelElement(model.getEnvironmentWithoutParents(), "SpecialFilm", CoreClassifier.class);
		CoreClassifier iproduto = (CoreClassifier) getModelElement(model.getEnvironmentWithoutParents(), "IProduct", CoreInterface.class);
		CoreClassifier ithing= (CoreClassifier) getModelElement(model.getEnvironmentWithoutParents(), "IThing", CoreInterface.class);

		assertTrue(produto.getAllSubClasses().contains(filme));
		assertTrue(produto.getAllSubClasses().contains(game));
		assertTrue(produto.getAllSubClasses().contains(specialFilm));
		assertTrue(ithing.getAllSubClasses().contains(iproduto));
	}


	public void testAllDirectImplementors() {
		CorePackage myExample = (CorePackage) getModelElement(model.getEnvironmentWithoutParents(), "MyExample", CorePackage.class);
		CoreClassifier produto = (CoreClassifier) getModelElement(myExample.getEnvironmentWithoutParents(), "Product", CoreClassifier.class);
		CoreClassifier filme = (CoreClassifier) getModelElement(model.getEnvironmentWithoutParents(), "Film", CoreClassifier.class);
		CoreClassifier game = (CoreClassifier) getModelElement(model.getEnvironmentWithoutParents(), "Game", CoreClassifier.class);
		CoreClassifier specialFilm = (CoreClassifier) getModelElement(model.getEnvironmentWithoutParents(), "SpecialFilm", CoreClassifier.class);
		CoreInterface iproduto = (CoreInterface) getModelElement(model.getEnvironmentWithoutParents(), "IProduct", CoreInterface.class);
		CoreInterface ithing= (CoreInterface) getModelElement(model.getEnvironmentWithoutParents(), "IThing", CoreInterface.class);

		assertTrue(ithing.getAllDirectSubClasses().contains(iproduto));
		assertFalse(iproduto.getAllDirectSubClasses().contains(game));
		assertFalse(iproduto.getAllDirectSubClasses().contains(specialFilm));
		
		assertTrue(iproduto.getAllDirectImplementors().contains(produto));
		assertFalse(iproduto.getAllDirectImplementors().contains(filme));
		assertFalse(iproduto.getAllDirectImplementors().contains(game));
		assertFalse(iproduto.getAllDirectImplementors().contains(specialFilm));
		assertTrue(ithing.getAllDirectImplementors().contains(specialFilm));
	}

	public void testAllImplementors() {
		CorePackage myExample = (CorePackage) getModelElement(model.getEnvironmentWithoutParents(), "MyExample", CorePackage.class);
		CoreClassifier clsProduct = (CoreClassifier) getModelElement(myExample.getEnvironmentWithoutParents(), "Product", CoreClassifier.class);
		CoreClassifier clsFilm = (CoreClassifier) getModelElement(model.getEnvironmentWithoutParents(), "Film", CoreClassifier.class);
		CoreClassifier clsGame = (CoreClassifier) getModelElement(model.getEnvironmentWithoutParents(), "Game", CoreClassifier.class);
		CoreClassifier clsSpecialFilm = (CoreClassifier) getModelElement(model.getEnvironmentWithoutParents(), "SpecialFilm", CoreClassifier.class);
		CoreClassifier clsDistributor = (CoreClassifier) getModelElement(model.getEnvironmentWithoutParents(), "Distributor", CoreClassifier.class);
		CoreInterface iproduto = (CoreInterface) getModelElement(model.getEnvironmentWithoutParents(), "IProduct", CoreInterface.class);
		CoreInterface iThing= (CoreInterface) getModelElement(model.getEnvironmentWithoutParents(), "IThing", CoreInterface.class);

		assertTrue(iThing.getAllSubClasses().contains(iproduto));
		assertFalse(iproduto.getAllSubClasses().contains(clsGame));
		assertFalse(iproduto.getAllSubClasses().contains(clsSpecialFilm));
		
		assertTrue(iproduto.getAllImplementors().contains(clsProduct));
		assertTrue(iproduto.getAllImplementors().contains(clsFilm));
		assertTrue(iproduto.getAllImplementors().contains(clsGame));
		assertTrue(iproduto.getAllImplementors().contains(clsSpecialFilm));
		assertFalse(iproduto.getAllImplementors().contains(clsDistributor));
		assertFalse(iproduto.getAllImplementors().contains(iThing));
		
		assertTrue(iThing.getAllImplementors().contains(clsProduct));
		assertTrue(iThing.getAllImplementors().contains(clsFilm));
		assertTrue(iThing.getAllImplementors().contains(clsGame));
		assertTrue(iThing.getAllImplementors().contains(clsSpecialFilm));
		assertFalse(iThing.getAllImplementors().contains(clsDistributor));
		assertFalse(iThing.getAllImplementors().contains(iproduto));

		List allImplementors = new ArrayList(iThing.getAllImplementors());
		
		assertEquals(allImplementors.indexOf(clsSpecialFilm), allImplementors.lastIndexOf(clsSpecialFilm)); 
		assertTrue(allImplementors.indexOf(clsSpecialFilm) != -1);
	}

	public void testGetFullPathName() {
		CorePackage myExample = (CorePackage) getModelElement(model.getEnvironmentWithoutParents(), "MyExample", CorePackage.class);
		CoreClassifier clsProduct = (CoreClassifier) getModelElement(myExample.getEnvironmentWithoutParents(), "Product", CoreClassifier.class);
		CoreClassifier clsFilm = (CoreClassifier) getModelElement(model.getEnvironmentWithoutParents(), "Film", CoreClassifier.class);
		CoreClassifier clsGame = (CoreClassifier) getModelElement(model.getEnvironmentWithoutParents(), "Game", CoreClassifier.class);
		CoreClassifier clsSpecialFilm = (CoreClassifier) getModelElement(model.getEnvironmentWithoutParents(), "SpecialFilm", CoreClassifier.class);
		CoreClassifier clsDistributor = (CoreClassifier) getModelElement(model.getEnvironmentWithoutParents(), "Distributor", CoreClassifier.class);
		CoreInterface iproduto = (CoreInterface) getModelElement(model.getEnvironmentWithoutParents(), "IProduct", CoreInterface.class);
		CoreInterface iThing= (CoreInterface) getModelElement(model.getEnvironmentWithoutParents(), "IThing", CoreInterface.class);
		CoreClassifier clsRental = (CoreClassifier) getModelElement(model.getEnvironmentWithoutParents(), "Rental", CoreClassifier.class);

		assertEquals("MyExample::Product", clsProduct.getFullPathName());		
		assertEquals("MyExample::Film", clsFilm.getFullPathName());
		assertEquals("MyExample::Game", clsGame.getFullPathName());
		assertEquals("SpecialFilm", clsSpecialFilm.getFullPathName());
		assertEquals("MyExample::Distributor", clsDistributor.getFullPathName());
		assertEquals("MyExample::IProduct", iproduto.getFullPathName());
		assertEquals("MyExample::IThing", iThing.getFullPathName());
		assertEquals("MyExample::package_1::package_1_2::Rental", clsRental.getFullPathName());
	}
	
	
	public	void	testMyOperation() {
		CoreClassifier film = (CoreClassifier) getModelElement(model.getEnvironmentWithoutParents(), "Film", CoreClassifier.class);
		CoreClassifier intType= (CoreClassifier) getModelElement(model.getEnvironmentWithoutParents(), "Integer", CoreClassifier.class);
		List params = new ArrayList();
		params.add(intType);
		CoreOperation oper = film.lookupSameSignatureOperation("setDaysForReturn", params , null);
		assertNull(oper.getReturnType());
	}
	
	public void testGetMostSpecificSuperType() {
		CorePackage myExample = (CorePackage) getModelElement(model.getEnvironmentWithoutParents(), "MyExample", CorePackage.class);
		CoreClassifier clsProduct = (CoreClassifier) getModelElement(myExample.getEnvironmentWithoutParents(), "Product", CoreClassifier.class);
		CoreClassifier clsFilm = (CoreClassifier) getModelElement(model.getEnvironmentWithoutParents(), "Film", CoreClassifier.class);
		CoreClassifier clsGame = (CoreClassifier) getModelElement(model.getEnvironmentWithoutParents(), "Game", CoreClassifier.class);
		CoreClassifier clsSpecialFilm = (CoreClassifier) getModelElement(model.getEnvironmentWithoutParents(), "SpecialFilm", CoreClassifier.class);
		CoreClassifier clsDistributor = (CoreClassifier) getModelElement(model.getEnvironmentWithoutParents(), "Distributor", CoreClassifier.class);

		assertEquals(clsProduct, clsProduct.getMostSpecificCommonSuperType(clsProduct));
		assertEquals(clsProduct, clsFilm.getMostSpecificCommonSuperType(clsProduct));
		assertEquals(clsProduct, clsFilm.getMostSpecificCommonSuperType(clsGame));
		assertEquals(clsProduct, clsSpecialFilm.getMostSpecificCommonSuperType(clsGame));
		assertEquals(clsProduct, clsSpecialFilm.getMostSpecificCommonSuperType(clsProduct));
		assertEquals(clsProduct, clsGame.getMostSpecificCommonSuperType(clsSpecialFilm));
		
		assertEquals("OclAny", clsDistributor.getMostSpecificCommonSuperType(clsFilm).getName());
	}

	public void testIsConcrete() {
		CorePackage myExample = (CorePackage) getModelElement(model.getEnvironmentWithoutParents(), "MyExample", CorePackage.class);
		CoreClassifier clsProduct = (CoreClassifier) getModelElement(myExample.getEnvironmentWithoutParents(), "Product", CoreClassifier.class);
		CoreClassifier clsFilm = (CoreClassifier) getModelElement(model.getEnvironmentWithoutParents(), "Film", CoreClassifier.class);
		CoreClassifier clsSpecialFilm = (CoreClassifier) getModelElement(model.getEnvironmentWithoutParents(), "SpecialFilm", CoreClassifier.class);
		
		assertTrue(clsFilm.isConcrete());
		assertTrue(clsSpecialFilm.isConcrete());
		assertFalse(clsProduct.isConcrete());
	}
	
	public void testIsDescendantOf() {
		CorePackage myExample = (CorePackage) getModelElement(model.getEnvironmentWithoutParents(), "MyExample", CorePackage.class);
		CoreClassifier clsProduct = (CoreClassifier) getModelElement(myExample.getEnvironmentWithoutParents(), "Product", CoreClassifier.class);
		CoreClassifier clsFilm = (CoreClassifier) getModelElement(model.getEnvironmentWithoutParents(), "Film", CoreClassifier.class);
		CoreClassifier clsGame = (CoreClassifier) getModelElement(model.getEnvironmentWithoutParents(), "Game", CoreClassifier.class);
		CoreClassifier clsSpecialFilm = (CoreClassifier) getModelElement(model.getEnvironmentWithoutParents(), "SpecialFilm", CoreClassifier.class);
		CoreClassifier clsDistributor = (CoreClassifier) getModelElement(model.getEnvironmentWithoutParents(), "Distributor", CoreClassifier.class);
		CoreInterface iProduct = (CoreInterface) getModelElement(model.getEnvironmentWithoutParents(), "IProduct", CoreInterface.class);
		
		assertTrue(clsSpecialFilm.isClassifierDescendantOf(clsFilm));
		assertTrue(clsSpecialFilm.isClassifierDescendantOf(clsProduct));
		assertTrue(clsFilm.isClassifierDescendantOf(clsProduct));
		assertFalse(clsFilm.isClassifierDescendantOf(clsFilm));
		assertFalse(clsFilm.isClassifierDescendantOf(iProduct));
		assertFalse(clsFilm.isClassifierDescendantOf(clsSpecialFilm));
	}
	
	public void testOclAnyOperations() {
		CoreClassifier clsFilm = (CoreClassifier) getModelElement(model.getEnvironmentWithoutParents(), "Film", CoreClassifier.class);

		Collection paramTypes = new ArrayList();
		paramTypes.add(clsFilm);
		assertNotNull(clsFilm.lookupOperation("=", paramTypes));
	}

	public void testParentEnvironment() {
		CoreClassifier clsFilm = (CoreClassifier) getModelElement(model.getEnvironmentWithoutParents(), "Film", CoreClassifier.class);

		Environment env = clsFilm.getEnvironmentWithParents();
		assertNotNull(env.lookupPathName("Integer"));
		assertNotNull(env.lookupPathName("SpecialFilm"));
		
		CoreClassifier myInteger = (CoreClassifier) env.lookupPathName("Integer");
		List params = new ArrayList();
		params.add(myInteger);
		assertNotNull(myInteger.lookupOperation(">", params));

		
	}

}

