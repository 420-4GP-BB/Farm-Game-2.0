using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arbre : MonoBehaviour, IBattable
{
    [SerializeField] public bool estCollation;
    [SerializeField] GameObject buchePrefab;

    public void Abbatre()
    {
        StartCoroutine(GererChuteEtDisparition());
    }

    private IEnumerator GererChuteEtDisparition()
    {
        yield return new WaitForSeconds(1.0f);
        Instantiate(buchePrefab, transform.position, Quaternion.identity);

        float dureeDescente = 0.5f;
        float tempsEcoule = 0.0f;
        Vector3 positionDebut = transform.position;
        Vector3 positionFin = positionDebut - new Vector3(0, 4, 0);

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
