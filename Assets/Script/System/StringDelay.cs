using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Unity.VisualScripting;
using System;

public class StringDelay : MonoBehaviour
{
    public bool startTyping=false;
    public TMP_Text tmp;

    
    public string str;
    public int answerType = 1;

    private float stringDelay = 0.1f;

    private WebTest webTest;
    private QuizManager quizManager;

    GameManager gameManager;
    // Start is called before the first frame update
    void Start()
    {
        gameManager = GameManager.instance;
        quizManager = gameManager.quizManager;
        webTest = gameManager.webTest;
        str = tmp.text;
        tmp.text = "";
    }

    // Update is called once per frame
    void Update()
    {
        if (startTyping)
        {
            StartCoroutine(DelayText());
            startTyping=!startTyping;
        }
    }
    IEnumerator DelayText()
    {
        string[] arrStr = str.Split("\\n");
        for(int j = 0; j < arrStr.Length; j++)
        {
            for (int i = 0; i < arrStr[j].Length; i++)
            {
                Debug.Log(i);
                tmp.text += arrStr[j][i];
                if (!char.IsWhiteSpace(arrStr[j][i]))
                {
                    gameManager.audioManager.CreateSFXAudioSource(gameManager.playerVR.gameObject, gameManager.audioManager.FindSFXAudioClipByString("SansSpeak"));
                }
                yield return new WaitForSeconds(stringDelay);
            }
            tmp.text += "\\n";
            tmp.text = tmp.text.Replace("\\n", "\n");
        }
    }
   
    public void DataPlay()
    {
        tmp.text = "";
        if (answerType == 1)
        {
            str = (string)webTest.getData(0, "trashCanText1", quizManager.question);
        }
        else if(answerType == 2)
        {
            str = (string)webTest.getData(0, "trashCanText2", quizManager.question);
        }
        else
        {
            Debug.LogError("asnwertype을 잘못 설정하였습니다");
        }
        startTyping = !startTyping;
    }
}
