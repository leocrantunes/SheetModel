using System;
using System.Collections.Generic;
using System.Linq;
using Ocl20.library.iface;
using Ocl20.library.iface.common;
using Ocl20.library.iface.expressions;
using Ocl20.library.impl.common;
using Ocl20.library.impl.expressions;
using CoreAssociationEnd = Ocl20.library.iface.common.CoreAssociationEnd;
using Environment = Ocl20.library.iface.environment.Environment;

namespace Ocl20.library.impl.environment
{
    public class EnvironmentImpl : Environment { // extends InstanceHandler implements Environment {
        private Dictionary<string, NamedElement> namedElements;
        private List<object> variableDeclarations;
        private Environment parent;
        private const string PACKAGE_NAMES_SEPARATOR = "::";
        private Ocl20Package oclPackage;

        public EnvironmentImpl() {
            namedElements = new Dictionary<string, NamedElement>();
            variableDeclarations = new List<object>();
        }
        
        public void setOclPackage(Ocl20Package oclPackage) {
            this.oclPackage = oclPackage;
        }
    
        public Ocl20Package getOclPackage() {
            return	this.oclPackage;
        }
    
        public List<object> getVariableDeclarations() {
            return	this.variableDeclarations;
        }
    
        public void addVariableDeclarations(List<object> variables) {
            this.variableDeclarations.AddRange(variables);
        }

        public void clear() {
            this.namedElements.Clear();
            this.variableDeclarations.Clear();
//    	this.parent = null;
        }
    
        /* (non-Javadoc)
     * @see br.cos.ufrj.lens.odyssey.tools.psw.parser.environment.Environment#addElement(string, br.cos.ufrj.lens.odyssey.tools.psw.metamodels.core.CoreModelElement, bool)
     */
        public Environment addElement(
            string name,
            object elem,
            bool implic) {
            // check pre conditions: 
            // 1 - name must be not null
            // 2 - there must be no element with the same incoming name
            if (name == null) {
                throw new NullNameException("name must be not null", 0);
            }

            if (this.lookupLocal(name) != null) {
                throw new NameClashException(
                    "element with name " + name +  " already exists");
            }

            // preconditions ok: add element to namedElements collection
//        NamedElement namedElement = EnvironmentFactoryManager.getInstance(oclPackage).createNamedElement(name, elem, implic);
            NamedElement namedElement = new PSWNamedElement(name, elem, implic);
            namedElements.Add(name, namedElement);

            if (elem.GetType() == typeof(VariableDeclarationImpl))
            variableDeclarations.Add(namedElement);
        
            return this;
            }

        /* (non-Javadoc)
     * @see br.cos.ufrj.lens.odyssey.tools.psw.parser.environment.Environment#addEnvironment(br.cos.ufrj.lens.odyssey.tools.psw.parser.environment.Environment)
     */
        public Environment addEnvironment(Environment env)
        {
            if (env == null) {
                return this;
            }

            List<object> otherEnvNamedElements = env.getNamedElements();
        
            // check if names from env
            // do not clash with the names locally defined in this environment
            foreach (NamedElement element in otherEnvNamedElements) {
                if (this.lookupLocal(element.getName()) != null) {
                    throw new NameClashException(
                        "element with this name already exists");
                }
            }

            // now create a new Environment merging this environment with the env parameter.
            Environment result = EnvironmentFactoryManager.getInstance(oclPackage).getEnvironmentInstance();
            result.addNamedElements(this.getNamedElements());
            result.addNamedElements(env.getNamedElements());

            ((EnvironmentImpl) result).addVariableDeclarations(this.getVariableDeclarations());
            ((EnvironmentImpl) result).addVariableDeclarations(((EnvironmentImpl) env).getVariableDeclarations());
        
            return result;
        }

        /* (non-Javadoc)
     * @see br.cos.ufrj.lens.odyssey.tools.psw.parser.environment.Environment#addNamespace(br.cos.ufrj.lens.odyssey.tools.psw.metamodels.core.CoreNamespace)
     */
        public Environment addNamespace(CoreNamespace ns) {
            this.addNamedElements(ns.getEnvironmentWithoutParents().getNamedElements());
            this.addVariableDeclarations(((EnvironmentImpl) ns.getEnvironmentWithoutParents()).getVariableDeclarations());
            return this;
        }

