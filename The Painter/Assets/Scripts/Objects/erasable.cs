using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class erasable : MonoBehaviour {

	// Public fields
	public List<GameObject> to_erase;
	public SpriteRenderer indicator_sprite;

	// Private vars
	private bool show_indicator = false;


	// Init the indicator as hidden
	private void Start() {
		set_indicator_alpha(0);
	}

	// Regularly adjust the opacity of the indicator
	private void Update() {
		float alpha_incr = (show_indicator ? 1f : -1f) / paintable.indicator_fade_time * Time.deltaTime;
		adjust_indicator_alpha(alpha_incr);
	}

	// Call this to erase all the corresponding objects
	public void erase() {
		foreach (GameObject GO in to_erase) {
			if (GO != null && GO.activeSelf) {
				GO.SetActive(false);
                FindObjectOfType<sound_manager>().playTurpentineSound();
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
