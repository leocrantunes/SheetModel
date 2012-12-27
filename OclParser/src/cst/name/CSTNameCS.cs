namespace OclParser.cst.name
{
    public abstract class CSTNameCS : CSTNode {
        public abstract string getName();

        public abstract string getLastName();

        public string toString() {
            return this.getName();
        }
    }
}
