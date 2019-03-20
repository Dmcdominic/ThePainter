using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class boss_exit_warning : MonoBehaviour {

	public GameObject painter_for_cam_focus;

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
			new cutscene_bit("Remember, once you cross this threshold, there is no going back. You have one chance to wash away the lies.", "The Painter?", painter_for_cam_focus)
		};
		dialogue_container.start_cutscene(sequence);
	}
}
