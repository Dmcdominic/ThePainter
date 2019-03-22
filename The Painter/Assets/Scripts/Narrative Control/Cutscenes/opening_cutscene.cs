using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class opening_cutscene : MonoBehaviour {

	//public GameObject painter_for_banter;

	private bool already_played = false;


	// Initialization [unused]
	private void init() {
		already_played = false;
	}

	// Trigger the cutscene when the player enters the collider
	private void OnTriggerEnter2D(Collider2D collision) {
		if (!already_played && collision.CompareTag("Player")) {
			play_cutscene();
		}
	}

	private void play_cutscene() {
		already_played = true;
		List<cutscene_bit> sequence = new List<cutscene_bit> {
			new cutscene_bit("John is dead. And the wedding reception is in shambles.", "Clara", null),
			new cutscene_bit("Lady Vivian is devastated. I need to reach her, before things get worse.", "Clara", null),
			new cutscene_bit("And I need to find out who did this...", "Clara", null)
		};
		dialogue_container.start_cutscene(sequence);
	}
}
