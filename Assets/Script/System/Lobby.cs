using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lobby : MonoBehaviour
{
    [SerializeField]
    private GameObject ui;
    public GameObject UI { get { return ui; } set { ui = value; } }
}
