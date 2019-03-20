using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class wait_until_dialogue_hidden : CustomYieldInstruction {
	public override bool keepWaiting {
		get {
			return dialogue_container.text_showing;
		}
	}
}
