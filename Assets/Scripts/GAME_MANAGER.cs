using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class GAME_MANAGER : MonoBehaviour
{
    [SerializeField]
    private GameObject spawn1;

    [SerializeField]
    private GameObject spawn2;

    private void Awake()
    {
        if (PhotonNetwork.IsMasterClient)
        {
            PhotonNetwork.Instantiate("Player", spawn1.transform.position, Quaternion.identity);
        }
        else
        {
            PhotonNetwork.Instantiate("Player", spawn2.transform.position, Quaternion.identity);
        }
    }
}
