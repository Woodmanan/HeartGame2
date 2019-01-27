using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    [SerializeField]
    private int health;

    private Vector2 direction;

    public float angle = 0;
    public int souls_collected;

    private Animator animator;
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        souls_collected += 5;
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

    // Update is called once per frame
    void Update()
    {
        direction = new Vector2(0, 0);
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

        if (Input.GetAxis("Interact") > .1)
        {
            /*foreach (GameObject enemy in GameObject.FindGameObjectsWithTag("Enemy"))
            {
                
                    //transform.rotation.eulerangles.z
            }*/
            Vector2 dir = angle2direction(angle);
            if (Physics2D.Raycast(new Vector2(transform.position.x, transform.position.y), dir, 2.0f))
            {
                GameObject nearestEnemy = null;
                float min_dist = Mathf.Pow(2, 28);
                foreach (GameObject enemy in GameObject.FindGameObjectsWithTag("Enemy"))
                {
                    if (Vector3.Distance(transform.position, enemy.transform.position) < min_dist){
                        min_dist = Vector3.Distance(transform.position, enemy.transform.position);
                        nearestEnemy = enemy;
                    }
                 //transform.rotation.eulerangles.z
                }
                nearestEnemy.GetComponent<Follow>().interact();
            }
            else
            {

                print(new Vector3(dir.x, dir.y, 0));
                print(transform.position);
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
        }
    }
}
