using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Photon.Pun;

public class QuizManager : MonoBehaviourPunCallbacks, IPunObservable
{

    
    public enum State
    {
        ready, quiz, result
    }
    public static QuizManager Instance;
    private PhotonView myPV;

    [Header("#Network")]
    public State state;
    public Question question;
    public Question[] questions;
    public float timetoquestion;
    public float quiztime;
    private float ctime;
    private float qtime;
    public float munje;
    public int jungdab;
    public int ohdab;
    public int is1;
    public int is2;
    public int playercount;
    public int currentquestion = 1;

    [Header("#NotNetwork")]
    public TextMeshProUGUI time;
    public TextMeshProUGUI question_text;
    public TextMeshProUGUI left_Text;
    public TextMeshProUGUI right_Text;
    
    public int answer;
    public Transform trashposition;
    public string trash;
    private GameObject trashobject;
    public Answercolor answercolor;
    public StringDelay answer1;
    public StringDelay answer2;

    private bool isgame;

    

    public GameObject trashcan1;
    public GameObject trashcan2;
    public int trashcan1code;
    public int trashcan2code;
    public Transform trashcan1place;
    public Transform trashcan2place;
    public GameObject[] trashcans;

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
        myPV = GetComponent<PhotonView>();
        if(PhotonNetwork.IsMasterClient)
        {
            State state = State.ready;
        }
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
                    question = questions[currentquestion - 1];
                    if (PhotonNetwork.IsMasterClient)
                    {
                        ctime -= Time.deltaTime;
                    }
                    if (ctime > 0)
                    {
                        time.text = ctime.ToString("F2");
                        question_text.text = "Wait for next Question...";
                    }

                    if (ctime <= 0)
                    {
                        if(PhotonNetwork.IsMasterClient)
                        {
                            
                        }
                        Quiz();
                    }
                    break;
                case State.quiz:
                    time.text = "Question!";
                    
                    if ((is1 + is2) >= playercount)
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
                    if (PhotonNetwork.IsMasterClient)
                    {
                        ctime -= Time.deltaTime;
                    }
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
        if(PhotonNetwork.IsMasterClient)
        {
            
        }
        
        qtime = quiztime;
        state = State.ready;
        state = State.quiz;
        trashcan1code = question.trashcan1code;
        trashcan2code = question.trashcan2code;
        trash = question.trash;
        answer1.DataPlay();
        answer2.DataPlay();

        //쓰레기통 생성 코드. 간단하게 짤 수 있으면 부탁함.
        trashcan1 = Instantiate(trashcans[trashcan1code - 1]);
        trashcan2 = Instantiate(trashcans[trashcan2code - 1]);
        trashcan1.transform.position = trashcan1place.position;
        trashcan1.transform.rotation = trashcan1place.rotation;
        trashcan2.transform.position = trashcan2place.position;
        trashcan2.transform.rotation = trashcan2place.rotation;
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
        if (PhotonNetwork.IsMasterClient)
        {
            ctime = timetoquestion - 2;
            state = State.result;
        }
        answercolor.ImageClear();
        TrashDelect();
        Destroy(trashcan1);
        Destroy(trashcan2);
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
            //trashobject = Instantiate(trash);
            //trashobject.transform.position = trashposition.position;
            //yield return new WaitForSeconds(0.2f);
            if(PhotonNetwork.IsMasterClient)
            {
                trashobject = PhotonNetwork.Instantiate(trash, trashposition.position, trashposition.rotation);
                if(i == GameManager.instance.player.myNumber)
                {
                    if (!PhotonNetwork.IsMasterClient)
                    {
                        trashobject.GetComponent<PhotonView>().RequestOwnership();
                    }
                }
                yield return new WaitForSeconds(0.2f);
            }
        }
        
    }

    /*void TrashSpawn()
    {
        if(GetComponent<PhotonView>().IsMine)
        {
            trashobject = PhotonNetwork.Instantiate(trash, trashposition.position, trashposition.rotation);
        }
    }*/

    void TrashDelect()
    {
        Destroy(trashobject);
    }

    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (stream.IsWriting && PhotonNetwork.IsMasterClient)
        {
            stream.SendNext(state);
            //stream.SendNext(question);
            stream.SendNext(ctime);
            stream.SendNext(qtime);
            stream.SendNext(munje);
            stream.SendNext(jungdab);
            stream.SendNext(ohdab);
            stream.SendNext(is1);
            stream.SendNext(is2);
            stream.SendNext(playercount);
            stream.SendNext(currentquestion);
        }
        else
        {
            this.state = (QuizManager.State)stream.ReceiveNext();
            //this.question = (Question)stream.ReceiveNext();
            this.ctime = (float)stream.ReceiveNext();
            this.qtime = (float)stream.ReceiveNext();
            this.munje = (float)stream.ReceiveNext();
            this.jungdab = (int)stream.ReceiveNext();
            this.ohdab = (int)stream.ReceiveNext();
            this.is1 = (int)stream.ReceiveNext();
            this.is2 = (int)stream.ReceiveNext();
            this.playercount = (int)stream.ReceiveNext();
            this.currentquestion = (int)stream.ReceiveNext();
        }
    }
}
