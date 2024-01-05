using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Unity.VisualScripting;

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
        string[] arrStr = str.Split("\n");
        for(int j = 0; j < arrStr.Length; j++)
        {
            for (int i = 0; i < arrStr[j].Length; i++)
            {
                Debug.Log(i);
                tmp.text += arrStr[j][i];
                if (!char.IsWhiteSpace(arrStr[j][i]))
                {
                    gameManager.audioManager.CreateSFXAudioSource(gameManager.playerChase.gameObject, gameManager.audioManager.FindSFXAudioClipByString("SansSpeak"));
                }
                yield return new WaitForSeconds(0.2f);
            }
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
            Debug.LogError("asnwertype�� �߸� �����Ͽ����ϴ�");
        }
        startTyping = !startTyping;
    }
}
