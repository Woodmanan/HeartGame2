using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrapController : MonoBehaviour
{
    [SerializeField]
    private int dmg;
    public bool isLethal;
    public int cost;    
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

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
                 gameObject.GetComponent<Animator>().SetBool("firing", true);
            }
        }
    }
    
    
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (isLethal)
        {
            if (collision.gameObject.tag.Equals("Player") || collision.gameObject.tag.Equals("Enemy"))
            {
                if (collision.gameObject.tag.Equals("Player"))
                {
                    collision.gameObject.GetComponent<PlayerController>().getHit(dmg);
                    gameObject.GetComponent<Animator>().SetBool("firing", true);
                    
                }
                else if (collision.gameObject.tag.Equals("Enemy"))
                {
                    collision.gameObject.GetComponent<Follow>().getHit();
                    gameObject.GetComponent<Animator>().SetBool("firing", true);
                }
            }
        }
        
    }
}
