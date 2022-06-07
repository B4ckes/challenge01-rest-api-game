using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;

public class LoginScreenController : MonoBehaviour
{
    public InputField Email;
    public InputField Password;
    public Text ErrorText;

    bool HasEmptyFields() {
        return Email.text.Equals("") || Password.text.Equals("");
    }

    void RenderErrorText(DownloadHandler downloadHandler)
    {
        if (HasEmptyFields())
        {
            ErrorText.text = "Preencha todos os campos";
        }
        else
        {
            ErrorText.text = downloadHandler.text;
        }

    }

    IEnumerator LoginRequest()
    {
        LoginModel loginModel = new LoginModel
        {
            Email = this.Email.text,
            Password = this.Password.text
        };

        string json = JsonUtility.ToJson(loginModel);

        byte[] jsonToSend = new System.Text.UTF8Encoding().GetBytes(json);

        UnityWebRequest request = new UnityWebRequest("http://localhost:5000/v1/login", "POST");

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

    public void OnSubmit() {
        StartCoroutine(LoginRequest());
    }

    public void OnRegisterPress() {
        SceneManager.LoadScene("RegisterScreen", LoadSceneMode.Single);
    }

    private void Start() {
        GlobalVars.Instance.ResetCurrentUser();
    }
}
