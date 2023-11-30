using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace archimedes.Funkcje
{
    internal class FitnesFunction
    {
        public string Name { get; set; } = "sphere";
        public double function(params double[] args) 
        {
            double result = 0.0;
            foreach (var arg in args)
            {
                result += arg*arg;
            }
            return result;
        }
    }
}
