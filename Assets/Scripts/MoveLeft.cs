using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveLeft : MonoBehaviour {
    private GameObject fundo1;
    private GameObject fundo2;
    public float speed;
	// Use this for initialization
	void Start () {
        fundo1 = transform.Find("Fundo1").gameObject;
        fundo2 = transform.Find("Fundo2").gameObject;
        print(fundo2.transform.position.x);
	}
	
	// Update is called once per frame
	void Update () {
        fundo1.transform.position = new Vector3(fundo1.transform.position.x - Time.deltaTime*speed, fundo1.transform.position.y, fundo1.transform.position.z);
        fundo2.transform.position = new Vector3(fundo2.transform.position.x - Time.deltaTime * speed, fundo2.transform.position.y, fundo2.transform.position.z);
        if(fundo1.transform.position.x < -40) {
            fundo1.transform.position = new Vector3(40, fundo1.transform.position.y, fundo1.transform.position.z);
        }
        if (fundo2.transform.position.x < -40) {
            fundo2.transform.position = new Vector3(40, fundo2.transform.position.y, fundo2.transform.position.z);
        }
    }
}
