using UnityEngine;
using System.Net.Sockets;
using System.IO;
using System;

public class NetworkManager : MonoBehaviour
{
    public static NetworkManager _NETWORK_MANAGER;

    private TcpClient socket;
    private NetworkStream stream;

    private StreamWriter writer;
    private StreamReader reader;

    const string HOST = "192.168.1.138";
    const int PORT = 6543;

    private bool connected = false;

    private string username = "";

    private void Awake()
    {
        if (_NETWORK_MANAGER != null && _NETWORK_MANAGER != this)
        {
            Destroy(this.gameObject);
        }
        else 
        {
            _NETWORK_MANAGER = this;
            DontDestroyOnLoad(gameObject);

            ConnectToServer();
        }
    }

    private void Update()
    {
        if (connected) 
        {
            if (stream.DataAvailable) 
            {
                string data = reader.ReadLine();
                if (data != null)
                {
                    ManageData(data);
                }
            }
        }
    }

    private void ManageData(string data) {
        if (data == "Ping") {
            Debug.Log("Recibido Ping");
            writer.WriteLine("1");
            writer.Flush();
        }
        else if (data == "Registered")
        {
            Debug.Log("Registered correctly");
            //writer.Flush();
        }
        else 
        {
            string[] parameters = data.Split('/');

            if (parameters[0] == "Races")
            {
                Debug.Log("Getting races");

                int racesLength = Int32.Parse(parameters[1]);
                int fields = 7;
                int paramIterator = 2;
                for (int i = 0; i < racesLength; i++)
                {
                    int id_race = -1;
                    float health = 0;
                    float damage = 0;
                    float speed = 0;
                    float jumping = 0;
                    float cadency = 0;
                    string name = "";

                    for (int j = 0; j < fields; j++)
                    {
                        switch (j)
                        {
                            default:
                                break;
                            case 0:
                                int result;
                                if (int.TryParse(parameters[paramIterator], out result))
                                {
                                    id_race = result;
                                }
                                break;
                            case 1: 
                                health = float.Parse(parameters[paramIterator]);
                                break;
                            case 2: 
                                damage = float.Parse(parameters[paramIterator]);
                                break;
                            case 3: 
                                speed = float.Parse(parameters[paramIterator]);
                                break;
                            case 4: 
                                jumping = float.Parse(parameters[paramIterator]);
                                break;
                            case 5: 
                                cadency = float.Parse(parameters[paramIterator]); 
                                break;
                            case 6: 
                                name = parameters[paramIterator];
                                break;
                        }
                        paramIterator++;
                    }
                    Race newRace = new Race(id_race, health, damage, speed, jumping, cadency, name);
                    GameManager._GAME_MANAGER.races.Add(newRace);
                }
                GameManager._GAME_MANAGER.LoadLevel("LogInRegister");
            }
            else if (parameters[0] == "UserRace")
            {
                Login_Screen loginScreen = GameObject.FindObjectOfType<Login_Screen>().GetComponent<Login_Screen>();
                if (loginScreen != null)
                {
                    int raceId = int.Parse(parameters[1]);
                    GameManager._GAME_MANAGER.AcceptLogin(loginScreen.GetLoginUsername(), raceId, "Lobby");
                    Debug.Log("Login accepted");
                }
            }
        }
    }

    public void ConnectToServer()
    {
        try
        {
            socket = new TcpClient(HOST, PORT);

            stream = socket.GetStream();

            connected = true;

            writer = new StreamWriter(stream);
            reader = new StreamReader(stream);
        }
        catch (Exception ex)
        {
            Debug.LogError("Error: " + ex.Message);
            connected = false;
        }

    }

    public void Login(string username, string password)
    {
        try
        {
            Debug.Log("username: " + username + " and password: " + password);
            writer.WriteLine("0" + "/" + username + "/" + password);
            writer.Flush();
        }
        catch (Exception ex)
        {
            Debug.LogError("Error: " + ex.Message);
        }
    }

    public void Register(string username, string password, int id_race)
    {
        try
        {
            Debug.Log("username: " + username + " and password: " + password);
            writer.WriteLine("2" + "/" + username + "/" + password + "/" + id_race.ToString());
            writer.Flush();
        }
        catch (Exception ex)
        {
            Debug.LogError("Error: " + ex.Message);
            connected = false;
        }
    }

    public void GetRaces()
    {
        try
        {
            writer.WriteLine("3");
            writer.Flush();
        }
        catch (Exception ex)
        {
            Debug.LogError("Error: " + ex.Message);
            connected = false;
        }
    }

    private void OnDestroy()
    {
        if (writer != null) {
            writer.WriteLine("5");
            //writer.Flush();
        }
    }
}
