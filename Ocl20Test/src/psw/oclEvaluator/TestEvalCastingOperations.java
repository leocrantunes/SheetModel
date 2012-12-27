/*
 * Created on 13/08/2004
 *
 * To change the template for this generated file go to
 * Window>Preferences>Java>Code Generation>Code and Comments
 */
package br.ufrj.cos.lens.odyssey.tools.psw.oclEvaluator;

import java.util.ArrayList;
import java.util.Arrays;

import ocl20.common.CoreAssociation;
import ocl20.common.CoreAssociationEnd;
import ocl20.common.CoreClassifier;

import br.ufrj.cos.lens.odyssey.tools.psw.semantics.values.OclIntegerValue;
import br.ufrj.cos.lens.odyssey.tools.psw.semantics.values.OclObjectValue;

/**
 * @author Administrator
 *
 * To change the template for this generated type comment go to
 * Window>Preferences>Java>Code Generation>Code and Comments
 */
public class TestEvalCastingOperations extends TestObjectEval {
	private	CoreClassifier personClass;
	private	CoreClassifier	filmClass;
		
	private	OclObjectValue person;
	private	OclObjectValue[] reservations;
	private	OclObjectValue[] films; 
	private	OclObjectValue[] specialFilms;

	private	CoreAssociation	personReservationAssoc;
	private	CoreAssociationEnd	personRole;
	private	CoreAssociationEnd   reservationRole;
		
	private	CoreAssociation	filmReservationAssoc;
	private	CoreAssociationEnd	filmRole;
	private	CoreAssociationEnd   filmReservationRole;


	public	void	testOclAsType() {
		String	sourceStream = "testOclAsType";

		createObjectsAndLinks();
		
		evalEnv.add("p", person);
		insertObjectInEnvironment("p", "Person");		

		evaluateIntegerExpression(sourceStream, "p.Reservation.Film->at(1).oclAsType(Film).rentalFee", 10);
		
		evaluateIntegerExpression(sourceStream, "p.Reservation.Film->at(5).oclAsType(SpecialFilm).rentalFee", 20);
		
		evaluateIntegerExpression(sourceStream, "p.Reservation.Film->at(6).oclAsType(SpecialFilm).rentalFee", 30);
		evaluateIntegerExpression(sourceStream, "p.Reservation.Film->at(5).oclAsType(SpecialFilm).lateReturnFee", 100);
		evaluateIntegerExpression(sourceStream, "p.Reservation.Film->at(6).oclAsType(SpecialFilm).lateReturnFee", 200);
		
		evaluateBooleanExpression(sourceStream, "p.Reservation.Film->at(1).oclAsType(SpecialFilm).oclIsUndefined()", true);
		evaluateBooleanExpression(sourceStream, "p.Reservation.Film->at(5).oclAsType(Reservation).oclIsUndefined()", true);

	}

	public	void	testOclIsTypeOf() {
		String	sourceStream = "testOclIsTypeOf";

		createObjectsAndLinks();
		
		evalEnv.add("p", person);
		insertObjectInEnvironment("p", "Person");		

		evalEnv.add("r0", reservations[0]);
		insertObjectInEnvironment("r0", "Reservation");		
		evalEnv.add("r2", reservations[2]);
		insertObjectInEnvironment("r2", "Reservation");		
		evalEnv.add("r4", reservations[4]);
		insertObjectInEnvironment("r4", "Reservation");		

		
		evaluateIntegerExpression(sourceStream, "p.Reservation->size()", 6);
		evaluateBooleanExpression(sourceStream, "p.Reservation->at(1)->notEmpty()", true);
		evaluateBooleanExpression(sourceStream, "r4.Film.oclIsTypeOf(SpecialFilm)", true);
		evaluateBooleanExpression(sourceStream, "r0.Film.oclIsTypeOf(Film)", true);
		evaluateBooleanExpression(sourceStream, "r2.Film.oclIsTypeOf(Film)", true);
		evaluateBooleanExpression(sourceStream, "p.Reservation.Film->at(1).oclIsTypeOf(Film)", true);
		evaluateBooleanExpression(sourceStream, "p.Reservation.Film->at(2).oclIsTypeOf(Film)", true);
		evaluateBooleanExpression(sourceStream, "p.Reservation.Film->at(2).oclIsTypeOf(SpecialFilm)", false);
		evaluateBooleanExpression(sourceStream, "p.Reservation.Film->at(3).oclIsTypeOf(Film)", true);
		evaluateBooleanExpression(sourceStream, "p.Reservation.Film->at(4).oclIsTypeOf(Film)", true);
		evaluateBooleanExpression(sourceStream, "p.Reservation.Film->at(5).oclIsTypeOf(SpecialFilm)", true);
		evaluateBooleanExpression(sourceStream, "p.Reservation.Film->at(5).oclIsTypeOf(Film)", false);
		evaluateBooleanExpression(sourceStream, "p.Reservation.Film->at(6).oclIsTypeOf(SpecialFilm)", true);
	}

