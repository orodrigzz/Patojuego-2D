using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class PHOTON_MANAGER : MonoBehaviourPunCallbacks 
{
    public static PHOTON_MANAGER _PHOTON_MANAGER;

    public string nickEnemy;
    public Race playerRace;
    public Race enemyRace;
    private void Awake()
    {
        //Generamos singleton
        if (_PHOTON_MANAGER != null && _PHOTON_MANAGER != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            _PHOTON_MANAGER = this;
            DontDestroyOnLoad(this.gameObject);

            //Realizo conexion
            PhotonConnect();
        }
    }

    public void PhotonConnect()
    {
        //Sincronizo la carga de la sala para todos los jugadores
        PhotonNetwork.AutomaticallySyncScene = true;

        //Conexion al servidor con la configuracion establecida
        PhotonNetwork.ConnectUsingSettings();
    }

    //Al conectarme al servidor
    public override void OnConnectedToMaster()
    {
        Debug.Log("Conexion realizada correctamente");
        PhotonNetwork.JoinLobby(TypedLobby.Default);
    }

    //Al desconectarme
    public override void OnDisconnected(DisconnectCause cause)
    {
        Debug.Log("He implosionado porque: " + cause);
    }

    //Al unirme el lobby
    public override void OnJoinedLobby()
    {
        Debug.Log("Accedido al Lobby");
        PhotonNetwork.LoadLevel("LogInRegister");
    }

    //Funcion para crear salas
    public void CreateRoom (string nameRoom)
    {
        PhotonNetwork.CreateRoom(nameRoom, new RoomOptions {MaxPlayers = 2});
    }

    //Funcion para unirme a salas
    public void JoinRoom (string nameRoom)
    {
        PhotonNetwork.JoinRoom(nameRoom);
    }

    //Al unirme a la sala
    public override void OnJoinedRoom()
    {
        Debug.Log("Me he unido a la Sala" + PhotonNetwork.CurrentRoom.Name + "con"
             + PhotonNetwork.CurrentRoom.PlayerCount + "Jugadores conectados a ellas");


        Debug.Log("Has joined: " + Network_Manager._NETWORK_MANAGER.GetCurrentUser().GetnicknameName());

        foreach (Player player in PhotonNetwork.CurrentRoom.Players.Values)
        {
            if (player.NickName != PhotonNetwork.NickName)
            {
                nickEnemy = player.NickName;

                
                Network_Manager._NETWORK_MANAGER.SendNickToGetRace(nickEnemy);
            }
        }
    }

    //Al no poderme conectar a una sala.
    public override void OnJoinRoomFailed(short returnCode, string message)
    {
        Debug.Log("No me he podido conectar a la sala dado el error: " + returnCode + " que significa: " + message);
    }

    public override void OnPlayerEnteredRoom(Player newPlayer)
    {
       
        

        if (PhotonNetwork.CurrentRoom.PlayerCount == PhotonNetwork.CurrentRoom.MaxPlayers)
        {
            if (PhotonNetwork.IsMasterClient)
            {
                nickEnemy = newPlayer.NickName;
                Network_Manager._NETWORK_MANAGER.SendNickToGetRace(nickEnemy);
                PhotonNetwork.LoadLevel("Game");
            }
        }
    }

    void LoadLevel()
    {

    }
    public void LeaveCurrentRoom()
    {
        PhotonNetwork.LeaveRoom(true);
    }
}



