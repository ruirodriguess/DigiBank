using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DigiBank.Classes
{
    public abstract class Banco
    {
        public Banco()
        {
            this.NomeDoBanco = "Banco do Rui";
            this.CodigoDoBanco = "001";
        }

        public string NomeDoBanco { get; private set; }
        public string CodigoDoBanco { get; private set; }
    }
}
