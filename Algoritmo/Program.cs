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
            Reader lector = new Reader("ruta.csv");
            lector.leerData();

            
            GRASP objGrasp;
            objGrasp = new GRASP(lector);

            objGrasp.asignacion_GRASP();

            objGrasp.imprimir_Asignacion(); // texto de salida
        }

    }
}
