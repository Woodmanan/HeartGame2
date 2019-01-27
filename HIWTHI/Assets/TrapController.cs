using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrapController : MonoBehaviour
{
    [SerializeField]
    private int dmg;
    public bool isLethal;
    public int cost;

    [SerializeField]
    private float cooldownTime;
    private bool cooldown;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

        GetComponent<Animator>().SetBool("Firing", false);
    }
    
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag.Equals("Player") || collision.gameObject.tag.Equals("Enemy"))
        {
            if (collision.gameObject.tag.Equals("Player"))
            {
                gameObject.GetComponent<Animator>().SetBool("firing", false);

            }
            else if (collision.gameObject.tag.Equals("Enemy"))
            {
                 gameObject.GetComponent<Animator>().SetBool("firing", false);
            }
        }
    }
    
    
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (isLethal && !cooldown)
        {
            if (collision.gameObject.tag.Equals("Player") || collision.gameObject.tag.Equals("Enemy"))
            {
                if (collision.gameObject.tag.Equals("Player"))
                {
                    collision.gameObject.GetComponent<PlayerController>().takeDamage(dmg);
                    
                }
                else if (collision.gameObject.tag.Equals("Enemy"))
                {
                    print("Enemy Triggered, printing before");
                    collision.gameObject.GetComponent<Follow>().getHit();
                    print("Enemy triggered, printing after");
                    
                }
                print("Trap triggered on " + collision.gameObject.name);
                cooldown = true;
                Invoke("offCooldown", cooldownTime);
                fire();
                print("Cooldown has been invoked");
                
            }
        }
        
    }

    private void offCooldown()
    {
        cooldown = false;
        
    }
    
    private void fire()
    {
        gameObject.GetComponent<Animator>().SetBool("firing", true);
        Invoke("stopFiringAssholes", 0.3f);
    }

    private void stopFiringAssholes()
    {
        print("Trap should have turned off it's animation");
        GetComponent<Animator>().SetBool("Firing", false);
        print("Trap just turned off it's animation");
    }
}
