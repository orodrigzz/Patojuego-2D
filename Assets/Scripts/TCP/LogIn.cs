using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LogIn : MonoBehaviour
{
    [SerializeField] private Button loginButton;
    [SerializeField] private Text loginText;
    [SerializeField] private Text passwordText;

    private void Awake()
    {
        loginButton.onClick.AddListener(Clicked);
    }

    private void Clicked()
    {
        if (loginText.text != "" && passwordText.text != "")
        {
            Network_Manager._NETWORK_MANAGER.LogIn(loginText.text, passwordText.text);
            SceneManager.LoadScene("Lobby");
        }
    }
}
