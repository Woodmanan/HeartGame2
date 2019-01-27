using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class fadeout : MonoBehaviour
{
    bool condition = true;
    bool condition2 = true;
    float curr;
    // Start is called before the first frame update
    void Start()
    {
        curr = Time.time;
    }

    // Update is called once per frame
    void Update()
    {
        if (Time.time > curr + 2 && condition2)
        {
            condition = false;
            condition2 = false;
        }
        Color32 current = GetComponent<SpriteRenderer>().color;
        int alpha = current.a - 1;
        if (!condition)
        {
            GetComponent<SpriteRenderer>().color = new Color32(current.r, current.g, current.b, (byte)alpha);
        }
        if (alpha <= 0)
        {
            condition = true;
        }
    }
}
