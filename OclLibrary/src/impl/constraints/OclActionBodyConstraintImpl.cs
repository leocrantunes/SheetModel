using OclLibrary.iface.constraints;

public class OclActionBodyConstraintImpl : OclOperationConstraintImpl, OclActionBodyConstraint {

	private	Action	action;
	private	Collection parameterNames;
	
	/**
	 * 
	 */
	public OclActionBodyConstraintImpl() {
		super();
		parameterNames = new ArrayList();
	}

	/* (non-Javadoc)
	 * @see ocl20.constraints.OclActionBodyConstraint#getAction()
	 */
	public Action getAction() {
		return action;
	}
	
	/* (non-Javadoc)
	 * @see ocl20.constraints.OclActionBodyConstraint#setAction(br.ufrj.cos.lens.odyssey.tools.psw.oclScript.metamodel.base.Action)
	 */
	public void setAction(Action action) {
		this.action = action;

	}
	
	/**
	 * @return Returns the parameterNames.
	 */
	public Collection getParameterNames() {
		return parameterNames;
	}
	/**
	 * @param parameterNames The parameterNames to set.
	 */
	public void setParameterNames(Collection parameterNames) {
		this.parameterNames = parameterNames;
	}
	
	public String toString() {
		return	"actionBody: click to open source file";
	}


}
