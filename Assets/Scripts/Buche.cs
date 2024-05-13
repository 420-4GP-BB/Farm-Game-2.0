using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Buche : MonoBehaviour, IRamassable
{
    public void Ramasser(Inventaire inventaireJoueur)
    {
        Debug.Log("Ramassage de buche reussi");
        inventaireJoueur.Buches++;
        Debug.Log(inventaireJoueur.Buches);
        Destroy(gameObject);
    }

    public EtatJoueur EtatAUtiliser(ComportementJoueur Sujet)
    {
        Debug.Log("Entree dans etat a utiliser");
        return new EtatRamasserObjet(Sujet, this);
    }

    public bool Permis(ComportementJoueur sujet)
    {
        return true;
    }
}
