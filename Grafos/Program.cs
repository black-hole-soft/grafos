using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Grafos
{
    class Program
    {
        static void Main(string[] args)
        {
            Grafo grafo = null;
            int i;
            String op;
            System.Console.Write(":::::::::: GRAFOS ::::::::::\n\n");
            System.Console.Write("1)Dririgido\n2)No dirigido\n3)Salir\n");
            op = System.Console.ReadLine();
            if (op == "1")
                grafo = new Grafo(true);
            else if (op == "2")
                grafo = new Grafo(false);
            while (op != "9")
            {
                System.Console.Write(":::::::::: GRAFOS ::::::::::\n\n");
                System.Console.Write("1) Agregar Vertice\n2) Agregar Arista\n3) Eliminar Vertice\n4) Eliminar Arista\n");
                System.Console.Write("5) Dijkstra\n6) Kruskal\n7) Prim\n8) Floyd\n9) Salir\n");
                op = System.Console.ReadLine();
                switch (op)
                {
                    case "1":
                            grafo.agregaV();
                            System.Console.Read();
                        break;
                    case "2":
                            if (grafo.numver < 1)
                                System.Console.Write("No hay vertices, no se puede agregar aristas");
                            else
                            {
                                System.Console.Write("Vertices existentes\n\n");
                                for (i = 0; i < grafo.numver; i++)
                                    System.Console.Write("[" + grafo.verts[i] + "]");
                                grafo.agregaA();
                            }
                            System.Console.Read();
                        break;
                    case "5":
                            if (grafo.numver == 0)
                                System.Console.Write("No hay ningun vertice agregado");
                            else
                            {
                                if (grafo.numaris == 0)
                                    System.Console.Write("No hay ninguna arista agregada");
                                else
                                    grafo.dijkstra();
                            }
                            System.Console.Read();
                        break;
                }
            }
            op = "";
        }
    }
}
