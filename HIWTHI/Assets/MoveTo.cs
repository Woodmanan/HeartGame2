using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class MoveTo : MonoBehaviour
{
    public int coord;
    public int des;
    public int[] path;
    public int index;
    public bool condition = false;
    public bool condition2 = false;
    public bool condition3 = false;
    public string target = "door";
    public float currTime = -4.0f;
    GameObject owner;
    // Start is called before the first frame update
    void Start()
    {
        Transform heart = GameObject.FindGameObjectWithTag("Heart").transform;
        UnityEngine.AI.NavMeshAgent agent = GetComponent<UnityEngine.AI.NavMeshAgent>();

        owner = transform.parent.gameObject;
        if (Vector3.Equals(new Vector3(-6.28f, 0, 0), closer(transform.position, new Vector3(-6.28f, 0, 0), new Vector3(6.28f, 0, 0)))){
            coord = 5;
        }
        else
        {
            coord = 3;
        }
        des = pos2coord(heart.position);

        print("Des: " + des);
        print("Coord: " + coord);
        if (coord == 5)
        {
            if (des == 0)
            {
                path = new int[4] { 5, 2, 1, 0 };
            }
            if (des == 4)
            {
                path = new int[4] { 5, 2, 1, 4 };
            }
            if (des == 8)
            {
                path = new int[6] { 5, 2, 1, 4, 7, 8 };
            }
        }
        else if (coord == 3)
        {
            if (des == 0)
            {
                path = new int[6] { 3, 6, 7, 4, 1, 0 };
            }
            if (des == 4)
            {
                path = new int[4] { 3, 6, 7, 4 };
            }
            if (des == 8)
            {
                path = new int[4] { 3, 6, 7, 8 };
            }
        }

        index = 0;

        //var strt = new Vector3((float)((((path[index]) % 3) - 1) * -4), (float)((((path[index]) / 3) - 1) * -4), transform.position.z);
        agent.SetDestination(closer(transform.position, new Vector3(-6.28f, 0, 0), new Vector3(6.28f, 0, 0)));
    }

    public void startTime()
    {
        currTime = Time.time;
    }

    public void setTarget(string newtarg)
    {
        target = newtarg;
    }

    int pos2coord(Vector3 pos)
    {
        Vector3 curr_pos = pos;
        var temp = new Vector2(curr_pos.x, curr_pos.y);
        //tmp.y should be greater than -6 and less than s
        int y = (int)temp.y;
        int row = ((y - 6) * -1) / 4;
        int x = (int)temp.x;
        int col = ((x - 6) * -1) / 4;
        return (row * 3 + col);
    }

    Vector3 closer(Vector3 source, Vector3 p1, Vector3 p2)
    {
        if (Vector3.Distance(source, p1) < Vector3.Distance(source, p2))
        {
            return p1;
        }
        else
        {
            return p2;
        }
    }

    // Update is called once per frame
    void Update()
    {
        UnityEngine.AI.NavMeshAgent agent = GetComponent<UnityEngine.AI.NavMeshAgent>();
        if (target == "door")
        {
            
            //agent.SetDestination(closer(transform.position, new Vector3(-6.28f, 0, 0), new Vector3(6.28f, 0, 0)));
            print(agent.remainingDistance);

            if (agent.remainingDistance <= 0.1)
            {
                target = "enter";
                currTime = Time.time;
                agent.isStopped = true;
            }
            
        }
        else if (target == "enter")
        {
            if (Time.time > currTime + 4)
            {
                agent.isStopped = false;
                agent.SetDestination(transform.position + new Vector3(-2 * Mathf.Abs(transform.position.x) / transform.position.x, 0, 0));
                condition3 = true;
                target = "heart";
            }
        }
        else if (target == "heart")
        {
            float currTime = -4.0f;
            if (condition3)
            {
                //print("Hey look at this");
                //foreach (int x in path) print(x);
                var strt = new Vector3((float)((((path[index]) % 3) - 1) * -4), (float)((((path[index]) / 3) - 1) * -4), transform.position.z);
                agent.SetDestination(strt);
                condition3 = false;
            }
            else if ((condition || condition2) && agent.remainingDistance <= 0.1)
            {
                agent.isStopped = true;
                condition = false;
                currTime = Time.time;
                if (condition2)
                {
                    currTime = currTime + 1;
                    condition2 = false;
                }
            }
            else if (Time.time > currTime + 4 && agent.isStopped)
            {
                agent.isStopped = false;
                currTime = -4.0f;
            }
            else if (agent.remainingDistance <= 0.1 && path.Length - 1 > index)
            {
                if (Random.Range(0.0f, 1.0f) > 0.32)
                {
                    var strt = new Vector3((float)((((path[index]) % 3) - 1) * -4) + 2 * Random.Range(-100.0f, 100.0f) / 100.0f, (float)((((path[index]) / 3) - 1) * -4) + 2 * Random.Range(-100.0f, 100.0f) / 100.0f, transform.position.z);
                    agent.SetDestination(strt);
                    int maxLoops = 100;
                    int count = 0;
                    while ((!agent.hasPath || agent.remainingDistance > 4.5 || Vector3.Distance(transform.position, strt) < 1) && count < maxLoops)
                    {
                        strt = new Vector3((float)((((path[index]) % 3) - 1) * -4) + 2 * Random.Range(-100.0f, 100.0f) / 100.0f, (float)((((path[index]) / 3) - 1) * -4) + 2 * Random.Range(-100.0f, 100.0f) / 100.0f, transform.position.z);
                        agent.SetDestination(strt);
                        count++;
                    }
                    condition2 = true;
                }
                else
                {
                    int curr = path[index];
                    int next = path[index + 1];

                    var strt = new Vector3((float)((((curr) % 3) - 1) * -4) + 1.8f * ((curr % 3) - (next % 3)), (float)((((curr) / 3) - 1) * -4) + 1.8f * ((curr / 3) - (next / 3)), transform.position.z);

                    condition = true;
                    index++;
                    agent.SetDestination(strt);
                }
            }
            else if (agent.remainingDistance <= 0.1 && path.Length - 1 == index)
            {
                var strt = new Vector3((float)((((path[index]) % 3) - 1) * -4), (float)((((path[index]) / 3) - 1) * -4), transform.position.z);
                agent.SetDestination(strt);
                index++;
            }
        }
        else if (target == "exit")
        {
            if (Time.time > currTime + 1)
            {
                agent.SetDestination(closer(transform.position, new Vector3(-9.28f, 0, 0), new Vector3(9.28f, 0, 0)));
                target = "escape";
            }
        }        
        else if (target == "player")
        {
            GameObject player = GameObject.FindGameObjectWithTag("Player");
            Vector2 dir = player.GetComponent<PlayerController>().angle2direction(player.GetComponent<PlayerController>().angle);
            agent.SetDestination(player.transform.position + new Vector3(dir.x * -.5f, dir.y * -.5f, transform.position.z));
        }
    }
}