        /* (non-Javadoc)
     * @see br.cos.ufrj.lens.odyssey.tools.psw.parser.environment.Environment#getNamedElements()
     */
        public List<object> getNamedElements() {
            return this.namedElements.Values.ToList<object>();
        }

        public void addNamedElements(List<object> pNamedElements) {
            foreach (NamedElement element in pNamedElements)
            {
                this.namedElements.Add(element.getName(), element);
            }
        }

        public void removeElement(string name) {
            this.namedElements.Remove(name);
        }

        /* (non-Javadoc)
     * @see br.cos.ufrj.lens.odyssey.tools.psw.parser.environment.Environment#lookup(string)
     */
        public object lookup(string name) {
            object element = this.lookupLocal(name);

            if (element != null) {
                return element;
            } else if (this.hasParent()) {
                return this.getParent()
                           .lookup(name);
            } else {
                return null;
            }
        }

        /* (non-Javadoc)
     * @see br.cos.ufrj.lens.odyssey.tools.psw.parser.environment.Environment#lookupImplicitAttribute(java.lang.string)
     */
        public CoreAttribute lookupImplicitAttribute(string name) {
            ImplicitAttributeElementFinder finder = new ImplicitAttributeElementFinder(name);

            return finder.find(this);
        }

        /* (non-Javadoc)
     * @see br.cos.ufrj.lens.odyssey.tools.psw.parser.environment.Environment#lookupImplicitAssociationEnd(string)
     */
        public CoreAssociationEnd lookupImplicitAssociationEnd(string name) {
            ImplicitAssociationEndElementFinder finder = new ImplicitAssociationEndElementFinder(name);

            return finder.find(this);
        }

        /* (non-Javadoc)
     * @see br.cos.ufrj.lens.odyssey.tools.psw.parser.environment.Environment#lookupImplicitAssociationEnd(string)
     */
        public CoreAssociationClass lookupImplicitAssociationClass(string name) {
            ImplicitAssociationClassElementFinder finder = new ImplicitAssociationClassElementFinder(name);

            return finder.find(this);
        }

        /* (non-Javadoc)
     * @see br.cos.ufrj.lens.odyssey.tools.psw.parser.environment.Environment#lookupImplicitOperation(string, List<object>)
     */
        public CoreOperation lookupImplicitOperation(
            string name,
            List<object> parms) {
            ImplicitOperationElementFinder finder = new ImplicitOperationElementFinder(name, parms);

            return finder.find(this);
            }

        /* (non-Javadoc)
     * @see br.cos.ufrj.lens.odyssey.tools.psw.parser.environment.Environment#lookupLocal(java.lang.string)
     */
        public object lookupLocal(string name) {
            NamedElement namedElement;
            this.namedElements.TryGetValue(name, out namedElement);
            if (namedElement != null)
                return	namedElement.getReferredElement();
            else
                return	null;
        }

        /* (non-Javadoc)
     * @see br.cos.ufrj.lens.odyssey.tools.psw.parser.environment.Environment#lookupLocal(string)
     */
        public List<object> lookupOperationLocal(string name) {
            List<object> operationsFound = new List<object>();

            foreach (KeyValuePair<string,NamedElement> entry in namedElements) {
                if (entry.Key.StartsWith(name)) {
                    NamedElement element;
                    this.namedElements.TryGetValue(entry.Key, out element);

                    if (element != null && element.getReferredElement().GetType() == typeof(CoreOperationImpl)) {
                        CoreOperation operation = (CoreOperation) element.getReferredElement();
                        if (operation.operationNameMatches(name)) {
                            operationsFound.Add(operation);
                        }
                    }
                }
            }

            return operationsFound;
        }

        public object lookupPathName(string name)
        {
            string[] names = name.Split(new[] {PACKAGE_NAMES_SEPARATOR}, StringSplitOptions.None);
            
