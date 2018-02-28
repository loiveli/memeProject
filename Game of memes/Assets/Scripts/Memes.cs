using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class Memes : MonoBehaviour {

	public GameObject player;
	
	
	// Update is called once per frame
	void Update () {
		
		gameObject.GetComponent<TextMeshProUGUI>().SetText("Dank memes:"+ player.GetComponent<palyerJump>().legitPisteet);
	}
}