using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RedeNeural.RedeNeural {

    class Camada {

        private List<Neuronio> neuronios = new List<Neuronio>();

        public Camada (double[][] pesos, double[] pesoBias, IFuncaoAtivacao funcao) {

            for (int i = 0; i < pesos.Length; i++) {
                neuronios.Add(new Neuronio(pesos[i], pesoBias[i], funcao));
            }
        }

        public double[] Estimular(double[] entradas) {

            double[] saidas = new double[neuronios.Count];

            for (int i = 0; i < saidas.Length; i++ ) {
                saidas[i] = neuronios.ElementAt(i).Estimular(entradas);
            }

            return saidas;
        }

        public List<Neuronio> Neuronios {
            get { return neuronios; }
            set { neuronios = value; }
        }
    }
}
