using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collation : MonoBehaviour, IRamassable
{
    private GameObject joueur;
    [SerializeField] string typeCollation;
    private Arbre arbreParent; // l'arbre auquel la collationAppartient

    void Start()
    {
        joueur = GameObject.Find("Fermier") != null ? GameObject.Find("Fermier") : GameObject.Find("Fermiere");
        if(joueur == null)
        {
            Debug.Log("Joueur non trouvé");
        }
    }

    public void designerArbreParent(Arbre arbre)
    {
        arbreParent = arbre;
    }

    public void Ramasser(Inventaire inventaireJoueur)
    {
        joueur.GetComponent<ComportementJoueur>().MangerCollation(typeCollation);
        arbreParent.collationRamassee();
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
