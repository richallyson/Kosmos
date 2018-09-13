using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Tutorial : MonoBehaviour {

    private Fade fade;
    private AudioManager audioManager;

    // Use this for initialization
    void Start () {
        fade = GetComponent<Fade>();
        audioManager = AudioManager.instance;
        audioManager.StopSound("titulo");

    }

    // Update is called once per frame
    void Update(){
        if (fade.done || Input.GetKeyDown("x")){
            audioManager.PlaySound("select");
            SceneManager.LoadScene("level1");
        }
    }
}
