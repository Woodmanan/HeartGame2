﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class HeartController : MonoBehaviour
{

    int count;
    int health;
    // Start is called before the first frame update
    void Start()
    {
        GetComponentInChildren<ParticleSystem>().Play();
        health = 5;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void teleport()
    {
        Vector3[] valid_locations = new Vector3[3] {new Vector3(0, 0, 0), new Vector3(3.66f, 3.66f, 0), new Vector3(-4.25f, -4.25f, 0)};
        transform.position = valid_locations[Random.Range(0, 3)];
    }

    public void game_over()
    {
        health--;
        if (health < 1)
        {
            print("Died! Loading new scene");
            SceneManager.LoadScene(3);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag.Equals("Enemy"))
        {
            GetComponent<Animator>().SetBool("Faster", true);
            
        }
        else if (collision.gameObject.tag.Equals("Player"))
        {
            GetComponent<Animator>().SetBool("Faster", false);
            GetComponent<Animator>().SetBool("Faster 2", false);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        print("We just left collision with " + collision.gameObject.name);
        if (collision.gameObject.tag.Equals("Player"))
        {
            GetComponent<Animator>().SetBool("Faster", true);
            GetComponent<Animator>().SetBool("Faster 2", true);
        }
    }
}
