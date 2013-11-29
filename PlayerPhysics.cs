using UnityEngine;
using System.Collections;
/*******/
/*class*/
/*******/
public class PlayerPhysics : MonoBehaviour {
	
	/***********/
	/*variables*/
	/***********/
	
	
	//variables : movement
	public float accelerationWalking   = 35;
	public float maxSpeedWalking       = 20;
	public float moveFriction          = 0.9f;
	public float speedToStopAt         = 0f;
	public float airFriction           = 0.98f;
	public float maxGroundWalkingAngle = 30.0f;
	
	//variables : jump
	public float jumpVelocity          = 12;
	public int jumpTimeFrames          = 15;
	public float gravityMutiplier      = 5f;
	
	//private variables
	bool mOnGround                     = false;
	Vector3 mGroundDirection           = Vector3.right;
	
	bool mBoosted = false;
	
	bool mInjump                       = false;
	bool mJumpPressed                  = false;
	int mJumpFramesLeft                = 0;
	
	float mStoppingforce               = 0;
	public bool mGoingRight                  = true;
	
	//bool mOnWall = false;
	//float wallStickness = 0;
	
	
	float mCharacterHeight;
	float mCharacterWidth;
	
	Vector3 mStartPosition;
	
	float origColliderCenterY;
	float origColliderSizeY;
	
	Vector3[] warpGrid = new Vector3[3];
	
	
	
	
	
	/**********/
	/*function*/
	/**********/	
	
	// Use this for initialization
	public void Start () {
	
	   // Default value
		mStartPosition = transform.position;
		RecalcBounds();
		origColliderCenterY = ((BoxCollider)collider).center.y;
		//origColliderSizeY = ((BoxCollider)collider).size.y;
	}
	
	//private variables initialize
	public void Reset(){
		mOnGround = false;
		mGroundDirection = Vector3.right;
		mInjump = false;
		mJumpPressed = false;
		mJumpFramesLeft = 0;
		mStoppingforce = 0;
		transform.position = mStartPosition;
		rigidbody.velocity = Vector3.zero;
		mGoingRight = true;
	}
	
	//void Update(){ Debug.Log (mOnGround);}
	
	
	//Player Update : 여러 프레임에 걸쳐 체크해야할 사항, 동작해야할 것
	void FixedUpdate(){
		UpdateGroundInfo();
		UpdateJumping();
		ApplyGravity();
		ApplyMovementFriction();
		if (Input.GetKey(KeyCode.Return))
			{
			GameObject Player = GameObject.Find("Player");
			Player.GetComponent<PlayerPhysics>().Reset ();
			}
	}
	
	
	//걷기 동작 입력 신호를 받았을 때 무엇을 할것인가
	public void Walk(float direction){
		
		float accel = accelerationWalking;
		rigidbody.AddForce (mGroundDirection * direction * accel, ForceMode.Acceleration);
		mStoppingforce = 1 - Mathf.Abs(direction);
		
				
		

		
		
			
		if(direction < 0){
			SendMessage("setDirection", PlayerAnimation.playerDirection.left);
			SendMessage("setState", PlayerAnimation.playerState.walk);
			mGoingRight = false;
			SendAnimMessage("GoLeft");
		}
		if(direction > 0 ){
			SendMessage("setDirection", PlayerAnimation.playerDirection.right);
			SendMessage("setState", PlayerAnimation.playerState.walk);
			mGoingRight = true;
			SendAnimMessage("GoRight");
		}
		
			if(direction >-0.1f && direction <0.1f)
			SendMessage("setState", PlayerAnimation.playerState.stop);
		
	}
	
	
	//점프 동작 입력을 받았을때 무엇을 할 것인가?
	public void Jump(){
		mJumpPressed = true;
		if(mJumpFramesLeft == 0 && !mInjump){
			if(mOnGround){
				mJumpFramesLeft = jumpTimeFrames;
				mInjump = true;
				SendAnimMessage("StartedJump");
			}
		}
	}
	
	//지형 정보 업데이트 : reset 안에 포함됨
	
