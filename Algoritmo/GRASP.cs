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
        Lector data;
        List<Proceso> solucionGRASP;
        Random rnd = new Random();

        public GRASP(Lector lector)
        {
            this.data = lector;
            solucionGRASP = null;
        }

        public Proceso ElegirProceso(List<Proceso> lstProcesos)
        {
            Proceso proc = lstProcesos.ElementAt(0);
            lstProcesos.RemoveAt(0);
            return proc;
        }

        public void EncolarProceso(List<Proceso> lstProcesos, Proceso proc)
        {
            lstProcesos.Add(proc);
        }

        Trabajador ElegirTrabajador(List<Trabajador> lstTrabajadores, Proceso proceso, double alfa)
        {

            //Random rnd = new Random(Guid.NewGuid().GetHashCode());


            //minimizar el ratio:
            double c_min = lstTrabajadores[0].CalcularIndiceProceso(proceso); // 99999
            double c_max = -1;
            for (int i = 0; i < lstTrabajadores.Count; ++i)
            {
                c_min = Math.Min(c_min, lstTrabajadores[i].CalcularIndiceProceso(proceso));
                c_max = Math.Max(c_max, lstTrabajadores[i].CalcularIndiceProceso(proceso));
            }
            double limInf = c_min;
            double limSup = c_min + alfa * (c_max - c_min);

            // Construimos la RCL
            List<Trabajador> RCL = new List<Trabajador>();
            for (int i = 0; i < lstTrabajadores.Count; ++i)
            {
                double indice = lstTrabajadores[i].CalcularIndiceProceso(proceso);
                if (indice >= limInf && indice <= limSup)
                    RCL.Add(lstTrabajadores[i]);
            }

            // Escogemos un elemento aleatorio
            int indRandom = rnd.Next(0, RCL.Count - 1);
            Trabajador trab = RCL.ElementAt(indRandom);

            RCL.RemoveAt(indRandom); // lo saco del RCL
                                     // no se borra el objeto trabajador pq todo es por referencia creo


            int indLstTrabajadores;
            for (indLstTrabajadores = 0; indLstTrabajadores < lstTrabajadores.Count; ++indLstTrabajadores)
            {
                if (lstTrabajadores[indLstTrabajadores].id == trab.id)
                    break;
            }
            lstTrabajadores.RemoveAt(indLstTrabajadores); // lo saco de la lstDeTrabajadores


            return trab;
        }

        public int FuncionObjetivo(List<Proceso> solucion)
        {
            int valor = 0;
            for (int i = 0; i < data.numTrabajadores; ++i)
            {
                for (int j = 0; j < data.numProcesos; ++j)
                {
                    double rotura = 1 + data.trabajadores[i].roturaProceso[j];
                    double tiempo = data.trabajadores[i].tiempoProceso[j];
                    int asignado = solucion[j].asignacionTrabajadores[i];

                    //ESTO HACE TRUNCATE
                    if (asignado != 0)
                        valor += Convert.ToInt32((data.tiempoTurno) / (rotura * tiempo * asignado));
                }
            }
            return valor;
        }

        public List<Proceso> MejorSolucion(List<Proceso> solucion1, List<Proceso> solucion2)
        {
            return FuncionObjetivo(solucion1) > FuncionObjetivo(solucion2) ? solucion1 : solucion2;
        }

        public List<Proceso> AsignacionGRASP()
        {

            for (int i = 0; i < 10000; ++i) // condicion de parada
            {
                List<Proceso> solucionConstruccion = FaseConstruccion(data.alfa);
                List<Proceso> solucionMejorada = FaseMejora(solucionConstruccion); // compara la construccino

                if (solucionGRASP == null)
                    solucionGRASP = solucionMejorada;
                else
                    solucionGRASP = MejorSolucion(solucionGRASP, solucionMejorada);

                //DEBUG
                //Console.WriteLine("Iteracion {0}", i + 1);
                //Console.WriteLine("FO: {0}", FuncionObjetivo(solucionConstruccion));
                //ImprimirAsignacionMatriz(solucionConstruccion);
                //Console.WriteLine("-----------------------------------------------------------------------");

                //Console.WriteLine("Iteracion {0}", i + 1);
                //Console.WriteLine("FO: {0}", FuncionObjetivo(solucionConstruccion));
                //ImprimirAsignacion(solucionConstruccion);
                //Console.WriteLine("-----------------------------------------------------------------------");
                //Console.WriteLine("Iteracion {0}", i + 1);
                //Console.WriteLine("FO: {0}", FuncionObjetivo(solucionGRASP));
                //ImprimirAsignacion(solucionGRASP);
                //Console.WriteLine("=======================================================================");

            }
            return solucionGRASP;
        }

        public List<Proceso> FaseConstruccion(double alfa)
        {
            // Solucion con todos los procesos sin asignaciones (Vacia)
            List<Proceso> solucion = data.procesos.ConvertAll(proc => new Proceso(proc));
            // Lista de procesos por asignar
            List<Proceso> lstProcesos = data.procesos.ConvertAll(proc => new Proceso(proc));
            // Lista de trabajadores por asignar
            List<Trabajador> lstTrabajadores = data.trabajadores.ConvertAll(trab => new Trabajador(trab));

            // Mientras hayan procesos que puedan asignarse y trabajadores por asignar
            while (lstProcesos.Count > 0 && lstTrabajadores.Count > 0)
            {
                Proceso proc = ElegirProceso(lstProcesos); // obtengo el primer proceso

                // si no se hace, no lo cuento (ya lo removi de la lista)
                if (!proc.esConsiderado)
                    continue;

                // escogo a un trabajador (dentro esta el RCL)
                Trabajador trab = ElegirTrabajador(lstTrabajadores, proc, alfa);

                // asigno dicho trabajador al proceso
                //proc.asignarTrabajador(trab); juanjo

                // lo agrego a la solucion
                solucion[proc.id].asignarTrabajador(trab);

                // si el proceso aun se le pueden asignar trabajadores lo agrego al final
                //if (proc.trabajadoresAsignados < proc.puestosDeTrabajo)
                if (solucion[proc.id].trabajadoresAsignados < proc.puestosDeTrabajo)
                    EncolarProceso(lstProcesos, proc);
            }
            return solucion;
        }

        public List<Proceso> FaseMejora(List<Proceso> solucionConstruccion)
        {
            List<Proceso> solucion_mejorada = null;

            return solucionConstruccion;
        }

        public void ImprimirAsignacion()
        {
            for (int i = 0; i < solucionGRASP.Count; ++i)
            {
                Console.WriteLine("Proceso #{0}:", i + 1);

                if (!solucionGRASP[i].esConsiderado)
                {
                    Console.WriteLine("Este proceso no se ha considerado para la asignacion.");
                    continue;
                }

                //for (int j = 0; j < solucion[i].trabajadoresAsignados; ++j)
                for (int j = 0; j < solucionGRASP[i].asignacionTrabajadores.Length; ++j)
                {
                    if (solucionGRASP[i].asignacionTrabajadores[j] == 1)
                        Console.Write(" T{0:D3}", j + 1);
                }
                Console.WriteLine();
            }
        }

        public void ImprimirFuncionObjetivo()
        {
            Console.WriteLine("F.O.: {0}", FuncionObjetivo(solucionGRASP));
        }

        public void ImprimirAsignacion(List<Proceso> sol)
        {
            for (int i = 0; i < sol.Count; ++i)
            {
                Console.WriteLine("Proceso #{0}:", i + 1);

                if (!sol[i].esConsiderado)
                {
                    Console.WriteLine("Este proceso no se ha considerado para la asignacion.");
                    continue;
                }

                //for (int j = 0; j < solucion[i].trabajadoresAsignados; ++j)
                for (int j = 0; j < sol[i].asignacionTrabajadores.Length; ++j)
                {
                    if (sol[i].asignacionTrabajadores[j] == 1)
                        Console.Write(" T{0:D3}", j + 1);

                }
                Console.WriteLine();
            }
        }


        public void ImprimirAsignacionMatriz()
        {
            ImprimirAsignacionMatriz(solucionGRASP);
        }

        public void ImprimirAsignacionMatriz(List<Proceso> sol)
        {
            Console.Write("Proceso:  ");

            for (int i = 0; i < data.numProcesos; ++i)
            {
                Console.Write(" {0}", i + 1);
            }
            Console.WriteLine();

            for (int i = 0; i < data.numTrabajadores; ++i)
            {
                Console.Write("Trab. #{0:D2}:", i + 1);

                for (int j = 0; j < sol.Count; ++j)
                {
                    if (sol[j].asignacionTrabajadores[i] == 1)
                        Console.Write(" 1");
                    else
                        Console.Write(" 0");
                }
                Console.WriteLine();
                                
            }
        }

    }
}
