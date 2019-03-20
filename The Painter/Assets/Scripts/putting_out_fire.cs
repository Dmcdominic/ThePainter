using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class putting_out_fire : MonoBehaviour
{
    public GameObject fire;
    public GameObject button;
    public fire_control s1;
    public fire_control_2 s2;
    private bool a, b;

    // Start is called before the first frame update
    void Start()
    {


    }

    // Update is called once per frame
    void Update()
    {
        a = s1.waterfall1;
        b = s2.waterfall2;

        if (a && b)
        {
            fire.SetActive(false);
            button.SetActive(true);
            FindObjectOfType<sound_manager>().playPuttingOutFire();
        }
    }
}
