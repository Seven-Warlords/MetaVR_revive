using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.PackageManager;
using UnityEngine;

public class ExecuteOrder : MonoBehaviour
{
    public static List<OrderBehaviour>[] TimeList=new List<OrderBehaviour>[5];
    
    private void Awake()
    {
        for(int i = 0; i < TimeList.Length; i++)
        {
            if (ExecuteOrder.TimeList[i] == null)
            {
                ExecuteOrder.TimeList[i] = new List<OrderBehaviour>();
            }
            if (TimeList[i].Count == 0)
            {
                continue;
            }
            else
            {
                try
                {
                    for(int j = 0; j < TimeList[i].Count; j++)
                    {
                        TimeList[i][j].NumberAwake();
                    }
                }
                catch (Exception e)
                {
                    Debug.LogError(e);
                }
            }
        }

    }
    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < TimeList.Length; i++)
        {
            if (TimeList[i].Count == 0)
            {
                continue;
            }
            else
            {
                try
                {
                    for (int j = 0; j < TimeList[i].Count; j++)
                    {
                        TimeList[i][j].NumberStart();
                    }
                }
                catch (Exception e)
                {
                    Debug.LogError(e);
                }
            }
        }
    }
}
