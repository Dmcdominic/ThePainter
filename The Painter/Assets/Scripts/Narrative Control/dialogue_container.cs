using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class dialogue_container : MonoBehaviour {

	// Public fields
	public Image panel;
	public TextMeshProUGUI dialogue_text_obj;
	public TextMeshProUGUI speaker_text_obj;

	// Fade settings
	public float panel_fade_time;
	public float text_fade_delay;
	public float text_fade_time;
	public float fade_out_rate_mult;

	// Private vars
	private bool displaying;
	private float display_fade_in_timer;
	private string speaker;
	

	// Static instance setup
	private static dialogue_container Instance;

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
		set_display(false);
		dialogue_text_obj.text = "";
		speaker_text_obj.text = "";
	}

	// Each frame, adjust the opacity of the panel and text
	private void Update() {
		if (displaying) {
			display_fade_in_timer += Time.deltaTime;
		} else {
			display_fade_in_timer -= Time.deltaTime * fade_out_rate_mult;
		}
		display_fade_in_timer = Mathf.Clamp(display_fade_in_timer, 0, text_fade_delay*2 + text_fade_time);


		// Panel opacity
		float new_panel_alpha = Mathf.Lerp(0, 1, display_fade_in_timer / panel_fade_time);
		Color col = panel.color;
		panel.color = new Color(col.r, col.g, col.b, new_panel_alpha);

		// Dialogue text opacity
		float new_dText_alpha = Mathf.Lerp(0, 1, (display_fade_in_timer - text_fade_delay) / text_fade_time);
		col = dialogue_text_obj.color;
		dialogue_text_obj.color = new Color(col.r, col.g, col.b, new_dText_alpha);

		// Speaker text opacity
		float new_sText_alpha = Mathf.Lerp(0, 1, (display_fade_in_timer - text_fade_delay*2) / text_fade_time);
		col = speaker_text_obj.color;
		speaker_text_obj.color = new Color(col.r, col.g, col.b, new_sText_alpha);
	}

	// Call this to update the dialogue display
	public static void update_text(string dialogue, string speaker, bool display) {
		Instance.dialogue_text_obj.text = dialogue;
		Instance.speaker_text_obj.text = "- " + speaker;
		Instance.speaker = speaker;
		Instance.set_display(display);
	}

	// Call with true to show dialogue, false to hide it
	public void set_display(bool display) {
		displaying = display;
		//panel.gameObject.SetActive(display);
	}

	// Check the current speaker
	public static string get_current_speaker() {
		if (!Instance.displaying) {
			return "";
		}
		return Instance.speaker;
	}

	// =========== Cutscene management ===========
	public static void prompted_dialogue(string dialogue, string speaker) {
		// todo - take in a list of cutscene_bits instead
		update_text(dialogue, speaker, true);
		movement.set_movement_enabled(false);
		// todo - wait until dialogue_yield_instruction
	}

	//public IEnumerator test() {
	//	yield return new WaitForSeconds(10);
	//}
}

// Used to construct a list of dialogue lines and camera movements
// which make up a single cutscene
public struct cutscene_bit {
	public string dialogue;
	public string speaker;
	public GameObject cam_focus;
	public cutscene_bit(string _dialogue, string _speaker, GameObject _cam_focus) {
		dialogue = _dialogue;
		speaker = _speaker;
		cam_focus = _cam_focus;
	}
}
