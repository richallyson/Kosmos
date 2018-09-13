using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameMusic : MonoBehaviour {
    private AudioManager audioManager;
    public float fadeDuration;
    private float timer;
    [Range(0.0f, 1.0f)]
    public float maxVolume;
	// Use this for initialization
	void Start () {
        audioManager = AudioManager.instance;
        audioManager.PlaySound("miasma");
        audioManager.SetVolume(0);
        timer = 0;
	}
	
	// Update is called once per frame
	void Update () {
        timer += Time.deltaTime;
        if(timer < fadeDuration) {
            audioManager.SetVolume((timer / fadeDuration)*maxVolume);
        }
	}
}
