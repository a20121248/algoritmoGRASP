using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Algoritmo
{
    class GRASP
    {
        //int numTrabajadores;
        //int numProcesos;
        Lector data;

        public GRASP(Lector lector)
        {
            this.data = lector;            
            //Console.WriteLine(data.numTrabajadores);
            //Console.WriteLine(data.numProcesos);
        }

        public Proceso elegirProceso()
        {
            Proceso proc = data.procesos.ElementAt(0);
            data.procesos.RemoveAt(0);
            return proc;
        }

        public void encolarProceso(Proceso proc)
        {
            data.procesos.Add(proc);
        }

        public List<Proceso> asignacion_GRASP()
        {
            List<Proceso> solucion_construccion = null;
            List<Proceso> solucion_mejorada = null;            
            

            for (int i = 0; i < 100000; i++) // condicion de parada
            {
                solucion_construccion = faseConstruccion(0.6);
                solucion_mejorada = faseMejora(solucion_construccion);
            }

            return solucion_mejorada;
        }

        public List<Proceso> faseConstruccion(double alfa)
        {
            
            List<Proceso> solucion = new List<Proceso>(); // Solucion (lista) inicialmente vacia
            /*
            for (int i = 0; ; i++)
            {
                int c_min = valor_mejorElemento();
                int c_max = valor_peorElemento();

                List<AsignacionPP> RCL = construir_RCL(alfa);
                //int index = Random(RCL)

            }
            */
            return solucion;
        }

        public List<Proceso> faseMejora(List<Proceso> solucion_construccion)
        {
            List<Proceso> solucion_mejorada = null;

            return solucion_mejorada;
        }

        private int valor_mejorElemento()
        {
            return 1;
        }

        private int valor_peorElemento()
        {
            return 1;
        }

        private List<Proceso> construir_RCL(double alfa)
        {
            List<Proceso> RCL = null;



            return RCL;

        }


        public void imprimir_Asignacion()
        {

        }

    }
}
