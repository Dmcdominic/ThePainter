using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class boss_final_dialogue : MonoBehaviour {

	public GameObject painter_for_cam_focus;

	public List<GameObject> truths;
	public List<GameObject> lies;

	private bool already_played = false;

	// Static vars
	public static bool final_result;


	// Initialization [unused]
	private void init() {
		already_played = false;
	}

	// Trigger the cutscene when the player enters the collider
	private void OnTriggerEnter2D(Collider2D collision) {
		if (!already_played && collision.CompareTag("Player")) {
			final_result = check_result();
			play_cutscene();
		}
	}

	// Evaluate whether or not the player passed the test
	private bool check_result() {
		foreach (GameObject painter in truths) {
			if (!painter.activeSelf) {
				return false;
			}
		}

		foreach (GameObject painter in lies) {
			if (painter.activeSelf) {
				return false;
			}
		}

		return true;
	}

	// Show the cutscene based on whether or not you passed the test
	private void play_cutscene() {
		already_played = true;
		List<cutscene_bit> sequence;

		if (final_result) {
			// Success cutscene
			sequence = new List<cutscene_bit> {
				new cutscene_bit("Thank you. Thank you for giving me closure.", "The Painter", painter_for_cam_focus)
			};
		} else {
			// Failure cutscene
			sequence = new List<cutscene_bit> {
				new cutscene_bit("For some, it's because they have nothing left.", "The Painter", painter_for_cam_focus),
				new cutscene_bit("I have plenty in my life to go on with. But... I just can't do it.", "The Painter", null),
				new cutscene_bit("Not without him.", "The Painter", null)
			};
		}

		dialogue_container.start_cutscene(sequence);
	}
}
