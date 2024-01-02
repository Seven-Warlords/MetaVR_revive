using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public int myNumber;
    // Start is called before the first frame update
    private void OnEnable()
    {
        int a = GameManager.instance.netWorkGameManager.currentplayerNum;
        GameManager.instance.player.gameObject.transform.position = GameManager.instance.spawnpoints[a - 1].position;
        GameManager.instance.player.gameObject.transform.rotation = GameManager.instance.spawnpoints[a - 1].rotation;
        GameManager.instance.player.JoinGame();
    }
    // Update is called once per frame
    public void JoinGame()
    {
        myNumber = GameManager.instance.netWorkGameManager.currentplayerNum;
    }
}
