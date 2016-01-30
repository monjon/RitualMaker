using UnityEngine;
using System.Collections;
using DG.Tweening;

public class GodsPowerArrowUp : MonoBehaviour {

	// Use this for initialization
	void Start () {
		Vector3 newPos = new Vector3 (transform.position.x, transform.position.y + 1.0f, transform.position.y);
		transform.DOMove(newPos, 1);
		transform.DOFade (1, 1);
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
