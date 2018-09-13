using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class SubmitScore : MonoBehaviour {

    public InputField inputField;

    public Button button;


	// Use this for initialization
	void Start () {
        //PlayerPrefs.SetInt("score", 2500);
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void submitScore() {
        string name = inputField.text;
        if (name == "")
        {
            return;
        }
        int score = 0;
        if (PlayerPrefs.HasKey("score")) {
            score = PlayerPrefs.GetInt("score");
        }
        
        GameScore gameScore = new GameScore();
        gameScore.name = name;
        gameScore.score = score;

        GetComponent<ScoreDataController>().AddScore(gameScore);
        SceneManager.LoadScene("Scores", LoadSceneMode.Single);
    }
}
