


using UnityEngine;
using System.Collections;

public class Clearzone : MonoBehaviour 
{
	public enum GameStage {stage1, stage2, stage3};
	public GameStage nextStage;
	
	public string targetStage;
	
	void OnTriggerEnter(Collider other)
	{
		PlayerPhysics physics = other.gameObject.GetComponent<PlayerPhysics>();
		if (physics)
		{
			//Debug.Log ("checked");
			//set new respawn point
			if(nextStage == GameStage.stage1)
				Application.LoadLevel(targetStage);
		}
	}
}