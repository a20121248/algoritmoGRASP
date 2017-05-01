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
        public List<int> asignacionTrabajadores;
        public int trabajadoresAsignados; // cantidad
        public int puestosDeTrabajo;
        public bool esConsiderado;

        public Proceso(Proceso otroProceso)
        {
            this.id = otroProceso.id;

            this.asignacionTrabajadores = new List<int>(otroProceso.asignacionTrabajadores);
            //this.asignacionTrabajadores = new List<int>(new int[otroProceso.asignacionTrabajadores.Count]);
            //Array.Copy(otroProceso.asignacionTrabajadores, this.asignacionTrabajadores, otroProceso.asignacionTrabajadores.Length);
            //this.asignacionTrabajadores = new int[otroProceso.asignacionTrabajadores.Length];
            //for (int i = 0; i < this.asignacionTrabajadores.Length; ++i)
            //this.asignacionTrabajadores[i] = otroProceso.asignacionTrabajadores[i];
            this.trabajadoresAsignados = otroProceso.trabajadoresAsignados;
            this.puestosDeTrabajo = otroProceso.puestosDeTrabajo;
            this.esConsiderado = otroProceso.esConsiderado;
        }

        public Proceso(int id, int numTrabajadores)
        {
            this.id = id;
            this.asignacionTrabajadores = new List<int>(new int[numTrabajadores]); // inicializa en 0 por default
            this.trabajadoresAsignados = 0;
            this.puestosDeTrabajo = -1;
            this.esConsiderado = true;
        }
        
        public void asignarTrabajador(Trabajador trab){
            this.asignacionTrabajadores[trab.id] = 1;
            ++this.trabajadoresAsignados;
        }

        public void Imprimir()
        {
            Console.WriteLine("PROCESO: {0}", id);
            Console.WriteLine("Hace o no hace: {0}", esConsiderado);
            Console.WriteLine("Puestos de trabajo: {0}", puestosDeTrabajo);
        }        
        
    }
}