            return this.lookupPathName(names.ToList());
        }

        /* (non-Javadoc)
     * @see br.cos.ufrj.lens.odyssey.tools.psw.parser.environment.Environment#lookupPathName(List<object>)
     */
        public object lookupPathName(List<string> names) {
            if (names.Count == 1) {
                return lookup((string) names[0]);
            }

            CoreModelElement result = lookupPathNameInsideInnerPackages(names);

            if (result == null) {
                Environment rootEnvironment = getRootEnvironment();

                if (rootEnvironment != null) {
                    result = ((EnvironmentImpl) rootEnvironment).lookupPathNameInsideInnerPackages(names);
                } else {
                    result = null;
                }
            }

            return result;
        }

        public CoreModelElement lookupPathNameInsideInnerPackages(List<string> pNames) {
            CoreModelElement result;
            CoreModelElement firstNamespace;

            List<string> names = new List<string>();
            names.AddRange(pNames);
        
            if (names.Count < 1) {
                return null;
            }

            object element = this.lookupLocal((string) names[0]);

            if (element != null && element.GetType() == typeof(CoreModelElementImpl)) {
                firstNamespace = (CoreModelElement) element;
            } else {
                firstNamespace = null;
            }

            if ((firstNamespace != null) && firstNamespace.GetType() == typeof(CoreNamespaceImpl)) {
                    if (names.Count > 1) {
                        List<string> namesTail = new List<string>();
                        namesTail.AddRange(names.GetRange(1, names.Count));
                        result = ((EnvironmentImpl) nestedEnvironment().addNamespace((CoreNamespace) firstNamespace))
                            .lookupPathNameInsideInnerPackages(namesTail);
                    } else {
                        result = firstNamespace;
                    }
                } else {
                    result = null;
                }

            return result;
        }

        /* (non-Javadoc)
     * @see br.cos.ufrj.lens.odyssey.tools.psw.parser.environment.Environment#nestedEnvironment()
     */
        public Environment nestedEnvironment() {
            EnvironmentImpl result = EnvironmentFactoryManager.getInstance(oclPackage).getNestedEnvironmentInstance();
            result.setParent(this);
            return	result;
        }
    
        public void release() {
            EnvironmentFactoryManager.getInstance(oclPackage).releaseEnvironment(this);
        }

        /**
     * @return
     */
        public Environment getParent() {
            return this.parent;
        }

        /**
     * @param environment
     */
        public void setParent(Environment environment) {
            this.parent = environment;
        }

        public bool hasParent() {
            return this.parent != null;
        }

        public Environment getRootEnvironment() {
            Environment root = getParent();

            if (root != null) {
                while (root.getParent() != null) {
                    root = root.getParent();
                }
            }

            return root;
        }

        public VariableDeclaration lookupVariable(string name) {
            object variable = lookup(name);

            if (variable.GetType() == typeof(VariableDeclarationImpl)) {
                return (VariableDeclaration) variable;
            } else {
                return null;
            }
        }

        /* (non-Javadoc)
     * @see br.cos.ufrj.lens.odyssey.tools.psw.parser.environment.Environment#lookupSourceForImplicitAttribute(string)
     */
        public VariableDeclaration lookupSourceForImplicitFeature(
            string simpleName) {
            return lookupSourceForImplicitProperty(simpleName, null);
            }

        public VariableDeclaration lookupSourceForImplicitOperation(
            string simpleName,
            List<object> parms) {
            return lookupSourceForImplicitProperty(simpleName, parms);
            }

        public VariableDeclaration lookupSourceForImplicitProperty(
            string simpleName,
            List<object> parms) {
            List<object> variables = this.getVariableElements();

            if (variables.Count > 0) {
                foreach (NamedElement element  in variables) {
                    VariableDeclaration variable = (VariableDeclaration) element.getReferredElement();
                    CoreClassifier classifier = variable.getType();

                    if (element.isMayBeImplicit() &&
                        isFeatureDefinedInClassifier(classifier, simpleName,
                                                     parms)) {
                                                         return variable;
                                                     }
                }
            }

            if (this.hasParent()) {
                return this.getParent()
                           .lookupSourceForImplicitFeature(simpleName);
            }

            return null;
            }

