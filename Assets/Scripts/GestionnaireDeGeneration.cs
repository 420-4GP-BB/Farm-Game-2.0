using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Le gestionnaire de generation
public class GestionnaireDeGeneration : MonoBehaviour
{
    [SerializeField] GameObject[] zoneArbres;
    private StrategieGenerationArbres strategie;
    [SerializeField] GameObject prefabArbre;

    private void Start()
    {
        
        switch (ParametresParties.Instance.generationSelec)
        {
            case 0:
                strategie = new StrategieGrille();
                break;
            case 1:
                strategie = new StrategieRandom();
                break; 
            case 2:
                strategie = new StrategieGameOfLife();
                break;
            default:
                break;
        }
        

        Debug.Log("Foret générée");

        foreach (GameObject zone in zoneArbres) 
        {
            Vector3 positionDepart = zone.GetComponent<BoxCollider>().bounds.min;
            strategie.genererForet(prefabArbre, positionDepart, zone.GetComponent<ZoneArbre>().NombreArbresX, zone.GetComponent<ZoneArbre>().NombreArbresZ, zone.GetComponent<ZoneArbre>().EspaceEntreArbres);

        }

    }
}
