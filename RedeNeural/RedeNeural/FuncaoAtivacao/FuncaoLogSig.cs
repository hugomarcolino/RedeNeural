using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RedeNeural.RedeNeural.FuncaoAtivacao 
{
    class FuncaoLogSig : IFuncaoAtivacao {

        public FuncaoLogSig() { }

        public double GetValor(double valor) {
            return 1/(1 + Math.Exp(-valor));
        }
    }
}
