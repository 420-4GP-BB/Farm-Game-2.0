using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class EtatAbattre : EtatJoueur
{
    public override bool EstActif => true;
    public override bool DansDialogue => false;
    public override float EnergieDepensee => ConstantesJeu.COUT_ABBATRE;

    private IBattable _abattable;
    private GameObject _arbre;
    private float _tempsAbattage = 0.0f;

    public EtatAbattre(ComportementJoueur sujet, IBattable abattable, GameObject arbre) : base(sujet)
    {
        _abattable = abattable;
        _arbre = arbre;
    }

    public override void Enter()
    {
        Animateur.SetBool("Abattre", true);
    }

    public override void Handle()
    {
        _tempsAbattage += Time.deltaTime;

        if (_tempsAbattage < 2.0f) 
        {
            float vitesseTombee = 90.0f / 2;
            _arbre.transform.Rotate(Sujet.transform.right, Time.deltaTime * vitesseTombee, Space.World);
        }

        if (_tempsAbattage >= 2.0f)
        {
            _abattable.Abbatre();
            Sujet.ChangerEtat(Sujet.EtatNormal);
        }
    }

    public override void Exit()
    {
        Animateur.SetBool("Abattre", false);
    }
}
