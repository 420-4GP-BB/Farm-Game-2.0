using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// La classe pour la strategie random
public class StrategieRandom : StrategieGenerationArbres
{
    public override void genererForet(GameObject prefabArbre, Vector3 positionDepart, float superficieX, float superficieZ, float espace)
    {
        // Utiliser le Rect pour les arbes
        List<Rect> rectArbres = new List<Rect>();
        for (float x = positionDepart.x; x < positionDepart.x + superficieX; x += espace)
        {
            for (float z = positionDepart.z; z < positionDepart.z + superficieZ; z += espace)
            {
                System.Random rand = new System.Random();
                double pourcentage = rand.NextDouble(); // 50% pour instancier, 50% pour ne pas instancier
                if (pourcentage > 0.5)
                {
                    Vector3 arbre1 = new Vector3(x, positionDepart.y, z); // initialiser la position de l'Arbre
                    
                    Rect arbre2 = new Rect(arbre1.x - espace / 2, arbre1.z - espace / 2, espace, espace); // initialiser l'Arbre qui peut etre collé avec arbre1

                    bool positionValide = true;
                    foreach (Rect rect in rectArbres)
                    {

                        if (rect.Overlaps(arbre2)) // s'il touche, on peut pas instancier
                        {
                            positionValide = false;
                            break;
                        }
                    }
                    if (positionValide) // sinon
                    {
                        rectArbres.Add(arbre2);
                        GameObject arbre = Instantiate(prefabArbre); // on peut instancier
                        arbre.transform.position = arbre1;
                    }
                }
            }
        }
    }
}
