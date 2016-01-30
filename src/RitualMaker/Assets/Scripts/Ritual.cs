using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Ritual : MonoBehaviour 
{

    [HideInInspector]
    public List<string> keywords;
    [HideInInspector]
    public string godAction;
    [HideInInspector]
    public int faith;

	void Start ()
    {
		// TMP, to be sure thta a villager is checked by the creat ritual from RitualManager
		Debug.Log("Ritual.Start");

	}
	
	void Update ()
    {
	
	}
}
