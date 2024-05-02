using UnityEngine;
using UnityEngine.AI;

public class MouvementPoulet : MonoBehaviour
{
    private UnityEngine.GameObject _zoneRelachement;
    private float _angleDerriere;  
    private UnityEngine.GameObject joueur;
    private bool _suivreJoueur;
    private bool _estAlaFerme;
    [SerializeField] GameObject zoneEntree;

    private NavMeshAgent _agent;
    private Animator _animator;

    private GameObject[] _pointsDeDeplacement;

    void Start()
    {
        Debug.Log("Poule crée");
        _zoneRelachement = UnityEngine.GameObject.Find("NavMeshObstacle");
        joueur = GameObject.Find("GameManager").GetComponent<GameManager>().leJoueur;
        
        _angleDerriere = Random.Range(-60.0f, 60.0f);

        _animator = GetComponent<Animator>();
        _agent = GetComponent<NavMeshAgent>();
        _pointsDeDeplacement = GameObject.FindGameObjectsWithTag("PointsPoulet");
        _animator.SetBool("Walk", true);
        _agent.stoppingDistance = 3.5f;
        Initialiser();
    }

    void Initialiser()
    {
        _agent.enabled = false;
        _suivreJoueur = true;
        _estAlaFerme = false;
        Vector3 direction = Quaternion.Euler(0, _angleDerriere, 0) * joueur.transform.forward;
        Vector3 point = joueur.transform.position + direction.normalized * 0.5f;
        transform.position = point;
        _agent.enabled = true;
        _agent.speed = Vector3.Distance(transform.position, joueur.transform.position) * 5;

        Debug.Log("Suivre joueur : " + _suivreJoueur + ", Est la ferme : " + _estAlaFerme);
    }

    void ChoisirDestinationAleatoire()
    {
        GameObject point = _pointsDeDeplacement[Random.Range(0, _pointsDeDeplacement.Length)];
        _agent.SetDestination(point.transform.position);
        _agent.stoppingDistance = 0.5f;
    }

    void Update()
    {
         if (_suivreJoueur && !_estAlaFerme)
        {

            //_agent.SetDestination(joueur.transform.position);
            //Debug.Log("Update -> Suivre joueur : " + _suivreJoueur + ", Est la ferme : " + _estAlaFerme);


            if (Vector3.Distance(transform.position, joueur.transform.position) > 2f)
            {
                _agent.SetDestination(joueur.transform.position);
            }
            else
            {
                _agent.SetDestination(transform.position);
            }
            
        }

        if (_estAlaFerme && !_agent.pathPending && _agent.remainingDistance < _agent.stoppingDistance)
        {
            ChoisirDestinationAleatoire();
        }
        
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject == _zoneRelachement)
        {
            Debug.Log("On entre dans le trigger");
            _agent.speed = 1;
            _suivreJoueur = false;
            _estAlaFerme = true;
            ChoisirDestinationAleatoire();
            gameObject.GetComponent<PondreOeufs>().enabled = true;
        }
    }
}