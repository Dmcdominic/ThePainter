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

	// Call this to update the dialogue display
	public static void update_text(string dialogue, string speaker, bool display) {
		Instance.dialogue_text_obj.text = dialogue;
		Instance.speaker_text_obj.text = "- " + speaker;
		Instance.speaker = speaker;
		Instance.set_display(display);
	}

	// Call with true to show dialogue, false to hide it
	public void set_display(bool display) {
		panel.gameObject.SetActive(display);
		// todo - fade the panel and text in/out? Quickly tho
	}

	// Check the current speaker
	public static string get_current_speaker() {
		return Instance.speaker;
	}
}
