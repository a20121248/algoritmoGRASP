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
        public Lector data;
        public List<Trabajador> solucionFinal;
        public Random rnd;
        public Impresor impresor;

        public GRASP(Lector lector)
        {
            this.data = lector;
            this.solucionFinal = null;
            this.rnd = new Random();
            this.impresor = new Impresor(this);
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

        public int FuncionObjetivo(List<Trabajador> solucion)
        {
            int valor = 0;
            for (int i = 0; i < data.numTrabajadores; ++i)
            {
                for (int j = 0; j < data.numProcesos; ++j)
                {
                    double rotura = 1 + data.trabajadores[i].roturaProceso[j];
                    double tiempo = data.trabajadores[i].tiempoProceso[j];
                    int asignado = solucion[i].asignacionProcesos[j];

                    //ESTO HACE TRUNCATE
                    if (asignado != 0)
                        valor += Convert.ToInt32((data.tiempoTurno) / (rotura * tiempo * asignado));
                }
            }
            return valor;
        }

        public List<Trabajador> MejorSolucion(List<Trabajador> solucion1, List<Trabajador> solucion2)
        {
            return FuncionObjetivo(solucion1) > FuncionObjetivo(solucion2) ? solucion1 : solucion2;
        }

        public List<Trabajador> AsignacionGRASP()
        {

            for (int i = 0; i < 1000; ++i) // condicion de parada
            {
                List<Trabajador> solucionConstruccion = FaseConstruccion(data.alfa);
                List<Trabajador> solucionMejorada = FaseMejora(solucionConstruccion); // compara la construccino

                
                //impresor.FuncionObjetivo(solucionConstruccion);
                //impresor.FuncionObjetivo(solucionMejorada);
                //Console.WriteLine();
                

                if (solucionFinal == null)
                    solucionFinal = solucionMejorada;
                else
                    solucionFinal = MejorSolucion(solucionFinal, solucionMejorada);

                //Console.WriteLine("=======================================================================");

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
            impresor.FuncionObjetivo(solucionFinal);
            return solucionFinal;
        }

        public void AsignarTrabajadorAProceso(Trabajador trab, Proceso proc)
        {
            proc.asignarTrabajador(trab);
            trab.asignarProceso(proc);
        }

        public List<Trabajador> FaseConstruccion(double alfa)
        {
            // Solucion con todos los procesos sin asignaciones (Vacia)
            List<Proceso> solucionProcesos = data.procesos.ConvertAll(proc => new Proceso(proc));
            List<Trabajador> solucionTrabajadores = data.trabajadores.ConvertAll(trab => new Trabajador(trab));

            // Lista de procesos por asignar
            List<Proceso> bufferProcesos = data.procesos.ConvertAll(proc => new Proceso(proc));
            // Lista de trabajadores por asignar
            List<Trabajador> bufferTrabajadores = data.trabajadores.ConvertAll(trab => new Trabajador(trab));

            // Mientras hayan procesos que puedan asignarse y trabajadores por asignar
            while (bufferProcesos.Count > 0 && bufferTrabajadores.Count > 0)
            {
                Proceso proc = ElegirProceso(bufferProcesos); // obtengo el primer proceso

                // si no se hace, no lo cuento (ya lo removi de la lista)
                if (!proc.esConsiderado)
                    continue;

                // escogo a un trabajador (dentro esta el RCL)
                Trabajador trab = ElegirTrabajador(bufferTrabajadores, proc, alfa);

                // asigno dicho trabajador al proceso
                //proc.asignarTrabajador(trab); juanjo

                // lo agrego a la solucion
                AsignarTrabajadorAProceso(solucionTrabajadores[trab.id], solucionProcesos[proc.id]);
                //solucion[proc.id].asignarTrabajador(trab);

                // si el proceso aun se le pueden asignar trabajadores lo agrego al final
                if (solucionProcesos[proc.id].trabajadoresAsignados < proc.puestosDeTrabajo)
                    EncolarProceso(bufferProcesos, proc);
            }
            //impresor.AsignacionDeTrabajadoresEnCadaProceso(solucionProcesos);
            //impresor.AsignacionDeTrabajadoresEnCadaProcesoModoMatriz(solucionProcesos);
            //Console.WriteLine("-----------------------------------------------------------------------");
            //impresor.AsignacionDeProcesosEnCadaTrabajador(solucionTrabajadores);
            //impresor.AsignacionDeProcesosEnCadaTrabajadorModoMatriz(solucionTrabajadores);
            return solucionTrabajadores;
        }

        public List<Trabajador> CrearSolucionPorIntercambio(int indTrabajador1, int indTrabajador2, List<Trabajador> solucion)
        {
            List<Trabajador> solucionCopia = solucion.ConvertAll(trab => new Trabajador(trab));
            solucionCopia[indTrabajador1].asignacionProcesos = solucion[indTrabajador2].asignacionProcesos;
            solucionCopia[indTrabajador2].asignacionProcesos = solucion[indTrabajador1].asignacionProcesos;
            return solucionCopia;
        }

        public List<Trabajador> FaseMejora(List<Trabajador> solucionConstruccion)
        {
            List<Trabajador> solucionMejora = solucionConstruccion;
            List<Trabajador> nuevaSolucion = null;
            for (int i = 0; i < data.numTrabajadores-1; ++i)
            {
                for (int j = i+1; j < data.numTrabajadores; ++j)
                {
                    nuevaSolucion = CrearSolucionPorIntercambio(i, j, solucionConstruccion);

                    //Console.WriteLine();
                    //Console.WriteLine();
                    //Console.WriteLine();
                    //impresor.AsignacionDeProcesosEnCadaTrabajadorModoMatriz(solucionConstruccion);
                    //Console.WriteLine();
                    //impresor.AsignacionDeProcesosEnCadaTrabajadorModoMatriz(nuevaSolucion);

                    solucionMejora = FuncionObjetivo(nuevaSolucion) > FuncionObjetivo(solucionMejora) ? nuevaSolucion : solucionMejora;

                }
            }
                /*
            for i in 0,num_rows:
	            for j in ri+1,num_rows-1:
		            new_solutions.add(exchange(row[i],row[j]))
                    */
            
            return solucionMejora;
        }        

    }
}
