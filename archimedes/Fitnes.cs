using archimedes.@interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace archimedes
{
    public class FitnesFunction_Sphere : FitnesFunction
    {

        public string Name { get; set; } = "sphere";



        public double Function(params double[] args)
        {
            double result = 0.0;
            foreach (var arg in args)
            {
                result += arg * arg;
            }
            return result;

        }
    }
    public class FitnesFunction_Rastrigin : FitnesFunction
    {
        public string Name { get; set; } = "Rastrigin";
        public double Function(params double[] args)
        {
            double result = 10.0 * args.Length;
            foreach (var arg in args)
            {
                result += (arg * arg) - 10 * Math.Cos(2.0 * Math.PI * arg);
            }
            return result;
        }
    }
    public class FitnesFunction_Rosenbrock : FitnesFunction
    {
        public string Name { get; set; } = "rosenbrock";
        public double Function(params double[] args)
        {
            double result = 0.0;
            for (int i = 0; i < args.Length - 1; i++)
            {
                result += 100 * (args[i + 1] - (args[i] * args[i])) * (args[i + 1] - (args[i] * args[i])) + (1 - args[i]) * (1 - args[i]);
            }
            return result;
        }
    }
    public class FitnesFunction_Beale: FitnesFunction 
    {
        public string Name { get; set; } = "Beale";
        public double Function(params double[] args) {
            double result = 0.0;
            double x = args[0];
            double y = args[1];
            result = (1.5 - x + x * y) * (1.5 - x + x * y) + (2.25 - x + x * y * y) * (2.25 - x + x * y * y) + (2.625 - x + x * y * y * y) * (2.625 - x + x * y * y * y);
            return result;
        }
    }
    public class FitnesFunction_Himmelblau: FitnesFunction 
    {
        public string Name { get; set; } = "Himmelblau";
        public double Function(params double[] args) {
            double result = 0.0;
            double x = args[0];
            double y = args[1];

            result = (x * x + y - 11) * (x * x + y - 11) + (x + y * y - 7) * (x + y * y - 7);
            return result;

        } }
}
