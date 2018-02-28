using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TasoControl : MonoBehaviour {
	private int ajastin;
	private bool oikea;
	private int moveRange;
	
	void Start()
	{
		ajastin = 0;
		oikea = false;
		moveRange = Random.Range(25,75);
	}
	void FixedUpdate()
	{
		if (ajastin == -moveRange){
			oikea = true;
		}if (ajastin == moveRange){
			oikea = false;
		}

		if(oikea){
			ajastin++;
			transform.Translate( Vector3.right*Time.deltaTime);
		}else{
			ajastin--;
			transform.Translate( Vector3.left*Time.deltaTime);
		}	
			
	}
	
}
