using LitJson;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class SauvegardeJoueur : SauvegardeBase
{
    
    public override JsonData SavedData()
    {
        JsonData data = SavedTransform;
        data["energie"] = GetComponent<EnergieJoueur>().Energie.ToString();


        data["or"] = GetComponent<Inventaire>().Or.ToString();
        data["oeuf"] = GetComponent<Inventaire>().Oeuf.ToString();
        data["graines"] = GetComponent<Inventaire>().Graines.ToString();
        data["choux"] = GetComponent<Inventaire>().Choux.ToString();
        data["buches"] = GetComponent<Inventaire>().Buches.ToString();


        Debug.Log("Data Saved : " + data.ToJson());
        return data;
    }

    public override void LoadFromData(JsonData data)
    {
        GetComponent<CharacterController>().enabled = false;
        GetComponent<NavMeshAgent>().enabled = false;

        float energie;
        

        GetComponent<EnergieJoueur>().Energie = float.Parse(data["energie"].ToString());


        GetComponent<Inventaire>().Or = int.Parse(data["or"].ToString());
        GetComponent<Inventaire>().Oeuf = int.Parse(data["oeuf"].ToString());
        GetComponent<Inventaire>().Graines = int.Parse(data["graines"].ToString());
        GetComponent<Inventaire>().Choux = int.Parse(data["choux"].ToString());
        GetComponent<Inventaire>().Buches = int.Parse(data["buches"].ToString());


        Debug.Log("LoadTransformFromData : " + data.ToJson());
        LoadTransformFromData(data);


        GetComponent<NavMeshAgent>().enabled=true;
        GetComponent<CharacterController>().enabled = true;
    }
}
