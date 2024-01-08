using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerChase : MonoBehaviour
{
    // Start is called before the first frame update
    public static PlayerChase instance;
    public GameObject head;
    public GameObject hand1;
    public GameObject hand2;
    public UI_Player StatusUI;
    public Transform spawnpoint;

    private void OnEnable()
    {
        instance = this;
        StartCoroutine(WaitGameManager());
    }
    IEnumerator WaitGameManager()
    {
        while (GameManager.instance == null)
        {
            yield return new WaitForFixedUpdate();
        }
        GameManager.instance.playerChase = instance;
        GameManager.instance.player.trashspawnpoint = spawnpoint;
    }
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
    }
}
