using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class pre_boss_banter : MonoBehaviour {

	public GameObject painter_for_banter;
	public GameObject other_end_of_area;

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
			new cutscene_bit("Since you've made it this far, I think it's time you learn how you got here in the first place.", "???", painter_for_banter),
			new cutscene_bit("I won't give you my name, but... I created this piece. And I created you.", "The Painter", null),
			new cutscene_bit("If you really want to change Lady Vivian's fate, you must fully understand the circumstances.", "The Painter", null),
			new cutscene_bit("Prove to me that you know the whole truth. Wash away the lies, and only then may you realize your goal.", "The Painter", other_end_of_area)
		};
		dialogue_container.start_cutscene(sequence);
	}
}
