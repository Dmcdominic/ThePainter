using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class painter : MonoBehaviour {

	private bool paint_key_held;
	private bool erase_key_held;

	private Collider2D col;

	private List<paintable> touching_paintables;
	private List<erasable> touching_erasables;


	// Init
	void Awake() {
		col = GetComponent<Collider2D>();
	}

	// Main init
	private void Start() {
		touching_paintables = new List<paintable>();
		touching_erasables = new List<erasable>();
	}

	// Update is called once per frame
	void Update() {
		// Paint logic
		bool try_to_paint = Input.GetAxisRaw("Paint") > 0;
		if (try_to_paint && !paint_key_held) {
			try_paint();
		}
		paint_key_held = try_to_paint;

		// Erase logic
		bool try_to_erase = Input.GetAxisRaw("Erase") > 0;
		if (try_to_erase && !erase_key_held) {
			try_erase();
		}
		erase_key_held = try_to_erase;
	}

	// Paint anything paintable that you are colliding with
	private void try_paint() {
		foreach (paintable Paintable in touching_paintables) {
			Paintable.paint_in();
		}
	}
	
	// Erase anything erasable that you are colliding with
	private void try_erase() {
		foreach (erasable Erasable in touching_erasables) {
			Erasable.erase();
		}
	}

	// Add paintable/erasable objects to the list when you enter their colliders
	private void OnTriggerEnter2D(Collider2D collision) {
		paintable Paintable = collision.gameObject.GetComponent<paintable>();
		if (Paintable != null) {
			touching_paintables.Add(Paintable);
		}

		erasable Erasable = collision.gameObject.GetComponent<erasable>();
		if (Erasable != null) {
			touching_erasables.Add(Erasable);
		}
	}

	// Remove paintable/erasable objects to the list when you leave their colliders
	private void OnTriggerExit2D(Collider2D collision) {
		paintable Paintable = collision.gameObject.GetComponent<paintable>();
		if (Paintable != null) {
			touching_paintables.Remove(Paintable);
		}

		erasable Erasable = collision.gameObject.GetComponent<erasable>();
		if (Erasable != null) {
			touching_erasables.Remove(Erasable);
		}
	}
}
