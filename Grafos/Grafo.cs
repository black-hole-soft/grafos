using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Grafos
{
    class Grafo
    {
	    internal bool dirigido;
        internal int numaris = 0, numver = 0;
	    internal int[] verts = new int[1000], peso = new int[1000];
        internal int[] ar1 = new int[1000], ar2 = new int[1000];

        internal Grafo(bool d)
        {
            dirigido = d;
        }
        internal void agregaV()
        {
            if(numver == 0)
                verts[numver] = 0;
            else
                verts[numver] = verts[numver - 1] + 1;
            numver++;
            muestraVertices();
        }
        internal void muestraVertices()
        {
            System.Console.Write("Vertices existentes\n\n");
                for(int i = 0; i < numver; i++)
                    System.Console.Write("[" + verts[i] + "] ");
        }
        internal void agregaA()
        {
	        int ini, fin, pes, i;
            bool found = false;
	        do
	        {
		        System.Console.Write("\n\nVertice 1: ");
		        ini = int.Parse(System.Console.ReadLine());
		        for (i = 0; i < numver; i++)
		        {
			        if(verts[i] == ini)
			        {
				        found = true;
				        break;
			        }
		        }
		        if (!found)
			        System.Console.Write("No existe el vertice\n");
            } while (!found);
            found = false;
	        do
	        {
		        System.Console.Write("\nVertice 2: ");
                fin = int.Parse(System.Console.ReadLine());
		        for (i = 0; i < numver; i++)
		        {
			        if(verts[i] == fin)
			        {
				        found = true;
				        break;
			        }
		        }
		        if(!found)
			        System.Console.Write("No existe el vertice\n");
            } while (!found);
            found = false;
	        do
            {
		        System.Console.Write("\nPeso: ");
                pes = int.Parse(System.Console.ReadLine());
                if (pes < 0)
                {
                    System.Console.Write("Debe ser entero positivo\n");
                    found = false;
                }
                else
                    found = true;
	        } while (!found);
	        ar1[numaris] = ini;
	        ar2[numaris] = fin;
	        peso[numaris] = pes;
	        numaris++;
	        System.Console.Write("Aristas existentes\n\n");
	        muestraAristas();
        }
        internal void muestraAristas()
        {
	        int i;
	        for (i = 0; i < numaris; i++)
            {
		        System.Console.Write((i + 1).ToString() + ". [" +  ar1[i] + "] --");
                if(dirigido)
                    System.Console.Write(">");
                System.Console.WriteLine(" [" + ar2[i] + "] peso: " + peso[i]);
            }
        }
        internal void eliminaV(int elimVr)
        {
	        int i, j;
	        for (i = 0; i < numver; i++)
	        {
		        if(verts[i] == elimVr)
		        {
			        for(j = i; j < numver;j++)
			        {
				        verts[j] = verts[j + 1];
				        verts[j + 1] = 0;
			        }
			        numver--;
			        break;
		        }
	        }
            muestraVertices();
        }
        internal void eliminaA(int elimAr)
        {
	        int i;
	        for(i = (elimAr - 1); i < (numaris - elimAr); i++)
	        {
	           ar1[i] = ar1[i + 1];
	           ar1[i + 1] = 0;
	           ar2[i] = ar2[i + 1];
	           ar2[i + 1] = 0;
	           peso[i] = peso[i + 1];
	           peso[i + 1] = 0;
	        }
	        numaris--;
            muestraAristas();
        }
        internal void dijkstra()
        {
            int[,] final = new int[1000, 2];
            int[] P = new int[1000], D = new int[1000], C = new int[1000]; 
	        int i, verini, verD = 0, pesmen = 1000, banfin = -1, k;
	        for (i = 0; i < 1000; i++)
	        {
		        final[i, 0] = -1;
		        final[i, 1] = -1;
	        }
	        //inicia
	        verini = verts[0];
	        for(i = 1; i < numver; i++)
	        {
		        C[i - 1] = verts[i];
		        P[i - 1] = verini;
		        if(dirigido)
		        {
		            for (int j = verD; j < numaris; j++)
		            {
			            if(ar1[j] == verini && ar2[j] == verts[i] && verD < numver)
			            {
				            D[verD] = peso[j];
				            verD++;
			            }
			            else
				            if(j < numver)
					            D[j] = -1000;
		            }
		        }
		        else
		        {
		            for (int j = verD; j < numaris; j++)
		            {
			            if (ar1[j] == verini && ar2[j] == verts[i] && verD < numver)
			            {
				            D[verD] = peso[j];
				            verD++;
			            }
			            else
			            {
				            if (ar2[j] == verini && ar1[j] == verts[i] && verD < numver)
				            {
					            D[verD] = peso[j];
					            verD++;
				            }
				            else
                                if (j < numver)
						            D[j] = -1000;
			            }
		            }
		        }
	        }
	        verD = 0;
	        while (verD <= numver - 2)
	        {
		        for (i = 0; i < numver; i++)
			        if(D[i] != -1000 && D[i] <= pesmen && D[i] != -2)
			        {
				        pesmen=D[i];
				        banfin=i;
			        }
		        if (banfin > -1)
		        {
			        final[banfin, 0] = pesmen;
			        final[banfin, 1] = P[banfin];
			        D[banfin] = -2;
			        P[banfin] = -2;
			        verini = C[banfin];
			        if (dirigido)
			        {
			            for (int j = 0; j < numaris; j++)
			            {
				            if (ar1[j] == verini && final[j - 1, 0] == -1)
				            {
                                for (k = 0; k < numver - 2; k++)
                                    if (C[k] == ar2[j])
                                        break;
					            D[k] = peso[j] + pesmen;
					            P[k] = verini;
				            }
			            }
			        }
			        else
			        {
			            for (int j = 0; j < numaris; j++)
			            {
				            if (ar1[j] == verini && final[j - 1, 0] == -1)
				            {
					            for (k = 0; k < numver - 2; k++)
						            if (C[k] == ar2[j])
							            break;
					            D[k] = peso[j] + pesmen;
					            P[k] = verini;
				            }
				            else
				            {
					            if (ar2[j] == verini && final[j-1, 0] == -1)
					            {
						            for (k = 0; k < numver - 2; k++)
							            if (C[k] == ar2[j])
								            break;
						            if (final[k, 0]==-1)
						            {
							            D[k] = peso[j] + pesmen;
							            P[k] = verini;
						            }
					            }
				            }
			            }
			        }
			        pesmen = 1000;
			        banfin = -1;
			        verD++;
		        }
	        }
	        //imprime
            System.Console.Clear(); 
            System.Console.WriteLine("Camino mas corto desde el vertice " + verts[0]);
	        for (i = 0; i < verD; i++)
                System.Console.WriteLine("a vertice " + verts[i + 1] + ": D= " + final[i, 0] + ", P= " + final[i, 1]);
        }
        internal void kruskal()
        {
            int[] A=new int[1000], pes = new int[1000], comp = new int[2];
            int[,] T = new int[1000,2], Rest = new int[1000,2];
	        int pesmen = 1000, i, cont = 0, contres = numaris;

	        for (i = 0; i < numver; i++)
	        {
		        A[i] = verts[i];
		        T[i, 0] = -1;
		        T[i, 1] = -1;
	        }
	        for (i = 0; i < numaris; i++)
	        {
		        Rest[i, 0] = ar1[i];
		        Rest[i, 1] = ar2[i];
		        pes[i] = peso[i];
	        }
	        while (cont < numver - 1)
	        {
		        pesmen = 1000;
		        for (i = 0; i < contres; i++)
		        {
		             if(pes[i] < pesmen)
		             {
		                pesmen = pes[i];
		                comp[0] = Rest[i, 0];
		                comp[1] = Rest[i, 1];
		             }
		        }
		        T[cont, 0] = comp[0];
		        T[cont, 1] = comp[1];
		        for (i = 0; i < contres; i++)
		        {
			        if (comp[0] == Rest[i, 0] && comp[1] == Rest[i, 1])
			        {
				        Rest[i, 0] = Rest[i + 1, 0];
				        Rest[i, 1] = Rest[i + 1, 1];
				        pes[i] = pes[i + 1];
			        }
		        }
		        contres--;
		        cont++;
	        }
            System.Console.Clear();
	        System.Console.Write("Arbol de recubirmiento minimo obtenido\n\n{");
	        for (i = 0; i < cont; i++)
                System.Console.Write("{" + T[i, 0] + "," + T[i, 1] + "} ");
            System.Console.Write("}");
        }
        internal void prim(int verini)
        {
            int[] B = new int[1000], uv = new int[2], pesos = new int[1000];
            int[,] T = new int [1000,2], ars = new int [1000,2];
	        int enar = 1, i, j, k, peso = 1000, exB =0 , contaris = 0;
            bool b;
	        for (i = 0; i < numaris; i++)
	        {
		        ars[i, 0] = ar1[i];
		        ars[i, 1] = ar2[i];
		        pesos[i] = this.peso[i];
	        }
	        B[0] = verini;
	        while (enar != numver)
	        {
		        for(i = 0; i < enar; i++)
		        {
		            b = false;
		            for (j = 0; j < numaris; j++)
		            {
                        if (dirigido)
                        {
                            if (ars[j, 0] == B[i] && pesos[j] < peso)
                            {
                                peso = pesos[j];
                                uv[0] = B[i];
                                uv[1] = ars[j, 1];
                                b = true;
                            }
                        }
                        else
                        {
                            if (ars[j, 0] == B[i] && pesos[j] < peso)
                            {
                                peso = pesos[j];
                                uv[0] = B[i];
                                uv[1] = ars[j, 1];
                                b = true;
                            }
                            else
                            {
                                if (ars[j, 1] == B[i] && pesos[j] < peso)
                                {
                                    peso = pesos[j];
                                    uv[1] = B[i];
                                    uv[0] = ars[j, 0];
                                    b = true;
                                }
                            }
                        }
		            }
		            if (b)
		            {
			            for (k = 0; k < enar; k++)
			            {
				            if (uv[1] != B[k])
					            if(exB != 2)
						            exB = 0;
				            else
				            {
					            exB = 1;
					            if (!dirigido)
					            {
						            for (i = 0; i < numaris; i++)
						            {
							            if (uv[0] != B[i])
								            exB = 2;
								        else
							                exB = 1;
						            }
					            }
				            }

			            }
			            if (exB == 0 || exB == 2)
			            {
				            T[contaris, 0] = uv[0];
				            T[contaris, 1] = uv[1];
				            for (i = 0; i < numaris; i++)
				            {
					            if (ars[i, 0] == uv[0] && ars[i, 1] == uv[1])
					            {
						            for ( j = i; j < numaris; j++)
						            {
							            ars[j, 0] = ars[j + 1, 0];
							            ars[j, 1] = ars[j + 1, 1];
							            pesos[j] = pesos[j + 1];
						            }
					            }
				            }
				            contaris++;
				            numaris--;
				            if (exB == 0)
					            B[enar] = uv[1];
				            else
					            if(exB == 2)
						            B[enar] = uv[0];
				            enar++;
				            peso = 1000;
			            }
		            }
		        }
	        }
            System.Console.Clear();
            System.Console.Write("Arbol de recubirmiento minimo obtenido\n\n{");
	        for (i = 0; i < contaris; i++)
                System.Console.Write("{" + T[i, 0] + "," + T[i, 1] + "}");
	        System.Console.Write("}");
        }
        internal void floyd(int n)
        {
            int[,] D = new int[4,4];
	        int i, j, di, dj, k;
	        for (i = 0; i < n; i++)
	        {
		        di = verts[i];
		        for (j = 0; j < n; j++)
		        {
			        dj = verts[j];
			        for (k = 0; k < numaris; k++)
			        {
				        if (dirigido)
				        {
					        if (ar1[k] == di && ar2[k] == dj)
					        {
						        D[i, j] = peso[k];
						        break;
					        }
					        else
					        {
						        if(di == dj)
						        {
							        D[i, j] = 0;
							        break;
						        }
						        else
							        D[i, j] = 1000000;
					        }
				        }
				        else
				        {
					        if (ar1[k] == di && ar2[k] == dj || ar2[k] == di && ar1[k] == dj)
					        {
						        D[i, j] = peso[k];
						        D[j, i] = peso[k];
						        break;
					        }
					        else
					        {
						        if (di == dj)
						        {
							        D[i, j] = 0;
							        D[j, i] = 0;
							        break;
						        }
						        else
						        {
							        D[i, j] = 1000000;
							        D[j, i] = 1000000;
						        }
					        }
				        }
			        }
		        }
	        }
	        for (k = 0; k < n; k++)
		        for (i = 0; i < n; i++)
			        for (j = 0; j < n; j++)
				        D[i, j] = D[i, j] < D[i, k] + D[k, j] ? D[i, j] : D[i, k] + D[k, j];
            System.Console.Clear();
	        System.Console.Write("Distancia minima entre cada vertice\n\n");
	        for (i = 0; i < n; i++)
                System.Console.Write("\t" + verts[i]);
            System.Console.Write("\n\n");
	        for (k = 0; k < n; k++)
	        {
                System.Console.Write(verts[k] + "\t");
		        for (i = 0; i < n; i++)
			        System.Console.Write(D[k, i] + "\t");
                System.Console.Write("\n\n");
	        }
        }
    }
}
