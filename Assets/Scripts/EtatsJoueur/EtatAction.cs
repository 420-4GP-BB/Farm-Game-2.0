using UnityEngine;
using UnityEngine.AI;
using System.Collections;

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
        rotationInitiale = Sujet.transform.rotation;
        Vector3 direction = _destination.transform.position - Sujet.transform.position;
        rotationVersObjet = Quaternion.LookRotation(direction);

        Animateur.SetBool("Walking", false);
        ControleurMouvement.enabled = false;
        _navMeshAgent.enabled = false;

        Sujet.StartCoroutine(RotateOverTime(rotationVersObjet, dureeRotation));
    }

    private IEnumerator RotateOverTime(Quaternion targetRotation, float duration)
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
        Sujet.transform.rotation = targetRotation; // S'assure que la rotation atteint exactement la cible
        enRotation = false;
        
    }


    // On doit se rendre au point pour faire l'action
    public override void Handle()
    {
        // Lancer la rotation seulement si elle n'a pas déjà été commencée
        if (!enRotation)
        {
            Animateur.SetBool("Walking", true);
            _navMeshAgent.enabled = true;
            Vector3 pointProche = _destination.GetComponent<Collider>().ClosestPoint(Sujet.transform.position);
            pointDestination = pointProche - (Sujet.transform.position - _destination.transform.position).normalized * 0.3f;
            _navMeshAgent.SetDestination(pointDestination);
        }

        float distance = Vector3.Distance(pointDestination, Sujet.transform.position);
        if (!_navMeshAgent.pathPending && !enRotation && distance <= 0.4f)
        {
            _navMeshAgent.enabled = false;
            pointDestination.y = Sujet.transform.position.y;
            Sujet.transform.position = pointDestination;

            var actionnable = _destination.GetComponent<IActionnable>();
            if (actionnable != null)
            {
                Sujet.ChangerEtat(actionnable.EtatAUtiliser(Sujet));
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
    }

    public override void Exit()
    {
        ControleurMouvement.enabled = true;
        _navMeshAgent.enabled = false;
        Animateur.SetBool("Walking", false);
    }
}


//Animateur.SetBool("Walking", false);
//_navMeshAgent.enabled = false;

//isRotating = true;

//Debug.Log("Rotation finie");


//if (isRotating)
//{
//tempsPris += Time.deltaTime;
//float normalizedTime = tempsPris / dureeRotation;
//if (normalizedTime < 1.0f)
//{
//  isRotating = false;
//}
//else
//{
// Rotation is complete
//Sujet.transform.rotation = rotationVersObjet;
//isRotating = false;

// Now, start walking




/*Animateur.SetBool("Walking", true);
        ControleurMouvement.enabled = false;
        _navMeshAgent.enabled = true;
        Vector3 direction = _destination.transform.position - Sujet.transform.position;
        Sujet.transform.rotation = Quaternion.LookRotation(direction);
        Vector3 pointProche = _destination.GetComponent<Collider>().ClosestPoint(Sujet.transform.position);
        pointDestination = pointProche - direction.normalized * 0.3f;
        _navMeshAgent.SetDestination(pointDestination);*/


// L'enseignant m'a pardonne
//Animateur.SetBool("Walking", true);
//_navMeshAgent.enabled = true;
//Vector3 pointProche = _destination.GetComponent<Collider>().ClosestPoint(Sujet.transform.position);
//pointDestination = pointProche - (Sujet.transform.position - _destination.transform.position).normalized * 0.3f;
//_navMeshAgent.SetDestination(pointDestination);