using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class trigger : MonoBehaviour
{

    // Public fields
    public GameObject door;
    public TextMeshProUGUI trigger1;
    public TextMeshProUGUI trigger2;
    public TextMeshProUGUI trigger3;
    public GameObject trig1Holder;
    public GameObject trig2Holder;
    public GameObject trig3Holder;
    public GameObject Player;

    bool[] trigger_order = { false, false, false };

    // Private fields
    private bool paint_key_held;

    private Vector2 playerV;
    private Vector2 trig1V;
    private Vector2 trig2V;
    private Vector2 trig3V;

    IEnumerator Wait()
    {
        yield return new WaitForSeconds(1);
        Start();
    }

    // Start is called before the first frame update
    private void Start() {
        trigger1.color = Color.white;
        trigger2.color = Color.white;
        trigger3.color = Color.white;

        print(trigger_order.Length);
    }

    // Update is called once per frame
    void Update() {

        // Establish vectors
        playerV = Player.transform.position;
        trig1V = trig1Holder.transform.position;
        trig2V = trig2Holder.transform.position;
        trig3V = trig3Holder.transform.position;

        bool try_to_paint = Input.GetAxisRaw("Paint") > 0;
        if (try_to_paint && !paint_key_held)
        {
            painting(trigger_order);
            check_order(trigger_order);
        }
    }

    public void painting(bool[] trigger_order) {
        if (Vector2.Distance(playerV, trig1V) < 1.0) {
            trigger1.color = Color.red;
            trigger_order[0] = true;
        } else if (Vector2.Distance(playerV, trig2V) < 1.0) {
            trigger2.color = Color.red;
            trigger_order[1] = true;
        } else if (Vector2.Distance(playerV, trig3V) < 1.0) {
            trigger3.color = Color.red;
            trigger_order[2] = true;
        }

        unlock_door(trigger_order);
    }

    public void check_order(bool[] trigger_order) {
        for (int i = 1; i <= trigger_order.Length; i++) {
            if (trigger_order[i] == true && trigger_order[i-1] == false) {
                StartCoroutine(Wait());
            }
        }
    }

    public void unlock_door(bool[] trigger_order) {
        print("IS the door open?");
        for (int i = 0; i <= trigger_order.Length; i++) {
            if (trigger_order[i] == false)
            {
                print("Not opened");
            } else if (i == 2 && trigger_order[2] == true)
            {
                print("Open sesame");
                door.SetActive(false);
            }
        }
    }

}
