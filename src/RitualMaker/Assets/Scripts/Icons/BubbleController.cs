using UnityEngine;
using System.Collections;

public class BubbleController : MonoBehaviour {

	private SpriteRenderer sp;

	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
		ShowHide ();
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

	IEnumerator ShowHide() {
		gameObject.SetActive (false);
		yield return new WaitForSeconds(1f);
	}
}
