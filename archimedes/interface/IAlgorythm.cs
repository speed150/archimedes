using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace archimedes.@interface
{

    public interface IStateWriter
    {
        void SaveToFileStateOfAlgorythm(string path, IOptimizationAlgorithm algorythm);
    }
    public interface IStateReader
    {

        void LoadFromFileStateOfAlghoritm(string path);
    }


    public interface IGeneratePDFReport
    {

        void GenerateReport(string path);
    }

    public interface IGenerateTextReport
    {

        string ReportString { get; }
    }

    public class ParamInfo
    {
        string Name { get; set; }
        string Description { get; set; }
        double UpperBoundary { get; set; }
        double LowerBoundary { get; set; }
        double step { get; set; }
    }
    // na poziomie namespace
    public delegate double fitnessFunction(params double[] arg);

    // opis pojedynczego parametru algorytmu , warto ść jest zmienn ą typu double

    public interface IOptimizationAlgorithm
    {
        // Nazwa algorytmu
        string Name { get; set; }

        // Metoda zaczynaj ąca rozwi ą zywanie zagadnienia poszukiwania minimum funkcji

        // Jako argument przyjmuje :
        // funkcj ę celu ,
        // dziedzin ę zadania w postaci tablicy 2D,
        // list ę pozosta łych wymaganych parametr ów algorytmu ( tylko warto ści , w kolejno

        // Po wykonaniu ustawia odpowiednie właś ciwo ści: XBest , Fbest ,NumberOfEvaluationFitnessFunction
        void Solve(fitnessFunction f, double[,] domain, params double[] parameters);

        // Lista informacji o kolejnych parametrach algorytmu
        ParamInfo[] ParamsInfo { get; set; }

        // Obiekt odpowiedzialny za zapis stanu algorytmu do pliku
        // Po każ dej iteracji algorytmu , powinno się wywo łać metod ę
        // SaveToFileStateOfAlghoritm tego obiektu w celu zapisania stanu algorytmu
        IStateWriter writer { get; set; }

        // Obiekt odpowiedzialny za odczyt stanu algorytmu z pliku
        // Na pocz ątku metody Solve , obiekt ten powinien wczyta ć stan algorytmu
        // jeśli stan zosta ł zapisany
        IStateReader reader { get; set; }

        // Obiekt odpowiedzialny za generowanie napisu z raportem
        IGenerateTextReport stringReportGenerator { get; set; }

        // Obiekt odpowiedzialny za generowanie raportu do pliku pdf
        IGeneratePDFReport pdfReportGenerator { get; set; }

        // Właś ciow ść zwracaj ąca tablic ę z najlepszym osobnikiem
        double[] XBest { get; set; }

        // Właś ciwo ść zwracaj ąca warto ść funkcji dopasowania dla najlepszego osobnika
        double FBest { get; set; }

        // Właś ciwo ść zwracaj ąca liczb ę wywo łań funkcji dopasowania
        int NumberOfEvaluationFitnessFunction { get; set; }
    }

}
