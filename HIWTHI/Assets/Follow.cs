using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Follow : MonoBehaviour
{
    [SerializeField]
    private GameObject cube;

    public string target = "heart";
    public float currTime = -4.0f;
    Vector3 destination;
    // Start is called before the first frame update
    void Start()
    {
        if (transform.position.x < 0)
        {
            destination = new Vector3(-6.37f, -0.3f, transform.position.z);
        }
        else {
            destination = new Vector3(5.73f, -0.3f, transform.position.z);
        }
    }

    public void interact()
    {
        if (cube.GetComponent<MoveTo>().target == "door" || cube.GetComponent<MoveTo>().target == "enter") 
        {
            cube.GetComponent<MoveTo>().setTarget("exit");
            cube.GetComponent<MoveTo>().startTime();
        }
        else if (cube.GetComponent<MoveTo>().target == "heart")
        {
            cube.GetComponent<MoveTo>().setTarget("player");
        }
    }

    // Update is called once per frame
    void Update()
    {
        //print("Current target for " + gameObject.name + " is " + target);
        if (target == "heart"){

            transform.position = cube.transform.position;
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
    }
}
