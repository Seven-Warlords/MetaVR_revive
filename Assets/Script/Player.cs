using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public GameObject head;
    public GameObject handL;
    public GameObject handR;
    public int myNumber;
    // Start is called before the first frame update
    private void OnEnable()
    {
        int a = GameManager.instance.netWorkGameManager.currentplayerNum;
        GameManager.instance.player.gameObject.transform.position = GameManager.instance.spawnpoints[a - 1].position;
        GameManager.instance.player.gameObject.transform.rotation = GameManager.instance.spawnpoints[a - 1].rotation;
        GameManager.instance.player.JoinGame();

        
        GameManager.instance.playerVR.transform.position = GameManager.instance.spawnpoints[a - 1].localPosition;
        GameManager.instance.playerVR.transform.rotation = GameManager.instance.spawnpoints[a - 1].localRotation;
        PlayerChase chase= GameManager.instance.playerVR.GetComponent<PlayerChase>();
        head = chase.head;
        handL = chase.handL;
        handR = chase.handR;
    }
    // Update is called once per frame
    public void JoinGame()
    {
        myNumber = GameManager.instance.netWorkGameManager.currentplayerNum;
    }
}
