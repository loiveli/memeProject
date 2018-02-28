using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class palyerJump : MonoBehaviour {
	private GameObject[] gameObjects;
	public Vector3 mousepos;
	// Use this for initialization
	public int tasoMitta;
	public GameObject meme;
	public GameObject Taso;
	public GameObject rightWall;
	public GameObject memeTaso;
	public GameObject savePoint;
	public int lataus;
	public int laskuri;
	public int oldLaskuri;
	public int pisteet;
	public Vector3 suunta;
	public int legitPisteet;
	public Rigidbody memeBody;
	void Start () {
		memeBody = gameObject.GetComponent<Rigidbody>();
		laskuri = 5;
		oldLaskuri = 5;
		lataus = 50;
		tasoMitta = 10;
		rightWall.transform.localScale = new Vector3(1,tasoMitta +2, 1 );
		pisteet = 0;
		legitPisteet = 0;
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		if(Input.GetMouseButton(0)&&lataus < 100){
			
			lataus++;
		} if(Input.GetMouseButtonUp(0)&&memeBody.velocity == Vector3.zero){
			mousepos= Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y,transform.position.z- Camera.main.transform.position.z ));
			suunta = (transform.position-mousepos).normalized;
			
			Launch(lataus/10, suunta);
			lataus = 50;
		}
		if(laskuri <= 0){
			pisteet = 0;
			destroyWithTag("Taso");
			oldLaskuri++;
			laskuri = oldLaskuri;
		}
		
	}
	void Launch(float forceAmount, Vector3 dir){
		memeBody.AddForce(suunta*forceAmount,ForceMode.Impulse);
		laskuri--;
	}
	
	
	void OnCollisionEnter(Collision other)
	{
		if(other.gameObject.tag == "Box"){
			laskuri = oldLaskuri;
			randomLevel(tasoMitta, other.transform.position.y+1);
			pisteet = 0;
		}
		if(other.gameObject.tag == "Wall"){
			laskuri = oldLaskuri;
			
			destroyWithTag("Taso");
			randomLevel(tasoMitta, other.transform.position.y+1);
			pisteet = 0;
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
		if(other.gameObject.tag == "SavePlane"&&transform.position.y>other.transform.position.y){
			other.gameObject.GetComponent<Collider>().isTrigger = false ;
			other.gameObject.tag = "Box";
			other.gameObject.GetComponent<MeshRenderer>().material.SetColor("_Color", Color.green);
			Instantiate(rightWall, new Vector3(-4.5f, other.transform.position.y +((tasoMitta+2)/2), 0), Quaternion.identity);
			Instantiate(rightWall, new Vector3(4.5f,other.transform.position.y +((tasoMitta+2)/2) , 0), Quaternion.identity);
			legitPisteet += pisteet;
			pisteet = 0;
		}
	}
	
	void OnTriggerEnter(Collider other)
	{
		if(other.gameObject.tag== "DankMeme"){
			pisteet++;
			Destroy(other.gameObject);
		}
	}
	public void randomLevel(int lvlSize, float yStart){
		destroyWithTag("Taso");
		destroyWithTag("SavePlane");
		destroyWithTag("DankMeme");
		for( int i = 0; i<= lvlSize; i++){
			if(i < lvlSize){
				int rngTaso =Random.Range(0,3);
				for(int m= 0;m <rngTaso;m++){
					float rng = Random.Range(-4.0f, 4.0f);
					if(rngTaso <=1){
						Instantiate(memeTaso, new Vector3(rng, yStart+i, 0), Quaternion.identity);
					}else{
						Instantiate(Taso, new Vector3(rng, yStart+i, 0), Quaternion.identity);
					}

					if(Random.Range(0,10)>7.5){
						Instantiate(meme, new Vector3(rng, (yStart+i)+0.5f, 0), Quaternion.identity);
					
					}
					
				}
				
			}else if (i == lvlSize){
				Instantiate(savePoint, new Vector3(0, yStart+i, 0), Quaternion.identity);
				
			}
	}
	
 }

}
