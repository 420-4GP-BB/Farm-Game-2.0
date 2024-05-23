using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;

public class StrategieGameOfLife : StrategieGenerationArbres
{
    private const int Generations = 10; //Le nombre de génération

    public override void genererForet(GameObject prefabArbre, Vector3 positionDepart, float superficieX, float superficieZ, float espace)
    {
        int longueur = Mathf.FloorToInt(superficieZ/ espace);
        int largeur = Mathf.FloorToInt(superficieX / espace);
        
        
        Debug.Log("Height : " + longueur + " Width : " + largeur); 
        bool[,] grille2d = new bool[largeur, longueur]; // créer une grille de boolean pour commencer la génération

        // Initialisation aléatoire de la grille
        System.Random rand = new System.Random();
        for (int x = 0; x < largeur; x++)
        {
            for (int z = 0; z < longueur; z++)
            { 
                double random = rand.NextDouble();
                grille2d[x, z] = random < 0.7; // 70% de chance d'être vivant
            }
        }

        afficherGrid(grille2d, longueur, largeur);

         //faire les générations de la foret
            for (int gen = 0; gen < Generations; gen++)
            {
                bool[,] grilleProchainGen = new bool[largeur, longueur];
                for (int x = 0; x < largeur; x++)
                {
                    for (int z = 0; z < longueur; z++)
                    {
                        int voisins = compterVoisins(grille2d, x, z, largeur, longueur);
                        //Debug.Log(neighbors);

                        if (grille2d[x, z])
                        {
                            grilleProchainGen[x, z] = (voisins == 3 || voisins == 4 || voisins == 6 || voisins == 7 || voisins == 8);
                        }
                        else
                        {
                            grilleProchainGen[x, z] = (voisins == 3 || voisins == 6 || voisins == 7 || voisins == 8);
                        }

                        
                    }
                }

                grille2d = grilleProchainGen; // associer la nouvelle grille (de la nouvelle génération) avec l'ancienne grille 
            

        }

        // Instancier les arbres à la fin de la 10eme generation
        for (int x = 0; x < largeur; x++)
            {
                for (int z = 0; z < longueur; z++)
                {
                    if (grille2d[x, z])
                    {
                        Vector3 position = new Vector3(
                            positionDepart.x + (x * superficieX / largeur) + Random.Range(-1.25f, 1.25f) , // un random entre -1.25 et 1.25 pour que les arbres ne soient pas collés 
                            positionDepart.y,
                            positionDepart.z + (z * superficieZ / longueur) + Random.Range(-1.25f, 1.25f)
                        );

                    
                        Instantiate(prefabArbre, position, Quaternion.identity);
                    
                    }
                }
            }

    }


    // Une méthode qui compte les voisins

    // Code inspiré de cette source : https://stackoverflow.com/questions/36779050/check-neighbouring-numbers-in-matrix  
    private int compterVoisins(bool[,] laGrille, int x, int z, int largeur, int longueur)
        {
            int compteur = 0;

            for (int dx = -1; dx <= 1; dx++) // l'emplacement possible d'un voisin (si on prend que x est la position de la case en x), ce sera soit en haut de la case (x+1) ou en bas (x-1)
            {
                for (int dz = -1; dz <= 1; dz++) // meme chose pour z
                {
                    if (dx == 0 && dz == 0) // on ignore si on arrive a la case qu'on veut traiter (pas les voisins)
                    {
                        continue;
                    }

                    int voisinX = x + dx;
                    int voisinZ = z + dz;

                    if (voisinX >= 0 && voisinX < largeur && voisinZ >= 0 && voisinZ < longueur)
                    {
                        if (laGrille[voisinX, voisinZ]) // si le voisin est : true, donc on augmente le compteur qui compte le nombre de voisins
                        {
                            compteur++;
                        }
                    }
                }
            }

            return compteur;
        }


        private void afficherGrid(bool[,] grid, int height, int width)
        {
        for (int x = 0; x < width; x++)
        {
            for (int z = 0; z < height; z++)
            {
                
                Debug.Log("X : " + x + " Z : " + z + " : " + grid[x, z]);
            }
        }
    }
    }

