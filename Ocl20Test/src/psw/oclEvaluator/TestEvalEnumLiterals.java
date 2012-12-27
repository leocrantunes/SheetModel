/*
 * Created on Jun 28, 2004
 *
 * To change the template for this generated file go to
 * Window&gt;Preferences&gt;Java&gt;Code Generation&gt;Code and Comments
 */
package br.ufrj.cos.lens.odyssey.tools.psw.oclEvaluator;

/**
 * @author Administrator
 *
 * To change the template for this generated type comment go to
 * Window&gt;Preferences&gt;Java&gt;Code Generation&gt;Code and Comments
 */
public class TestEvalEnumLiterals extends TestEval {

	public	void	testComparisons() {
		String	name = "testComparisons";
		
		evaluateBooleanResult(name, "Situation::married = Situation::single", false);
		evaluateBooleanResult(name, "Situation::married = Situation::married", true);
		evaluateBooleanResult(name, "Situation::single = Situation::single", true);
		
		evaluateBooleanResult(name, "Situation::married <> Situation::single", true);
		evaluateBooleanResult(name, "Situation::married <> Situation::married", false);
		evaluateBooleanResult(name, "Situation::single <> Situation::single", false);
		
		evaluateBooleanResult(name, "Situation::married.oclIsTypeOf(Situation)", true);
	}

}
