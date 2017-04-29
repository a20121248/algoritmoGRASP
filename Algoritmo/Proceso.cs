using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Algoritmo
{
    class Proceso
    {
        public int id;
        public int[] asignacionTrabajadores;
        public int puestosDeTrabajo;
        public int trabajadoresAsignados;
        public bool esConsiderado;
        
        public Proceso(int id)
        {
            this.id = id;
        }


        public void imprimir()
        {
            Console.WriteLine("PROCESO: {0}", id);

            Console.WriteLine("Hace o no hace: {0}", esConsiderado);
            Console.WriteLine("Puestos de trabajo: {0}", puestosDeTrabajo);
            Console.WriteLine("=================================================");
        }
        
        
    }
}
