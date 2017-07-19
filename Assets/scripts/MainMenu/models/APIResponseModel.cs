using UnityEngine;

[System.Serializable]
public class APIResponseModel
{
    public int status;
    public string description;
    public string token;

    // create a string JSON from object
    public string SaveToString()
    {
        return JsonUtility.ToJson(this);
    }

    // initialize object from a string JSON
    public static APIResponseModel CreateFromJSON(string jsonString)
    {
        return JsonUtility.FromJson<APIResponseModel>(jsonString);
    }

    // update object from string JSON
    public void Load(string savedData)
    {
        JsonUtility.FromJsonOverwrite(savedData, this);
    }
}