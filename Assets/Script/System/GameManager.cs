using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : OrderBehaviour
{
    public GameManager() : base(0){}

    public static GameManager instance;
    [Header("#Player")]
    public Player player;
    [Header("#Manager")]
    public AudioManager audioManager;
    [Header("#QuizManager")]
    public QuizManager quizManager;
    [Header("#VrPlayerChase")]
    public PlayerChase playerChase;
    [Header("#NetworkGameManager")]
    public NetWorkGameManager netWorkGameManager;

    private void Awake()
    {
        if (!instance)
        {
            instance = this;
        }
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

    //생성자로 등록된 순서로 Awake작동되는 메소드
    public override void NumberAwake()
    {
        instance=this;
    }
    //생성자로 등록된 순서로 Start작동되는 메소드
    public override void NumberStart()
    {
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
