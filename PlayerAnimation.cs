using UnityEngine;
using System.Collections;

public class PlayerAnimation : MonoBehaviour {
	public Texture[] sprite = new Texture[5];
	
	public enum playerState {walk, stop, die};
	public enum playerDirection {left, right};
	public playerState nowState;
	public playerDirection nowDirection;
	
	private int nowFrameTime =0;
	private int nowFrame =0;
	// Use this for initialization
	void Start () {
	
		nowState = playerState.stop;
	}
	
	
	public void setDirection(playerDirection newDirection) 
	{
		if(newDirection == playerDirection.left)
		{
	
			
			this.gameObject.transform.localScale = new Vector3(1.0f,-1.0f,1.0f);
		}
		else if(newDirection == playerDirection.right)
		{
				this.gameObject.transform.localScale = new Vector3(-1.0f,-1.0f,1.0f);
		}
	}
		
	public void setState(playerState newState) 
	{
		nowState = newState;
	}
	// Update is called once per frame
	void Update () {
		if(nowState == playerState.walk)
		{
			nowFrameTime++;
			if(nowFrameTime >=4)
			{
				nowFrameTime=0;
				if(nowFrame < 4)
				{
					this.renderer.material.mainTexture = sprite[nowFrame++];
				}
				else
					nowFrame =0;
				
				
			}
			
		}
		else if(nowState == playerState.stop)
		{
			this.renderer.material.mainTexture = sprite[0];
			nowFrame = 0;
			nowFrameTime = 0;
		}
	
	}
}
