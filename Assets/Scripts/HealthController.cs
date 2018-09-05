using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthController : MonoBehaviour {
    private Heart heart1;
    private Heart heart2;
    private Heart heart3;
    // Use this for initialization
    void Start () {
        heart1 = transform.Find("Heart1").gameObject.GetComponent<Heart>();
        heart2 = transform.Find("Heart2").gameObject.GetComponent<Heart>();
        heart3 = transform.Find("Heart3").gameObject.GetComponent<Heart>();
    }
	
	// Update is called once per frame
	void Update () {
        
    }
    public void Full() {
        heart1.SetHeart(2);
        heart2.SetHeart(2);
        heart3.SetHeart(2);
    }
    public void SetBar(int n) {
        heart1.SetHeart(Mathf.Min(n, 2));
        heart2.SetHeart(Mathf.Min(n - 2, 2));
        heart3.SetHeart(Mathf.Min(n - 4, 2));
        print(n);
        print(n - 4);
    }
}
