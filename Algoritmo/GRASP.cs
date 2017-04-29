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

        public int funcionObjetivo(List<Proceso> solucion)
        {
            int valor = 0;
            for (int i = 0; i < data.numTrabajadores; ++i)
            {
                for (int j = 0; i < data.numProcesos; ++j)
                {
                    double rotura = 1 + data.trabajadores[i].roturaProceso[j];
                    double tiempo = data.trabajadores[i].tiempoProceso[j];
                    int asignado = data.procesos[j].asignacionTrabajadores[i];

                    //ESTO HACE TRUNCATE
                    valor += Convert.ToInt32((data.tiempoTurno) / (rotura * tiempo * asignado));
                }
            }
            return valor;
        }

        public List<Proceso> MejorSolucion(List<Proceso> solucion1, List<Proceso> solucion2)
        {
            int valor1 = funcionObjetivo(solucion1);
            int valor2 = funcionObjetivo(solucion2);

            if (valor1 > valor2)
            {
                return solucion1;
            } else
            {
                return solucion2;
            }
        }

        public List<Proceso> asignacion_GRASP()
        {
            List<Proceso> solucion = null;
            for (int i = 0; i < 100000; i++) // condicion de parada
            {
                List<Proceso> solucionConstruccion = FaseConstruccion(data.alfa);
                List<Proceso> solucionMejorada = FaseMejora(solucionConstruccion); // compara la construccino
                solucion = MejorSolucion(solucion, solucionMejorada);
            }
            return solucion;
        }

        Trabajador elegirTrabajador(List<Trabajador> trabajadores, Proceso proceso, double alfa)
        {
            /* TO DO !!! */
            Random rnd = new Random();

            //int c_min = valor_mejorElemento();
            //int c_max = valor_peorElemento();

            //List<AsignacionPP> RCL = construir_RCL(alfa);

            int indice = rnd.Next(0, trabajadores.Count-1);
            return trabajadores[indice];
        }


        public List<Proceso> FaseConstruccion(double alfa)
        {
            //List<Proceso> solucion = new List<Proceso>(); // Solucion (lista) inicialmente vacia
            List<Proceso> solucion = new List<Proceso>(data.procesos);

            while (data.trabajadores.Count > 0 && data.procesos.Count > 0)
            {
                Proceso proc = elegirProceso();

                if (!proc.esConsiderado)
                {
                    break;
                }

                Trabajador trab = elegirTrabajador(data.trabajadores, proc, alfa);

                solucion[proc.id].asignarTrabajador(trab);

                if (proc.trabajadoresAsignados < proc.puestosDeTrabajo)
                {
                    encolarProceso(proc);
                }                
            }           

            
            return solucion;
        }

        public List<Proceso> FaseMejora(List<Proceso> solucion_construccion)
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
        
    }
}
