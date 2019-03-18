using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class example_cutscene : MonoBehaviour {

	public GameObject waterfall_location;
	public GameObject that_guy_up_there;


	private void Start() {
		play_cutscene();
	}

	private void play_cutscene() {
		List<cutscene_bit> sequence = new List<cutscene_bit> {
			new cutscene_bit("Hi there. You must be new here. The name's Steve.", "Fake Steve", gameObject),
			new cutscene_bit("Don't talk much, huh? That's alright. One last thing before you go though...", "Fake Steve", null),
			new cutscene_bit("That waterfall up there is pretty sketchy, so be careful.", "Fake Steve", waterfall_location),
			new cutscene_bit("Oh and stay away from that guy up there too. He gives me the creeps.", "Fake Steve", that_guy_up_there)
		};
		dialogue_container.start_cutscene(sequence);
	}
}
