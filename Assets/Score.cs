using UnityEngine;
using UnityEngine.UI;
public class Score : MonoBehaviour {
    private Player player;
    private Text score;
    private void Start() {
        player = GameObject.Find("Kosmos").GetComponent<Player>();
        score = GetComponent<Text>();
    }
    // Update is called once per frame
    void Update () {
        score.text = player.score.ToString();
	}
}
