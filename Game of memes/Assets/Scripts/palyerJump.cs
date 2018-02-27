using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class palyerJump : MonoBehaviour {
	public Vector3 mousepos;
	// Use this for initialization
	public int lataus;
	public int laskuri;
	public Vector3 suunta;
	public Rigidbody memeBody;
	void Start () {
		memeBody = gameObject.GetComponent<Rigidbody>();
		laskuri = 4;
		lataus = 30;
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		if(Input.GetMouseButton(0)&&lataus < 75){
			
			lataus++;
		} if(Input.GetMouseButtonUp(0)&&memeBody.velocity == Vector3.zero){
			mousepos= Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y,transform.position.z- Camera.main.transform.position.z ));
			suunta = (transform.position-mousepos).normalized;
			
			Launch(lataus/10, suunta);
			lataus = 30;
		}
		if(laskuri <= 0){
			Physics.IgnoreLayerCollision(8, 10, true);
		}else {
			Physics.IgnoreLayerCollision(8, 10, false);
		}
	}
	void Launch(float forceAmount, Vector3 dir){
		memeBody.AddForce(suunta*forceAmount,ForceMode.Impulse);
		laskuri--;
	}
	
	
	void OnCollisionEnter(Collision other)
	{
		if(other.gameObject.tag == "Box"){
			laskuri = 4;
		}
	}
	
	
	void OnTriggerExit(Collider other)
	{
		Debug.Log("collided with:"+ other.gameObject.tag);
		if(other.gameObject.tag == "Box"&&transform.position.y>other.transform.position.y){
			other.gameObject.GetComponent<Collider>().isTrigger = false ;
			other.gameObject.GetComponent<MeshRenderer>().material.SetColor("_Color", Color.green);
			laskuri = 0;
		}
	}
}
