using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arbre : MonoBehaviour, IBattable
{
    [SerializeField] public bool estCollation; // un booleen qui verifie si l'arbre est un arbre de collation (à glisser dans l'interface de unity)
    [SerializeField] GameObject buchePrefab;
    [SerializeField] GameObject[] lesCollationPrefab;
    private bool uneCollationAuPied;
    private int unRandom;
    private GameObject uneCollation;

    void Start()
    {
        uneCollationAuPied = false; // au debut, il n'ya pas de collation 
        if (estCollation)
        {
            StartCoroutine(faireCollation());
        }

    }

   
    // une coroutine qui fait descendre une collation chaque 30s
    IEnumerator faireCollation()
    {
            yield return new WaitForSeconds(30.0f);
            if (!uneCollationAuPied) // s'il n'ya pas de collation au pied, on fait descendre une collation au hasard entre soda, hamburger et gateau
            {
                unRandom = Random.Range(0, lesCollationPrefab.Length);
                uneCollation = Instantiate(lesCollationPrefab[unRandom]);
                uneCollation.transform.position = new Vector3(transform.position.x +1, transform.position.y + 3, transform.position.z);
                uneCollation.AddComponent<Rigidbody>();
                uneCollation.GetComponent<Collation>().designerArbreParent(this);
                uneCollationAuPied = true;
            }
    }

    // une methode qui commence une coroutine pour faire tomber une nouvelle collation lorsqu'une collation est ramassé
    public void collationRamassee()
    {
        uneCollationAuPied = false;
        StartCoroutine(faireCollation());
    }
   
    // une methode pour abattre les arbres
    public void Abbatre()
    {
        StartCoroutine(GererChuteEtDisparition());
    }

    // une coroutine pour gerer la chute des arbres
    private IEnumerator GererChuteEtDisparition()
    {
        yield return new WaitForSeconds(1.0f);
        Instantiate(buchePrefab, transform.position, Quaternion.identity);

        float dureeDescente = 0.5f;
        float tempsEcoule = 0.0f;
        Vector3 positionDebut = transform.position;
        Vector3 positionFin = positionDebut - new Vector3(0, 4, 0);

        // faire une boucle pour faire tomber l'arbre selon la duree de la descente
        while (tempsEcoule < dureeDescente)
        {
            transform.position = Vector3.Lerp(positionDebut, positionFin, tempsEcoule / dureeDescente);
            tempsEcoule += Time.deltaTime;
            yield return null;
        }

        Destroy(gameObject);
    }

    
public EtatJoueur EtatAUtiliser(ComportementJoueur Sujet)
    {
        return new EtatAbattre(Sujet, this, gameObject);
    }

    public bool Permis(ComportementJoueur sujet)
    {
        return true;
    }
}


