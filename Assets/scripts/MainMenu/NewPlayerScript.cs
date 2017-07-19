using UnityEngine.UI;
using UnityEngine;
using System.Security.Cryptography;
using System;
using System.IO;


public class NewPlayerScript : MonoBehaviour {
    public GameObject username;
    public GameObject email;
    public GameObject password;
    public GameObject confpassword;
    private string Username;
    private string Email;
    private string Password;
    private string ConfPassword;
    public GameObject modalPanel;
    private string endpoint = "/newuser";

    // Use this for initialization
    void Start()
    {
        modalPanel.SetActive(false);
    }

    // Update is called once per frame
    void Update () {

        //navigate through form using tab key
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            if (username.GetComponent<InputField>().isFocused)
            {
                email.GetComponent<InputField>().Select();
            }
            if (email.GetComponent<InputField>().isFocused)
            {
                password.GetComponent<InputField>().Select();
            }
            if (password.GetComponent<InputField>().isFocused)
            {
                confpassword.GetComponent<InputField>().Select();
            }

        }

        // commit form using return key
        if (Input.GetKeyDown(KeyCode.Return) && confpassword.GetComponent<InputField>().isFocused)
        {
            NewPlayerButton();
        }

        Username = username.GetComponent<InputField>().text;
        Email = email.GetComponent<InputField>().text;
        Password = password.GetComponent<InputField>().text;
        ConfPassword = confpassword.GetComponent<InputField>().text;
    }

    public void NewPlayerButton()
    {
        if (Password != "" && Username != "" && Email != "" && ConfPassword != "")
        {
            //check email is a valid email
            if (!Utils.CheckEmail(Email))
            {
                Debug.Log("invalid email");
            }
            //check password == confpassword
            else if (!(Password == ConfPassword))
            {
                Debug.Log("please confirm password");
            }
            else
            {
                // set loading screen
                modalPanel.SetActive(true);

                string salt = BCrypt.Net.BCrypt.GenerateSalt();
                string hashedPassword = BCrypt.Net.BCrypt.HashPassword(Password, salt);

                // save salt to local file
                SaveSalt(salt, Username+".info");
                SaveSalt(salt, Email+".info");

                //send request to API (TODO make request asyncronous)
                CreatePlayer(Username, hashedPassword, Email, salt);
            }
        }
        else
        {
            Debug.Log("All fields must be filled");
        }
    }

    public static void SaveSalt(string salt, string fileName)
    {
        var sr = File.CreateText(fileName);
        sr.WriteLine(salt);
        sr.Close();
    }

    // generate a random string
    private static string CreateSalt(int size)
    {
        //Generate a cryptographic random number.
        RNGCryptoServiceProvider rng = new RNGCryptoServiceProvider();
        byte[] buff = new byte[size];
        rng.GetBytes(buff);

        // Return a Base64 string representation of the random number.
        return Convert.ToBase64String(buff);
    }

    private APIResponseModel CreatePlayer(string username, string hashedPassword, string email, string salt)
    {
        APIResponseModel response = new APIResponseModel();
        UserModel user = new UserModel()
        {
            username = username,
            email = email,
            password = hashedPassword,
            salt = salt
        };

        string json = user.SaveToString();

        //make the request to the remote API
        response = APIManager.MakeRequest(endpoint, "POST", json);

        if (response.status == 201)
        {
            user.token = response.token;
            Scenes.LoadGameView();
            Debug.Log("New user created");
        }
        else
        {
            Debug.Log(response.description);
            Debug.Log("Status code: " + response.status);
        }

        modalPanel.SetActive(false);

        return response;
    }
}
