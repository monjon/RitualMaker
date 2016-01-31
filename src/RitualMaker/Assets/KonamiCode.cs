using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class KonamiCode : MonoBehaviour 
{

    public List<KeyCode> KonamiCodeList = new List<KeyCode>();

    List<KeyCode> CurrentEntry = new List<KeyCode>();

    public bool KonamiCodeEntered = false;

	public GameObject DeadGoat;
	public GameObject SH;

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
							Vector3 tmpPos = SH.transform.position;
							tmpPos.y -= 1f;
							GameObject.Instantiate (DeadGoat, tmpPos, Quaternion.identity);

                            KonamiCodeEntered = true;

							Destroy (this.gameObject);
                        }
                    }
                }
            }
        }
    }
}
