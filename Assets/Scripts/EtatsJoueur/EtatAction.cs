using UnityEngine;
using UnityEngine.AI;
using System.Collections;
using Unity.VisualScripting;

public class EtatAction : EtatJoueur
{
    public override bool EstActif => true;
    public override bool DansDialogue => false;
    public override float EnergieDepensee => ConstantesJeu.COUT_MARCHER;


    private GameObject _destination;
    private NavMeshAgent _navMeshAgent;

    private Vector3 pointDestination;

    private Quaternion rotationInitiale;
    private Quaternion rotationVersObjet;
    private float dureeRotation = 0.25f;
    private bool enRotation = true;

    public EtatAction(ComportementJoueur sujet, GameObject destination) : base(sujet)
    {
        _destination = destination;
        _navMeshAgent = Sujet.GetComponent<NavMeshAgent>();
    }

    public override void Enter()
    {
        
        Arbre arbre = _destination.GetComponent<Arbre>();
        if (arbre != null && arbre.estCollation)
        {
            Debug.Log("Cet arbre est destiné à la collation, ne pas s'approcher.");
            Sujet.ChangerEtat(Sujet.EtatNormal);
            //return;
        }
        else
        {
            rotationInitiale = Sujet.transform.rotation;
            Vector3 direction = _destination.transform.position - Sujet.transform.position;
            rotationVersObjet = Quaternion.LookRotation(direction);

            Animateur.SetBool("Walking", false);
            ControleurMouvement.enabled = false;
            _navMeshAgent.enabled = false;

            Sujet.StartCoroutine(faireRotation(rotationVersObjet, dureeRotation));
        }

        
    }

    private IEnumerator faireRotation(Quaternion targetRotation, float duration)
    {
        enRotation = true;
        float timeElapsed = 0f;
        while (timeElapsed < duration)
        {
            enRotation = true;
            Sujet.transform.rotation = Quaternion.Slerp(rotationInitiale, targetRotation, timeElapsed / duration);
            timeElapsed += Time.deltaTime;
            yield return null;
        }
        Sujet.transform.rotation = targetRotation;
        enRotation = false;
        
    }


    public override void Handle()
    {
        float distanceMin = _destination.GetComponent<Arbre>() != null || _destination.GetComponent<Buche>() != null ? 1.2f : 0.6f;

        if (!enRotation)
        {
            Animateur.SetBool("Walking", true);
            _navMeshAgent.enabled = true;
            Vector3 pointProche = _destination.GetComponent<Collider>().ClosestPoint(Sujet.transform.position);
            pointDestination = pointProche - (Sujet.transform.position - _destination.transform.position).normalized * 0.3f;
            _navMeshAgent.SetDestination(pointDestination);
        }

        float distance = Vector3.Distance(pointDestination, Sujet.transform.position);

        if (!_navMeshAgent.pathPending && !enRotation && distance <= distanceMin)
        {
            _navMeshAgent.enabled = false;
            pointDestination.y = Sujet.transform.position.y;
            Sujet.transform.position = _destination.GetComponent<Arbre>() != null ? pointDestination : pointDestination;
            Debug.Log("Arrivé à la bûche et prêt à ramasser.");

            var actionnable = _destination.GetComponent<IActionnable>();
            if (actionnable != null && actionnable.Permis(Sujet))
            {
                Debug.Log("Changement d'état pour ramasser la bûche.");
                Sujet.ChangerEtat(actionnable.EtatAUtiliser(Sujet));
            }
            
        }



    }

    public override void Exit()
    {
        ControleurMouvement.enabled = true;
        _navMeshAgent.enabled = false;
        Animateur.SetBool("Walking", false);
    }
}

//Vector3 direction = _destination.transform.position - Sujet.transform.position;
//Sujet.transform.rotation = Quaternion.LookRotation(direction);
//Vector3 pointProche = _destination.GetComponent<Collider>().ClosestPoint(Sujet.transform.position);
//Vector3 pointDestination = pointProche - direction.normalized * 0.1f;

//if (Vector3.Distance(Sujet.transform.position, pointDestination) > 0.1f)
//{
//    float distanceAvant = Vector3.Distance(Sujet.transform.position, pointDestination);
//    ControleurMouvement.SimpleMove(Sujet.transform.forward * (Sujet.VitesseDeplacement));

//    Il faudrait peut - ?tre essayer avec un NavMesh ici
//    Sujet.transform.Translate(Sujet.transform.forward * (Sujet.VitesseDeplacement * Time.deltaTime), Space.World);
//    Sujet.transform.rotation = Quaternion.Euler(0, Sujet.transform.rotation.eulerAngles.y, 0);
//    float distanceApres = Vector3.Distance(Sujet.transform.position, pointDestination);

//}
//else
//{
//    ControleurMouvement.enabled = false;
//    Sujet.transform.position = pointDestination;

//    Chou chou = _destination.GetComponent<Chou>();
//    if (chou != null)
//    {
//        Sujet.ChangerEtat(new PlanterChou(Sujet, chou));
//    }

//    Oeuf oeuf = _destination.GetComponent<Oeuf>();
//    if (oeuf != null)
//    {
//        Sujet.ChangerEtat(new RamasserOeuf(Sujet, oeuf));
//    }
//    ControleurMouvement.enabled = true;
//}
