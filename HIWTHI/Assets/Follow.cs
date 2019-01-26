using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Follow : MonoBehaviour
{
    [SerializeField]
    private GameObject cube;

    public string target = "entrance";
    public float currTime = -4.0f;
    Vector3 destination;
    // Start is called before the first frame update
    void Start()
    {
        if (transform.position.x < 0)
        {
            destination = new Vector3(-6.37f, -0.3f, 0);
        }
        else {
            destination = new Vector3(5.73f, -0.3f, 0);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (target == "heart"){
            Transform cube = GameObject.FindGameObjectWithTag("Enemy").transform;
            transform.position = cube.position;
        }
        else if (target == "entrance"){
            if (destination.x > transform.position.x)
            {
                transform.position += new Vector3(0.1f, 0, 0);
                currTime = Time.time;
            }
            else if (Time.time > currTime + 2)
            {
                target = "cube";
            }
        }
        else if (target == "cube")
        {
            Transform cube = GameObject.FindGameObjectWithTag("Enemy").transform;
            destination = cube.position;
            if (destination.x - transform.position.x > -0.1f)
            {
                transform.position += new Vector3(0.1f, 0, 0);
                
            }
            else
            {
                target = "heart";
                cube.GetComponent<MoveTo>().setTarget("heart");
                print("the hell?");
            }
        }
    }
}
