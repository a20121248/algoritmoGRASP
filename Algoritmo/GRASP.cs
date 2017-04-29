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

        public Proceso ElegirProceso()
        {
            Proceso proc = data.procesos.ElementAt(0);
            data.procesos.RemoveAt(0);
            return proc;
        }

        public void EncolarProceso(Proceso proc)
        {
            data.procesos.Add(proc);
        }

        public int FuncionObjetivo(List<Proceso> solucion)
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
            int valor1 = FuncionObjetivo(solucion1);
            int valor2 = FuncionObjetivo(solucion2);

            if (valor1 > valor2)
            {
                return solucion1;
            } else
            {
                return solucion2;
            }
        }

        public List<Proceso> AsignacionGRASP()
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

        Trabajador ElegirTrabajador(List<Trabajador> trabajadores, Proceso proceso, double alfa)
        {
            /* TO DO !!! */
            Random rnd = new Random();
            
            //minimizar el ratio:



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
                Proceso proc = ElegirProceso();

                if (!proc.esConsiderado)
                {
                    break;
                }

                Trabajador trab = ElegirTrabajador(data.trabajadores, proc, alfa);

                solucion[proc.id].asignarTrabajador(trab);

                if (proc.trabajadoresAsignados < proc.puestosDeTrabajo)
                {
                    EncolarProceso(proc);
                }                
            }           

            
            return solucion;
        }

        public List<Proceso> FaseMejora(List<Proceso> solucion_construccion)
        {
            List<Proceso> solucion_mejorada = null;

            return solucion_mejorada;
        }
        
    }
}
