using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;

public class DeleteAccountScreenController : MonoBehaviour
{
    public Text ErrorLabel;

    IEnumerator DeleteAccount()
    {
        int currentUserId = GlobalVars.Instance.currentUser.id;

        UnityWebRequest request = new UnityWebRequest($"http://localhost:5000/v1/users/{currentUserId}", "DELETE");

        request.downloadHandler = new DownloadHandlerBuffer();

        
        yield return request.SendWebRequest();

        if (request.result != UnityWebRequest.Result.Success)
        {
            ErrorLabel.text = "Não foi possível excluir a conta no momento";
        }
        else
        {
            SceneManager.LoadScene("LoginScreen", LoadSceneMode.Single);
        }
    }

    public void OnYesPress()
    {
        StartCoroutine(DeleteAccount());
    }

    public void OnNoPress()
    {
        SceneManager.LoadScene("UserDataScreen", LoadSceneMode.Single);
    }
}
