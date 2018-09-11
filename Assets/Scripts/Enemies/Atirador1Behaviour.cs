using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Atirador1Behaviour : MonoBehaviour {

    public GameObject bullet;
    public float bullet_speed;

    private float shoot_timer = 0.0f;
    private float shoot_cooldown = 0.5f;
    private float shootStop;
    private bool shooting;

    private float move_timer = 0.0f;
    private float move_cooldown = 1.0f;
    

    public int x_step = 5;

    private GameObject player;
    public float speed;
    private float currentSpeed;
    private float angle;

    private Rigidbody2D rb;

	// Use this for initialization
	void Start () {
        shoot_timer = -100.0f;
        move_timer = -100.0f;

        player = GameObject.Find("Kosmos");
        if (player == null)
        {
            Debug.Log("Kosmos não foi encontrado");
        }
        rb = GetComponent<Rigidbody2D>();
        currentSpeed = speed;
    }
	
	// Update is called once per frame
	void Update () {
        if (GetComponent<EnemyLifeController>().dying)
            return;
        if (!shooting) {
            currentSpeed = speed;
            if (player.transform.position.y - transform.position.y > 0.5f) {
                angle = 135;
            } else if (player.transform.position.y - transform.position.y < -0.5f) {
                angle = 225;
            } else {
                angle = 180;
                shooting = true;
                shootStop = shoot_cooldown*2;
            }
        }
        if(shooting){
            currentSpeed = 0;
            shootStop -= Time.deltaTime;
            if (Time.time - shoot_timer >= shoot_cooldown) {
                shoot();
                shoot_timer = Time.time;
            }
            if(shootStop <= 0) {
                shooting = false;
            }
        }
        rb.velocity = new Vector2(currentSpeed * Mathf.Cos(Mathf.Deg2Rad * angle), currentSpeed * Mathf.Sin(Mathf.Deg2Rad * angle));
        
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
