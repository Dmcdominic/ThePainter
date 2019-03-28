using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class invisible_wall_control : MonoBehaviour
{
    public GameObject invisible_wall_1;
    public GameObject trigger_1;



    // Start is called before the first frame update
    void Start()
    {


    }

    // Update is called once per frame
    void Update()
    {
        invisible_wall_1.SetActive(true);

        if (trigger_1.activeSelf == true)
        {
            invisible_wall_1.SetActive(false);
        }



    }


}
