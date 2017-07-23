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

            //send request to API (TODO make request asyncronous)
            LoginPlayer(insertedUserEmail, insertedPassword);
        }
        else
        {
            Debug.Log("All fields must be filled");
        }
    }

    // send a request to API to log in with a user
    private APIResponseModel LoginPlayer(string userEmail, string password)
    {
        APIResponseModel response = new APIResponseModel();

        UserModel user = new UserModel()
        {
            password = password,
        };

        // check if userEmail is an email or a username
        if (Utils.CheckEmail(userEmail))
        {
            user.email = userEmail;
        }
        else
        {
            user.username = userEmail;
        }

        // create the JSON
        string json = user.SaveToString();

        //make the request to the remote API (TODO: make request asynchronous)
        response = APIManager.MakeRequest(endpoint, "POST", json);

        if (response.status == 200)
        {
            user.token = response.token;
            Scenes.LoadGameView();
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
