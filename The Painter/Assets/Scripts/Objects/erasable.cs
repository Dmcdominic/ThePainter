using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class erasable : MonoBehaviour {

	public List<GameObject> to_erase;


	// Call this to erase all the corresponding objects
	public void erase() {
		foreach (GameObject GO in to_erase) {
			if (GO != null && GO.activeSelf) {
				GO.SetActive(false);
			}
		}
	}
}
