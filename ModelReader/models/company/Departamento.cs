using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ModelReader
{
    public class Departamento
    {
        private int numero;
        private int nome;
        private Empregado gerente;

        public Empregado Gerente
        {
            get { return gerente; }
            set { gerente = value; }
        }

        public int Nome
        {
            get { return nome; }
            set { nome = value; }
        }
    }
}
