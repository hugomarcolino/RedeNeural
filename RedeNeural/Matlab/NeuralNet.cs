using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RedeNeural.Matlab {

    class NeuralNet {


        // Input 1

        public static double[,] offset = { { 0.0997480028194516 }, { 0.018204368049405 }, { 0.000180893172216675 } };
        public static double[,] gain = { { 2.22160018113117 }, { 2.03708382367364 }, { 2.00036185180095 } };
        public static double[,] yMin = { { -1 } };

        // Layer 1
        public static double[,] b1 = { { 3.7842806968827492 }, 
                                     { 3.0412759810100587 }, 
                                     { -2.9690044040189698 }, 
                                     { -2.7272547362586272 }, 
                                     { -2.1754637176228093 }, 
                                     { 1.9398098682776823 }, 
                                     { -1.4809780560907471 }, 
                                     { -0.96500974321864708 }, 
                                     { 0.75546542263303951 }, 
                                     { -0.54429808652570411 }, 
                                     { 0.54197285844517407 }, 
                                     { 0.05043753782020307 }, 
                                     { -0.82387788038733878 }, 
                                     { -0.7677868685017345 }, 
                                     { 1.8516803662552361 }, 
                                     { -1.7640340743102811 }, 
                                     { 2.6880315856124639 }, 
                                     { -2.7971932322113893 }, 
                                     { -4.2410293007107231 }, 
                                     { 4.8398199583694606 } };


        public static double[,] IW1_1 =  {{-2.4059349402412802, 2.4132309758721076, -1.7058588767581184},
                                        {-2.5208677651314799, 2.95334579410043, 0.81782098197670994},
                                        {2.3146218586565133, 2.2276136280040246, 2.0631250418975484},
                                        {0.36495776030392729, 0.48647611327649032, -3.6782918992440528},
                                        {2.1981728786601753, 0.73150250292110963, 3.0209520789903963},
                                        {-2.7419969567499187, -2.55460396214347, 0.21721616605939209},
                                        {1.5122797552511935, 0.07778520085458332, 3.4318368624575752},
                                        {0.93933030107341753, -3.4114675745610494, -1.1538476701500637},
                                        {-2.5308199203939661, -2.2215606553814395, 1.8961072206041321},
                                        {2.1101146498997845, -0.91461689470726271, 3.0994725327958013},
                                        {3.6495659038049921, 0.88019933681319662, 0.38782225784013347},
                                        {-2.6124944769581449, 1.6262174626456714, -2.5029696012063223},
                                        {-2.130260733070668, 2.249550100437804, -2.2336182139015488},
                                        {-3.3840188170987595, 1.0098118656132424, -2.1172647714087183},
                                        {2.1384764253202104, 2.0380580972491229, -2.341958698351402},
                                        {-2.3462758185500121, 2.4504975230719968, -2.14171382220893},
                                        {2.5907648088661102, 0.84471132938947979, -2.6508974099237208},
                                        {-3.3021345727214864, -0.14623792779980416, 2.2081494374300559},
                                        {-2.9192811609992866, -1.7935839526033019, -0.35614039458751218},
                                        {0.82132032360451623, 3.3094989416180849, 0.89185359070165771}};

        // Layer 2
        public static double[,] b2 = { { 0.25127216417618459 } };

        public static double[,] LW2_1 = { { -0.19587434112693536, 1.3937775806644448, 0.77240078029432491, -1.7486367316103273, 0.19880140849330424, 0.47991404185246195, 0.42300625354291499, 0.93870240676095418, -1.1900143281113411, 1.0118780120751172, 1.154768615700031, -0.91560202111083389, 0.48241810274440694, -0.90327888870297657, -0.23453426998078047, 1.4689111336669114, 0.79098624802651196, -0.95589841493126382, -1.7180762530471334, 3.1874601379597292 } };


        public static int Sim(double[] input) {
            double y = 0;

            double[,] x = NeuralNet.VetorColumn(input);

            double[,] Xp = Mapminmax(x, NeuralNet.gain, NeuralNet.offset, NeuralNet.yMin);

            double[,] a1 = TanSigApply(NeuralNet.PlusMatrix(b1, NeuralNet.TimesMatrix(IW1_1, Xp)));

            double[,] a2 = LogSigApply(NeuralNet.PlusMatrix(b2, NeuralNet.TimesMatrix(LW2_1, a1)));

            y = a2[0, 0];

            Console.WriteLine(y);

            return y > 0.5 ? 1 : 0;
        }

        public static double[,] Repmat(double[,] m, int x, int y)
        {

            x = m.GetLength(0) * x;
            y = m.GetLength(1) * y;

            double[,] mat = new double[x, y];

            for (int i = 0; i < x; i++)
            {
                for (int j = 0; j < y; j++)
                {
                    mat[i, j] = m[i % m.GetLength(0), j % m.GetLength(1)];
                }
            }

            return mat;
        }

        public static double[,] Mapminmax(double[,] x, double[,] gain, double[,] offset, double[,] yMin)
        {

            double[,] y = new double[x.GetLength(0), x.GetLength(1)];

            y = Bsxfun("@minus", x, offset);
            y = Bsxfun("@times", y, gain);
            y = Bsxfun("@plus", y, NeuralNet.Repmat(yMin, x.GetLength(0), 1));

            return y;
        }

        public static double[,] Bsxfun(String fun, double[,] a, double[,] b)
        {

            double[,] r = new double[a.GetLength(0), a.GetLength(1)];

            for (int i = 0; i < a.GetLength(0); i++)
            {
                for (int j = 0; j < a.GetLength(1); j++)
                {
                    switch (fun)
                    {
                        case "@minus":
                            r[i, j] = a[i, j] - b[i, j];
                            break;
                        case "@plus":
                            r[i, j] = a[i, j] + b[i, j];
                            break;
                        case "@times":
                            r[i, j] = a[i, j] * b[i, j];
                            break;
                    }
                }
            }
            return r;
        }

        public static double TanSig(double valor)
        {
            return 2 / (1 + Math.Exp(-2 * valor)) - 1;
        }

        public static double[,] TanSigApply(double[,] matriz)
        {
            double[,] mat = new double[matriz.GetLength(0), matriz.GetLength(1)];

            for (int i = 0; i < matriz.GetLength(0); i++)
            {
                for (int j = 0; j < matriz.GetLength(1); j++)
                {
                    mat[i, j] = TanSig(matriz[i, j]);
                }
            }

            return mat;
        }

        public static double LogSig(double valor)
        {
            return 1 / (1 + Math.Exp(-valor));
        }

        public static double[,] LogSigApply(double[,] matriz)
        {

            double[,] mat = new double[matriz.GetLength(0), matriz.GetLength(1)];

            for (int i = 0; i < matriz.GetLength(0); i++)
            {
                for (int j = 0; j < matriz.GetLength(1); j++)
                {
                    mat[i, j] = LogSig(matriz[i, j]);
                }
            }

            return mat;
        }


        public static double[,] PlusMatrix(double[,] a, double[,] b)
        {

            double[,] r = new double[a.GetLength(0), a.GetLength(1)];

            for (int i = 0; i < a.GetLength(0); i++)
            {
                for (int j = 0; j < a.GetLength(1); j++)
                {
                    r[i, j] = a[i, j] + b[i, j];

                }
            }
            return r;
        }


        public static double[,] TimesMatrix(double[,] a, double[,] b) {

            double[,] r = new double[a.GetLength(0), b.GetLength(1)];

            for (int i = 0; i < a.GetLength(0); i++)
            {
                for (int j = 0; j < b.GetLength(1); j++)
                {
                    for (int k = 0; k < b.GetLength(0); k++)
                    {
                        r[i, j] += a[i, k] * b[k, j];
                    }
                }
            }
            return r;
        }

        public static double[,] VetorColumn(double[] input) {
            double[,] column = new double[input.Length, 1];

            for (int i = 0; i < input.Length; i++)
            {
                column[i, 0] = input[i];
            }
            return column;
        }
    }
}
