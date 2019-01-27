using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class scrollin : MonoBehaviour
{

    //public AudioClip sound;

    string message;
    public bool cond = false;
    public float curr = -4.0f;
    private int count = 0;


    // Start is called before the first frame update
    void Start()
    {
        message = GetComponent<UnityEngine.UI.Text>().text;
        GetComponent<UnityEngine.UI.Text>().text = "";
    }

    // Update is called once per frame
    void Update()
    {
        print(cond);
        if (!cond)
        {
            GetComponent<UnityEngine.UI.Text>().text += message.ToCharArray()[count];
            cond = true;
            curr = Time.time;
            count++;
        }
        else
        {
            if (Time.time - curr > 0.05f && count < message.ToCharArray().Length)
            {
                cond = false;
            }
        }
        if (Time.time - curr > 1 && count >= message.ToCharArray().Length)
        {
            //add scene transition
        }
    }
}
