using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class paintable : MonoBehaviour {

	// Public fields
	public List<GameObject> to_paint_in;
	public SpriteRenderer indicator_sprite;

	// Private vars
	private bool show_indicator = false;

	// Static settings
	public static float indicator_fade_time = 0.4f;


	// Init the indicator as hidden
	private void Start() {
		set_indicator_alpha(0);
	}

	// Regularly adjust the opacity of the indicator
	private void Update() {
		float alpha_incr = (show_indicator ? 1f : -1f) / indicator_fade_time * Time.deltaTime;
		adjust_indicator_alpha(alpha_incr);
	}

	// Call this to paint in all the corresponding objects
	public void paint_in() {
		foreach (GameObject GO in to_paint_in) {
			if (GO != null && !GO.activeSelf) {
				GO.SetActive(true);
                FindObjectOfType<sound_manager>().playPaintingSound();
			}
		}
	}

	// Show paintable indicator when touching player
	private void OnTriggerEnter2D(Collider2D collision) {
		painter Painter = collision.gameObject.GetComponent<painter>();
		if (Painter != null) {
			show_indicator = true;
		}
	}

	// Hide paintable indicator when not touching player
	private void OnTriggerExit2D(Collider2D collision) {
		painter Painter = collision.gameObject.GetComponent<painter>();
		if (Painter != null) {
			show_indicator = false;
		}
	}

	// Set the alpha of the indicator sprite
	private void set_indicator_alpha(float a) {
		a = Mathf.Clamp(a, 0, 1);
		Color col = indicator_sprite.color;
		indicator_sprite.color = new Color(col.r, col.g, col.b, a);
	}
	private void adjust_indicator_alpha(float delta) {
		Color col = indicator_sprite.color;
		set_indicator_alpha(col.a + delta);
	}
}
