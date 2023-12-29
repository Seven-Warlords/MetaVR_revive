using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Answercolor : MonoBehaviour
{
    public Image image;
    public Color color1;
    public Color color2;
    // Start is called before the first frame update
    void Start()
    {
        ImageClear();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ImageIn(int i)
    {
        Image newimage = Instantiate(image, transform);
        if(i == 1)
        {
            newimage.color = color1;
        }
        else if(i == 2)
        {
            newimage.color = color2;
        }
    }

    public void ImageClear()
    {
        Image[] images = gameObject.GetComponentsInChildren<Image>();

        foreach (Image i in images)
        {
            Destroy(i.gameObject);
        }
    }
}
