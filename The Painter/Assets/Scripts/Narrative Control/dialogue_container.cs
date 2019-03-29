using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class dialogue_container : MonoBehaviour {

	// Public fields
	public Image panel;
	public TextMeshProUGUI dialogue_text_obj;
	public TextMeshProUGUI speaker_text_obj;

	public Image continue_panel;
	public TextMeshProUGUI continue_text_obj;

	// Fade settings
	public float panel_fade_time;
	public float text_fade_delay;
	public float text_fade_time;
	public float fade_out_rate_mult;

	// Private vars
	private bool displaying;
	private float display_fade_in_timer;
	private string speaker;

	// Static vars
	public static float FULL_fade_time;
	public static bool text_showing;
	public static bool fully_displayed;

	public static bool waiting_for_input;


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

		FULL_fade_time = text_fade_delay * 2 + text_fade_time;
		waiting_for_input = false;
	}

	// Each frame, adjust the opacity of the panel and text
	private void Update() {
		if (displaying) {
			display_fade_in_timer += Time.deltaTime;
		} else {
			display_fade_in_timer -= Time.deltaTime * fade_out_rate_mult;
		}
		display_fade_in_timer = Mathf.Clamp(display_fade_in_timer, 0, FULL_fade_time);


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

		// Update static bools
		text_showing = new_dText_alpha > 0;
		fully_displayed = new_sText_alpha == 1;

		// Update the "press any button to continue" bubble
		continue_panel.gameObject.SetActive(waiting_for_input);
	}

	// Call this to update the dialogue display
	public static void update_text(string dialogue, string speaker, bool display) {
		Instance.dialogue_text_obj.text = dialogue;
		if (speaker != null && speaker != "") {
			Instance.speaker_text_obj.text = "- " + speaker;
			Instance.speaker = speaker;
		} else {
			Instance.speaker_text_obj.text = "";
			Instance.speaker = "";
		}
		Instance.set_display(display);
	}

	// Call with true to show dialogue, false to hide it
	public void set_display(bool display) {
		displaying = display;
		//panel.gameObject.SetActive(display);
	}

	// Check the current speaker or dialogue
	public static string get_current_speaker() {
		if (!Instance.displaying) {
			return "";
		}
		return Instance.speaker;
	}
	public static string get_current_dialogue() {
		//if (!Instance.displaying) {
		//	return "";
		//}
		return Instance.dialogue_text_obj.text;
	}

	// =========== Cutscene management ===========
	public static void start_cutscene(List<cutscene_bit> cutscene_Bits, bool fade_to_menu = false, bool fade_to_credits = false) {
		Instance.StartCoroutine(cutscene_sequence(cutscene_Bits, fade_to_menu, fade_to_credits));
	}

	private static IEnumerator cutscene_sequence(List<cutscene_bit> cutscene_Bits, bool fade_to_menu = false, bool fade_to_credits = false) {
		// Setup
		movement.set_movement_enabled(false);

		// Iterate over the dialogue list
		foreach (cutscene_bit bit in cutscene_Bits) {
			yield return new wait_until_dialogue_hidden();
			update_text(bit.dialogue, bit.speaker, !bit.cam_only);
			if (bit.cam_focus) {
				camera_controller.focus = bit.cam_focus.transform;
			}
			if (bit.cam_size != 0) {
				camera_controller.target_size = bit.cam_size;
			}

			yield return new wait_until_dialogue_ready();
			waiting_for_input = true;
			yield return new wait_until_any_input();
			waiting_for_input = false;
			update_text(bit.dialogue, bit.speaker, false);
		}

		// If we are fading to menu or credits, do so now
		if (fade_to_menu) {
			black_overlay.fade_to_black();
			yield return new WaitForSeconds(black_overlay.total_fade_time + 0.5f);
			SceneManager.LoadScene(1);
			yield break;
		} else if (fade_to_credits) {
			black_overlay.fade_to_black();
			yield return new WaitForSeconds(black_overlay.total_fade_time + 0.5f);
			SceneManager.LoadScene("Final Credits");
			yield break;
		}
		
		// Wrap up before returning
		camera_controller.focus = movement.player_instance.transform;
		yield return new wait_until_dialogue_hidden();
		yield return new WaitUntil(() => camera_controller.velo.magnitude < 0.2f);
		movement.set_movement_enabled(true);
	}
}

// Used to construct a list of dialogue lines and camera movements
// which make up a single cutscene
public struct cutscene_bit {
	public string dialogue;
	public string speaker;
	public GameObject cam_focus;
	public float cam_size;
	public bool cam_only;
	public cutscene_bit(string _dialogue, string _speaker, GameObject _cam_focus, float _cam_size = 0, bool _cam_only = false) {
		dialogue = _dialogue;
		speaker = _speaker;
		cam_focus = _cam_focus;
		cam_size = _cam_size;
		cam_only = _cam_only;
	}
}
