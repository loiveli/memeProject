using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class palyerJump : MonoBehaviour {
	private GameObject[] gameObjects;
	public Vector3 mousepos;
	// Use this for initialization
	public GameObject Taso;
	public GameObject savePoint;
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
			
			destroyWithTag("Taso");
			laskuri = 5;
		}
		
	}
	void Launch(float forceAmount, Vector3 dir){
		memeBody.AddForce(suunta*forceAmount,ForceMode.Impulse);
		laskuri--;
	}
	
	
	void OnCollisionEnter(Collision other)
	{
		if(other.gameObject.tag == "Box"){
			laskuri = 5;
			
		}
		if(other.gameObject.tag == "Wall"){
			laskuri = 5;
			destroyWithTag("Box");
			destroyWithTag("Taso");
			randomLevel(10, other.transform.position.y);
			
		}
	}
	
	public void destroyWithTag(string tag){
		gameObjects = GameObject.FindGameObjectsWithTag(tag);
		for(int i = 0;i<gameObjects.Length;i++){
			Destroy(gameObjects[i]);
		}

	}
	
	void OnTriggerExit(Collider other)
	{
		Debug.Log("collided with:"+ other.gameObject.tag);
		if(other.gameObject.tag == "Box"&&transform.position.y>other.transform.position.y){
			other.gameObject.GetComponent<Collider>().isTrigger = false ;
			other.gameObject.GetComponent<MeshRenderer>().material.SetColor("_Color", Color.green);
			laskuri = 5;
			randomLevel(10, other.transform.position.y);
		}
	}
	public void randomLevel(int lvlSize, float yStart){
		destroyWithTag("Taso");
		for( int i = 0; i<= lvlSize; i++){
			if(i < lvlSize){
				for(int m= 0;m <Random.Range(0,3);m++){
					Instantiate(Taso, new Vector3(Random.Range(-4.0f, 4.0f), yStart+i, 0), Quaternion.identity);
				}
				
			}else if (i == lvlSize){
				Instantiate(savePoint, new Vector3(0, yStart+i, 0), Quaternion.identity);
			}
	}
	
 }

}
