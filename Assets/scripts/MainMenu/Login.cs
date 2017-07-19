using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using UnityEngine;
using UnityEngine.UI;

public class Login : MonoBehaviour {

    public GameObject userEmail;
    public GameObject password;
    private string insertedUserEmail;
    private string insertedPassword;
    public GameObject modalPanel;
    private string endpoint = "/login";

    // Use this for initialization
    void Start() {
        modalPanel.SetActive(false);
    }

    // Update is called once per frame
    void Update() {
        //navigate through form using tab key
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            if (userEmail.GetComponent<InputField>().isFocused)
            {
                password.GetComponent<InputField>().Select();
            }
            // commit form using return key
            if (Input.GetKeyDown(KeyCode.Return) && password.GetComponent<InputField>().isFocused)
            {
                MakeLogin();
            }
        }
        insertedUserEmail = userEmail.GetComponent<InputField>().text;
        insertedPassword = password.GetComponent<InputField>().text;
    }

    // send a request to API with hashed password and username or email
    public void MakeLogin()
    {
        if (insertedUserEmail != "" && insertedPassword != "")
        {
            
            // set loading screen
            modalPanel.SetActive(true);

             //read salt
            string salt = LoadSalt(insertedUserEmail+".info");

            string hashedPassword = BCrypt.Net.BCrypt.HashPassword(insertedPassword, salt);
            Debug.Log(hashedPassword);

            Debug.Log("Making request...");
            //send request to API (TODO make request asyncronous)
            LoginPlayer(insertedUserEmail, hashedPassword, salt);

        }
        else
        {
            Debug.Log("All fields must be filled");
        }
    }

    // load salt
    public static string LoadSalt(string fileName)
    {
        string salt = "";
        salt = System.IO.File.ReadAllText(fileName);
        return salt;
    }

    private APIResponseModel LoginPlayer(string userEmail, string hashedPassword, string salt)
    {
        APIResponseModel response = new APIResponseModel();

        UserModel user = new UserModel()
        {
            password = hashedPassword,
            salt = salt
        };

        // check if userEmail is an email or the username
        if (Utils.CheckEmail(userEmail))
        {
            user.email = userEmail;
        }
        else
        {
            user.username = userEmail;
        }

        string json = user.SaveToString();
        Debug.Log(json);
        //make the request to the remote API
        response = APIManager.MakeRequest(endpoint, "POST", json);

        if (response.status == 200)
        {
            user.token = response.token;
            Scenes.LoadGameView();
            Debug.Log("Logged in!");
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
