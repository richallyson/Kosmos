using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FocusOnIF : MonoBehaviour {
    private GameObject inputfield;
	// Use this for initialization
	void Start () {
        inputfield = GameObject.Find("InputField");
        inputfield.GetComponent<InputField>().ActivateInputField();

    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
