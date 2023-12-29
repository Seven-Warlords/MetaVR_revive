using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Question", menuName = "Scriptable Object/QuestionData")]
public class Question : ScriptableObject
{
    [Header("#Question Type")]
    public GameObject trash;
    public int correct = 1;
    public string question;
    public string correcttext;
    public string all_Failtext;
    public string half_Failtext;

    public string left_Text;
    public string right_Text;
    
}
