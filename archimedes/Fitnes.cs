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
    public class FitnesFunction_Rastrigin: FitnesFunction
    {
        public string Name { get; set; } = "Rastrigin";
        public double Function(params double[] args) { 
        double result = 10.0* args.Length;
            foreach (var arg in args) {
                result += (arg * arg) - 10 * Math.Cos(2.0 * Math.PI * arg);
            }
            return result;
        }
    }
}
