using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class npc_thinking : MonoBehaviour {

	public string thoughts;
	public string npc_name;


	// When the player collides with the npc, display their thoughts
	private void OnTriggerEnter2D(Collider2D collision) {
		if (collision.gameObject.CompareTag("Player")) {
			show_or_hide_dialogue(true);
		}
	}

	// When the player walks away from the npc, hide their thoughts
	private void OnTriggerExit2D(Collider2D collision) {
		if (collision.gameObject.CompareTag("Player")) {
			show_or_hide_dialogue(false);
		}
	}

	// If this is disabled, the dialogue should be hidden
	private void OnDisable() {
		show_or_hide_dialogue(false);
	}

	// Show/hide the dialogue
	private void show_or_hide_dialogue(bool show) {
		if (!show && dialogue_container.get_current_speaker() == npc_name) {
			dialogue_container.update_text(thoughts, npc_name, false);
		} else if (show) {
			dialogue_container.update_text(thoughts, npc_name, true);
		}
	}
}
