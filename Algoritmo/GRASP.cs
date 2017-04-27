using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Algoritmo
{
    class GRASP
    {
        int numTrabajadores;
        int numProcesos;        


        public GRASP(Reader lector)
        {
            numTrabajadores = lector.numTrabajadores;
            numProcesos = lector.numProcesos;
            
                        
        }        


        public List<AsignacionPP> asignacion_GRASP()
        {
            List<AsignacionPP> solucion_construccion = null;
            List<AsignacionPP> solucion_mejorada = null;
            
            for (int i = 0; i < 100000; i++) // condicion de parada
            {
                solucion_construccion = faseConstruccion();
                solucion_mejorada = faseMejora(solucion_construccion);
            }

            return solucion_mejorada;
        }

        public List<AsignacionPP> faseConstruccion(double alfa)
        {
            List<AsignacionPP> solucion = new List<AsignacionPP>(); // Solucion (lista) inicialmente vacia
            for (int i = 0; i < numProcesos; i++)
            {
                int c_min = valor_mejorElemento();
                int c_max = valor_peorElemento();

                List<AsignacionPP> RCL = construir_RCL(alfa);
                //int index = Random(RCL)

            }

            return solucion;
        }

        public List<AsignacionPP> faseMejora(List<AsignacionPP> solucion_construccion)
        {
            List<AsignacionPP> solucion_mejorada = null;

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

        private List<AsignacionPP> construir_RCL(double alfa)
        {
            List<AsignacionPP> RCL = null;



            return RCL;

        }


        public void imprimir_Asignacion()
        {

        }

    }
}
