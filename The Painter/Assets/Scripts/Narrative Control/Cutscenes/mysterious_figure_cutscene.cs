using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class mysterious_figure_cutscene : MonoBehaviour {

	public float figure_alpha_incr;

	public SpriteRenderer mysterious_figure;
	public GameObject paintable_indicator;
	public GameObject erasable_indicator;

	// Private vars
	private bool already_played = false;
	private bool cutscene_playing = false;
	private bool cutscene_concluded = false;

	private string paintable_showing_dialogue = "You can use this to develop your world further (using the 'q' button).";
	private string erasable_showing_dialogue = "You can use this to clear away whatever is in your path (using the 'e' button).";


	// Init on awake
	private void Awake() {
		init();
	}

	// Initialization
	private void init() {
		already_played = false;
		cutscene_playing = false;
		cutscene_concluded = false;
		Color col = mysterious_figure.color;
		mysterious_figure.color = new Color(col.r, col.g, col.b, 0);
		paintable_indicator.SetActive(false);
		erasable_indicator.SetActive(false);
		paintable_indicator.GetComponentInChildren<SpriteRenderer>().color = Color.white;
		erasable_indicator.GetComponentInChildren<SpriteRenderer>().color = Color.white;
	}

	// Trigger the cutscene when the player enters the collider
	private void OnTriggerEnter2D(Collider2D collision) {
		if (!already_played && collision.CompareTag("Player")) {
			play_cutscene();
		}
	}

	// Show the cutscene based on whether or not you passed the test
	private void play_cutscene() {
		already_played = true;
		List<cutscene_bit> sequence;

		sequence = new List<cutscene_bit> {
			new cutscene_bit("Wait.", "???", null),
			new cutscene_bit("Before you go, take these.", "???", mysterious_figure.gameObject),
			new cutscene_bit(paintable_showing_dialogue, "???", paintable_indicator),
			new cutscene_bit(erasable_showing_dialogue, "???", erasable_indicator),
			new cutscene_bit("Wait, who are you?", "Clara", movement.player_instance.gameObject),
			new cutscene_bit("All in due time, Clara...", "???", mysterious_figure.gameObject)
		};

		dialogue_container.start_cutscene(sequence);
		cutscene_playing = true;
	}

	// Update is called once per frame
	void Update() {
		if (!cutscene_playing && !already_played) {
			return;
		}
		if (cutscene_concluded) {
			incr_figure_alpha(-figure_alpha_incr * Time.deltaTime);
			return;
		}
		if (movement.movement_enabled && already_played) {
			paintable_indicator.SetActive(false);
			erasable_indicator.SetActive(false);
			cutscene_playing = false;
			cutscene_concluded = true;
			return;
		}

		incr_figure_alpha(figure_alpha_incr * Time.deltaTime);
		
		paintable_indicator.SetActive(dialogue_container.get_current_dialogue().Equals(paintable_showing_dialogue));
		erasable_indicator.SetActive(dialogue_container.get_current_dialogue().Equals(erasable_showing_dialogue));
	}

	// Update only the alpha of the mysterious figure's sprite renderer
	private void set_figure_alpha(float a) {
		Color col = mysterious_figure.color;
		float new_a = Mathf.Clamp(a, 0f, 1f);
		mysterious_figure.color = new Color(col.r, col.g, col.b, a);
	}
	private void incr_figure_alpha(float a_incr) {
		Color col = mysterious_figure.color;
		float new_a = Mathf.Clamp(col.a + a_incr, 0f, 1f);
		mysterious_figure.color = new Color(col.r, col.g, col.b, new_a);
	}
}
