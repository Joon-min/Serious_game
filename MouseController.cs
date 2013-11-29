using UnityEngine;
using System.Collections;

public class MouseController : MonoBehaviour {
	
	
	// 1 : jump picker(blue)  2: acc picker(green)
	public int mousePicker = 0;
	bool[] mousepicker = new bool [5]; // 획득 여부 저장하기.
	public Vector3[] warpGrid = new Vector3[2];
	public AudioClip scratch;
	int warpLeft = 2;

	
	RaycastHit hit;
	public GameObject Target;
	public Transform curTransform;
	
	
	
	
	float raycastLength = 500;
	

	
	
	void Update()
	{	
		//Debug.Log (warpGrid[0]);
		
		GameObject Target = GameObject.Find ("Mouse");
		Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
		
		
		if(Input.GetMouseButtonDown (0)){
			if(hit.collider.name == "Ground"){
			AudioSource.PlayClipAtPoint(scratch, transform.position);
				if(mousePicker == 3){
				if(warpLeft != 0){
					curTransform = hit.collider.transform;			
					if(warpLeft == 2){
						warpGrid[0] = curTransform.position;
					    hit.collider.SendMessage("objectTypeToWarp",SendMessageOptions.DontRequireReceiver);
						warpLeft--;
					}
					else if(warpLeft == 1){
						if(Vector3.Distance(hit.collider.transform.position,warpGrid[0])<5.5f){
							warpGrid[1] = curTransform.position;
							hit.collider.SendMessage("objectTypeToWarp",SendMessageOptions.DontRequireReceiver);							
							warpLeft--;
						}
					}
				}
				}
			}	
			
			
			
			
			
		}
		
		if(Input.GetMouseButton(0)){
			if(hit.collider.name == "Ground"){
			
			if(mousePicker == 0){
				
				
				int temp = hit.collider.GetComponent<ObjectPhysics>().mObjectType();
				if(temp == 3){warpLeft++;}
				hit.collider.SendMessage("objectTypeToNone",SendMessageOptions.DontRequireReceiver);
				
			}
			if(mousePicker == 1){hit.collider.SendMessage("objectTypeToJump",SendMessageOptions.DontRequireReceiver);}
			if(mousePicker == 2){hit.collider.SendMessage("objectTypeToAcc",SendMessageOptions.DontRequireReceiver);}
			//if(mousePicker == 3){
			
			//}
			if(mousePicker == 4){hit.collider.SendMessage("objectTypeToMagnetic",SendMessageOptions.DontRequireReceiver);}
			}
			}
		
		
		
		
		if(Input.GetMouseButtonDown(1)){
			if(mousePicker == 4){mousePicker =0; }
			else {mousePicker++;}
			pickerChange ();
			
		}
		
		if(Input.GetKey("1")){mousePicker=0; pickerChange();}
		if(Input.GetKey("2")){mousePicker=1; pickerChange();}
		if(Input.GetKey("3")){mousePicker=2; pickerChange();}
		if(Input.GetKey("4")){mousePicker=3; pickerChange();}
		if(Input.GetKey("5")){mousePicker=4; pickerChange();}
		
		
		
		if(Physics.Raycast(ray, out hit, raycastLength)){
			Vector3 temp = hit.point;
			temp.z = -0.5f;
			Target.transform.position = temp;	
			
		}
		
		//transform.position = hit.transform.position;
	}

	void pickerChange(){
	if(mousePicker == 0){this.renderer.material.color = Color.white;}
	if(mousePicker == 1){this.renderer.material.color = Color.cyan;}	
	if(mousePicker == 2){this.renderer.material.color = Color.magenta;}	
	if(mousePicker == 3) {this.renderer.material.color = Color.blue;}	
	if(mousePicker == 4) {this.renderer.material.color = Color.black;}
		
	}

	
	public Vector3 GetWarpPoint(){return this.warpGrid[0];}
	public Vector3 GetWarpPoint2(){return this.warpGrid[1];}
	public void resetWarp(){warpLeft =2;}
	public int hasWarp(){return this.warpLeft;}
	
}