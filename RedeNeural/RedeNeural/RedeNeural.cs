using RedeNeural.RedeNeural.FuncaoAtivacao;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RedeNeural.RedeNeural {

    class RedeNeural {
        private List<Camada> camadas = new List<Camada>();
        
        private double[][] pesosCamadaEscondida;
        private double[] pesosBiasCamadaEscondida;

        private Camada camadaEscondida;

        private double[][] pesosCamadaSaida;
        private double[] pesosBiasCamadaSaida;

        private Camada camadaSaida;

        public RedeNeural() {

            lerPesos();

            camadaEscondida = new Camada(pesosCamadaEscondida, pesosBiasCamadaEscondida, new FuncaoTanSig());

            camadaSaida = new Camada(pesosCamadaSaida, pesosBiasCamadaSaida, new FuncaoLogSig());

        }

        public double Estimular(double[] entradas) {
            
            return camadaSaida.Estimular(camadaEscondida.Estimular(entradas))[0];
        }

        public void lerPesos() {

            pesosCamadaEscondida = new double[][] {
                                    new double[] {-0.6042, -2.2177}, 
                                    new double[] {-0.8684, 1.4526}};

            pesosCamadaSaida = new double[][] {
                                new double[] {-2.4867, 0.1123}};

            pesosBiasCamadaEscondida = new double[] {-2.7902, -2.4096};

            pesosBiasCamadaSaida = new double[] {-0.2314};
        }
    }
}