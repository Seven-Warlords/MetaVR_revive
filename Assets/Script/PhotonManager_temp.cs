using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class PhotonManager_temp : MonoBehaviourPunCallbacks {

    public GameObject SpawnLocation;
    private Transform startpoint;
    private int playerNumber;

    private void Awake() {
        PhotonNetwork.NickName = "Player";
        PhotonNetwork.GameVersion = "1.0";
        PhotonNetwork.AutomaticallySyncScene = true;
        PhotonNetwork.ConnectUsingSettings();

        // startpoint = SpawnLocation.GetComponent<Transform>();
#if On
        Debug.Log(PhotonNetwork.SendRate);
#endif
    }

    public override void OnConnectedToMaster() {
#if On
        Debug.Log("Connected to Master!");
        Debug.Log($"PhotonNetwork.InLobby = {PhotonNetwork.InLobby}");
#endif
        PhotonNetwork.JoinLobby();
    }

    public override void OnJoinedLobby() {
#if On
        Debug.Log($"PhotonNetwork.InLobby = {PhotonNetwork.InLobby}");
#endif
        PhotonNetwork.JoinRandomRoom();

      
    }

    public override void OnJoinRandomFailed(short returnCode, string message) {
#if On
        Debug.Log($"JoinRoom Faild {returnCode}:{message}");
#endif
        RoomOptions ro = new RoomOptions();
        ro.MaxPlayers = 4;
        ro.IsOpen = true;
        ro.IsVisible = true;
        PhotonNetwork.CreateRoom("My Room", ro);
    }

    public override void OnCreatedRoom() {
#if On
    Debug.Log("Created Room");
    Debug.Log($"Room Name = {PhotonNetwork.CurrentRoom.Name}");
#endif
    }

    public override void OnJoinedRoom() {
#if On
    Debug.Log($"PhotonNetwork.InRoom = {PhotonNetwork.InRoom}");
    Debug.Log($"Player Count = {PhotonNetwork.CurrentRoom.PlayerCount}");
#endif
        GameManager.instance.netWorkGameManager.PlayerJoin();
        PhotonNetwork.NickName = "Player : " + (GameManager.instance.netWorkGameManager.currentplayerNum).ToString();
        GameManager.instance.player.myNumber = GameManager.instance.netWorkGameManager.currentplayerNum;
        foreach (var player in PhotonNetwork.CurrentRoom.Players) {
            Debug.Log($"{player.Value.NickName}");
        }
       // PhotonNetwork.Instantiate("PPP", startpoint.position, startpoint.rotation, 0);
    }
}

