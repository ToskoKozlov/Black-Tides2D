using UnityEngine;

[System.Serializable]
public class UserModel
{
    public string username = "";
    public string email = "";
    public string password = "";
    public string token = "";

    // create a string JSON from object
    public string SaveToString()
    {
        return JsonUtility.ToJson(this);
    }

    // initialize object from a string JSON
    public static UserModel CreateFromJSON(string jsonString)
    {
        return JsonUtility.FromJson<UserModel>(jsonString);
    }

    // update object from string JSON
    public void Load(string savedData)
    {
        JsonUtility.FromJsonOverwrite(savedData, this);
    }

}