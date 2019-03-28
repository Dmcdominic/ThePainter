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
	public float deccel_mult;

	// Private vars
	private Camera cam;
	public static Vector2 velo { get; private set; }

	// The current object for the camera to track
	public static Transform focus;

	// The target size for the camera
	public static float target_size;


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
			// If we are trying to deccelerate or turning sharply, do it faster than normal
			if (Vector2.Angle(accel_direction, velo) > 60f) {
				velo += acceleration * deccel_mult * Time.deltaTime * accel_direction;
			} else {
				velo += acceleration * Time.deltaTime * accel_direction;
			}
		}

		// Todo - update camera size based on target_size

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
