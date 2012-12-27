/*
 * Created on 29/10/2005
 *
 * TODO To change the template for this generated file go to
 * Window - Preferences - Java - Code Style - Code Templates
 */
package br.ufrj.cos.lens.odyssey.tools.psw.objectSpace;

import java.math.BigDecimal;

/**
 * @author Gloria Martins
 *
 * TODO To change the template for this generated type comment go to
 * Window - Preferences - Java - Code Style - Code Templates
 */
public class Teste {

	public static void main(String[] args) {
		BigDecimal b = new BigDecimal("5.5");
		BigDecimal c = new BigDecimal("10");
		BigDecimal d = new BigDecimal(String.valueOf(c.doubleValue() / b.doubleValue()));
		System.out.println(d);
		
		BigDecimal x = new BigDecimal("1600.0" );
		BigDecimal y = new BigDecimal("1.1" );
		BigDecimal z = x.multiply(y);
		
		System.out.println(z);
		
	}
}
