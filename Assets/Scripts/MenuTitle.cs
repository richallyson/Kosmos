﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuTitle : MonoBehaviour {
    private AudioManager audioManager;
    // Use this for initialization
    void Start () {
        audioManager = AudioManager.instance;
        audioManager.PlaySound("titulo");
    }
	
	// Update is called once per frame
	void Update () {
        if (Input.GetButtonDown("Fire1")) {
            audioManager.PlaySound("select");
            SceneManager.LoadScene("Tutorial");
        }
	}
}
