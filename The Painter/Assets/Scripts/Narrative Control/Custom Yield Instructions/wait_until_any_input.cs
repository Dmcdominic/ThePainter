using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class wait_until_any_input : CustomYieldInstruction {
	public override bool keepWaiting {
		get {
			return !Input.anyKey;
		}
	}
}