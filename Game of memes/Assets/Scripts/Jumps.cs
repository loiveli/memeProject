using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Jumps : MonoBehaviour {

	public GameObject player;
	public Text ScoreText;
	
	// Update is called once per frame
	void Update () {
		
		ScoreText.text= ("Jumps left: "+ (player.GetComponent<palyerJump>().laskuri).ToString());
	}
}
