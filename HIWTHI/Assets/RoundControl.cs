using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RoundControl : MonoBehaviour
{
    private int round;
    private float timeTillNext;

    [SerializeField]
    private GameObject Enemy;

    [SerializeField]
    private Text gui;

    private bool waiting;
    
    // Start is called before the first frame update
    void Start()
    {
        round = 0;
        timeTillNext = Time.time + 10;
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Time.time < timeTillNext)
        {
            waiting = false;
        }
        else
        {
            if (!waiting)
            {
                //Do a round change
                round++;
                gui.text = "Round: " + round;
                for (int i = 0; i < round * 2 - 1; i++)
                {
                    Invoke("spawnRandom", Random.Range(.2f, 20));
                }
                waiting = true;
            }
            else
            {
                if (GameObject.FindGameObjectsWithTag("Enemy").Length == 0)
                {
                    timeTillNext = Time.time + 10;
                    
                }
            }
        }
    }

    private void spawnRandom()
    {
        float angle = Random.Range(0, Mathf.PI * 2);
        GameObject justSpawned = Instantiate(Enemy);
        justSpawned.transform.position = 10 * new Vector3(Mathf.Cos(angle), Mathf.Sin(angle), 0);
    }
}
