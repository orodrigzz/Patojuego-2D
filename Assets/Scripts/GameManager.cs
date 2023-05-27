using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using UnityEngine.SceneManagement;
using Photon.Realtime;

public class GameManager : MonoBehaviour
{
    private int userRaceId = -1;
    private string currentUsername;
    public void SetCurrentUsername(string newName) { currentUsername = newName; }
    public string GetCurrentUsername() { return currentUsername; }

    public List<Race> races;

    [SerializeField] private GameObject spawnPlayer1;
    [SerializeField] private GameObject spawnPlayer2;

    public static GameManager _GAME_MANAGER;
    private void Awake()
    {
        if (_GAME_MANAGER != null && _GAME_MANAGER != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            _GAME_MANAGER = this;
            DontDestroyOnLoad(this.gameObject);
        }
    }

    private void Start()
    {
        races = new List<Race>();
        NetworkManager._NETWORK_MANAGER.GetRaces();

        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void InitRound() 
    {
        GameObject[] spawnPoints = GameObject.FindGameObjectsWithTag("SpawnPoint");
        foreach (GameObject spawnPoint in spawnPoints)
        {
            if (spawnPoint.name == "SpawnPoint1") 
            {
                spawnPlayer1 = spawnPoint;
            }
            else if (spawnPoint.name == "SpawnPoint2")
            {
                spawnPlayer2 = spawnPoint;
            }
        }

        if (PhotonNetwork.IsMasterClient)
        {
            if (userRaceId == 1) 
            {
                GameObject playerGO = PhotonNetwork.Instantiate("Duck2", spawnPlayer1.transform.position, Quaternion.identity);
                playerGO.GetComponent<Character>().SetAttributes(races[0]);
                playerGO.GetComponent<Character>().SetSpawnPosition(spawnPlayer1.transform.position);
                playerGO.GetComponent<Character>().GetPlayerCanvas().SetPlayerName(currentUsername);
                playerGO.GetComponent<Character>().GetPlayerCanvas().SetHealth(100);
            }
            else if (userRaceId == 2) 
            {
                GameObject playerGO = PhotonNetwork.Instantiate("Duck", spawnPlayer1.transform.position, Quaternion.identity);
                playerGO.GetComponent<Character>().SetAttributes(races[1]);
                playerGO.GetComponent<Character>().SetSpawnPosition(spawnPlayer1.transform.position);
                playerGO.GetComponent<Character>().GetPlayerCanvas().SetPlayerName(currentUsername);
                playerGO.GetComponent<Character>().GetPlayerCanvas().SetHealth(100);
            }
        }
        else
        {
            if (userRaceId == 1)
            {
                GameObject playerGO = PhotonNetwork.Instantiate("Duck2", spawnPlayer2.transform.position, Quaternion.identity);
                playerGO.GetComponent<Character>().SetAttributes(races[0]);
                playerGO.GetComponent<Character>().SetSpawnPosition(spawnPlayer2.transform.position);
                playerGO.GetComponent<Character>().GetPlayerCanvas().SetPlayerName(currentUsername);
                playerGO.GetComponent<Character>().GetPlayerCanvas().SetHealth(100);
            }
            else if (userRaceId == 2)
            {
                GameObject playerGO = PhotonNetwork.Instantiate("Duck", spawnPlayer2.transform.position, Quaternion.identity);
                playerGO.GetComponent<Character>().SetAttributes(races[1]);
                playerGO.GetComponent<Character>().SetSpawnPosition(spawnPlayer2.transform.position);
                playerGO.GetComponent<Character>().GetPlayerCanvas().SetPlayerName(currentUsername);
                playerGO.GetComponent<Character>().GetPlayerCanvas().SetHealth(100);
            }
        }

    }

    public void AcceptLogin(string username, int raceId, string sceneToLoad) {
        currentUsername = username;
        userRaceId = raceId;
        LoadLevel(sceneToLoad);
    }

    public void LoadLevel(string levelName)
    {
        SceneManager.LoadSceneAsync(levelName);
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (scene.name.Equals("Game")) {
            InitRound();
        }
    }
}
