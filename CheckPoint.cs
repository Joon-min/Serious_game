using UnityEngine;
using System.Collections;

public class CheckPoint : MonoBehaviour 
{
	void OnTriggerEnter(Collider other)
	{
		PlayerPhysics physics = other.gameObject.GetComponent<PlayerPhysics>();
		if (physics)
		{
			//Debug.Log ("checked");
			//set new respawn point
			physics.SetRespawnPoint(transform.position);
		}
	}
}