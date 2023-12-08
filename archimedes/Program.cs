
using archimedes;
using archimedes.@interface;

Archimedes aoa = new();


double[,] domain = { { -1.0, -1.0,-1.0 }, { 1.0, 1.0,1.0 } };
double[] param = {2.0, 2.0, 2.0, 1.0, 10, 10};
FitnesFunction_Sphere fitnesFunction_Sphere = new FitnesFunction_Sphere();
aoa.Solve(fitnesFunction_Sphere, domain,param );