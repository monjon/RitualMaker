using UnityEngine;
using System.Collections;
using DG.Tweening;

public class GodsPowerArrowUp : MonoBehaviour {

	// Use this for initialization
	void Start () {
		Vector3 newPos = new Vector3 (transform.position.x, transform.position.y + 1, transform.position.y);
		transform.DOMove (newPos, 0.5f).OnComplete (()=>ChangeAlpha()).SetDelay(0.5f);
	}

	void ChangeAlpha(){

		DOTween.ToAlpha(()=> gameObject.GetComponent<SpriteRenderer>().color, x => gameObject.GetComponent<SpriteRenderer>().color = x, 0, 0.5f);
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
