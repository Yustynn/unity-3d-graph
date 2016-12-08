using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class BackToWelcomeScreen : MonoBehaviour
{
    public void NavigateToWelcomeScreen()
    {
        SceneManager.LoadScene("WelcomeScreen");
    }
}
