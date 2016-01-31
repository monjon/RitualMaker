using UnityEngine;
using System.Collections;

public class GodPower : MonoBehaviour {

	// Properties
	//

	public string PowerID;

	[Tooltip("If true, FearLove = -1, else 1")]
	public bool isFearfull= false;

	public int FearLove{
		get {
				if(this.isFearfull){
					return 1;
				}
				else{
					return -1;
				}
			}
	}

	public float Range = 5f;

	public float Cooldown = 5f;
	private float currentCooldown = 5f;

	private bool isOnCooldown = false; 
	public bool IsOnCooldown{
		get {return this.isOnCooldown;}
	}
	public float CooldownRate{
		get {return (float)this.currentCooldown/(float)this.Cooldown;}
	}

	// Methods
	//

	public void Update(){

		// If we are on cd
		if(this.isOnCooldown){

			this.currentCooldown += Time.deltaTime;

			// If the cd is over
			if(this.currentCooldown >= this.Cooldown){

				this.currentCooldown = this.Cooldown;
				this.isOnCooldown = false;

			}

		}		

	}

	public void UsePower(){

		// Cooldown Start
		this.isOnCooldown = true;
		this.currentCooldown = 0f;

	}

}
