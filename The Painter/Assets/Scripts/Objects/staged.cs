using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class staged : MonoBehaviour {

	public List<GameObject> staged_objects;

	public List<int> stages;


	// Refresh the staged objects every frame
	void Update() {
		set_all_active(stages.Contains(pocketwatch.stage));
	}

	// Update the status of all objects
	private void set_all_active(bool active) {
		foreach (GameObject obj in staged_objects) {
			obj.SetActive(active);
		}
	}
}
