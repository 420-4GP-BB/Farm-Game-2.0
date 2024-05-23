using UnityEngine;
using LitJson;

// code inspiré du celui fait en classe
public abstract class SauvegardeBase : MonoBehaviour,
    ISaveable,
    ISerializationCallbackReceiver
{
    [HideInInspector][SerializeField] private string _saveID;

    public string SaveID
    {
        get => _saveID;
        set => _saveID = value;
    }

    public void OnAfterDeserialize()
    {
        // Rìen à faire
    }

    public void OnBeforeSerialize()
    {
        if (string.IsNullOrEmpty(_saveID))
        {
            _saveID = System.Guid.NewGuid().ToString();
        }
    }

    
    protected JsonData SavedTransform
    {
        get
        {
            JsonData data = new JsonData();
            data["localPosition"] = JsonUtility.ToJson(transform.localPosition);
            data["localRotation"] = JsonUtility.ToJson(transform.localRotation);
            data["localScale"] = JsonUtility.ToJson(transform.localScale);
            //Debug.Log("SavedTransform : " + data.ToJson());
            return data;
        }
    }

    protected void LoadTransformFromData(JsonData data)
    {
        transform.localPosition = JsonUtility.FromJson<Vector3>(data["localPosition"].ToString());
        transform.localRotation = JsonUtility.FromJson<Quaternion>(data["localRotation"].ToString());
        transform.localScale = JsonUtility.FromJson<Vector3>(data["localScale"].ToString());
        //Debug.Log("LoadTransformFromData : " + data.ToJson());
    }
    
    // Les classes filles peuvent utiliser LoadTransformFromData et SavedTransform
    // au besoin pour implanter ces deux méthodes
    public abstract JsonData SavedData();
    public abstract void LoadFromData(JsonData data);
}