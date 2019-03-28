using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class hit_player : MonoBehaviour
{
    public GameObject reset_position;
    public GameObject player;
    private int count;
   

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

        
    }

    void OnParticleCollision(GameObject other)
    {
        if (other.gameObject.tag == "Player")
        {
            count += 1;

            if (count == 6)
            {
                player.transform.position = reset_position.transform.position;
                count = 0;
            }
        }
    }
}
