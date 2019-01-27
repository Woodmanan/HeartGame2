﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Follow : MonoBehaviour
{
    public GameObject cube;

    public string target = "heart";
    public float currTime = -4.0f;
    private float dodge_prob = 1.0f;//0.3f;
    public bool panicked = false;
    public float angle;
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
        angle = 0;
    }

    public void getHit()
    {
        print("Enemy has been hit");
        if (Random.Range(0.0f, 100.0f) / 100.0f <= dodge_prob)
        {
            print("OHNO IVE BEEN HIT");
            //insert a death animation of some kind
            GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>().souls_collected++;
            Destroy(cube);
            Destroy(this.gameObject);
        }
        else
        {
            panicked = true;
            cube.GetComponent<UnityEngine.AI.NavMeshAgent>().SetDestination(new Vector3(GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>().angle2direction(angle).x * 1.5f, GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>().angle2direction(angle).y * 1.5f, cube.transform.position.z));
            cube.GetComponent<MoveTo>().setTarget("heart");
            cube.GetComponent<UnityEngine.AI.NavMeshAgent>().speed = 3.6f;
            dodge_prob = 0.5f;
        }

    }

    public void interact()
    {
        if (!panicked)
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
    }

    // Update is called once per frame
    void Update()
    {

        GameObject heart = GameObject.FindGameObjectWithTag("Heart");
        if (Vector3.Distance(transform.position, heart.transform.position) < 1)
        {
            target = "wait what";
            heart.GetComponent<HeartController>().game_over();
        }
        //print("Current target for " + gameObject.name + " is " + target);
        if (target == "heart"){

            transform.position = cube.transform.position;
            GetComponent<Animator>().SetFloat("Speed", cube.GetComponent<NavMeshAgent>().velocity.magnitude);


            Vector3 direction = cube.GetComponent<NavMeshAgent>().velocity;

            if (direction.magnitude != 0)
            {
                angle = 0;
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
                transform.rotation = Quaternion.Euler(0, 0, angle);
            }
                

        }
    }
}
