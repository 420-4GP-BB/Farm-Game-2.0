using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class StrategieGenerationArbres : MonoBehaviour
{
    public abstract void genererForet(GameObject prefabArbre, Vector3 positionDepart, float superficieX, float superficieZ, float espace);
}
