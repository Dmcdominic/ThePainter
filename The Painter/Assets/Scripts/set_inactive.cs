using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class set_inactive : MonoBehaviour
{
    public GameObject paintable_obj;
    // Start is called before the first frame update
    void Start()
    {
        paintable_obj.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
