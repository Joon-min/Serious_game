using UnityEngine;
using System.Collections;

public class monsterController : MonoBehaviour {
	public int moveRange = 5;
	Vector3 presentPosition;
	Vector3 originPosition;
	int mGoingRight = 1;
	float distance;
	
	public AudioClip deathSound;
	public AudioClip boxPop;
	GameObject player;	

	
	
	
	
	
	
	
	// Use this for initialization
	void Start () {
		originPosition = transform.position;
		
	}
	
	
	
	void Update () {
		presentPosition = transform.position;
		distance = Vector3.Distance(originPosition, presentPosition);
		transform.position += mGoingRight * (transform.right/25) ;
		//Debug.Log(Time.frameCount);
		
		if(Time.frameCount % (moveRange*10) == 0){
			mGoingRight = mGoingRight*-1;
			
		}
	}
	
	
	
		
	void OnTriggerEnter(Collider other){
			player = GameObject.Find (other.name);
			StartCoroutine(PlayerDeath(other.gameObject));
		
		
			if(other.collider.tag == "Box"){
			BoxController control = other.collider.GetComponent<BoxController>();
			
			
			
			if(control.velCheck()){
			Destroy(other.gameObject);
			Destroy (gameObject);
			AudioSource.PlayClipAtPoint(boxPop, other.transform.position);
			}
		}	
			
			
		}
	
		
		
		//if(frame>moveRange*10)
		//frame=0;}
	IEnumerator PlayerDeath(GameObject temp)
	{
		//player.GetComponent<PlatformerAnimation>().PlayerDied();
		temp = this.player;
	
		if(temp.name=="Player"){
		temp.GetComponent<PlayerController>().RemoveControl();
		AudioSource.PlayClipAtPoint(deathSound, temp.transform.position);
		yield return new WaitForSeconds(2.0f);
		temp.GetComponent<PlayerPhysics>().Reset();
		//player.GetComponent<PlatformerAnimation>().PlayerLives();
		
		temp.GetComponent<PlayerController>().GiveControl();
		}
		
		
		
		else {
			/*BoxController control = temp.GetComponent<BoxController>();
			
			if(control.velCheck()){
			AudioSource.PlayClipAtPoint(boxPop, temp.transform.position);
			//yield return new WaitForSeconds(2.0f);
				temp.GetComponent<PlayerPhysics>().Reset();
			}	
			*/
		}
		
		
		
		
		
	}		

			
		
		
	
	



	
}
	
