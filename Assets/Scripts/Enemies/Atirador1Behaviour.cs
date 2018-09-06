using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Atirador1Behaviour : MonoBehaviour {

    public GameObject bullet;
    public float bullet_speed;

    private float shoot_timer = 0.0f;
    private float shoot_cooldown = 0.5f;

    private float move_timer = 0.0f;
    private float move_cooldown = 1.0f;

    public int x_step = 5;

    private GameObject player;

	// Use this for initialization
	void Start () {
        shoot_timer = -100.0f;
        move_timer = -100.0f;

        player = GameObject.Find("Kosmos");
        if (player == null)
        {
            Debug.Log("Kosmos não foi encontrado");
        }
    }
	
	// Update is called once per frame
	void Update () {
		if (Time.time - shoot_timer >= shoot_cooldown) {
            shoot();
            shoot_timer = Time.time;
        }

        if (Time.time - move_timer >= move_cooldown) {
            moveToPlayer();
            move_timer = Time.time;
        }
	}

    //função que move o inimigo a direção do player
    private void moveToPlayer() {
        float y_p = player.transform.position.y;

        Vector2 n_pos = new Vector2(transform.position.x - x_step, y_p);
        transform.position = n_pos;
    }

    //função que faz o inimigo atirar
    private void shoot() {
        GameObject b = Instantiate(bullet);
        //GameObject.Find("Main Camera").GetComponent<ShakeCamera>().Shake(0.04f, 0.04f);
        b.transform.position = transform.Find("BulletPosition").position;
        b.GetComponent<Rigidbody2D>().velocity = new Vector2(-bullet_speed, 0);

    }
}
