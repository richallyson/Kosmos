using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraBehavior : MonoBehaviour {
    private GameObject player;
    private Vector2 target;
    public int factor;
	// Use this for initialization
	void Start () {
        player = GameObject.Find("Kosmos");
	}
	
	// Update is called once per frame
	void FixedUpdate () {
        target = new Vector2((player.transform.position.x + transform.position.x*factor) / (factor+1), (player.transform.position.y + transform.position.y * factor) / (factor + 1));
        transform.position = new Vector3(target.x, target.y, transform.position.z);
	}
}
