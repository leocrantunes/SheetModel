using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SheetModel.Model
{
    public class AssociationMember
    {
        private string _className;
        public string ClassName
        {
            get { return _className; }
            set { _className = value; }
        }

        private string _multiplicity;
        public string Multiplicity
        {
            get { return _multiplicity; }
            set { _multiplicity = value; }
        }

        private string _role;
        public string Role
        {
            get { return _role; }
            set { _role = value; }
        }
    }
}
