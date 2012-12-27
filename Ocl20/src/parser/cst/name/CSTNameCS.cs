namespace Ocl20.parser.cst.name
{
    public abstract class CSTNameCS : CSTNode {
        public abstract string getName();

        public abstract string getLastName();

        public override string ToString() {
            return this.getName();
        }
    }
}
