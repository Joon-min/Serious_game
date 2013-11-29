using UnityEngine;
using System.Collections;

public class DeathZone : MonoBehaviour
{
	
	public AudioClip deathSound;
	public AudioClip boxPop;
	GameObject player;	
	void OnTriggerEnter(Collider other)
	
	
	{
		player = GameObject.Find (other.name);
		if("Player"==player.name){
		
		//PlayerController controller = other.gameObject.GetComponent<PlayerController>();
		
			//let player die
			
			
		}
			
		//else {BoxController controller2 = other.gameObject.GetComponent<BoxController>();}

			StartCoroutine(PlayerDeath(other.gameObject));
		
	}
	
	
	
	
	
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
			BoxController control = temp.GetComponent<BoxController>();
			
			if(control.velCheck()){
			AudioSource.PlayClipAtPoint(boxPop, temp.transform.position);
			//yield return new WaitForSeconds(2.0f);
				temp.GetComponent<PlayerPhysics>().Reset();
			}	
		}
		
		
		
		
		
	}
}