using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RedeNeural.RedeNeural.FuncaoAtivacao {
    
    class FuncaoTanSig : IFuncaoAtivacao {

        public FuncaoTanSig() { }

        public double GetValor(double valor) {
            return 2/(1 + Math.Exp(-2 * valor)) - 1;
        }

    }
}
