﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPlayer : MonoBehaviour {
    private GameObject player;
	// Use this for initialization
	void Start () {
        player = GameObject.Find("Kosmos");
	}
	
	// Update is called once per frame
	void Update () {
        transform.position = player.transform.position;
	}
}
