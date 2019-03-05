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
			dialogue_container.update_text(thoughts, npc_name, true);
		}
	}

	// When the player walks away from the npc, hide their thoughts
	private void OnTriggerExit2D(Collider2D collision) {
		if (collision.gameObject.CompareTag("Player")) {
			if (dialogue_container.get_current_speaker() == npc_name) {
				dialogue_container.update_text("", "", false);
			}
		}

	}
}
