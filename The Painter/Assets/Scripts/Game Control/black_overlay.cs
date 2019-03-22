using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class black_overlay : MonoBehaviour {

	// Private vars
	private bool show_overlay;
	private float display_fade_in_timer;

	private Image image;

	// Static vars
	public static float total_fade_time = 2f;


	// Static instance setup
	private static black_overlay Instance;

	private void Awake() {
		if (Instance != null && Instance != this) {
			Destroy(gameObject);
			return;
		}
		Instance = this;

		init();
	}

	// Init
	private void init() {
		image = GetComponent<Image>();
		set_alpha(0);
	}

	// Update is called once per frame
	void Update() {
		if (show_overlay) {
			display_fade_in_timer += Time.deltaTime;
		} else {
			display_fade_in_timer -= Time.deltaTime;
		}
		display_fade_in_timer = Mathf.Clamp(display_fade_in_timer, 0, total_fade_time);

		// Update opacity
		float new_alpha = Mathf.Lerp(0, 1, display_fade_in_timer / total_fade_time);
		set_alpha(new_alpha);
	}

	// Set the image alpha directly
	private void set_alpha(float new_alpha) {
		Color col = image.color;
		image.color = new Color(col.r, col.g, col.b, new_alpha);
	}

	// Fade in/out
	public static void fade_to_black() {
		Instance.set_alpha(0);
		Instance.show_overlay = true;
	}
	public static void fade_in_from_black() {
		Instance.set_alpha(1f);
		Instance.show_overlay = false;
	}
}