        public List<object> getAllOfType(Type clazz) {
            List<object> result = new List<object>();

            foreach (KeyValuePair<string, NamedElement> element in namedElements) {
                if (clazz.IsInstanceOfType(element.Value.getReferredElement())) {
                    result.Add(element.Value.getReferredElement());
                }
            }

            return result;
        }

        private bool isFeatureDefinedInClassifier(
            CoreClassifier classifier,
            string simpleName,
            List<object> parms) {
            if (parms == null) {
                return (classifier.lookupAttribute(simpleName) != null) ||
                       (classifier.lookupAssociationEnd(simpleName) != null) ||
                       (classifier.lookupAssociationClass(simpleName) != null);
            } else {
                return classifier.lookupOperation(simpleName, parms) != null;
            }
            }

        private List<object>  getVariableElements() {
            List<object> variables = new List<object>();

            foreach (KeyValuePair<string,NamedElement> element in namedElements) {
                if (element.Value.getReferredElement().GetType() == typeof(VariableDeclarationImpl)) {
                    variables.Add(element);
                }
            }
            return variables;
        }

        /* (non-Javadoc)
	 * @see ocl20.environment.Environment#getChildren()
	 */
        public List<object> getChildren() {
            return null;
        }
    
        // Strategy classes for finding implicit attributes, association ends and operations
        abstract class ModelElementFinder {
            protected string name;
            protected EnvironmentImpl env;

            public ModelElementFinder(string name) {
                this.name = name;
            }

            protected abstract CoreModelElement doSpecificLookup(NamedElement currentElement);

            public CoreModelElement doLookup(Environment env) {
                CoreModelElement foundElement = null;

                foreach (NamedElement currentElement in ((EnvironmentImpl) env).getVariableDeclarations()) {
                    if (currentElement.isMayBeImplicit() &&
                        currentElement.getType() != null &&
                        ((foundElement = doSpecificLookup(currentElement)) != null)) {
                            return foundElement;
                        }
                }
            
                if (env.getParent() != null) {
                    CoreModelElement result = doLookup(env.getParent());
                    return	result;
                } else {
                    return null;
                }
            }
        }

        class ImplicitAttributeElementFinder : ModelElementFinder {
            public ImplicitAttributeElementFinder(string name) : base(name) {
            }

            protected override CoreModelElement doSpecificLookup(NamedElement currentElement) {
                return currentElement.getType().lookupAttribute(name);
            }

            public CoreAttribute find(EnvironmentImpl env) {
                return (CoreAttribute) base.doLookup(env);
            }
        }

        class ImplicitAssociationEndElementFinder : ModelElementFinder {
            public ImplicitAssociationEndElementFinder(string name) : base(name) {
            }

            protected override CoreModelElement doSpecificLookup(NamedElement currentElement) {
                CoreModelElement element = currentElement.getType().lookupAssociationEnd(name);
                return	element;
            }

            public CoreAssociationEnd find(EnvironmentImpl env) {
                return (CoreAssociationEnd) base.doLookup(env);
            }
        }

        class ImplicitAssociationClassElementFinder : ModelElementFinder {
            public ImplicitAssociationClassElementFinder(string name) : base(name) {
            }

            protected override CoreModelElement doSpecificLookup(NamedElement currentElement) {
                return currentElement.getType()
                                     .lookupAssociationClass(name);
            }

            public CoreAssociationClass find(EnvironmentImpl env) {
                return (CoreAssociationClass) base.doLookup(env);
            }
        }

        class ImplicitOperationElementFinder : ModelElementFinder {
            List<object> parms;

            public ImplicitOperationElementFinder(string name, List<object> parms) : base(name) {
                this.parms = parms;
            }

            protected override CoreModelElement doSpecificLookup(NamedElement currentElement) {
                return currentElement.getType().lookupOperation(name, parms);
            }

            public CoreOperation find(EnvironmentImpl env) {
                return (CoreOperation) base.doLookup(env);
            }
        }
    }
}
