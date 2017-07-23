using UnityEngine.UI;
using UnityEngine;

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
            //check strength of password
            if (!StrongPass(Password))
            {
                Debug.Log("Password must have at least 5 characters");
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

                //send request to API (TODO make request asyncronous)
                CreatePlayer(Username, Password, Email);
            }
        }
        else
        {
            Debug.Log("All fields must be filled");
        }
    }

    // send a request to API to create a new player
    private APIResponseModel CreatePlayer(string username, string password, string email)
    {
        APIResponseModel response = new APIResponseModel();
        UserModel user = new UserModel()
        {
            username = username,
            email = email,
            password = password
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

    // check strength of a password
    private bool StrongPass(string Password)
    {
        return (Password.Length >= 5);
    }
}
