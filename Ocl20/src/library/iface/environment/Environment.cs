using System;
using System.Collections.Generic;
using Ocl20.library.iface.common;
using Ocl20.library.iface.expressions;
using CoreAssociationEnd = Ocl20.library.iface.common.CoreAssociationEnd;

namespace Ocl20.library.iface.environment
{
    public interface Environment
    {
        //extends javax.jmi.reflect.RefObject {
        /**
     * @param namedElements 
     */
        void addNamedElements(List<object> namedElements);
        /**
     * @return 
     */
        List<object> getNamedElements();
        /**
     * @param name 
     * @return 
     */
        object lookupLocal(string name);
        /**
     * @param name 
     * @return 
     */
        List<object> lookupOperationLocal(string name);
        /**
     * @param name 
     * @return 
     */
        object lookup(string name);
        /**
     * @param name 
     * @return 
     */
        object lookupPathName(string name);
        /**
     * @param names 
     * @return 
     */
        object lookupPathName(List<string> names);
        /**
     * @param name 
     * @return 
     */
        CoreAttribute lookupImplicitAttribute(string name);
        /**
     * @param name 
     * @return 
     */
        CoreAssociationEnd lookupImplicitAssociationEnd(string name);
        /**
     * @param name 
     * @return 
     */
        CoreAssociationClass lookupImplicitAssociationClass(string name);
        /**
     * @param name 
     * @param params 
     * @return 
     */
        CoreOperation lookupImplicitOperation(string name, List<object> parms);
        /**
     * @param name 
     * @return 
     */
        VariableDeclaration lookupVariable(string name);
        /**
     * @param name 
     * @return 
     */
        VariableDeclaration lookupSourceForImplicitFeature(string name);
        /**
     * @param name 
     * @param params 
     * @return 
     */

        VariableDeclaration lookupSourceForImplicitOperation(string name, List<object> parms);

        /**
     * @param name 
     * @param elem 
     * @param implicit 
     * @return 
     */
        Environment addElement(string name, object elem, bool implic);
        /**
     * @param env 
     * @return 
     */
        Environment addEnvironment(Environment env);
        /**
     * @param ns 
     * @return 
     */
        Environment addNamespace(CoreNamespace ns);
        /**
     * @return 
     */
        Environment nestedEnvironment();
        /**
     * @param name 
     */
        void removeElement(string name);
        /**
     * Returns the value of reference parent.
     * @return Value of reference parent.
     */
        Environment getParent();
        /**
     * Sets the value of reference parent. See {@link #getParent} for description 
     * on the reference.
     * @param newValue New value to be set.
     */
        void setParent(Environment newValue);
        /**
     * Returns the value of reference children.
     * @return Value of reference children.
     */
        List<object> getChildren();

        List<object> getAllOfType(Type clazz);
    }
}