	public	void	testOclIsKindOf() {
		String	sourceStream = "testOclIsKindOf";

		createObjectsAndLinks();
		
		evalEnv.add("p", person);
		insertObjectInEnvironment("p", "Person");		

		evaluateBooleanExpression(sourceStream, "p.Reservation.Film->at(1).oclIsKindOf(MyExample::Product)", true);
		evaluateBooleanExpression(sourceStream, "p.Reservation.Film->at(1).oclIsKindOf(Film)", true);
		evaluateBooleanExpression(sourceStream, "p.Reservation.Film->at(1).oclIsKindOf(SpecialFilm)", false);
		
		evaluateBooleanExpression(sourceStream, "p.Reservation.Film->at(2).oclIsKindOf(MyExample::Product)", true);
		evaluateBooleanExpression(sourceStream, "p.Reservation.Film->at(2).oclIsKindOf(Film)", true);
		evaluateBooleanExpression(sourceStream, "p.Reservation.Film->at(2).oclIsKindOf(SpecialFilm)", false);
		evaluateBooleanExpression(sourceStream, "p.Reservation.Film->at(2).oclIsKindOf(IProduct)", true);
		
		evaluateBooleanExpression(sourceStream, "p.Reservation.Film->at(3).oclIsKindOf(Film)", true);
		evaluateBooleanExpression(sourceStream, "p.Reservation.Film->at(4).oclIsKindOf(Film)", true);
		
		evaluateBooleanExpression(sourceStream, "p.Reservation.Film->at(5).oclIsKindOf(MyExample::Product)", true);
		evaluateBooleanExpression(sourceStream, "p.Reservation.Film->at(5).oclIsKindOf(Film)", true);
		evaluateBooleanExpression(sourceStream, "p.Reservation.Film->at(5).oclIsKindOf(SpecialFilm)", true);
		evaluateBooleanExpression(sourceStream, "p.Reservation.Film->at(5).oclIsKindOf(IProduct)", true);

		evaluateBooleanExpression(sourceStream, "p.Reservation.Film->at(6).oclIsKindOf(MyExample::Product)", true);
		evaluateBooleanExpression(sourceStream, "p.Reservation.Film->at(6).oclIsKindOf(Film)", true);
		evaluateBooleanExpression(sourceStream, "p.Reservation.Film->at(6).oclIsKindOf(SpecialFilm)", true);
		
	}

	protected	void createObjectsAndLinks() {
		personClass = (CoreClassifier) environment.lookup("Person");
		filmClass = (CoreClassifier) environment.lookup("Film");
		
		person = objectSpace.createObject(personClass);
		reservations = createInstances("Reservation", 6);
		films = createInstances("Film", 2); 
		specialFilms = createInstances("SpecialFilm", 3);
		
		personReservationAssoc = personClass.lookupAssociationEnd("Reservation").getTheAssociation();
		personRole = personReservationAssoc.getAssociationEnd("Person");
		reservationRole = personReservationAssoc.getAssociationEnd("Reservation");
		
		filmReservationAssoc = filmClass.lookupAssociationEnd("Reservation").getTheAssociation();
		filmRole = filmReservationAssoc.getAssociationEnd("Film");
		filmReservationRole = filmReservationAssoc.getAssociationEnd("Reservation");
		
		films[0].setValueOf("rentalFee", new OclIntegerValue(10));
		
		specialFilms[0].setValueOf("rentalFee", new OclIntegerValue(20));
		specialFilms[1].setValueOf("rentalFee", new OclIntegerValue(30));
		
		specialFilms[0].setValueOf("lateReturnFee", new OclIntegerValue(100));
		specialFilms[1].setValueOf("lateReturnFee", new OclIntegerValue(200));
		
		for (int i = 0; i < reservations.length; i++) {
			objectSpace.createLink(personReservationAssoc,  new ArrayList(Arrays.asList(new Object[] { person, reservations[i] } )), 
																							   new  ArrayList(Arrays.asList(new Object[] { personRole, reservationRole } )));
		}
		
		objectSpace.createLink(filmReservationAssoc,  new ArrayList(Arrays.asList(new Object[] { films[0], reservations[0] } )), 
																						   new  ArrayList(Arrays.asList(new Object[] { filmRole, filmReservationRole } )));

		objectSpace.createLink(filmReservationAssoc,  new ArrayList(Arrays.asList(new Object[] { films[0], reservations[1] } )), 
																						   new  ArrayList(Arrays.asList(new Object[] { filmRole, filmReservationRole } )));

		objectSpace.createLink(filmReservationAssoc,  new ArrayList(Arrays.asList(new Object[] { films[1], reservations[2] } )), 
																						   new  ArrayList(Arrays.asList(new Object[] { filmRole, filmReservationRole } )));

		objectSpace.createLink(filmReservationAssoc,  new ArrayList(Arrays.asList(new Object[] { films[1], reservations[3] } )), 
																						   new  ArrayList(Arrays.asList(new Object[] { filmRole, filmReservationRole } )));

		objectSpace.createLink(filmReservationAssoc,  new ArrayList(Arrays.asList(new Object[] { specialFilms[0], reservations[4] } )), 
																						   new  ArrayList(Arrays.asList(new Object[] { filmRole, filmReservationRole } )));

		objectSpace.createLink(filmReservationAssoc,  new ArrayList(Arrays.asList(new Object[] { specialFilms[1], reservations[5] } )), 
																						   new  ArrayList(Arrays.asList(new Object[] { filmRole, filmReservationRole } )));
	}


}
