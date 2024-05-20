using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StrategieRandom : StrategieGenerationArbres
{
    public override void genererForet(GameObject prefabArbre, Vector3 positionDepart, float superficieX, float superficieZ, float espace)
    {
        List<Rect> rectArbres = new List<Rect>();
        for (float x = positionDepart.x; x < positionDepart.x + superficieX; x += espace)
        {
            for (float z = positionDepart.z; z < positionDepart.z + superficieZ; z += espace)
            {
                System.Random rand = new System.Random();
                double pourcentage = rand.NextDouble();
                if (pourcentage > 0.5)
                {
                    Vector3 arbre1 = new Vector3(x, positionDepart.y, z);
                    
                    Rect arbre2 = new Rect(arbre1.x - espace / 2, arbre1.z - espace / 2, espace, espace);

                    bool positionValide = true;
                    foreach (Rect rect in rectArbres)
                    {
                        if (rect.Overlaps(arbre2))
                        {
                            positionValide = false;
                            break;
                        }
                    }
                    if (positionValide)
                    {
                        rectArbres.Add(arbre2);
                        GameObject arbre = Instantiate(prefabArbre);
                        arbre.transform.position = arbre1;
                    }
                }
            }
        }
    }
}
