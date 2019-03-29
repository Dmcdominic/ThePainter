using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class final_credits : MonoBehaviour {

	public float initial_delay;
	public float cam_size_delta;
	public float cam_size_accel;

	public Color light_background;
	public Color dark_background;

	public GameObject good_ending;
	public GameObject bad_ending;

	// Private vars
	private float delay_timer;
	private float cam_current_delta;


	// Start is called before the first frame update
	void Start() {
		// TESTING!
		//boss_final_dialogue.final_result = true;

		good_ending.SetActive(boss_final_dialogue.final_result);
		bad_ending.SetActive(!boss_final_dialogue.final_result);
		Camera.main.backgroundColor = boss_final_dialogue.final_result ? light_background : dark_background;
		delay_timer = 0f;
	}

	// Update is called once per frame
	void Update() {
		if (delay_timer < initial_delay) {
			delay_timer += Time.deltaTime;
			return;
		}

		if (cam_current_delta < cam_size_delta) {
			cam_current_delta += cam_size_accel * Time.deltaTime;
			cam_current_delta = Mathf.Clamp(cam_current_delta, 0f, cam_size_delta);
		}
		Camera.main.orthographicSize += cam_current_delta * Time.deltaTime;
		print(cam_current_delta);
	}
}
