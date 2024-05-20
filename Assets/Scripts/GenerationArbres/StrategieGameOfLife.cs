using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;

public class StrategieGameOfLife : StrategieGenerationArbres
{
    private const int Generations = 10; // Nombre de générations à simuler

    public override void genererForet(GameObject prefabArbre, Vector3 positionDepart, float superficieX, float superficieZ, float espace)
    {
        int longueur = Mathf.FloorToInt(superficieZ/ espace);
        int largeur = Mathf.FloorToInt(superficieX / espace);
        //int compteurVrai = 0;
        //int compteurPlante = 0;
        
        Debug.Log("Height : " + longueur + " Width : " + largeur); 
        bool[,] grille2d = new bool[largeur, longueur];

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

         //Simulation de l'évolution de la forêt
            for (int gen = 0; gen < Generations; gen++)
            {
                bool[,] grilleProchainGen = new bool[largeur, longueur];
                //countTrue = 0;
                for (int x = 0; x < largeur; x++)
                {
                    for (int z = 0; z < longueur; z++)
                    {
                        int neighbors = compterVoisins(grille2d, x, z, largeur, longueur);
                        //Debug.Log(neighbors);

                        if (grille2d[x, z])
                        {
                            grilleProchainGen[x, z] = (neighbors == 3 || neighbors == 4 || neighbors == 6 || neighbors == 7 || neighbors == 8);
                        }
                        else
                        {
                            grilleProchainGen[x, z] = (neighbors == 3 || neighbors == 6 || neighbors == 7 || neighbors == 8);
                        }

                        //if (newForestGrid[x, z])
                        //{
                        //    countTrue++;
                        //}
                    }
                }

                grille2d = grilleProchainGen;
            //Debug.Log("Generation : " + gen + " Nombre d'arbres générée : " + countTrue);
            //Debug.Log("------------------------------------------------------------------");
            //afficherGrid(forestGrid, height, width);

        }

        // Placement des arbres en fonction de la grille finale
        for (int x = 0; x < largeur; x++)
            {
                for (int z = 0; z < longueur; z++)
                {
                    if (grille2d[x, z])
                    {
                        Vector3 position = new Vector3(
                            positionDepart.x + (x * superficieX / largeur) + Random.Range(-1.25f, 1.25f) ,
                            positionDepart.y,
                            positionDepart.z + (z * superficieZ / longueur) + Random.Range(-1.25f, 1.25f)
                        );

                    
                        Instantiate(prefabArbre, position, Quaternion.identity);
                    //Debug.Log("On plante dans la position : " + x + ", " + z);
                    //countPlanted++;
                    }
                }
            }

        //Debug.Log("True : " + countTrue + " Planted : " + countPlanted);
    }

    
        // Une méthode qui compte les voisins
        private int compterVoisins(bool[,] laGrille, int x, int z, int largeur, int longueur)
        {
            int compteur = 0;

            for (int dx = -1; dx <= 1; dx++)
            {
                for (int dz = -1; dz <= 1; dz++)
                {
                    if (dx == 0 && dz == 0)
                    {
                        continue;
                    }

                    int voisinX = x + dx;
                    int voisinZ = z + dz;

                    if (voisinX >= 0 && voisinX < largeur && voisinZ >= 0 && voisinZ < longueur)
                    {
                        if (laGrille[voisinX, voisinZ])
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

