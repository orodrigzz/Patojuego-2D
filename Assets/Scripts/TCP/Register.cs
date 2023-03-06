using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Register : MonoBehaviour
{
    [SerializeField] Button registerButton;
    [SerializeField] Text nickRegisterText;
    [SerializeField] Text registerPasswordText;
    [SerializeField] Dropdown raceSelector;
    [SerializeField] string raceSelected;
    public Race race;

    private void Awake()
    {
        Network_Manager._NETWORK_MANAGER.GetAvaiableRaces();
    }
    public void OnRegisterButtonClick()
    {
        if (nickRegisterText.text != ""  && registerPasswordText.text != "" && raceSelected != null)
        {
            Network_Manager._NETWORK_MANAGER.Register(nickRegisterText.text, registerPasswordText.text);
            
            SceneManager.LoadScene("LogIn");
        }
    }

    public void RaceSelector()
    {
        if (raceSelector.value == 0)
        {
            race.SetClass(Race.RaceType.PatitoFeo);
            raceSelected = "PatitoFeo";
        }
        else if (raceSelector.value == 1)
        {
            race.SetClass(Race.RaceType.PatitoGuapo);
            raceSelected = "PatitoGuapo";
        }
    }
}
