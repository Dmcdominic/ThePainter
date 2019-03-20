using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class fire_control : MonoBehaviour
{
    public bool waterfall1;

    // Start is called before the first frame update
    void Start()
    {
        waterfall1 = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnParticleCollision(GameObject other)
    {
        waterfall1 |= other.gameObject.tag == "fire";
    }
}
