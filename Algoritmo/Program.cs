using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Algoritmo
{
    class Program
    {

        static void Main(string[] args)
        {
            Lector lector = new Lector("data.csv");
            lector.LeerData();
            //lector.Imprimir();

            GRASP grasp = new GRASP(lector);
            grasp.AsignacionGRASP();
            grasp.ImprimirFuncionObjetivo(); // texto de salida
            grasp.ImprimirAsignacion(); // texto de salida


            //Console.ReadLine();
        }

    }
}
