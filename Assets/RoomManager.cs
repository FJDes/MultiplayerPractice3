using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class RoomManager : MonoBehaviourPunCallbacks
{

    public GameObject player;

    [Space]
    public Transform spawnPoint;

    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("Connecting...");

        PhotonNetwork.ConnectUsingSettings();
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
        
        GameObject _player = PhotonNetwork.Instantiate(player.name, spawnPoint.position, Quaternion.identity);
        Debug.Log("New Player instantiate");
        _player.GetComponent<PlayerSetup>().IsLocalPlayer();
    }
}
