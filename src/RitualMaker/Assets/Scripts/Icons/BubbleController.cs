using UnityEngine;
using System.Collections;

public class BubbleController : MonoBehaviour {

	private SpriteRenderer sp;

	// Use this for initialization
	void Start () {
		StartCoroutine(ShowHide());
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

	IEnumerator ShowHide() {
		var rndTmps = Random.Range (2.0F, 10.0F);
		yield return new WaitForSeconds(rndTmps);
		gameObject.SetActive (false);
	}
}
