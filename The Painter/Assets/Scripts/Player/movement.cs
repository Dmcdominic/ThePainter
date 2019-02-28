using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(Rigidbody2D), typeof(Collider2D), typeof(Animator))]
public class movement : MonoBehaviour {

	// Public fields
	public float x_mult;

	public float jump_velo;
	public float falling_grav_mult;
	public float raycast_dist;

	public float cam_adjust_time;

	// Static settings
	public static bool movement_enabled = true;
	public static int current_scene = 1;
	public static movement player_instance;

	// Private vars
	private readonly string horizontal_axis = "Horizontal";
	private readonly string vertical_axis = "Vertical";

	private float base_grav_scale;
	private bool jump_held = false;

	private int landing_layer_mask;

	// Component references
	private Rigidbody2D rb;
	private Collider2D col;
	private Animator animator;


	// Init
	void Awake() {
		if (player_instance == null) {
			player_instance = this;
		} else if (player_instance != this) {
			Destroy(gameObject);
			return;
		}

		rb = GetComponent<Rigidbody2D>();
		col = GetComponent<Collider2D>();
		animator = GetComponent<Animator>();

		base_grav_scale = rb.gravityScale;
		landing_layer_mask = LayerMask.GetMask(new string[] { "platform" });
	}

	public static void full_init() {
		movement_enabled = true;
		current_scene = 1;
		// Call other full init methods here(?)
	}

	// Called once per frame
	void FixedUpdate() {
		// Reset the current_scene
		current_scene = SceneManager.GetActiveScene().buildIndex;

		if (!movement_enabled) {
			camera_track();
			rb.velocity = Vector3.zero;
			return;
		}

		// Get input
		float x_input = Input.GetAxis(horizontal_axis);
		float x_input_raw = Input.GetAxisRaw(horizontal_axis);
		float y_input_raw = Input.GetAxisRaw(vertical_axis);

		// Horizontal movement
		transform.Translate(Mathf.Abs(x_input) * (-1f) * x_mult * Time.deltaTime, 0, 0);
		//rb.velocity += new Vector2(x_input * x_mult * Time.deltaTime, 0);

		// Jump controls
		Vector3 feet_pos = new Vector3(transform.position.x, col.bounds.min.y);
		RaycastHit2D raycast = Physics2D.Raycast(feet_pos, Vector2.down, raycast_dist, landing_layer_mask);
		bool can_jump = raycast.collider != null && rb.velocity.y <= 0;
		
		if (can_jump && y_input_raw > 0 && !jump_held) {
			jump();
		}

		if (y_input_raw <= 0) {
			jump_held = false;
		} else {
			jump_held = true;
		}

		// Fall through platforms if you are holding down
		//col.enabled = y_input >= 0;

		// Check if you should be running
		if (Mathf.Abs(x_input_raw) > 0 && Mathf.Abs(rb.velocity.y) < 0.05f) {
			//animator.SetBool("running", true);
		} else {
			//animator.SetBool("running", false);
		}

		// Check if you should be jumping
		if (rb.velocity.y > 0.5f) {
			//animator.SetBool("jumping", true);
		} else {
			//animator.SetBool("jumping", false);
		}

		// Check if you should be falling
		if (rb.velocity.y < 0) {
			rb.gravityScale = base_grav_scale * falling_grav_mult;
			//animator.SetBool("falling", true);
		} else {
			//animator.SetBool("falling", false);
			if (y_input_raw > 0) {
				rb.gravityScale = base_grav_scale;
			} else {
				rb.gravityScale = base_grav_scale * falling_grav_mult;
			}
		}

		// Transform flip
		if (x_input > 0) {
			transform.rotation = Quaternion.Euler(new Vector3(transform.rotation.x, 180f, transform.rotation.z));
		} else if (x_input < 0) {
			transform.rotation = Quaternion.Euler(new Vector3(transform.rotation.x, 0, transform.rotation.z));
		}

		// Camera tracking
		camera_track();
	}

	// Todo - move this tracking system to the camera itself,
	// with a public static GameObject "focus" which can be set at different times,
	// so we can more easily do dynamic camera work
	private void camera_track() {
		Transform cam = Camera.main.transform;
		Vector2 displacement = transform.position - cam.position;
		if (displacement.magnitude > 0.05f) {
			Camera.main.transform.Translate(displacement * Time.deltaTime * cam_adjust_time);
		}
	}

	public void set_movement_enabled(bool enabled) {
		movement_enabled = enabled;
		// todo - freeze timeScale too? instead of the grav_scale change below?
		rb.gravityScale = enabled ? base_grav_scale : 0;
	}

	// Jump!
	private void jump() {
		rb.velocity = new Vector2(rb.velocity.x, jump_velo);
		//SoundManager.instance.playJump();
		// todo - jump sound
	}
}

