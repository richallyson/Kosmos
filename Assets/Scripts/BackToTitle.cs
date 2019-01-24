using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BackToTitle : MonoBehaviour {
    private AudioManager audioManager;
	// Use this for initialization
	void Start () {
        audioManager = AudioManager.instance;
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetButtonDown("Fire1")) {
            audioManager.StopSound("game over");
            SceneManager.LoadScene("Logo");
        }
	}
}
