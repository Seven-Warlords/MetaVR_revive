using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class QuizManager : MonoBehaviour
{
    public enum State
    {
        ready, quiz, result
    }
    public static QuizManager Instance;
    public State state;
    public Question question;
    public Question[] questions;
    public TextMeshProUGUI time;
    public TextMeshProUGUI question_text;
    public TextMeshProUGUI left_Text;
    public TextMeshProUGUI right_Text;
    private float ctime;
    private float qtime;
    public float timetoquestion;
    public float quiztime;
    public int answer;
    public Transform trashposition;
    public GameObject trash;
    private GameObject trashobject;
    public Answercolor answercolor;
    public StringDelay answer1;
    public StringDelay answer2;

    public float munje;
    public float jungdab;
    public float ohdab;
    private bool isgame;
    public int currentquestion = 1;

    public int is1;
    public int is2;
    public int playercount = 4;

    // Start is called before the first frame update

    private void Awake()
    {
        if(!Instance)
        {
            Instance = this;
        }
    }
    void Start()
    {
        State state = State.ready;
        ctime = timetoquestion;
        isgame = true;
    }

    // Update is called once per frame
    void Update()
    {
        if(isgame)
        {
            switch (state)
            {
                case State.ready:
                    ctime -= Time.deltaTime;
                    if (ctime > 0)
                    {
                        time.text = ctime.ToString("F2");
                        question_text.text = "Wait for next Question...";
                    }

                    if (ctime <= 0)
                    {
                        Quiz();
                    }
                    break;
                /*case State.quiz:
                    qtime -= Time.deltaTime;
                    if (qtime >= (quiztime - 2))
                    {
                        time.text = "Question!";
                    }
                    else if (qtime > 0)
                    {
                        time.text = qtime.ToString("F2");
                    }

                    if (qtime <= 0)
                    {
                        NOResult();
                    }

                    break;*/
                case State.quiz:
                    time.text = "Question!";

                    if((is1 + is2) >= playercount)
                    {
                        if(is1 > is2)
                        {
                            Answer(1);
                        }
                        else if(is1 < is2)
                        {
                            Answer(2);
                        }
                        else if(is1 == is2)
                        {
                            Answer(3);
                        }
                    }
                    break;
                case State.result:
                    ctime -= Time.deltaTime;
                    if (ctime <= (timetoquestion - 4) && ctime > 0)
                    {
                        time.text = ctime.ToString("F2");
                    }

                    if (ctime <= 0)
                    {
                        is1 = 0;
                        is2 = 0;
                        Nextquiz();
                    }

                    break;
            }
            if ((jungdab + ohdab) >= munje)
            {
                isgame = false;
            }
        }
        else if(!isgame)
        {
            
            if (jungdab >= 7)
            {
                question_text.text = "Victory";
            }
            else
            {
                question_text.text = "Defeat";
            }
        }
        
    }

    void Quiz()
    {
        question = questions[currentquestion - 1];
        qtime = quiztime;
        state = State.ready;
        state = State.quiz;
        trash = question.trash;
        answer1.DataPlay();
        answer2.DataPlay();
        StartCoroutine(TrashSpawn());
        question_text.text = question.question;
        //left_Text.text = question.left_Text;
        //right_Text.text = question.right_Text;
    }


    public void Answer(int myanswer)
    {
        answer = myanswer;
        if (state == State.quiz)
        {
            Result();
        }
    }

    public void VoteAnswer(int myanswer)
    {
        if (state == State.quiz)
        {
            if(myanswer == 1)
            {
                is1++;
            }
            else if(myanswer == 2)
            {
                is2++;
            }
            answercolor.ImageIn(myanswer);
        }
    }
    void Result()
    {
        ctime = timetoquestion - 2;
        state = State.result;
        answercolor.ImageClear();
        TrashDelect();
        if (answer == 3)
        {
            Debug.Log("무승부");
            ctime = 3;
            question_text.text = "Draw!";
        }
        else if (answer == question.correct)
        {
            Debug.Log("정답");
            question_text.text = question.correcttext;
            jungdab++;
            time.text = "Correct!";
            currentquestion++;

        }
        else if(answer != question.correct)
        {
            Debug.Log("오답");
            question_text.text = question.all_Failtext;
            if (time)
            time.text = "Fail!";
            ohdab++;
            currentquestion++;
        }

        
    }

    void NOResult()
    {
        ctime = timetoquestion - 2;
        state = State.result;
        TrashDelect();

        Debug.Log("노답");
            question_text.text = question.all_Failtext;
            if (time)
            {
                time.text = "No DAB....";
            }
            ohdab++;

    }
    void Nextquiz()
    {
        ctime = timetoquestion;
        state = State.ready;
    }

    IEnumerator TrashSpawn()
    {
        for(int i = 1; i <= playercount; i ++)
        {
            trashobject = Instantiate(trash);
            trashobject.transform.position = trashposition.position;
            yield return new WaitForSeconds(0.2f);
        }
        
    }

    void TrashDelect()
    {
        Destroy(trashobject);
    }
}