	void UpdateGroundInfo(){
	
		
	
		float epsilon = 0.01f;
		float extraHeight = mCharacterHeight * 0.75f;
		float halfPlayerWidth = mCharacterWidth * 0.49f;
		
		Vector3 origin1 = GetBottomCenter() + Vector3.right * halfPlayerWidth + Vector3.up * extraHeight;
		Vector3 origin2 = GetBottomCenter() + Vector3.left * halfPlayerWidth + Vector3.up * extraHeight;
		Vector3 direction = Vector3.down;
		RaycastHit hit;
		
		
		if(Physics.Raycast(origin1, direction, out hit) && (hit.distance < extraHeight+epsilon)){
			HitGround(origin1, hit);			
		}
		else if(Physics.Raycast(origin2, direction, out hit) && (hit.distance < extraHeight+epsilon)){
			HitGround(origin2, hit);
		}
		else {
			mOnGround = false;
			mGroundDirection = Vector3.right;
		}
	
		
	}
	
	
	
	void HitGround(Vector3 origin, RaycastHit hit){
		mGroundDirection = new Vector3(hit.normal.y, -hit.normal.x, 0);
		float groundAngle = Vector3.Angle (mGroundDirection, new Vector3(mGroundDirection.x, 0, 0));
		
		if(groundAngle <= maxGroundWalkingAngle){
			if(!mOnGround){
				//SendAnimMessage("LandedOnGround");	
			}
			
			Debug.DrawLine(hit.point+Vector3.up, hit.point, Color.green);
			Debug.DrawLine(hit.point, hit.point + mGroundDirection, Color.magenta);
			mOnGround = true;
			
		}
		else{
			Debug.DrawLine(hit.point, hit.point + mGroundDirection, Color.grey);
	
		}
		
	}
	
	
	 
	
	
	//중력 적용하기 : reset 안에 포함됨.(여러 프레임에 걸쳐 계속 해야할 일이므로)
	void ApplyGravity(){
		if(!mOnGround){
			rigidbody.AddForce(Physics.gravity * gravityMutiplier , ForceMode.Acceleration);
		}
	}
	
	
	//점프 체크하기, 업데이트하기 : reset 안에 포함됨. (여러 프레임에 걸쳐 계속 해야할 일이므로)
	void UpdateJumping(){
		if(!mJumpPressed && mInjump){
			mJumpFramesLeft = 0;
			mInjump = false;
		}
		
		mJumpPressed = false;
		
		if(mJumpFramesLeft != 0){
			mJumpFramesLeft--;
		}
	}

	
	
	
	//마찰력 적용하기 : reset안에 포함.
	void ApplyMovementFriction(){
		Vector3 velocity = rigidbody.velocity;
		
		//ground friction
		if(mOnGround && mStoppingforce > 0.0){
			Vector3 velocityInGroundDir = Vector3.Dot(velocity, mGroundDirection) * mGroundDirection;	
			Vector3 newVelocityInGroundDir = velocityInGroundDir * Mathf.Lerp (1.0f, moveFriction, mStoppingforce);
			velocity -= (velocityInGroundDir - newVelocityInGroundDir);
		}
			
		//air friction
		velocity *= airFriction;
		
		//Max Speed
		float absSpeed = Mathf.Abs (velocity.x );
		float maxSpeed = maxSpeedWalking;
		if(absSpeed > maxSpeed){
			velocity.x *= maxSpeed / absSpeed;	
		}
		
		//min Speed
		if(absSpeed < speedToStopAt && mStoppingforce ==1.0){
			
			velocity.x = 0;		
			
		}

		
		//final velocity to rigid body
		rigidbody.velocity = velocity;
		mStoppingforce = 1.0f;
		
	}

	//동작 결과를 다른 스크립트에 전달하기 위한 펑션
	void SendAnimMessage(string message){
		SendMessage(message, SendMessageOptions.DontRequireReceiver);	
	}
	
	
	void RecalcBounds(){
		mCharacterHeight = collider.bounds.size.y;
		mCharacterWidth = collider.bounds.size.x;
	}
	
	public Vector3 GetBottomCenter(){
		return collider.bounds.center+collider.bounds.extents.y*Vector3.down;		
	}
	
	public bool GoingRight() { return mGoingRight; }
	public void SetRespawnPoint(Vector3 spawnPoint)	{ mStartPosition = spawnPoint; 	}
	
	public void GiveBoost() {mBoosted = true;}
	public void RemoveBoost() {mBoosted = false;}
	public bool HasBoost() { return mBoosted; }
	
	

}
