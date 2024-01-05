using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{

    public static GameManager instance;
    [Header("#Player")]
    public Player player;
    public GameObject playerVR;
    [Header("#Manager")]
    public AudioManager audioManager;
    [Header("#QuizManager")]
    public QuizManager quizManager;
    [Header("#VrPlayerChase")]
    public PlayerChase playerChase;
    [Header("#NetworkGameManager")]
    public NetWorkGameManager netWorkGameManager;
    [Header("#StatusManager")]
    public StatusManager statusManager;
    [Header("#InGame")]
    public Transform[] spawnpoints;
    [Header("#Lobby")]
    public Lobby lobby;
    [Header("#Backend")]
    public WebTest webTest;

    private void Awake()
    {
        if (!instance)
        {
            instance = this;
        }
        audioManager.Init();
    }
    public void Save(string key, float value)
    {
        PlayerPrefs.SetFloat(key, value);
    }
    public float Load(string key)
    {
        if (PlayerPrefs.HasKey(key))
        {
            return PlayerPrefs.GetFloat(key);
        }
        return -1;
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
