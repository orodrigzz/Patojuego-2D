using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Photon.Pun;

public class GAME_MANAGER : MonoBehaviour
{
    [SerializeField]
    private GameObject spawnPlayer1;

    [SerializeField]
    private GameObject spawnPlayer2;

    private void Awake()
    {

        //SPAWNS IN GAME
        if (PhotonNetwork.IsMasterClient)
        {
            if (spawnPlayer1 != null)
            {
                PhotonNetwork.Instantiate("Player", spawnPlayer1.transform.position, Quaternion.identity);
            }
        }
        else
        {
            if (spawnPlayer2 != null)
            {
                PhotonNetwork.Instantiate("Player", spawnPlayer2.transform.position, Quaternion.identity);
            }
        }
    }

    // SCENE MANAGER
    public void ToLogin()
    {
        SceneManager.LoadScene("LogIn");
    }

    public void ToRegister()
    {
        SceneManager.LoadScene("Register");
    }
}
