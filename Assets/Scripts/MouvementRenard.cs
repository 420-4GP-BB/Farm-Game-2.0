using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

// La classe pour le mouvement du renard
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
        // initialiser le renard
        agent = GetComponent<NavMeshAgent>();
        soleil = GameObject.FindObjectOfType<Soleil>();
        pointsDeDeplacement = GameObject.FindGameObjectsWithTag("PointRenard");
        int pointDepart = Random.Range(0, pointsDeDeplacement.Length);// une position du depart random
        transform.position = pointsDeDeplacement[pointDepart].transform.position;
        Debug.Log("Position du renard : " + transform.position);
        enChasse = false;
        choisirDestinationAleatoire();// commencer par une destination aleatoire

    }

    void Update()
    {
        if (soleil.EstRenardActif)
        {
            // Pour chaque frame, il doit rechercher les poules, comme ca si il trouve une poule il annule son point et il se dirige vers la poule
            rechercherPoules();
            if (!enChasse)// s'il n'est pas en chasse, il choisit une destination aleatoire
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
    
    // pour la recherche des poules, j'ai fait en sorte qu'il prend la poule la plus proche de lui. 
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

    // Lorsqu'il poursuit la poule, si il est proche d'elle, il la mange et il cherche une autre poule qui est proche pour aussi la manger
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
