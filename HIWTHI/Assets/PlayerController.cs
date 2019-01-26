using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    [SerializeField]
    private int health;

    private Vector2 direction;

    private Animator animator;
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
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


        animator.SetFloat("speed", direction.magnitude);

        float angle = 0;

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

            transform.rotation = Quaternion.Euler(0, 0, angle);
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
