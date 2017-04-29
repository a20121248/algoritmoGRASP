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
        public int trabajadoresAsignados=0; // cantidad
        public bool esConsiderado;
        
        public Proceso(int id, int numTrabajadores)
        {
            this.id = id;
            this.asignacionTrabajadores = new int[numTrabajadores];
        }

        public void asignarTrabajador(Trabajador trab){
            asignacionTrabajadores[trab.id] = 1;
            ++trabajadoresAsignados;
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
