using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Unity.VisualScripting;

public class RoomManager : MonoBehaviourPunCallbacks
{

    public static RoomManager instance;

    public GameObject player;

    [Space]
    public Transform spawnPoint;

    [Space]
    public GameObject roomCam;

    [Space]
    public GameObject nameUI;
    public GameObject connectingUI;

    private string nickname = "unnamed";

    void Awake() {
        instance = this;       
    }

    public void ChangeNickname(string _name)
    {
        nickname = _name;
    }

    public void JoinRoomButtonPressed() 
    {
        Debug.Log("Connecting...");
        PhotonNetwork.ConnectUsingSettings(); 

        nameUI.SetActive(false);
        connectingUI.SetActive(true);
    }

    // Start is called before the first frame update
    void Start()
    {

    }


    public override void OnConnectedToMaster()
    {
        base.OnConnectedToMaster();

        Debug.Log("Connected to Server");

        PhotonNetwork.JoinLobby();
    }

    public override void OnJoinedLobby()
    {
        base.OnJoinedLobby();

        PhotonNetwork.JoinOrCreateRoom("MyRoomName", null, null);
        Debug.Log("We Create/Join room");
    }

    public override void OnJoinedRoom()
    {
        Debug.Log("We are connected and in a room now");

        roomCam.SetActive(false);

        RespawnPlayer();
        
    }

    public void RespawnPlayer()
    {
        GameObject _player = PhotonNetwork.Instantiate(player.name, spawnPoint.position, Quaternion.identity);
        Debug.Log("New Player instantiate");
        _player.GetComponent<PlayerSetup>().IsLocalPlayer();
        _player.GetComponent<Health>().IsLocalPlayer = true;

        _player.GetComponent<PhotonView>().RPC("SetNickname", RpcTarget.All, nickname);
    }
}
