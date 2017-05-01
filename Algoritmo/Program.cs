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
            /* TIEMPO LECTURA */
            var watch = System.Diagnostics.Stopwatch.StartNew();

            Lector lector = new Lector("data.csv");
            lector.LeerData();
            //lector.Imprimir();

            watch.Stop();
            var elapsedMs = watch.ElapsedMilliseconds;
            Console.WriteLine("Tiempo de lectura {0} segundos.", elapsedMs / 1000.0);


            /* TIEMPO GRASP */
            watch = System.Diagnostics.Stopwatch.StartNew();

            GRASP grasp = new GRASP(lector);
            grasp.AsignacionGRASP();
            grasp.ImprimirFuncionObjetivo(); // texto de salida
            //grasp.ImprimirAsignacion(); // texto de salida
            grasp.ImprimirAsignacionMatriz(); // texto de salida

            watch.Stop();
            elapsedMs = watch.ElapsedMilliseconds;
            Console.WriteLine("Tiempo del algoritmo {0} segundos.", elapsedMs / 1000.0);


            //Console.ReadLine();
        }

    }
}
