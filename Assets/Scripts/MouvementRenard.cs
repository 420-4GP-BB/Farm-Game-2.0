using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class MouvementRenard : MonoBehaviour
{
    private GameObject[] pointsDeDeplacement;
    private NavMeshAgent agent;
    private GameObject cible;
    private Soleil soleil;
    private float distancePoule = 5.0f;
    private bool enChasse;

    private void Start()
    {
        Debug.Log("Renard crée");

        agent = GetComponent<NavMeshAgent>();
        soleil = GameObject.FindObjectOfType<Soleil>();
        pointsDeDeplacement = GameObject.FindGameObjectsWithTag("PointRenard");
        int pointDepart = Random.Range(0, pointsDeDeplacement.Length);
        transform.position = pointsDeDeplacement[pointDepart].transform.position;
        Debug.Log("Position du renard : " + transform.position);
        enChasse = false;
        choisirDestinationAleatoire();

    }

    void Update()
    {
        if (soleil.EstRenardActif)
        {
            rechercherPoules();
            if (!enChasse)
            {
                if(!agent.pathPending && agent.remainingDistance < agent.stoppingDistance)
                {
                    choisirDestinationAleatoire();
                }
                
            }
            else
            {
                poursuivrePoule();
            }
        }
        else
        {
            Debug.Log("Renard détruit");
            soleil.nbRenards--;
            Destroy(gameObject);
        }
    }


    private void rechercherPoules()
    {
        GameObject[] poules = GameObject.FindGameObjectsWithTag("Poule");
        GameObject pouleLaPlusProche = null;
        float distanceMin = Mathf.Infinity;

        foreach (var poule in poules)
        {
            float distance = Vector3.Distance(transform.position, poule.transform.position);
            if (distance <= distancePoule && distance < distanceMin)
            {
                pouleLaPlusProche = poule;
                distanceMin = distance;
            }
        }

        if (pouleLaPlusProche != null)
        {
            cible = pouleLaPlusProche;
            enChasse = true;
        }
    }


    private void poursuivrePoule()
    {
        if (cible != null)
        {
            agent.SetDestination(cible.transform.position);
            if (Vector3.Distance(transform.position, cible.transform.position) <= 1f)
            {
                Debug.Log("Le renard a mangé la poule");
                Destroy(cible);
                cible = null;
                enChasse = false;

                rechercherPoules();
            }
        }
        else
        {
            enChasse = false;
            choisirDestinationAleatoire();
        }
    }

    private void choisirDestinationAleatoire()
    {
        GameObject prochain = pointsDeDeplacement[Random.Range(0, pointsDeDeplacement.Length)];
        agent.SetDestination(prochain.transform.position);
    }

}
