﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class level_shortcuts : MonoBehaviour {

	private void Awake() {
		DontDestroyOnLoad(this);
	}

	// Update is called once per frame
	void Update() {
		if (Input.GetKeyDown(KeyCode.Alpha1)) {
			SceneManager.LoadScene(1);
		} else if (Input.GetKeyDown(KeyCode.Alpha2)) {
			SceneManager.LoadScene(2);
		} else if (Input.GetKeyDown(KeyCode.Alpha3)) {
			SceneManager.LoadScene(3);
		}
	}
}
