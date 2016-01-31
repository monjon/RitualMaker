using UnityEngine;
using System.Collections;

public class BubbleController : MonoBehaviour {

	public string VillageoisJob;

	public GameObject FarmIcon;
	public GameObject FishingIcon;

	public GameObject ChildObject;

	private SpriteRenderer sp;

	// Use this for initialization
	void Start () {
		sp = ChildObject.GetComponent<SpriteRenderer> ();

	}
	
	// Update is called once per frame
	void Update () {
//		switch (VillageoisJob) {
//		case "Farmer":
//			sp.sprite = FarmIcon.GetComponent<SpriteRenderer>().sprite;
//			break;
//		case "Fisher":
//			sp.sprite = FishingIcon.GetComponent<SpriteRenderer> ().sprite;
//			break;
//		default:
//			break;
//		}
	}

	public void ChangeSprite(string jobName){
//		Debug.Log (jobName);
//		switch (jobName) {
//		case "Farmer":
//			sp.sprite = FarmIcon.GetComponent<SpriteRenderer>().sprite;
//			break;
//		case "Fisher":
//			sp.sprite = FishingIcon.GetComponent<SpriteRenderer> ().sprite;
//			break;
//		default:
//			break;
//		}
	}
}
