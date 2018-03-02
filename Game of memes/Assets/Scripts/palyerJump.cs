using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class palyerJump : MonoBehaviour {
	private GameObject[] gameObjects;
	public Vector3 mousePos;
	// Use this for initialization
	public int tasoMitta;
	public GameObject meme;
	public GameObject Taso;
	public GameObject rightWall;
	public GameObject memeTaso;
	public GameObject savePoint;
	public float lataus;
	public int laskuri;
	public int oldLaskuri;
	public int pisteet;
	private SpriteRenderer nuoliRenderer;
	public Vector3 suunta;
	public Vector3 suuntaNorm;
	public int legitPisteet;
	public Rigidbody memeBody;
	public GameObject lastSave;
	public int tasot;
	public GameObject nuoli;
	void Start () {
		tasot = 0;
		memeBody = gameObject.GetComponent<Rigidbody>();
		laskuri = 5;
		oldLaskuri = 5;
		lataus = 50;
		tasoMitta = 5;
		rightWall.transform.localScale = new Vector3(1,tasoMitta +2, 1 );
		pisteet = 0;
		legitPisteet = 0;
		randomLevel(tasoMitta,0+1);
		nuoliRenderer= nuoli.GetComponent<SpriteRenderer>();
		nuoliRenderer.enabled = false;
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		mousePos= Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y,transform.position.z- Camera.main.transform.position.z ));
		suunta= transform.position-mousePos;
		suuntaNorm = suunta.normalized;
		nuoli.transform.localScale = new Vector3(1,lataus/30,1) ;
		if(Input.GetMouseButtonDown(0)){
			nuoliRenderer.enabled = true;
			
		}
		if(Input.GetMouseButtonUp(0)){
			nuoliRenderer.enabled = false;
			
		}
		if(Input.GetMouseButton(0)&&lataus < 100){
			
			lataus++;
		} if(Input.GetMouseButtonUp(0)&&memeBody.velocity == Vector3.zero){
			
			
			
			Launch(lataus/15, suuntaNorm);
			lataus = 50;
		}
		if(Input.GetKeyDown(KeyCode.Escape)){
			SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex+ -1);
		}
		if(Input.GetKeyDown(KeyCode.R)){
			randomLevel(tasoMitta,lastSave.transform.position.y+1);
		}
		if(laskuri <= 0){
			pisteet = 0;
			destroyWithTag("Taso");
			oldLaskuri++;
			laskuri = oldLaskuri;
		}
		transform.LookAt(mousePos);
		float angle = Mathf.Atan2(suunta.y, suunta.x) * Mathf.Rad2Deg;
		nuoli.transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle-90));
	}
	void Launch(float forceAmount, Vector3 dir){
		memeBody.AddForce(dir*forceAmount,ForceMode.Impulse);
		laskuri--;
	}
	
	
	void OnCollisionEnter(Collision other)
	{
		if(other.gameObject.tag == "Box"){
			laskuri = oldLaskuri;
			lastSave = other.gameObject;
			pisteet = 0;
		}
		if(other.gameObject.tag == "Wall"){
			laskuri = oldLaskuri;
			
			lastSave = other.gameObject;
			
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
			tasot++;
			tasoMitta++;
			other.gameObject.GetComponent<Collider>().isTrigger = false ;
			other.gameObject.tag = "Box";
			lastSave = other.gameObject;
			other.gameObject.GetComponent<MeshRenderer>().material.SetColor("_Color", Color.green);
			Instantiate(rightWall, new Vector3(-4.5f, other.transform.position.y +((tasoMitta+2)/2), 0), Quaternion.identity);
			Instantiate(rightWall, new Vector3(4.5f,other.transform.position.y +((tasoMitta+2)/2) , 0), Quaternion.identity);
			randomLevel(tasoMitta,lastSave.transform.position.y+1);
			legitPisteet += pisteet;
			pisteet = 0;
			
			rightWall.transform.localScale = new Vector3(1,tasoMitta +2, 1 );
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
					if((rngTaso <=1) && (tasot >5)){
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
