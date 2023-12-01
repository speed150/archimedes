
using archimedes;
using archimedes.@interface;

Archimedes aoa = new();

static double function(params double[] args)
{
    double result = 0.0;
    foreach (var arg in args)
    {
        result += arg * arg;
    }
    return result;
}
double[,] domain = { { -1.0, -1.0,-1.0 }, { 1.0, 1.0,1.0 } };
double[] param = {2.0, 2.0, 2.0, 1.0, 10, 10};
aoa.Solve(function, domain,param );