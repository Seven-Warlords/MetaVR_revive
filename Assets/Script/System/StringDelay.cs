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

    GameManager gameManager;
    // Start is called before the first frame update
    void Start()
    {
        gameManager = GameManager.instance;
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
                    if (gameManager.playerChase != null)
                    {
                        gameManager.audioManager.CreateSFXAudioSource(gameManager.playerChase.gameObject, gameManager.audioManager.FindSFXAudioClipByString("SansSpeak"));
                    }
                }
                yield return new WaitForSeconds(0.2f);
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
            str = QuizManager.Instance.question.left_Text;
        }
        else if(answerType == 2)
        {
            str = QuizManager.Instance.question.right_Text;
        }
        else
        {
            Debug.LogError("asnwertype을 잘못 설정하였습니다");
        }
        startTyping = !startTyping;
    }
}
