using UnityEngine;
using System.Collections;

public class BoxController : MonoBehaviour {
	bool mHasVel = false;
	
	
	PlayerPhysics mPlayer;
	// Use this for initialization
	void Start () {
		mPlayer  = GetComponent<PlayerPhysics>();
		if(mPlayer == null){
			Debug.LogError("needs a Physics compo.");	
		}
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
	void FixedUpdate() {
		float direction = Input.GetAxisRaw("Horizontal");
		if(direction < 0 && mPlayer.mGoingRight){
			mPlayer.mGoingRight = false;
		}
		if(direction > 0 && mPlayer.mGoingRight == false){
			mPlayer.mGoingRight = true;
		}
		
	}
	
	
	public bool velCheck(){
		
		if(rigidbody.velocity != Vector3.zero){return true;}
		else{return false;}
		
		
	}
		

	public void Destroy(){ Collider.Destroy(this);}

}
