using UnityEngine;
using UnityEngine.UI;

public class Matchmaking_Screen : MonoBehaviour
{
    [SerializeField] private Button createBttn;
    [SerializeField] private Button joinBttn;
    [SerializeField] private Text createText;
    [SerializeField] private Text joinText;

    private void Awake()
    {
        createBttn.onClick.AddListener(CreateRoom);
        joinBttn.onClick.AddListener(JoinRoom);
    }

    private void CreateRoom() 
    {
        PHOTON_MANAGER._PHOTON_MANAGER.CreateRoom(createText.text.ToString());
    }

    private void JoinRoom()
    {
        Debug.Log("Joining room " + joinText.text.ToString());
        PHOTON_MANAGER._PHOTON_MANAGER.JoinRoom(joinText.text.ToString());
    }
}
