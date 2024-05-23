using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// une classe abstraite pour le patron strategie avec une methode genererForet qui génère les arbres dans la foret
public abstract class StrategieGenerationArbres : MonoBehaviour
{
    public abstract void genererForet(GameObject prefabArbre, Vector3 positionDepart, float superficieX, float superficieZ, float espace);
}
