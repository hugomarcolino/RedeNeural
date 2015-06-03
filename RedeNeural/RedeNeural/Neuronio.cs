using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RedeNeural.RedeNeural {

    class Neuronio {

        private double[] pesos;
        private double pesoBias;
        private double bias;
        private IFuncaoAtivacao funcao;

        public Neuronio(double[] pesos, double pesoBias, IFuncaoAtivacao funcao) {
            this.pesoBias = pesoBias;
            this.pesos = pesos;
            this.funcao = funcao;
            this.bias = 1;
        }

        public double Estimular(double[] entradas) {

            double saida = 0;

            for (int i = 0; i < pesos.Length; i++ ) {
                saida += pesos[i] * entradas[i];
            }

            saida += pesoBias * bias;

            saida = funcao.GetValor(saida);

            return saida;
        }

        public double PesoBias {
            get { return pesoBias; }
            set { pesoBias = value; }
        }

        public double Bias {
            get { return bias; }
            set { bias = value; }
        }

        public double[] Pesos {
            get { return pesos; }
            set { pesos = value; }
        }

        public IFuncaoAtivacao Funcao {
            get { return funcao; }
            set { funcao = value; }
        }
    }
}
