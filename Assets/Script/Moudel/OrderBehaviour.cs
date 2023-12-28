using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 추상 클래스 정의
public abstract class OrderBehaviour : MonoBehaviour
{
    public OrderBehaviour(int n)
    {
        // TimeList가 초기화되어 있는지 확인하고 초기화되어 있지 않다면 초기화
        if (ExecuteOrder.TimeList[n] == null)
        {
            ExecuteOrder.TimeList[n] = new List<OrderBehaviour>();
        }
        ExecuteOrder.TimeList[n].Add(this);
    }
    public abstract void NumberAwake();
    public abstract void NumberStart();
}

