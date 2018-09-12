using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Atirador2Behaviour : MonoBehaviour {

    public GameObject bullet;
    public float bullet_speed;

    private float shoot_timer = 0.0f;
    public float shoot_cooldown = 1.0f;

    public float mov_speed;
    private AudioManager audioManager;

    // Use this for initialization
    void Start () {
        audioManager = AudioManager.instance;
    }
	
	// Update is called once per frame
	void Update () {
        if (GetComponent<EnemyLifeController>().dying)
            return;
        if (Time.time - shoot_timer >= shoot_cooldown)
        {
            shoot();
            shoot_timer = Time.time;
        }

        transform.position = new Vector2(transform.position.x - mov_speed * Time.deltaTime, transform.position.y);
    }

    private void shoot()
    {
        audioManager.PlaySound("alien1 shoot");
        GameObject b = Instantiate(bullet);
        //GameObject.Find("Main Camera").GetComponent<ShakeCamera>().Shake(0.04f, 0.04f);
        b.transform.position = transform.Find("BulletPosition").position;
        b.GetComponent<Rigidbody2D>().velocity = new Vector2(-bullet_speed, 0);

    }
}
