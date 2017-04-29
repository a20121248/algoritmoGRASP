using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Threading.Tasks;
using System.Collections;

namespace Algoritmo
{
    class Lector
    {
        public double alfa; // va a ser agregado en el Excel

        public int numTrabajadores;
        public int numProcesos;
        public int tiempoTurno; // en minutos

        string ruta;

        public List<Proceso> procesos = new List<Proceso>();
        public List<Trabajador> trabajadores = new List<Trabajador>();
        


        public Lector(string ruta)
        {
            this.ruta = ruta;
        }

        public void LeerData()
        {
            StreamReader file = new System.IO.StreamReader(ruta);
            String linea;

            file.ReadLine(); //cabecera
            linea = file.ReadLine();
            String[] data = linea.Split(',');
            this.numTrabajadores = Convert.ToInt32(data[0]);
            this.numProcesos = Convert.ToInt32(data[1]);
            this.alfa = Convert.ToDouble(data[2]);
            this.tiempoTurno = Convert.ToInt32(data[3]);

            file.ReadLine(); //newline
            file.ReadLine(); //cabecera de rotura

            // Leo indices de rotura, matriz de trabajador x proceso
            for (int i = 0; i < this.numTrabajadores; ++i)
            {
                Trabajador trabajador = new Trabajador(i);

                linea = file.ReadLine();
                String[] linea_rotura = linea.Split(',');
                for (int j = 1; j < this.numProcesos+1; ++j)
                {                    
                    trabajador.roturaProceso.Add(Convert.ToDouble(linea_rotura[j]));
                    //rotura[i, j-1] = Convert.ToDouble(linea_rotura[j]);
                }
                trabajadores.Add(trabajador);
            }
            
            linea = file.ReadLine(); //newline
            linea = file.ReadLine(); //cabecera de tiempo

            

            // Leo indices de tiempo, matriz de trabajador x proceso
            for (int i = 0; i < this.numTrabajadores; ++i)
            {
                linea = file.ReadLine();
                String[] linea_tiempo = linea.Split(',');
                for (int j = 1; j < this.numProcesos+1; ++j)
                {
                    trabajadores[i].tiempoProceso.Add(Convert.ToInt32(linea_tiempo[j]));
                    //tiempo[i, j-1] = Convert.ToInt32(linea_tiempo[j]);
                }
                trabajadores[i].Imprimir();
            }

            linea = file.ReadLine(); //newline

            linea = file.ReadLine();
            data = linea.Split(',');
            for (int i = 1; i < this.numProcesos + 1; ++i)
            {
                Proceso proceso = new Proceso(i-1, this.numTrabajadores);
                proceso.puestosDeTrabajo = Convert.ToInt32(data[i]);
                procesos.Add(proceso);
            }


            linea = file.ReadLine(); //newline

            linea = file.ReadLine();
            data = linea.Split(',');
            for (int i = 1; i < this.numProcesos + 1; ++i)
            {
                procesos[i-1].esConsiderado = (Convert.ToInt32(data[i]) == 1) ? true : false;
                procesos[i-1].imprimir();
            }
            
            file.Close();
        }
    }
}
