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
    public string target = "heart";
    // Start is called before the first frame update
    void Start()
    {
        Transform heart = GameObject.FindGameObjectWithTag("Heart").transform;
        UnityEngine.AI.NavMeshAgent agent = GetComponent<UnityEngine.AI.NavMeshAgent>();
        coord = pos2coord(transform.position);
        des = pos2coord(heart.position);
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
        agent.SetDestination(transform.position);
    }

    public void setTarget(string newtarg)
    {
        target = newtarg;
    }

    int pos2coord(Vector3 pos) {
        Vector3 curr_pos = pos;
        var temp = new Vector2(curr_pos.x, curr_pos.y);
        //tmp.y should be greater than -6 and less than s
        int y = (int) temp.y;
        int row = ((y - 6) * -1) / 4;
        int x = (int) temp.x;
        int col = ((x - 6) * -1) / 4;
        return(row * 3 + col);
    }
    // Update is called once per frame
    void Update()
    {
        UnityEngine.AI.NavMeshAgent agent = GetComponent<UnityEngine.AI.NavMeshAgent>();
        if (target == "heart")
        {
            float currTime = -4.0f;
            if ((condition || condition2) && agent.remainingDistance <= 0.1)
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
    }
}
