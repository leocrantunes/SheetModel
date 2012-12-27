using Ocl20.library.iface.expressions;

namespace Ocl20.library.impl
{
    public interface IASTOclVisitor {
        void visitIntegerLiteralExp(IntegerLiteralExp exp);

        void visitRealLiteralExp(RealLiteralExp exp);

        void visitBooleanLiteralExp(BooleanLiteralExp exp);

        void visitStringLiteralExp(StringLiteralExp exp);
	    
        void visitNullLiteralExp(NullLiteralExp exp);
	    
        void visitInvalidLiteralExp(InvalidLiteralExp exp);

        void visitEnumLiteralExp(EnumLiteralExp exp);

        void visitTupleLiteralExp(TupleLiteralExp exp);

        void visitCollectionLiteralExp(CollectionLiteralExp exp);

        void visitOclTypeLiteralExp(OclTypeLiteralExp exp);

        void visitVariableDeclaration(VariableDeclaration varDecl);

        void visitVariableExp(VariableExp exp);

        void visitIfExp(IfExp exp);

        void beginVisitLetExp(LetExp exp);

        void endVisitLetExp(LetExp exp);

        void visitAttributeCallExp(AttributeCallExp exp);

        void visitOperationCalllExpBegin(OperationCallExp exp);
        void visitOperationArgumentExpEnd(OperationCallExp exp);
        void visitOperationCalllExpEnd(OperationCallExp exp);

        void visitAssociationEndCallExp(AssociationEndCallExp exp);

        void visitIteratorExp(IteratorExp exp);

        void visitIterateExp(IterateExp exp);
    }
}
