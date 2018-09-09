﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Atirador2Behaviour : MonoBehaviour {

    public GameObject bullet;
    public float bullet_speed;

    private float shoot_timer = 0.0f;
    private float shoot_cooldown = 1.0f;

    public float mov_speed;

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if (Time.time - shoot_timer >= shoot_cooldown)
        {
            shoot();
            shoot_timer = Time.time;
        }

        transform.position = new Vector2(transform.position.x - mov_speed * Time.deltaTime, transform.position.y);
    }

    private void shoot()
    {
        GameObject b = Instantiate(bullet);
        //GameObject.Find("Main Camera").GetComponent<ShakeCamera>().Shake(0.04f, 0.04f);
        b.transform.position = transform.Find("BulletPosition").position;
        b.GetComponent<Rigidbody2D>().velocity = new Vector2(-bullet_speed, 0);

    }
}