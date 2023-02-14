using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_MANAGER : MonoBehaviour
{
    [SerializeField]
    private Button createButton;

    [SerializeField]
    private Button joinButton;

    [SerializeField]
    private Text createTxt;

    [SerializeField]
    private Text joinTxt;

    private void Awake()
    {
        createButton.onClick.AddListener(CreateRoom);
        joinButton.onClick.AddListener(JoinRoom);
    }

    public void CreateRoom()
    {
        PHOTON_MANAGER._PHOTON_MANAGER.CreateRoom(createTxt.text.ToString());
    }

    public void JoinRoom()
    {
        PHOTON_MANAGER._PHOTON_MANAGER.JoinRoom(joinTxt.text.ToString());
    }
}
