using UnityEngine;
using UnityEngine.UI;

public class Login_Screen : MonoBehaviour
{
    [SerializeField] private GameObject registerPanel;
    [SerializeField] private GameObject loginPanel;

    [SerializeField] private Button registerBttn;
    [SerializeField] private Button loginBttn;
    [SerializeField] private Text usernameText;
    [SerializeField] private Text passwordText;

    [SerializeField] private Button submitBttn;
    [SerializeField] private Text registerUsernameText;
    [SerializeField] private Text registerPasswordText;
    [SerializeField] private Button LeftRaceBttn;
    [SerializeField] private Button RightRaceBtnn;

    [SerializeField] private Image duckImage;
    [SerializeField] private Image duck2Img;

    [SerializeField] private int idRace;

    private void Awake()
    {
        registerPanel.SetActive(false);
        loginBttn.onClick.AddListener(Log);
        LeftRaceBttn.onClick.AddListener(LeftRace);
        RightRaceBtnn.onClick.AddListener(RightRace);
    }

    private void Start()
    {
        idRace = 1;
    }

    private void LeftRace() 
    {
        if (idRace > 1) {
            idRace--;
            duck2Img.enabled = true;
            duckImage.enabled = false;
        }
    }

    private void RightRace()
    {
        if (idRace < 2)
        {
            idRace++;
            duck2Img.enabled = false;
            duckImage.enabled = true;
        }
    }

    private void Log() 
    {
        NetworkManager._NETWORK_MANAGER.Login(usernameText.text.ToString(), passwordText.text.ToString());
    }

    public void Register()
    {
        NetworkManager._NETWORK_MANAGER.Register(registerUsernameText.text.ToString(), registerPasswordText.text.ToString(), idRace);
        ShowLoginScreen();
    }

    public void ShowRegisterScreen() 
    {
        loginPanel.SetActive(false);
        registerPanel.SetActive(true);
    }

    public void ShowLoginScreen()
    {
        loginPanel.SetActive(true);
        registerPanel.SetActive(false);
    }

    public string GetLoginUsername() 
    {
        if (usernameText != null) { 
            return usernameText.text.ToString();
        }
        return null;
    }
}
