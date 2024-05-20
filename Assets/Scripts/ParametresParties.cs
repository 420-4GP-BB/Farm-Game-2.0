using Unity.VisualScripting;
using UnityEngine;

public class ParametresParties
{

    public string NomJoueur { get; set; } = "Mathurin";
    public int OrDepart { get; set; } = 200;
    public int OeufsDepart { get; set; } = 5;
    public int SemencesDepart { get; set; } = 5;

    ///// <summary>
    ///// Nombre de jours nécessaires à un chou pour être prêts
    ///// 0 = le chou est déjà prêt dès qu'on le plante
    ///// </summary>
    public int TempsCroissance { get; set; } = 3;

    ///// <summary>
    ///// Nombre de jours pendant lesquels on peut cueillir un chou prêt
    ///// Plus cette valeur est petite, plus on doit se dépêcher avant qu'ils ne soient plus bons
    ///// </summary>
    public int DelaiCueillete { get; set; } = 5;

    public int personnageSelec { get; set; }

    public int generationSelec { get; set; }

    private static ParametresParties _instance;

    public static ParametresParties Instance {get;set;} = new ParametresParties();

    private ParametresParties()
    {
    }

    /*
    void Awake()
    {
        if (_instance != null && _instance != this)
        {
            _instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            Destroy(this.gameObject);
        }
    }
    */
}