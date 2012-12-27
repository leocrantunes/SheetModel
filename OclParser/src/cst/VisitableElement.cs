namespace OclParser.cst
{
    public interface VisitableElement {
        void accept(CSTVisitor visitor);
    }
}
