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

    //�����ڷ� ��ϵ� ������ Awake�۵��Ǵ� �޼ҵ�
    public override void NumberAwake()
    {
        instance=this;
    }
    //�����ڷ� ��ϵ� ������ Start�۵��Ǵ� �޼ҵ�
    public override void NumberStart()
    {
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
