using archimedes.@interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace archimedes
{
    public class Archimedes : IOptimizationAlgorithm
    {
        public string Name { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public ParamInfo[] ParamsInfo { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public IStateWriter writer { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public IStateReader reader { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public IGenerateTextReport stringReportGenerator { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public IGeneratePDFReport pdfReportGenerator { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public double[] XBest { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public double FBest { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public int NumberOfEvaluationFitnessFunction { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        public void Solve(fitnessFunction f, double[,] domain, params double[] parameters)
        {
            throw new NotImplementedException();
        }
    }
}
