﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class Jumps : MonoBehaviour {

	public GameObject player;
	
	
	// Update is called once per frame
	void Update () {
		
		gameObject.GetComponent<TextMeshProUGUI>().SetText("Jumps left: "+ player.GetComponent<palyerJump>().laskuri);
	}
}
