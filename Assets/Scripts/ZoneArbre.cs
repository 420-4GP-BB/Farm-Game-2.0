using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZoneArbre : MonoBehaviour
{
    [SerializeField] private int _nombreArbresX;
    [SerializeField] private int _nombreArbresZ;
    [SerializeField] private float _espaceEntreArbres;

    public int NombreArbresX
    {
        get { return _nombreArbresX; }
        private set { }
    }

    public int NombreArbresZ
    {
        get { return _nombreArbresZ; }
        private set { }
    }

    public float EspaceEntreArbres
    {
        get { return _espaceEntreArbres; }
        private set { }
    }
}
