using UnityEngine;

public class GlobalVars : MonoBehaviour
{
    public static GlobalVars Instance { get; private set; }


    public CurrentUserModel currentUser { get; private set; } = null;
    public int score { get; private set; } = 0;

    private void Awake() {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
        }

        DontDestroyOnLoad(gameObject);
    }

    public void SetCurrentUser(CurrentUserModel user) {
        this.currentUser = user;
    }

    public void ResetCurrentUser() {
        this.currentUser = new CurrentUserModel();
    }

    public void Score() {
        this.score++;
    }
}
