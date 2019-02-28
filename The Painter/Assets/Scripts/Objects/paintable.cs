using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class paintable : MonoBehaviour {

	public List<GameObject> to_paint_in;
	

	// Call this to paint in all the corresponding objects
	public void paint_in() {
		foreach (GameObject GO in to_paint_in) {
			if (GO != null && !GO.activeSelf) {
				GO.SetActive(true);
			}
		}
	}
}
