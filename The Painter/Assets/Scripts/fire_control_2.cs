using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class fire_control_2 : MonoBehaviour
{
    public bool waterfall2;

    // Start is called before the first frame update
    void Start()
    {
        waterfall2 = false;
    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnParticleCollision(GameObject other)
    {
        waterfall2 |= other.gameObject.tag == "fire";
    }
}
