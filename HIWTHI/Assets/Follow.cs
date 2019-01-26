using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

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
        print("Current target for " + gameObject.name + " is " + target);
        if (target == "heart"){
            Transform cube = GameObject.FindGameObjectWithTag("Enemy").transform;
            transform.position = cube.position;
            GetComponent<Animator>().SetFloat("Speed", cube.GetComponent<NavMeshAgent>().velocity.magnitude);

            float angle = 0;

            Vector3 direction = cube.GetComponent<NavMeshAgent>().velocity;

            if (direction.magnitude != 0)
            {
                if (direction.x == 0)
                {
                    if (direction.y > 0)
                    {
                        angle = 90;
                    }
                    else
                    {
                        angle = 270;
                    }
                }
                else
                {
                    angle = Mathf.Atan(direction.y / direction.x) * 180 / Mathf.PI;
                    if (direction.x < 0)
                    {
                        angle += 180;
                    }
                }
            }
                transform.rotation = Quaternion.Euler(0, 0, angle);

        }
        else if (target == "entrance"){
            if (destination.x > transform.position.x)
            {
                transform.rotation = Quaternion.Euler(0, 0, 0);
                GetComponent<Animator>().SetFloat("Speed", .2f);
                transform.position += new Vector3(0.1f, 0, 0);
                currTime = Time.time;
            }
            else if (Time.time > currTime + .2)
            {
                GetComponent<Animator>().SetBool("Knocking", true);
                transform.position += new Vector3(0.01f, 0, 0);

            }
            else if (Time.time > currTime + 3)
            {
                transform.position += new Vector3(-0.01f, 0, 0);
                
            }
            else
            {
                target = "cube";
            }
        }
        else if (target == "cube")
        {
            GetComponent<Animator>().SetBool("Knocking", false);
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
