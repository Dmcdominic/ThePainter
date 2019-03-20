using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class shelf_control : MonoBehaviour
{
    public GameObject shelf;

    // Start is called before the first frame update
    void Start()
    {
        
        
    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        painter player = collision.gameObject.GetComponent<painter>();
        if (player != null)
        {
            shelf.SetActive(false);
        }
    }
}
