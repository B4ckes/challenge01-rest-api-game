using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;

public class RegisterScreenController : MonoBehaviour
{    
    public const string API_USERS_URI = "https://localhost:5001/v1/users";

    public Text ErrorLabel;
    public InputField Username;
    public InputField Email;
    public InputField Password;
    
    bool HasEmptyFields() {
        return Username.text.Equals("") || Email.text.Equals("") || Password.text.Equals("");
    }

    void RenderErrorText(DownloadHandler downloadHandler)
    {
        Debug.Log(HasEmptyFields());
        if (HasEmptyFields())
        {
            ErrorLabel.text = "Preencha todos os campos";
        }
        else
        {
            ErrorLabel.text = downloadHandler.text;
        }

    }

    IEnumerator RegisterRequest()
    {
        CreateUserModel createUserModel = new CreateUserModel
        {
            Username = this.Username.text,
            Email = this.Email.text,
            Password = this.Password.text
        };

        string json = JsonUtility.ToJson(createUserModel);

        byte[] jsonToSend = new System.Text.UTF8Encoding().GetBytes(json);

        UnityWebRequest request = new UnityWebRequest("https://localhost:5001/v1/users", "POST");

        request.uploadHandler = (UploadHandler) new UploadHandlerRaw(jsonToSend);
        request.downloadHandler = new DownloadHandlerBuffer();
        request.SetRequestHeader("Content-Type", "application/json");

        yield return request.SendWebRequest();

        if (request.result != UnityWebRequest.Result.Success)
        {
            RenderErrorText(request.downloadHandler);
        }
        else
        {
            CurrentUserModel currentUser = JsonUtility.FromJson<CurrentUserModel>(request?.downloadHandler?.text);

            GlobalVars.Instance.SetCurrentUser(currentUser);

            SceneManager.LoadScene("MainMenuScreen", LoadSceneMode.Single);
        }
    }

    public void OnBackButton() {
        SceneManager.LoadScene("LoginScreen", LoadSceneMode.Single);
    }

    public void OnSubmit()
    {
        StartCoroutine(RegisterRequest());
    }
}
