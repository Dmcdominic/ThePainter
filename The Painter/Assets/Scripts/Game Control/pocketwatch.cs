using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class pocketwatch : MonoBehaviour {

	public static bool[] unlocked;
	public static int stage = 2;

	private bool forward_key_held;
	private bool back_key_held;


	// Init
	private void Awake() {
		// todo - init this with only last stage unlocked, then unlock the second one later
		// NO LONGER USING STAGE 0. ONLY STAGES 2 (full color) AND 1 (sketch)
		unlocked = new bool[3] { false, true, true };
	}

	// Update is called once per frame
	void Update() {
		// Stage forward/backward logic
		float input = Input.GetAxisRaw("Pocketwatch");
		bool forward = input > 0;
		bool backward = input < 0;

		if (forward && !forward_key_held) {
			shift_stage(true);
		} else if (backward && !back_key_held) {
			shift_stage(false);
		}

		forward_key_held = forward;
		back_key_held = backward;
	}

	// Swap to the the next, or previous, stage (if possible)
	private void shift_stage(bool forward) {
		int next_stage = 0;
		if (forward) {
			next_stage = stage + 1;
		} else {
			next_stage = stage - 1;
		}

		if (next_stage > 2 || next_stage < 0) {
			// Todo - negative audio feedback here?
			return;
		}

		// todo - transition or anything?
		stage = next_stage;

		print("New stage: " + stage);
	}
}
