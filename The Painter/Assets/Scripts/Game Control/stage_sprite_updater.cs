using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class stage_sprite_updater : MonoBehaviour {

	public Sprite sketch;
	public Sprite color;

	private SpriteRenderer sr;


	// Start is called before the first frame update
	void Start() {
		sr = GetComponent<SpriteRenderer>();
	}

	// Update is called once per frame
	void Update() {
		if (sr && color && sketch) {
			sr.sprite = pocketwatch.stage > 1 ? color : sketch;
		}
	}
}
