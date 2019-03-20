using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class wait_until_dialogue_ready : CustomYieldInstruction {
	public override bool keepWaiting {
		get {
			return !dialogue_container.fully_displayed;
		}
	}
}
