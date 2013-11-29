using UnityEngine;
using System.Collections;

public class FollowCam2D : MonoBehaviour
{
	public Transform target;
	
	public float distance = 5f;
	

	float origDist;

	void Start () 
	{
		origDist = distance;
	}
	
	void FixedUpdate () 
	{
		if (target)
		{
			if (Input.GetKey(KeyCode.LeftControl))
			{distance = origDist * 5;
			 this.camera.isOrthoGraphic=false;
			
			}
			else
			{distance = origDist;
			this.camera.isOrthoGraphic=true;
			}

			Vector3 targetPos = target.position;
			targetPos.z = -distance;
			//targetPos.y = 10;
			transform.position -= (transform.position - targetPos);
			
		}
	}

	public void SetTarget(Transform inTarget)
	{
		target = inTarget;
	}
}

