using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Unity.VisualScripting;

public class StringDelay : MonoBehaviour
{
    public bool startTyping=false;
    public TMP_Text tmp;

    private string str;

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
        for(int i = 0; i < str.Length; i++)
        {
            tmp.text += str[i];
            if (!char.IsWhiteSpace(str[i]))
            {
                gameManager.audioManaer.CreateSFXAudioSource(gameManager.player.gameObject, gameManager.audioManaer.FindSFXAudioClipByString("SansSpeak"));
            }
            yield return new WaitForSeconds(0.2f);
        }

    }
}
