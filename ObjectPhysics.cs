using UnityEngine;
using System.Collections;

//점프 o , 가속 , 워프, 감속,

public class ObjectPhysics : MonoBehaviour {
	public float velocity = 20f;
	// object type [0: default, 1:jump] 현재 타입을 기억하기 위한 변수
	public int objectType = 0;
	public GameObject Player;
	public GameObject Box;
	Vector3 jumpHeight = new Vector3(0, 5, 0);
	Vector3[] warpGrid = new Vector3[2];
	bool mHitMagnetic = false;
	Vector3 attatchPoint;
	
    public AudioClip jumpSound;
	public AudioClip accSound;
	public AudioClip magSound;
	public AudioClip warpSound;
	public AudioClip accJumpSound;
	

	
	
	void Start () {
		
		
	}
	
	
	void Update () {
		
	}
	
	
	void FixedUpdate () {
		
		if(objectType ==4){
			GameObject Player = GameObject.Find ("Player");	
			Transform player = Player.transform;
			
			float distance = Vector3.Distance(Vector3.right*player.position.x , transform.position.x*Vector3.right);
			
			
			float objectHeight = transform.position.y - player.position.y;
			
			
			
			
			if(distance < 0.5 && objectHeight > 1.2){
				if(!mHitMagnetic){
					player.rigidbody.AddForce (0,500,0);
					AudioSource.PlayClipAtPoint(magSound,transform.position);

				}
				else { 
					player.rigidbody.AddForce (0,0.001f,0,ForceMode.Force);
					AudioSource.PlayClipAtPoint(magSound,transform.position);
				}				
			}
			else{mHitMagnetic = false;}
			
	
			
		}
		
		//warpGrid = mouse.GetComponent<MouseController>().GetWarpPoint();
		//if(objectType) //애니메이션 메세지?
	}
	
	
		
		
		
	
	void OnCollisionEnter(Collision other){
		//
		
		
		PlayerController controller = other.gameObject.GetComponent<PlayerController>();
		if(objectType==0){other.collider.GetComponent<PlayerPhysics>().RemoveBoost();}
		if(objectType==1){
			
			bool temp = other.collider.GetComponent<PlayerPhysics>().HasBoost();
			if(temp){
				bool way = other.collider.GetComponent<PlayerPhysics>().GoingRight();
				if(way){other.collider.rigidbody.AddForce(2500,3500, 0);}
				else{other.collider.rigidbody.AddForce(-2500,3500, 0);}
			
				AudioSource.PlayClipAtPoint(accJumpSound,transform.position);
	
			}
			if(!temp){
				other.collider.rigidbody.AddForce(0, 2000, 0);
				AudioSource.PlayClipAtPoint(jumpSound,transform.position);
				}
			}
		//Player.transform.position += jumpHeight;
		//temp.y = 12;
		//rigidbody.velocity = temp;
		
		else if(objectType==2){
			other.collider.GetComponent<PlayerPhysics>().GiveBoost();

				bool temp = other.collider.GetComponent<PlayerPhysics>().GoingRight();
				if(temp){other.collider.rigidbody.AddForce(2500, 0, 0,ForceMode.Force);}
				else   				 {other.collider.rigidbody.AddForce (-2500,0,0,ForceMode.Force);}
				AudioSource.PlayClipAtPoint(accSound,transform.position);
		}
		else if(objectType==3){
			StartCoroutine(PlayerWarp(other.gameObject));
			
			
		}
		
		else if(objectType==4){
			attatchPoint = other.collider.transform.position;
			mHitMagnetic = true;	
		}
		
	}
	
	IEnumerator PlayerWarp(GameObject temp)
	{					GameObject mouse = GameObject.Find ("Mouse");

		//player.GetComponent<PlatformerAnimation>().PlayerDied();
		//temp = this.player;
		int doWarp= mouse.GetComponent<MouseController>().hasWarp();
		if(doWarp == 0){
			if(temp.name == "Player"){temp.GetComponent<PlayerController>().RemoveControl();}
				Vector3 enter = mouse.GetComponent<MouseController>().GetWarpPoint();
				Vector2 exit = mouse.GetComponent<MouseController>().GetWarpPoint2();
				Vector3 destination;
				AudioSource.PlayClipAtPoint(warpSound,transform.position);
				yield return new WaitForSeconds(1.0f);
					if(enter == transform.position){
					destination = exit;
					destination.y += 1.1f;
					temp.transform.position = destination;
					}
					else{
					
					destination = enter;
					destination.y += 1.1f;
					temp.transform.position = destination;
				
					}
			objectTypeToNone();
			mouse.GetComponent<MouseController>().resetWarp();
		}
			
		if(doWarp == 2){
			AudioSource.PlayClipAtPoint(warpSound,transform.position);
			yield return new WaitForSeconds(1.0f);	
			objectTypeToNone();
			
			
		
		}
		if(temp.name == "Player"){temp.GetComponent<PlayerController>().GiveControl();}
		
			//temp.GetComponent<PlayerPhysics>().Reset();
			//player.GetComponent<PlatformerAnimation>().PlayerLives();
			
	}
		
		
		
	
	void objectTypeToNone(){objectType = 0; this.renderer.material.color = Color.white;}
	void objectTypeToJump(){objectType = 1; this.renderer.material.color = Color.cyan;}
	void objectTypeToAcc() {objectType = 2; this.renderer.material.color = Color.magenta;}		
	void objectTypeToWarp() {objectType = 3; this.renderer.material.color = Color.blue;}
	void objectTypeToMagnetic() {objectType = 4; this.renderer.material.color = Color.black;}

	
	public int mObjectType() {return objectType;}
	 
	 
		
	}
	


