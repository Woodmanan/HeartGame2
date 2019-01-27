using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    public bool open;
    public float delay;
    // Start is called before the first frame update
    void Start()
    {
        open = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        print("Door collided with " + collision.gameObject.name);
        if (collision.gameObject.tag.Equals("Player"))
        {
            openDoor(.35f);
            Invoke("closeDoor", 1.5f);
        }

        if (collision.gameObject.tag.Equals("Enemy"))
        {
            Invoke("openInitially", delay);
        }
    }

    public void openDoor(float f)
    {
        GetComponent<Animator>().SetBool("Open", true);
        Invoke("toggleCollider", f);
        
    }

    public void closeDoor()
    {
        GetComponent<Animator>().SetBool("Open", false);
        Invoke("colliderOn", .8f);
        
    }

    private void toggleCollider()
    {
        GetComponent<BoxCollider2D>().enabled = !GetComponent<BoxCollider2D>().enabled;
        open = !open;
    }

    private void colliderOn()
    {
        GetComponent<BoxCollider2D>().enabled = true;
        open = false;
    }

    private void openInitially()
    {
        GetComponent<Animator>().SetBool("Open", true);
        toggleCollider();
    }
}
