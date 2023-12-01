using archimedes.@interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using System.Xml.Schema;

namespace archimedes
{
    public static class MyExtensions
    {
        public static T[] Column<T>(this T[,] multidimArray, int wanted_column)
        {
            int l = multidimArray.GetLength(0);
            T[] columnArray = new T[l];
            for (int i = 0; i < l; i++)
            {
                columnArray[i] = multidimArray[i, wanted_column];
            }
            return columnArray;
        }

        public static T[] Row<T>(this T[,] multidimArray, int wanted_row)
        {
            int l = multidimArray.GetLength(1);
            T[] rowArray = new T[l];
            for (int i = 0; i < l; i++)
            {
                rowArray[i] = multidimArray[wanted_row, i];
            }
            return rowArray;
        }



    }
    public class Archimedes : IOptimizationAlgorithm
    {
        public string Name { get; set; }
        public ParamInfo[] ParamsInfo { get ; set ; }
        public IStateWriter writer { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public IStateReader reader { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public IGenerateTextReport stringReportGenerator { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public IGeneratePDFReport pdfReportGenerator { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public double[] XBest { get ; set ; }
        public double FBest { get ; set; }
        public int NumberOfEvaluationFitnessFunction { get; set; }
        static void fun_checkpositions(int dim, double[,] vec_pos, int var_no_group, double[] lb, double[] ub)
        {
            double[] Lb = new double[dim];
            double[] Ub = new double[dim];

            for (int i = 0; i < dim; i++)
            {
                Lb[i] = lb[i];
                Ub[i] = ub[i];
            }

            for (int i = 0; i < var_no_group; i++)
            {
                for (int j = 0; j < dim; j++)
                {
                    if (vec_pos[i, j] < Lb[j])
                    {
                        vec_pos[i, j] = Lb[j];
                    }
                    else if (vec_pos[i, j] > Ub[j])
                    {
                        vec_pos[i, j] = Ub[j];
                    }
                }
            }
        }
        static double Min(double[] array)
        {
            double min = array[0];
            for (int i = 1; i < array.Length; i++)
            {
                if (array[i] < min)
                {
                    min = array[i];
                }
            }
            return min;
        }

        static double Max(double[] array)
        {
            double max = array[0];
            for (int i = 1; i < array.Length; i++)
            {
                if (array[i] > max)
                {
                    max = array[i];
                }
            }
            return max;
        }

        static void ArrayMin(double[] array, out double min, out int minIndex)
        {
            min = array[0];
            minIndex = 0;
            for (int i = 1; i < array.Length; i++)
            {
                if (array[i] < min)
                {
                    min = array[i];
                    minIndex = i;
                }
            }
        }
        public void Solve(fitnessFunction f, double[,] domain, params double[] parameters)
        {
            //int Materials_no, int Max_iter, Func<double[], double> j, int dim, double[] lb, double[] ub, double C3, double C4,
            //out double[] XBest, out double Scorebest, out double[] Convergence_curve)
            int Materials_no = (int)parameters[4];
            int dim = domain.GetLength(1);
            int Max_iter= (int)parameters[5];
            // Initialization
            double C1 = parameters[0], C2 = parameters[1], C3 = parameters[2], C4 = parameters[3];
            double u = 0.9, l = 0.1;   // Parameters in Eq. (12)
            double[,] X = new double[Materials_no, dim];
            double[,] den = new double[Materials_no, dim];
            double[,] vol = new double[Materials_no, dim];
            double[,] acc = new double[Materials_no, dim];
            double[] minmax = new double[dim];
            for(int i =0; i< dim; i++)
            {
                minmax[i] = domain[1,i] - domain[0,i];
            }
            Random rand = new();

            for (int i = 0; i < Materials_no; i++)
            {
                for (int j = 0; j < dim; j++)
                {
                    X[i, j] = domain[0,j] + rand.NextDouble() * minmax[j]; // Initial positions Eq. (4)
                    den[i, j] = rand.NextDouble(); // Eq. (5)
                    vol[i, j] = rand.NextDouble();
                    acc[i, j] = domain[0,j] + rand.NextDouble() * minmax[j]; // Eq. (6)
                }
            }

            double[] Y = new double[Materials_no];
            double[,] acc_norm = new double[Materials_no,dim];

            for (int i = 0; i < Materials_no; i++)
            {
                double[] X_row = new double[dim];
                for (int j = 0; j < dim; j++)
                {
                    X_row[j] = X[i, j];
                }
                Y[i] = f(X_row);
            }

            ArrayMin(Y, out double Scorebest, out int Score_index);
            FBest = Scorebest;
            XBest = new double[dim];
            Array.Copy(X.Row(Score_index), XBest, dim);
            double[] den_best = new double[dim];
            double[] vol_best = new double[dim];
            double[] acc_best = new double[dim];
            Array.Copy(den.Row(Score_index), den_best, dim);
            Array.Copy(vol.Row(Score_index), vol_best, dim);
            Array.Copy(acc.Row(Score_index), acc_best, dim);
            Array.Copy(acc, acc_norm, Materials_no * dim);

             double[] Convergence_curve = new double[Max_iter];

            for (int t = 0; t < Max_iter; t++)
            {
                double TF = Math.Exp((t - Max_iter) / (double)Max_iter);   // Eq. (8)
                if (TF > 1)
                    TF = 1;

                double d = Math.Exp((Max_iter - t) / (double)Max_iter) - (t / (double)Max_iter); // Eq. (9)
                Array.Copy(acc_norm, acc, Materials_no * dim);

                double r = rand.NextDouble();
                for (int i = 0; i < Materials_no; i++)
                {
                    for (int j = 0; j < dim; j++)//row update
                    {
                        den[i, j] = den[i, j] + r * (den_best[j] - den[i, j]);   // Eq. (7)
                        vol[i, j] = vol[i, j] + r * (vol_best[j] - vol[i, j]);

                    }
                    for (int j = 0; j < dim; j++)
                    {
                        double[] acc_temp = new double[dim];
                        if (TF < 0.45) // collision
                        {
                            int mr = rand.Next(Materials_no);
                            acc_temp[j] = (den[mr, j] + (vol[mr, j] * acc[mr, j])) / (rand.NextDouble() * den[i, j] * vol[i, j]);   // Eq. (10)
                        }
                        else
                        {
                            acc_temp[j] = (den_best[j] + (vol_best[j] * acc_best[j])) / (rand.NextDouble() * den[i, j] * vol[i, j]);   // Eq. (11)
                        }
                        acc_norm[i, j] = ((u * (acc_temp[j] - Min(acc_temp))) / (Max(acc_temp) - Min(acc_temp))) + l;   // Eq. (12)
                    }
                }

                double[,] Xnew = new double[Materials_no, dim];
                for (int i = 0; i < Materials_no; i++)
                {
                    for (int j = 0; j < dim; j++)
                    {
                        if (TF < 0.4)
                        {
                            int mrand = rand.Next(Materials_no);
                            Xnew[i, j] = X[i, j] + C1 * rand.NextDouble() * acc_norm[i, j] * (X[mrand, j] - X[i, j]) * d;  // Eq. (13)
                        }
                        else
                        {
                            double p = 2 * rand.NextDouble() - C4;  // Eq. (15)
                            double T = C3 * TF;
                            if (T > 1)
                                T = 1;
                            if (p <= 0.5)
                            {
                                Xnew[i, j] = XBest[j] + C2 * rand.NextDouble() * acc_norm[i, j] * (T * XBest[j] - X[i, j]) * d;  // Eq. (14)
                            }
                            else
                            {
                                Xnew[i, j] = XBest[j] - C2 * rand.NextDouble() * acc_norm[i, j] * (T * XBest[j] - X[i, j]) * d;
                            }
                        }
                    }
                }

                fun_checkpositions(dim, Xnew, Materials_no, domain.Row(0), domain.Row(1));

                for (int i = 0; i < Materials_no; i++)
                {
                    double v = f(Xnew.Row(i));
                    if (v < Y[i])
                    {
                        for (int j = 0; j < dim; j++)
                        {
                            X[i, j] = Xnew[i, j];
                        }
                        Y[i] = v;
                    }
                }

                ArrayMin(Y, out double Ybest, out int var_index);
                Convergence_curve[t] = Ybest;

                if (Ybest < FBest)
                {
                    FBest = Ybest;
                    Score_index = var_index;
                    Array.Copy(X.Row(Score_index), XBest, dim);
                    Array.Copy(den.Row(Score_index), den_best, dim);
                    Array.Copy(vol.Row(Score_index), vol_best, dim);
                    Array.Copy(acc_norm.Row(Score_index), acc_best, dim);
                }
                
                // TODO: Writer()


            }
            Console.WriteLine(string.Join("\n", Convergence_curve));


            }
    }
}