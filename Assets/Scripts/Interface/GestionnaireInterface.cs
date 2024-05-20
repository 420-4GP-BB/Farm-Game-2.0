using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GestionnaireInterface : MonoBehaviour
{
    [SerializeField] private Button _boutonDemarrer;

    enum Difficulte
    {
        Facile,
        Moyen,
        Difficile
    }

    enum Personnage
    {
        Fermier,
        Fermiere
    }

    enum Generation
    {
        Grille, 
        Random, 
        GameOfLife
    }

    private Difficulte difficulte;
    private Personnage personnage;
    private Generation generation;
    private GameObject lePersonnage;

    [SerializeField] private TMP_InputField nomJoueur;
    [SerializeField] private TMP_Text presentation;

    [SerializeField] private int[] valeursFacile;
    [SerializeField] private int[] valeursMoyen;
    [SerializeField] private int[] valeursDifficile;

    [SerializeField] private TMP_Text[] valeursDepart;
    [SerializeField] private TMP_Dropdown difficulteDropdown;

    [SerializeField] private TMP_Dropdown personnagesDropdown;
    [SerializeField] private GameObject fermier;
    [SerializeField] private GameObject fermiere;

    [SerializeField] private TMP_Dropdown generationDropdown;

    void Start()
    {
        nomJoueur.text = "Mathurin";
        ChangerNomJoueur();

        difficulte = Difficulte.Facile;
        MettreAJour(valeursFacile);

        personnage = Personnage.Fermier;

        generation = Generation.Grille;


    }

    void Update()
    {
        _boutonDemarrer.interactable = nomJoueur.text != string.Empty;
        changerPersonnage();
    }

    public void ChangerDifficulte()
    {
        difficulte = (Difficulte)difficulteDropdown.value;

        switch (difficulte)
        {
            case Difficulte.Facile:
                MettreAJour(valeursFacile);
                break;
            case Difficulte.Moyen:
                MettreAJour(valeursMoyen);
                break;
            case Difficulte.Difficile:
                MettreAJour(valeursDifficile);
                break;
        }
    }


    public void DemarrerPartie()
    {
        int[] valeursActuelles = null;
        switch (difficulte)
        {
            case Difficulte.Facile:
                valeursActuelles = valeursFacile;
                break;
            case Difficulte.Moyen:
                valeursActuelles = valeursMoyen;
                break;
            case Difficulte.Difficile:
                valeursActuelles = valeursDifficile;
                break;
        }

        //generation = (Generation)generationDropdown.value;
        //switch (generation)
        //{
        //    case Generation.Grille:

        //        break;
        //    case Generation.Random:
        //        break;
        //    case Generation.GameOfLife:
        //        break;
        //}

        ParametresParties.Instance.NomJoueur = nomJoueur.text;
        ParametresParties.Instance.OrDepart = valeursActuelles[0];
        ParametresParties.Instance.OeufsDepart = valeursActuelles[1];
        ParametresParties.Instance.SemencesDepart = valeursActuelles[2];
        ParametresParties.Instance.TempsCroissance = valeursActuelles[3];
        ParametresParties.Instance.DelaiCueillete = valeursActuelles[4];

        ParametresParties.Instance.personnageSelec = personnagesDropdown.value;
        ParametresParties.Instance.personnageSelec = personnagesDropdown.value;

        ParametresParties.Instance.generationSelec = generationDropdown.value;





        if (nomJoueur.text != string.Empty)
        {
            UnityEngine.SceneManagement.SceneManager.LoadScene("Ferme");
        }
    }

    public void QuitterJeu()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }

    private void MettreAJour(int[] valeurs)
    {
        for (int i = 0; i < valeursDepart.Length; i++)
        {
            valeursDepart[i].text = valeurs[i].ToString();
        }
    }

    public void ChangerNomJoueur()
    {
        presentation.text = $"\u266A \u266B Dans la ferme \u00e0  {nomJoueur.text} \u266B \u266A";
    }

    private void changerPersonnage()
    {
        personnage = (Personnage)personnagesDropdown.value;
        switch (personnage)
        {
            case Personnage.Fermier:
                lePersonnage = fermier;
                fermier.SetActive(true);
                fermiere.SetActive(false);
                break;
            case Personnage.Fermiere:
                lePersonnage = fermiere;
                fermier.SetActive(false);
                fermiere.SetActive(true);
                break;

        }
    }
}