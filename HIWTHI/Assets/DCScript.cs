using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DCScript : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        GetComponent<MoveTo>().owner = transform.parent.gameObject;
        transform.parent = null;
        Destroy(this);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
