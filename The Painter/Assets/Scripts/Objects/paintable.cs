using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class paintable : MonoBehaviour {

	//public Sprite indicator_sprite;
	public List<GameObject> to_paint_in;

	//private bool show_indicator = false;


	// Call this to paint in all the corresponding objects
	public void paint_in() {
		foreach (GameObject GO in to_paint_in) {
			if (GO != null && !GO.activeSelf) {
				GO.SetActive(true);
			}
		}
	}

	//// Show paintable indicator when touching player
	//private void OnTriggerEnter2D(Collider2D collision) {
	//	painter Painter = collision.gameObject.GetComponent<painter>();
	//	if (Painter != null) {
	//		show_indicator = true;
	//	}
	//}

	//// Hide paintable indicator when not touching player
	//private void OnTriggerExit2D(Collider2D collision) {
	//	painter Painter = collision.gameObject.GetComponent<painter>();
	//	if (Painter != null) {
	//		show_indicator = false;
	//	}
	//}
}
