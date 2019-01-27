using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class PointerMovement : MonoBehaviour
{
    int temp = 0;
    int counter = 0;
    int placeMode = 0;
    int positionValid = 0;
    [SerializeField]
    private Sprite[] sprites;
    // Start is called before the first frame update
    [SerializeField]
    private Sprite defaultSprite;
    [SerializeField]
    private string Folder;
    [SerializeField]
    private Object[] choices;
    [SerializeField]
    private int choice;
    [SerializeField]
    private GameObject selected;
    [SerializeField]
    private Sprite selectedSprite;
    [SerializeField]
    private GameObject player;



    void start()
    {
        choices = Resources.LoadAll("Prefabs/" + Folder, typeof(Object));
        selectedSprite = (Sprite)sprites[choice];
        selected = (GameObject)choices[choice];
        choice = 0;
        transform.localScale = new Vector3(.2065864f, .2065864f, .2065864f);
        gameObject.GetComponent<BoxCollider2D>().offset = new Vector2(0, 0);
        gameObject.GetComponent<BoxCollider2D>().size = new Vector2(5, 10);
    }




    void PlaceTrap()
    {
        selected = (GameObject)choices[choice];
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (player.GetComponent<PlayerController>().souls_collected >= selected.GetComponent<TrapController>().cost)
        {
            player.GetComponent<PlayerController>().souls_collected -= selected.GetComponent<TrapController>().cost;
            GameObject newTrap = (GameObject)Instantiate(selected, transform.position, transform.rotation);
        }
        
    }

    // Update is called once per frame
    void Update()
    {

        if ((Input.GetAxis("Horizontal") > 0.1) && (placeMode == 1))
        {
            transform.position += new Vector3(.1f, 0, 0);
        }
        else if ((Input.GetAxis("Horizontal") < -0.1) && (placeMode == 1))
        {
            transform.position += new Vector3(-.1f, 0, 0);
        }

        if ((Input.GetAxis("Vertical") > 0.1) && (placeMode == 1))
        {
            transform.position += new Vector3(0, .1f, 0);
        }
        if ((Input.GetAxis("Vertical") < -0.1) && (placeMode == 1))
        {
            transform.position += new Vector3(0, -.1f, 0);
        }

        if (Input.GetKeyDown(KeyCode.M))
        {
            if (placeMode == 0)
            {
                placeMode = 1;
                GetComponent<SpriteRenderer>().enabled = true;
            }
            else
            {
                placeMode = 0;
                GetComponent<SpriteRenderer>().enabled = false;
            }
            player.GetComponent<PlayerController>().enabled = !(player.GetComponent<PlayerController>().enabled);
            player.GetComponent<BoxCollider2D>().enabled = !player.GetComponent<BoxCollider2D>().enabled;
        }

        if (placeMode == 1)
            {
                

                if (Input.GetKeyDown(KeyCode.R))
                {
                    counter = 0;
                    transform.Rotate(new Vector3(0, 0, 90), 90);
                }

                print(positionValid);
                if (Input.GetKeyDown(KeyCode.P))
                {
                    if (positionValid > 10)
                    {
                        PlaceTrap();
                    }
                    else
                    {
                        print("Unable to Place Trap");
                    }

                }


                if (Input.GetKeyDown(KeyCode.E))
                {
                    counter = 0;
                    choice = (choice + 1) % choices.Length;
                   if (choice == 0)
                    {
                        transform.localScale = new Vector3(.2065864f, .2065864f, .2065864f);
                        gameObject.GetComponent<BoxCollider2D>().offset = new Vector2(0, 0);
                        gameObject.GetComponent<BoxCollider2D>().size = new Vector2(5, 10);
                    }
                    if (choice == 1)
                    {
                        transform.localScale = new Vector3(.332f, .332f, .332f);
                        gameObject.GetComponent<BoxCollider2D>().offset = new Vector2(0, 0);
                        gameObject.GetComponent<BoxCollider2D>().size = new Vector2(2, 2.69f);
                    }
                    if (choice == 2)
                    {
                        transform.localScale = new Vector3(.380555f, .380555f, .380555f);
                        gameObject.GetComponent<BoxCollider2D>().offset = new Vector2(0, 0);
                        gameObject.GetComponent<BoxCollider2D>().size = new Vector2(2.5f, 2.5f);
                    }
                }
                counter += 2;
                if (counter % 100 < 50) { this.GetComponent<SpriteRenderer>().sprite = sprites[choice]; }
                else this.GetComponent<SpriteRenderer>().sprite = null;
            }


        }



    private void NewMethod()
    {
        placeMode = 0;
    }

    void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag.Equals("Wall"))
        {
            positionValid = 1000000;
        }
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag.Equals("Wall"))
        {
            positionValid = 0;
        }
    }


    void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.tag.Equals("Wall"))
        {
            positionValid = 0;
        }
    }

}


