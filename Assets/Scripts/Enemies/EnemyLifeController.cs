using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyLifeController : MonoBehaviour {

    //HP do inimigo
    public int hp;

    //Total de pontos que o inimigo da ao player
    public int points;

    private GameObject player;

    public void Start()
    {
        player = GameObject.Find("Kosmos");
        if (player == null)
        {
            Debug.Log("Kosmos não foi encontrado");
        }
    }

    public void damage(int amount)
    {
        hp -= amount;
        if (hp <= 0)
            Death();

    }

    private void Death()
    {
        Destroy(gameObject);

        //adciona os pontos ao player
        player.GetComponent<Player>().IncreaseScore(points);
        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            collision.gameObject.GetComponent<Player>().TakeDamage(1);
            //Death();
            knockBack(5, collision.transform);
            //falta dar o knocBack no player também
        }
    }

    private void knockBack(float speed, Transform col)
    {
        float dif_x = col.position.x - transform.position.x;
        float dif_y = col.position.y - transform.position.y;

        float ang = -Mathf.Atan2(dif_y, dif_x);

        GetComponent<Rigidbody2D>().velocity = new Vector2(speed * Mathf.Cos(ang), speed * Mathf.Sin(ang));


    }
}
