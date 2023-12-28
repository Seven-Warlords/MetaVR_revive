using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class QuizButton : MonoBehaviour
{
    // Start is called before the first frame update
    //public RectTransform uiObject;
    //Transform parentTransform;
    //public TextMeshProUGUI text;
    public int type;
    private bool istouched;
    void Start()
    {
        //uiObject = GetComponent<RectTransform>();
        //parentTransform = uiObject.parent;
        istouched = false;
        // �θ� ��ü�� �ִٸ� �׿� ���� ������ �� �� �ֽ��ϴ�.
    }

    private void Update()
    {
        /*if(QuizManager.Instance.state == QuizManager.State.quiz)
        {
            if(!istouched)
            {
                GetComponent<Button>().interactable = true;
            }
            else if(istouched)
            {
                GetComponent<Button>().interactable = false;
            }
            
            switch (type)
            {
                case 1:
                    text.text = QuizManager.Instance.question.left_Text;
                    break;
                case 2:
                    text.text = QuizManager.Instance.question.right_Text;
                    break;
            }
            
           
        }
        else
        {
            GetComponent<Button>().interactable = false;
            if (istouched)
            {
                Respawn();
            }
            //istouched = true;
        }*/
    }
    public void Click()
    {
        QuizManager.Instance.VoteAnswer(type);
        GetComponent<Button>().interactable = false;
        istouched = true;
    }

    void Respawn()
    {
        istouched = false;
    }
}
