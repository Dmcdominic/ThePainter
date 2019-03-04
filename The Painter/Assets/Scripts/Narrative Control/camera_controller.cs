using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Camera))]
public class camera_controller : MonoBehaviour {

	// Tracking settings
	public float acceleration;
	public float max_speed;
	public float start_movement_radius;
	public float stop_movement_radius;

	public float cam_adjust_time;

	// Private vars
	private Camera cam;
	private Vector2 velo;

	// The current object for the camrea to track
	public static Transform focus;


	// Init
	private void Awake() {
		cam = GetComponent<Camera>();
		velo = Vector2.zero;
	}

	// Jump to to_track, if it is already set
	private void Start() {
		if (focus) {
			set_pos_2d(focus.position);
		}
	}

	// Update is called once per frame
	void Update() {
		if (focus) {
			Vector2 displacement = focus.position - cam.transform.position;
			if (displacement.magnitude > 0.05f) {
				cam.transform.Translate(displacement * Time.deltaTime * cam_adjust_time);
			}
		}
	}

	// Sets the transform's position just according to x and y values, without touching the z value
	private void set_pos_2d(Vector2 new_pos) {
		set_pos_2d(new_pos.x, new_pos.y);
	}
	private void set_pos_2d(float x, float y) {
		transform.position = new Vector3(x, y, transform.position.z);
	}
}
