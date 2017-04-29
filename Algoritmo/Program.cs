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
            Lector lector = new Lector("ruta2.csv");
            lector.LeerData();
            

            

            //GRASP grasp;
            //grasp = new GRASP(lector);

            //objGrasp.asignacion_GRASP();

            //objGrasp.imprimir_Asignacion(); // texto de salida
        }

    }
}
