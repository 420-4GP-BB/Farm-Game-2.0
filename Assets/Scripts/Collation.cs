using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collation : MonoBehaviour, IRamassable
{
    // LA classe pour collation
    private GameObject joueur;
    [SerializeField] string typeCollation;
    private Arbre arbreParent; // l'arbre auquel la collationAppartient

    void Start()
    {
        // on trouve le joueur actif
        joueur = GameObject.Find("Fermier") != null ? GameObject.Find("Fermier") : GameObject.Find("Fermiere");
        if(joueur == null)
        {
            Debug.Log("Joueur non trouvé");
        }
    }

    // une methode pour designer l'Arbre parent
    public void designerArbreParent(Arbre arbre)
    {
        arbreParent = arbre;
    }

    // une methode pour ramasser la collation et la manger
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
