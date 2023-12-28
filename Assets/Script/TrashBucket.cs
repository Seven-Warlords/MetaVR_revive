using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrashBacket : MonoBehaviour
{
    public GameObject[] catchObj;
    public int trashType = 0;

    private bool shootTrashToPlayer=true;
    private void Awake()
    {
        catchObj = new GameObject[4];
    }
    // Start is called before the first frame update
    void Start()
    {
            
    }

    // Update is called once per frame
    void Update()
    {
        int checkCatchObj = 0;
        for(int i = 0; i < catchObj.Length; i++)
        {
            if (catchObj[i] != null)
            {
                checkCatchObj++;
            }
        }
        if (checkCatchObj == 4 && shootTrashToPlayer)
        {
            StartCoroutine(ThrowTrash());
        }
    }
    IEnumerator ThrowTrash()
    {
        shootTrashToPlayer = false;
        yield return new WaitForSeconds(2f);
        int i = 0;
        while (i < catchObj.Length)
        {
            Trash trash;
            if (catchObj[i].TryGetComponent<Trash>(out trash))
            {
                    Debug.Log("test2");
                if (trash.trashTag != (Trash.TrashTag)trashType)
                {
                    Debug.Log("test1");
                    trash.transform.position = gameObject.transform.position + Vector3.up * 3f;
                    catchObj[i].SetActive(true);
                    trash.distance = Vector3.Distance(transform.position, trash.catchPlayer.transform.position);
                    trash.direction = trash.catchPlayer.transform.position - transform.position;
                    trash.shootTrigger = true;
                    catchObj[i] = null;
                }
            }
            yield return new WaitForSeconds(0.5f);
            i++;
        }
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Trash"))
        {
            Rigidbody shootTrash;
            if (collision.gameObject.TryGetComponent<Rigidbody>(out shootTrash))
            {
                shootTrash.velocity = Vector3.zero;
            }
            collision.gameObject.SetActive(false);
            Push<GameObject>(collision.gameObject, catchObj);
        }
    }
    int  Push<T>(T t, T[] arrT)
    {
        for(int i = 0; i < arrT.Length; i++)
        {
            if (arrT[i] == null)
            {
                arrT[i] = t;
                return 1;
            }
        }
        return 0;
    }
}
