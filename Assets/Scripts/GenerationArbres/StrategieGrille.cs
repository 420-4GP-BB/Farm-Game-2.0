using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// la classe pour la strategie de grille
public class StrategieGrille : StrategieGenerationArbres
{

    public override void genererForet(GameObject prefabArbre, Vector3 positionDepart, float superficieX, float superficieZ, float espace)
    {
        // pour chaque emplacement trouvé, on instancie un arbre
        for (float x = positionDepart.x; x < positionDepart.x + superficieX; x+= espace)
        {
            for (float z = positionDepart.z; z < positionDepart.z + superficieZ; z+= espace)
            {
                Vector3 position = new Vector3(x, positionDepart.y, z);
                GameObject arbre = Instantiate(prefabArbre);
                arbre.transform.position = position;
            }
        }
    }
}
