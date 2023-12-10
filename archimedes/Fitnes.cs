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
}
