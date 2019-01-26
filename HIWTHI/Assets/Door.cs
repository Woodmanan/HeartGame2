﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    private bool open;
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
            openDoor(.35f);
        }
    }

    private void openDoor(float f)
    {
        GetComponent<Animator>().SetBool("Open", true);
        Invoke("toggleCollider", f);
        open = true;
    }

    private void closeDoor()
    {
        GetComponent<Animator>().SetBool("Open", false);
        Invoke("toggleCollider", .8f);
        open = false;
    }

    private void toggleCollider()
    {
        GetComponent<BoxCollider2D>().enabled = !GetComponent<BoxCollider2D>().enabled;
    }
}
