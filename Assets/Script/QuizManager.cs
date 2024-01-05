using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Photon.Pun;
using Photon.Realtime;

public class QuizManager : MonoBehaviourPunCallbacks, IPunObservable
{

    
    public enum State
    {
        dead, ready, quiz, result
    }
    public static QuizManager Instance;
    private PhotonView myPV;

    [Header("#Network")]
    public State state;
    public int question;
    public int[] questions;
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
    public int collectanswer;

    [Header("#NotNetwork")]
    public TextMeshProUGUI time;
    public TextMeshProUGUI question_text;
    public TextMeshProUGUI left_Text;
    public TextMeshProUGUI right_Text;
    
    public int answer;
    public Transform trashposition;
    public string quizItem;
    private GameObject trashobject;
    public Answercolor answercolor;
    public GameObject answer1Place;
    public GameObject answer2Place;
    public StringDelay answer1;
    public StringDelay answer2;

    private bool isgame;
    private WebTest webTest;
    

    public GameObject trashcan1;
    public GameObject trashcan2;
    public int trashcan1code;
    public int trashcan2code;
    public Transform trashcan1place;
    public Transform trashcan2place;
    public GameObject[] trashcans;
    private bool resulted;

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
        webTest = GameManager.instance.webTest;
        answer1Place.SetActive(false);
        answer2Place.SetActive(false);
        ctime = timetoquestion;
        isgame = true;
    }

    

    // Update is called once per frame
    void Update()
    {
        if(isgame)
        {
            playercount = GameManager.instance.netWorkGameManager.playercount;
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
                            myPV.RPC("Quiz", RpcTarget.All, null);
                        }
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
                        
                        if(PhotonNetwork.IsMasterClient)
                        {
                            is1 = 0;
                            is2 = 0;
                            Nextquiz();
                        }
                    }

                    break;
                default:
                    return;
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

    void LateUpdate()
    {
        switch (state)
        {
            case State.ready:
                resulted = false;
                break;
            case State.quiz:
                
                break;
            case State.result:
                

                break;
        }
    }


    [PunRPC]
    void Quiz()
    {
        
        qtime = quiztime;
        state = State.ready;
        state = State.quiz;
        answer1Place.SetActive(true);
        answer2Place.SetActive(true);
        trashcan1code = (int)(long)webTest.getData(0, "trashCanObjects1", question);
        trashcan2code = (int)(long)webTest.getData(0, "trashCanObjects2", question);
        quizItem = (string)webTest.getData(0, "quizItem", question);
        collectanswer = (int)(long)webTest.getData(0, "answer", question);
        answer1.DataPlay();
        answer2.DataPlay();

        //쓰레기통 생성 코드. 간단하게 짤 수 있으면 부탁함.
        trashcan1 = Instantiate(trashcans[trashcan1code]);
        trashcan2 = Instantiate(trashcans[trashcan2code]);
        trashcan1.transform.position = trashcan1place.position;
        trashcan1.transform.rotation = trashcan1place.rotation;
        trashcan2.transform.position = trashcan2place.position;
        trashcan2.transform.rotation = trashcan2place.rotation;
        StartCoroutine(TrashSpawn());
        question_text.text = (string)webTest.getData(1, "Text", ((int)(long)webTest.getData(0, "question", question)));

        if (PhotonNetwork.IsMasterClient) {
            Room ro= PhotonNetwork.CurrentRoom;
            ro.IsOpen = false;
        }

        //left_Text.text = question.left_Text;
        //right_Text.text = question.right_Text;

    }


    public void Answer(int myanswer)
    {
        answer = myanswer;
        if (state == State.quiz)
        {
            StartCoroutine(Result());
            //Result();
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
    IEnumerator Result()
    {
        yield return new WaitForSeconds(1f);
        if (PhotonNetwork.IsMasterClient)
        {
            ctime = timetoquestion - 2;
            state = State.result;
        }
        answer1Place.SetActive(false);
        answer2Place.SetActive(false);
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
        else if (answer == collectanswer)
        {
            Debug.Log("정답");
            question_text.text = (string)webTest.getData(0, "correctText", question);
            
            time.text = "Correct!";
            if(PhotonNetwork.IsMasterClient)
            {
                if(!resulted)
                {
                    resulted = true;
                    jungdab++;
                    currentquestion++;
                }
                
            }

        }
        else if(answer != collectanswer)
        {
            Debug.Log("오답");
            question_text.text = (string)webTest.getData(0, "wrongText", question);
            if (time)
            time.text = "Fail!";
            
            if (PhotonNetwork.IsMasterClient)
            {
                if (!resulted)
                {
                    resulted = true;
                    ohdab++;
                    currentquestion++;
                }
                
            }
        }

        
    }

    void Nextquiz()
    {
        resulted = false;
        ctime = timetoquestion;
        state = State.ready;
    }

    IEnumerator TrashSpawn()
    {
        yield return new WaitForSeconds(0.2f);
        trashobject = PhotonNetwork.Instantiate(quizItem, GameManager.instance.player.trashspawnpoint.position, GameManager.instance.player.trashspawnpoint.rotation);
        
    }

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
            //this.question = (int)stream.ReceiveNext();
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
