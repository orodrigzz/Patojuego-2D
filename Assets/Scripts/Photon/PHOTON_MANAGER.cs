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
            DontDestroyOnLoad(this.gameObject);

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
    {;
        Debug.Log("Conexión KO: " + cause);
        Application.Quit();
    }

    public override void OnJoinedLobby()
    {
        Debug.Log("Pal Lobby");
    }

    public void CreateRoom(string nameRoom) 
    { 
        PhotonNetwork.CreateRoom(nameRoom, new RoomOptions { MaxPlayers = 2});
    }

    public void JoinRoom(string nameRoom)
    {
        Debug.LogError("Joining room");
        PhotonNetwork.JoinRoom(nameRoom);
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
        if (PhotonNetwork.CurrentRoom.PlayerCount == PhotonNetwork.CurrentRoom.MaxPlayers && PhotonNetwork.IsMasterClient) {
            PhotonNetwork.LoadLevel("Game");
        }
    }
}
