using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    [SerializeField]
    private int health;

    private Vector2 direction;

    public float angle = 0;
    public int souls_collected = 0;
    public string text_extension = "";
    public bool spawned = false;
    public bool isWalking = true;

    private Animator animator;
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        souls_collected += 15;
        despawn();
    }

    public int pos2coord(Vector3 pos)
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

    //I probably shouldnt have hard coded this but if it works, it works
    public Vector2 angle2direction(float ang)
    {
        if (ang == 0)
        {
            return (new Vector2(1, 0));
        }
        else if (ang == 45)
        {
            return (new Vector2(1, 1));
        }
        else if (ang == 90)
        {
            return (new Vector2(0, 1));
        }
        else if (ang == 135)
        {
            return (new Vector2(-1, 1));
        }
        else if (ang == 180)
        {
            return (new Vector2(-1, 0));
        }
        else if (ang == 225)
        {
            return (new Vector2(-1, -1));
        }
        else if (ang == 270)
        {
            return (new Vector2(0, -1));
        }
        else if (ang == 315)
        {
            return (new Vector2(1, -1));
        }
        else if (ang == 360)
        {
            return (new Vector2(1, 0));
        }
        return (new Vector2(0, 0));
    }

    public void spawn()
    {
        animator.enabled = true;
        GetComponent<SpriteRenderer>().color = new Color32(255, 255, 255, 255);
        spawned = true;
        health = 0;
    }

    public void despawn()
    {

        transform.position = new Vector3(0, -1, 0);
        animator.enabled = false;
        GetComponent<SpriteRenderer>().color = new Color32(69, 69, 69, 69);
        spawned = false;
    }

    // Update is called once per frame
    void Update()
    {
        
        //numericalScore.GetComponent<UnityEngine.UI.Text>().text = "Souls Collected : " + souls_collected;
        GameObject counter = GameObject.FindGameObjectWithTag("soulcount");
        counter.GetComponent<UnityEngine.UI.Text>().text = "Souls : \n" + souls_collected + text_extension;
        direction = new Vector2(0, 0);
        if (!spawned)
        {
            if (Input.GetAxis("Interact") > .1 && souls_collected >= 10)
            {
                spawn();
                souls_collected -= 10;
            }
        }

        else if (isWalking && spawned)
        {
            if (Input.GetAxis("Horizontal") > .1)
            {
                transform.position += new Vector3(.05f, 0, 0);
                direction += new Vector2(1, 0);
            }
            if (Input.GetAxis("Horizontal") < -.1)
            {
                transform.position += new Vector3(-.05f, 0, 0);
                direction += new Vector2(-1, 0);
            }
            if (Input.GetAxis("Vertical") > .1)
            {
                transform.position += new Vector3(0, 0.05f, 0);
                direction += new Vector2(0, 1);
            }
            if (Input.GetAxis("Vertical") < -.1)
            {
                transform.position += new Vector3(0, -.05f, 0);
                direction += new Vector2(0, -1);
            }

            if (Input.GetAxis("Attack") > .1)
            {
                foreach (GameObject go in GameObject.FindGameObjectsWithTag("Enemy"))
                {
                    Vector3 pos = go.transform.position - transform.position;
                    if (pos.magnitude < 1)
                    {
                        go.GetComponent<Follow>().getHit();
                    }
                }
            }

            if (Input.GetAxis("Interact") > .1)
            {
                /*foreach (GameObject enemy in GameObject.FindGameObjectsWithTag("Enemy"))
                {

                        //transform.rotation.eulerangles.z
                }*/
                foreach(GameObject go in GameObject.FindGameObjectsWithTag("Enemy"))
                {
                    Vector3 mag = go.transform.position - transform.position;
                    if (mag.magnitude < 1.2)
                    {
                        go.GetComponent<Follow>().cube.GetComponent<MoveTo>().target = "player";
                    }
                }

                foreach (GameObject go in GameObject.FindGameObjectsWithTag("Door"))
                {
                    Vector3 mag = go.transform.position - transform.position;
                    if (mag.magnitude < .5)
                    {
                        go.GetComponent<Door>().closeDoor();
                    }
                }
            }
        }
        animator.SetFloat("speed", direction.magnitude);
        if (direction.magnitude != 0)
        {
            angle = 0;

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

                print("We are updating angle!");
                transform.rotation = Quaternion.Euler(0, 0, angle);
            }
        }

        


    }

    public void takeDamage(int dmg)
    {
        print("Player just took " + dmg + " damage");
        health -= dmg;
        if (health <= 0)
        {
            print("Oh no! The player has died!");
            despawn();
        }
    }
}
