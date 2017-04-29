using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Algoritmo
{
    class Trabajador
    {
        public int id = -1;
        public List<double> roturaProceso = new List<double>(); // entre 0 y 1
        public List<int> tiempoProceso = new List<int>(); //en minutos

        public Trabajador(int id)
        {
            this.id = id;
        }

        public Trabajador(Trabajador otroTrabajador)
        {
            this.id = otroTrabajador.id;
            this.roturaProceso = new List<double>(otroTrabajador.roturaProceso);
            this.tiempoProceso = new List<int>(otroTrabajador.tiempoProceso);
        }

        public double CalcularIndiceProceso(Proceso proc)
        {
            return (1 + roturaProceso[proc.id])*tiempoProceso[proc.id];
        }

        public void Imprimir()
        {
            Console.WriteLine("TRABAJADOR: {0}", id);

            Console.Write("Roturas: ");
            for (int i = 0; i < roturaProceso.Count; ++i)
            {
                Console.Write("  {0}", roturaProceso[i]);
            }
            Console.WriteLine();

            Console.Write("Tiempos: ");
            for (int i = 0; i < tiempoProceso.Count; ++i)
            {
                Console.Write("  {0}", tiempoProceso[i]);
            }
            Console.WriteLine();
        }
    }
}
