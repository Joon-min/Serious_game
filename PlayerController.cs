using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {
	
	PlayerPhysics mPlayer;
	bool mHasControl;
	// Use this for initialization
	void Start () {
		mHasControl = true;
		mPlayer  = GetComponent<PlayerPhysics>();
		if(mPlayer == null){
			Debug.LogError("needs a Physics compo.");	
		}
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
	void FixedUpdate() {
		
		
		
		if(mPlayer && mHasControl)
		{
			
			mPlayer.Walk (Input.GetAxisRaw("Horizontal"));
		}

		

	}
	
	public void GiveControl() {mHasControl = true; }
	public void RemoveControl() {mHasControl = false; }
	public bool HasControl() { return mHasControl; }

	
	
}
