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

	public float cam_adjust_speed;
	public float velo_margin_of_error;

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
		if (!focus) {
			velo = Vector2.zero;
			return;
		}

		// Check if you are close enough to stop moving,
		// or if you're already stopped and too close to start moving
		Vector2 displacement = focus.position - cam.transform.position;
		if (displacement.magnitude < stop_movement_radius) {
			velo = Vector2.zero;
			return;
		} else if (velo == Vector2.zero && displacement.magnitude < start_movement_radius) {
			return;
		}
		
		Vector2 target_velo = displacement * cam_adjust_speed;
		Vector2 accel_direction = (target_velo - velo).normalized;
		// If the velocity is within a certain percentage of the target velocity, no need to adjust
		if ((target_velo - velo).magnitude > target_velo.magnitude * velo_margin_of_error) {
			velo += acceleration * Time.deltaTime * accel_direction;
		}

		velo = Vector2.ClampMagnitude(velo, max_speed);
		cam.transform.Translate(velo * Time.deltaTime);
	}

	// Sets the transform's position just according to x and y values, without touching the z value
	private void set_pos_2d(Vector2 new_pos) {
		set_pos_2d(new_pos.x, new_pos.y);
	}
	private void set_pos_2d(float x, float y) {
		transform.position = new Vector3(x, y, transform.position.z);
	}
}
