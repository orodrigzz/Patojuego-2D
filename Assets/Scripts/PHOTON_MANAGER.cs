using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class PHOTON_MANAGER : MonoBehaviourPunCallbacks
{
    public static PHOTON_MANAGER _PHOTON_MANAGER;
    
    private void Awake()
    {
        if (_PHOTON_MANAGER != null && _PHOTON_MANAGER != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            _PHOTON_MANAGER = this;
            DontDestroyOnLoad(gameObject);

            PhotonConnect();
        }
    }

    public void PhotonConnect()
    {
        PhotonNetwork.AutomaticallySyncScene = true;
        PhotonNetwork.ConnectUsingSettings();
    }

    public override void OnConnectedToMaster()
    {
        Debug.Log("Conexión OK");
        PhotonNetwork.JoinLobby(TypedLobby.Default);
    }

    public override void OnDisconnected(DisconnectCause cause)
    {
        Debug.Log("Conexión KO: " + cause);
    }

    public override void OnJoinedLobby()
    {
        Debug.Log("Pal Lobby");
    }

    public void CreateRoom(string name)
    {
        PhotonNetwork.CreateRoom(name, new RoomOptions { MaxPlayers = 2});
    }

    public void JoinRoom(string name)
    {
        PhotonNetwork.JoinRoom(name);
    }

    public override void OnJoinedRoom()
    {
        Debug.Log("Unido a la sala " + PhotonNetwork.CurrentRoom.Name);
    }

    public override void OnJoinRoomFailed(short returnCode, string message)
    {
        Debug.Log("Error Sala: " + returnCode + " : " + message);
    }

    public override void OnPlayerEnteredRoom(Player newPlayer)
    {
        if (PhotonNetwork.CurrentRoom.PlayerCount == PhotonNetwork.CurrentRoom.MaxPlayers && PhotonNetwork.IsMasterClient)
        {
            PhotonNetwork.LoadLevel("Game");
        }
    }

}
