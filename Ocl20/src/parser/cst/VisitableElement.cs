namespace Ocl20.parser.cst
{
    public interface VisitableElement {
        void accept(CSTVisitor visitor);
    }
}
