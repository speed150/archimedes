
using archimedes;
using archimedes.@interface;
using Newtonsoft.Json;

Archimedes aoa = new();


double[,] domain = { { -1.0, -1.0,-1.0 }, { 1.0, 1.0,1.0 } };
double[] param = {2.0, 2.0, 2.0, 1.0, 10, 10};
FitnesFunction_Sphere fitnesFunction_Sphere = new FitnesFunction_Sphere();
FitnesFunction_Rastrigin fitnesFunction_Rastrigin = new FitnesFunction_Rastrigin();
string json = JsonConvert.SerializeObject(fitnesFunction_Sphere);
Console.WriteLine(json);
aoa.Solve(fitnesFunction_Rastrigin, domain,param );