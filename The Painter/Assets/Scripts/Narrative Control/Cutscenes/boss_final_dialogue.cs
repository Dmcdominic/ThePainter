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


	// Init on awake
	private void Awake() {
		init();
	}

	// Initialization
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
				new cutscene_bit("Thank you.", "The Painter", painter_for_cam_focus),
				new cutscene_bit("I think you deserve a full explanation now. I am Vivian.", "The Painter", null),
				new cutscene_bit("Well, not exactly. My real name is not important, but I painted Vivian in my image...", "The Painter", null),
				new cutscene_bit("...because my husband was killed too.", "The Painter", null),
				new cutscene_bit("I had a difficult decision to make. I felt trapped. The only thing I could turn to was painting.", "The Painter", null),
				new cutscene_bit("So I created you, to help me. And you have.", "The Painter", movement.player_instance.gameObject),
				new cutscene_bit("The truth is there, of course. But the will to go on and be happy...", "The Painter", null),
				new cutscene_bit("That is what you have given me.", "The Painter", painter_for_cam_focus)
			};
		} else {
			// Failure cutscene
			sequence = new List<cutscene_bit> {
				new cutscene_bit("For some, it's because they have nothing left.", "The Painter", painter_for_cam_focus),
				new cutscene_bit("I still have much to go on with. But... I just can't do it.", "The Painter", null),
				new cutscene_bit("Not without him.", "The Painter", null)
			};
		}

		dialogue_container.start_cutscene(sequence, true);
	}
}
