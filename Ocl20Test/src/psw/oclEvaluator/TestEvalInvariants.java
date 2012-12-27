/*
 * Created on Aug 16, 2004
 *
 * To change the template for this generated file go to
 * Window&gt;Preferences&gt;Java&gt;Code Generation&gt;Code and Comments
 */
package br.ufrj.cos.lens.odyssey.tools.psw.oclEvaluator;

import java.io.PrintWriter;
import java.util.List;

import br.ufrj.cos.lens.odyssey.tools.psw.parser.OCLSemanticException;
import br.ufrj.cos.lens.odyssey.tools.psw.semantics.values.OclIntegerValue;
import br.ufrj.cos.lens.odyssey.tools.psw.semantics.values.OclObjectValue;
import br.ufrj.cos.lens.odyssey.tools.psw.semantics.values.OclRealValue;
import br.ufrj.cos.lens.odyssey.tools.psw.semantics.values.OclStringValue;

/**
 * @author Administrator
 *
 * To change the template for this generated type comment go to
 * Window&gt;Preferences&gt;Java&gt;Code Generation&gt;Code and Comments
 */
public class TestEvalInvariants extends TestObjectEval {

	public	void	testSingleClassInvariant() {
		try {
			String	sourceStream = "testSingleClassInvariant";
			
			oclCompiler.compileOclStream("context SpecialFilm inv: days1 > 20  inv: lateReturnFee > 20.0", sourceStream, new PrintWriter(System.out));
			
			createInstanceOfSpecialFilm("AI", 10, "1234", "5.90");
			createInstanceOfSpecialFilm("Texas", 21, "1234", "21.90");
			createInstanceOfSpecialFilm("Minority Report", 15, "1234", "21.90");
			createInstanceOfSpecialFilm("Dallas", 25, "1234", "6.90");
			
			PSWInvariantEvaluator invariantEvaluator = new PSWInvariantEvaluator(environment);
			List	violations = invariantEvaluator.evaluate(objectSpace);
			assertEquals(4, violations.size());
		} catch (OCLSemanticException e) {
			System.out.println(e);
			fail();
		} catch (Exception e) {
			e.printStackTrace();
			fail();
		}
	}


	private	void	createInstanceOfSpecialFilm(String name, int days, String code, String lateReturnFee) {
		OclObjectValue	obj = createInstanceOf("SpecialFilm");
		obj.setValueOf("name", new OclStringValue(name));
		obj.setValueOf("days1", new OclIntegerValue(days));
		obj.setValueOf("code", new OclStringValue(code));
		obj.setValueOf("lateReturnFee", new OclRealValue(lateReturnFee));
	}
}
