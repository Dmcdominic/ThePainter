using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class businessman_suspicious_cutscene : MonoBehaviour {

	public GameObject tobias;
	public GameObject Shuji;

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
			new cutscene_bit("Psst... ", "Tobias (Businessman)", null),
			new cutscene_bit("Over here...", "Tobias (Businessman)", tobias),
			new cutscene_bit("I can tell you're looking for red hands around here. You should keep an eye on Shuji.", "Tobias (Businessman)", null),
			new cutscene_bit("He and John have been business partners for some years, I'm talking big money, but things went south recently.", "Tobias (Businessman)", Shuji),
			new cutscene_bit("He's been real shady since then...", "Tobias (Businessman)", tobias)
		};
		dialogue_container.start_cutscene(sequence);
	}
}
