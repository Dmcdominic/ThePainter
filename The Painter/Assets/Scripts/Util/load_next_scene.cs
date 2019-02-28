using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class load_next_scene : MonoBehaviour {

	public int scene_to_load = 1;


	// Start is called before the first frame update
	void Start() {
		SceneManager.LoadScene(scene_to_load);
	}
}
