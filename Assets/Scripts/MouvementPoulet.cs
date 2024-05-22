using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

public class MouvementPoulet : MonoBehaviour
{
    private UnityEngine.GameObject _zoneRelachement;
    private float _angleDerriere;  
    private UnityEngine.GameObject joueur;
    private bool _suivreJoueur;
    public bool _estAlaFerme;
    private Soleil soleil;
    private NavMeshAgent _agent;
    private Animator _animator;

    private GameObject[] _pointsDeDeplacement;
    private GameObject[] _pointsJour;
    private GameObject[] _pointsNuit;
    //[SerializeField] GameObject _nouveauPoint;
    private GameObject _nouveauPoint;
    //[SerializeField] GameObject prefabRenard;
    

    void Start()
    {
        Debug.Log("Poule crée");
        _zoneRelachement = UnityEngine.GameObject.Find("NavMeshObstacle");
        joueur = GameObject.Find("GameManager").GetComponent<GameManager>().leJoueur;
        
        _angleDerriere = Random.Range(-60.0f, 60.0f);

        _animator = GetComponent<Animator>();
        _agent = GetComponent<NavMeshAgent>();
        _pointsDeDeplacement = GameObject.FindGameObjectsWithTag("PointsPoulet");
        _pointsJour = _pointsDeDeplacement;
        _pointsNuit = new GameObject[_pointsJour.Length + 1];
        for (int i = 0; i < _pointsJour.Length; i++)
        {
            _pointsNuit[i] = _pointsJour[i];
        }

        _nouveauPoint = GameObject.Find("DevantFerme");
        _pointsNuit[_pointsJour.Length] = _nouveauPoint;

        _animator.SetBool("Walk", true);
        _agent.stoppingDistance = 3.5f;
        if (!_estAlaFerme)
        {
            Initialiser();
        }

       
        

        soleil = GameObject.FindObjectOfType<Soleil>();
        if(soleil != null)
        {
            Debug.Log("SOleil trouvé");
        }

       
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
            if (Vector3.Distance(transform.position, joueur.transform.position) > 2f)
            {
                _agent.SetDestination(joueur.transform.position);
            }
            else
            {
                _agent.SetDestination(transform.position);
            }

        }

        Debug.Log(_pointsDeDeplacement.Length);

        if (soleil.EstRenardActif)
        {
            Debug.Log("Creation du point");
            
            _pointsDeDeplacement = _pointsNuit;
        }
        else
        {
            Debug.Log("Point enleve");
            _pointsDeDeplacement = _pointsJour;
        }
        
       
        if (_estAlaFerme && !_agent.pathPending && _agent.remainingDistance < _agent.stoppingDistance)
        {
            _agent.speed = 1;
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