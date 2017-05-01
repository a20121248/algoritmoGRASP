using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Algoritmo
{
    class Impresor
    {
        Lector data;
        GRASP grasp;

        public Impresor(GRASP grasp)
        {
            this.grasp = grasp;
            this.data = grasp.data;
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
                for (int j = 0; j < sol[i].asignacionTrabajadores.Count; ++j)
                {
                    if (sol[i].asignacionTrabajadores[j] == 1)
                        Console.Write(" T{0:D3}", j + 1);

                }
                Console.WriteLine();
            }
        }


        public void FuncionObjetivo(List<Trabajador> solucion)
        {
            Console.WriteLine("F.O.: {0}", grasp.FuncionObjetivo(solucion));
        }

        public void AsignacionDeTrabajadoresEnCadaProceso(List<Proceso> solucionProcesos)
        {
            for (int i = 0; i < solucionProcesos.Count; ++i)
            {
                Console.WriteLine("Proceso #{0:D2}:", i + 1);
                if (!solucionProcesos[i].esConsiderado)
                {
                    Console.WriteLine("Este proceso no se ha considerado para la asignacion.");
                    continue;
                }

                //for (int j = 0; j < solucion[i].trabajadoresAsignados; ++j)
                for (int j = 0; j < solucionProcesos[i].asignacionTrabajadores.Count; ++j)
                {
                    if (solucionProcesos[i].asignacionTrabajadores[j] == 1)
                        Console.Write(" T{0:D3}", j + 1);
                }
                Console.WriteLine();
            }
        }

        public void AsignacionDeProcesosEnCadaTrabajador(List<Trabajador> solucionTrabajadores)
        {
            for (int i = 0; i < solucionTrabajadores.Count; ++i)
            {
                Console.WriteLine("Trabajador #{0:D2}:", i + 1);

                for (int j = 0; j < solucionTrabajadores[i].asignacionProcesos.Count; ++j)
                {
                    if (solucionTrabajadores[i].asignacionProcesos[j] == 1)
                        Console.Write(" {0:D3}", j + 1);
                }
                Console.WriteLine();
            }
        }

        
        public void AsignacionDeTrabajadoresEnCadaProcesoModoMatriz(List<Proceso> solucionProcesos)
        {
            for (int i = 0; i < data.numTrabajadores; ++i)
            {
                Console.Write("T#{0:D2}:", i + 1);

                for (int j = 0; j < data.numProcesos; ++j)
                {
                    Console.Write(" {0}", solucionProcesos[j].asignacionTrabajadores[i]);
                }
                Console.WriteLine();
            }
        }

        public void AsignacionDeProcesosEnCadaTrabajadorModoMatriz(List<Trabajador> solucionTrabajadores)
        {
            for (int i = 0; i < solucionTrabajadores.Count; ++i)
            {
                Console.Write("T#{0:D2}:", i + 1);
                for (int j = 0; j < solucionTrabajadores[i].asignacionProcesos.Count; ++j)
                {
                    Console.Write(" {0}", solucionTrabajadores[i].asignacionProcesos[j]);
                }
                Console.WriteLine();
            }
        }


    }
}
