using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class dialogue_yield_instruction : CustomYieldInstruction {
	public override bool keepWaiting {
		get {
			//return dialogue_container
			return !Input.anyKey;
		}
	}
}
