using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CouchTrap : MonoBehaviour
{
    [SerializeField]
    private int dmg;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag.Equals("Player") || collision.gameObject.tag.Equals("Enemy"))
        {
            if (collision.gameObject.tag.Equals("Player"))
            {
                collision.gameObject.GetComponent<PlayerController>().takeDamage(dmg);
            }
        }
    }
}
