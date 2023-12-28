using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// �߻� Ŭ���� ����
public abstract class OrderBehaviour : MonoBehaviour
{
    public OrderBehaviour(int n)
    {
        // TimeList�� �ʱ�ȭ�Ǿ� �ִ��� Ȯ���ϰ� �ʱ�ȭ�Ǿ� ���� �ʴٸ� �ʱ�ȭ
        if (ExecuteOrder.TimeList[n] == null)
        {
            ExecuteOrder.TimeList[n] = new List<OrderBehaviour>();
        }
        ExecuteOrder.TimeList[n].Add(this);
    }
    public abstract void NumberAwake();
    public abstract void NumberStart();
}

