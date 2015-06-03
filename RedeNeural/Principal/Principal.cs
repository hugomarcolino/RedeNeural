using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RedeNeural.Matlab;

namespace RedeNeural.Principal
{
    class Principal {

        static void Main(String[] args) {

            double[] entrada = {0.153313670000000, 12.2154136125058, 0.00736051600000000};
            Console.WriteLine(NeuralNet.Sim(entrada));
            Console.ReadLine();

        }
    }
}
