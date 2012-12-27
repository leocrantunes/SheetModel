/*
 * Created on 07/07/2004
 *
 * To change the template for this generated file go to
 * Window>Preferences>Java>Code Generation>Code and Comments
 */
package br.ufrj.cos.lens.odyssey.tools.psw.oclEvaluator;

/**
 * @author Administrator
 *
 * To change the template for this generated type comment go to
 * Window>Preferences>Java>Code Generation>Code and Comments
 */
public class TestEvalIfExp extends TestEval {
	public void	testIf() {
		String name = "testIf";
		
		evaluateIntegerResult(name, "if 10 > 15 then 20 + 30 else 10 + 8 endif", 18);
		evaluateIntegerResult(name, "if 15 > 10 then 20 + 30 else 10 + 8 endif", 50);
		evaluateIntegerResult(name, "if 15 > 10 then if 15 > 5 then 30 + 30 else 10 + 8 endif else 90 endif", 60);
		evaluateIntegerResult(name, "if 15 > 10 then if 5 > 15 then 30 + 30 else 10 + 8 endif else 90 endif", 18);
		evaluateIntegerResult(name, "if 10 > 15 then if 15 > 5 then 30 + 30 else 10 + 8 endif else 90 endif", 90);
		
		evaluateNullResult(name, "if 10 > 15 + null then 20 + 30 else 10 + 8 endif");
		evaluateNullResult(name, "if 15 > 10 then 20 + null else 10 + 8 endif");
		evaluateNullResult(name, "if 10 > 15 + null then 20 + 30 else 10 + null endif");
		
		evaluateInvalidResult(name, "if 10 > 15.div(0) then 20 + 30 else 10 + 8 endif");
		evaluateInvalidResult(name, "if 10 > 5 then (20 + 30).div(0) else 10 + 8 endif");
		evaluateInvalidResult(name, "if 10 > 15 then 20 + 30 else (10 + 8).div(0) endif");
	}
}
