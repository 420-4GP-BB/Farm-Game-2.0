using System;
using UnityEngine;

public class Oeuf : MonoBehaviour, IRamassable
{
    private float tempsEnJeu;
    [SerializeField] private GameObject light;
    private Soleil _soleil;
    private const float JOURS_ECLORE = 3;
    [SerializeField] GameObject prefabPoule;

    void Start()
    {
        _soleil = FindObjectOfType<Soleil>();

        if(_soleil != null)
        {
            Debug.Log("Soleil trouvé!!");
        }
        tempsEnJeu = 0;
    }

    void Update()
    {
        tempsEnJeu += _soleil.DeltaMinutesEcoulees;

        if (tempsEnJeu >= JOURS_ECLORE * ConstantesJeu.MINUTES_PAR_JOUR)
        {
            deciderEclore();
        }
    }

    private void deciderEclore()
    {
        System.Random rand = new System.Random();
        double pourcentage = rand.NextDouble();
        Debug.Log(pourcentage);
        if(pourcentage <= 0.75)
        {
            Destroy(gameObject);
        }
        else
        {
            GameObject poule = Instantiate(prefabPoule, transform.position, Quaternion.identity);
            poule.GetComponent<MouvementPoulet>()._estAlaFerme = true;
            Destroy(gameObject);
        }
    }



    public void Ramasser(Inventaire inventaireJoueur)
    {
        inventaireJoueur.Oeuf++;
        Destroy(gameObject);
    }

    public EtatJoueur EtatAUtiliser(ComportementJoueur Sujet)
    {
        return new EtatRamasserObjet(Sujet, this);
    }

    public bool Permis(ComportementJoueur sujet)
    {
        return true;
    }
}