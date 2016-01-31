using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class KonamiCode : MonoBehaviour 
{

    public List<KeyCode> KonamiCodeList = new List<KeyCode>();

    List<KeyCode> CurrentEntry = new List<KeyCode>();

    public bool KonamiCodeEntered = false;

	void Start () 
    {
	
	}
	
    bool continuee = false;

	void Update () 
    {
        if (KonamiCodeEntered == false)
        {
            foreach (KeyCode vKey in System.Enum.GetValues(typeof(KeyCode)))
            {
                if (Input.GetKeyDown(vKey))
                {
                    CurrentEntry.Add(vKey);

                    bool good = true;

                    if (CurrentEntry[CurrentEntry.Count - 1] == KonamiCodeList[CurrentEntry.Count - 1])
                        continuee = true;
                    else
                        continuee = false;

                    if (continuee == false)
                    {
                        CurrentEntry.Clear();
                    }
                    else
                    {
                        if (KonamiCodeList.Count == CurrentEntry.Count)
                        {
                            Debug.Log("KONAMI CODE ACTIVATED");
                            KonamiCodeEntered = true;
                        }
                    }
                }
            }
        }
    }
}
