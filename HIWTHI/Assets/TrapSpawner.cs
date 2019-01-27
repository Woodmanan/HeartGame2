using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrapSpawner : MonoBehaviour
{
    [SerializeField]
    private int numChoices;
    private int choice;
    private GameObject selected;
    private Object[] choices;
    
    // Start is called before the first frame update
    void Start()
    {
        choices = Resources.LoadAll("Prefabs/Traps", typeof(Object));
        selected = (GameObject)choices[choice];
        choice = 0;

        for (int i = 0; i < numChoices; i++)
        {
            choice = Random.Range(0, transform.childCount);
            Transform kid = transform.GetChild(choice);
            
            GameObject toSpawn = (GameObject)choices[Random.Range(0, choices.Length)];
            print("Spawning " + toSpawn.name + " to " + kid.position);
            GameObject spawned = Instantiate(toSpawn, kid.position, kid.rotation);
            spawned.GetComponent<TrapController>().isLethal = false;
            Destroy(kid.gameObject);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
