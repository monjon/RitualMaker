﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class villageois : MonoBehaviour
{
    private Vector3 destination;
    private List<Vector3> villageToWorkplace = new List<Vector3>();
	private Animator animator;
	private GameObject goBubble;

    public float speed;
    public float collectTime = 10f;

    public GameObject Village;
	public GameObject Bubble;

    public int maxFood = 10;
    public int food = 0;
    public string sex = "Male";
    public string job;
    public string health = "Healthy";
    public string age = "Young";
    int cycles = 0;
    private float timer = 0;
    private int i = 0;
    private float variantX = 0.5f;
    private float variantY = 0.5f;
    private float faith = 0.0f;
    public bool boosted = false;

    private Dictionary<int, int> chanceToPray = new Dictionary<int, int>();

    int cyclesSick = 0;

    [HideInInspector]
    public Dictionary<string, int> Ritual = new Dictionary<string, int>();

    private enum playerState
    {
        isWorking,
        isGoingToWork,
        isGoingBackHome,
        isResting,
        isSleeping,
    }

    private playerState pState;

    public void Randomize()
    {
        sex = Random.Range(0, 2) == 0 ? "Male" : "Female";
        health = "Healthy";
        age = "Young";
    }

    void SetPathPoints()
    {
		
        job = Village.GetComponent<Village>().UnlockedJobs[Random.Range(0, Village.GetComponent<Village>().UnlockedJobs.Count)];

        villageToWorkplace.Clear();
        List<Spots> spots = Village.GetComponent<Village>().Spots;

        villageToWorkplace.Add(Village.transform.position);

        // Go pray ?

        List<string> conditions = new List<string>();//GetComponent<KeyWords>()
        conditions.AddRange(GetComponent<KeyWords>().KeyWordsList);

        List<string> selectedConditions = new List<string>();

        if (conditions.Count < 3)
        {
            foreach (string t in conditions)
            {
                selectedConditions.Add(t);
            }
        }
        else
        {
            for (int i = 0; i < 3; ++i)
            {
                string s = conditions[Random.Range(0, conditions.Count)];
                selectedConditions.Add(s);
                conditions.Remove(s);
            }
        }

        int cnt = 0;
        int total = 0;
        foreach (string checkConditions in selectedConditions)
        {
            if (Ritual.ContainsKey(checkConditions) == false)
            {
                total += 0;
            }
            else
            {
                total += Ritual[checkConditions];
            }
            ++cnt;
        }

        int mean = total / cnt;

        if (mean < -3)
            mean = -3;
        if (mean > 3)
            mean = 3;

        int percentage = chanceToPray[mean];

        int result = Random.Range(0, 100);

        if (result <= percentage)
        {
            job = "Prayer";
        }

        List<Spots> jobSpots = new List<Spots>();

        foreach (Spots spot in spots)
        {
            if (spot.Job == job)
            {
                jobSpots.Add(spot);
            }
        }

        Spots selectedSpot = jobSpots[Random.Range(0, jobSpots.Count)];

        foreach (GameObject pos in selectedSpot.waypoints)
        {
            villageToWorkplace.Add(pos.transform.position);
        }

//		bubbleScript.ChangeSprite (job);
			
    }

    void SetKeywords()
    {
        KeyWords k = GetComponent<KeyWords>();

        k.KeyWordsList.Clear();

        k.KeyWordsList.Add(job);
        k.KeyWordsList.Add(health);
        k.KeyWordsList.Add(age);
        k.KeyWordsList.Add(sex);
    }

    void Awake()
    {
        chanceToPray[-3] = 100;
        chanceToPray[-2] = 80;
        chanceToPray[-1] = 60;
        chanceToPray[0] = 30;
        chanceToPray[1] = 25;
        chanceToPray[2] = 15;
        chanceToPray[3] = 0;
    }

    // Use this for initialization
    void Start()
    {
        pState = playerState.isGoingToWork;

		animator = GetComponent<Animator> ();
		goBubble = (GameObject) GameObject.Instantiate (Bubble);
		goBubble.transform.parent = this.transform;
		goBubble.transform.localPosition = new Vector3 (0.5f, 1f, 0);

		StartCoroutine (ShowHideBubble ());

        SetKeywords();

        SetPathPoints();

        Village.GetComponent<Village>().dwellers.Add(this.gameObject);
    }

    public void Fear()
    {
        if (pState == playerState.isGoingToWork && i != 0)
            i--;
        if (pState != playerState.isResting)
            pState = playerState.isGoingBackHome;
        speed *= 2;
    }

    bool wakeUp = false;

    public void UpdateWakeUp()
    {
        pState = playerState.isGoingToWork;

        ++cycles;

        if (cycles == 10)
        {
            age = "Adult";
        }
        if (cycles == 23)
        {
            age = "Elder";
        }
        if (cycles >= 35)
        {
            // Dead;
            Die();
        }

        if (health == "Sick")
        {
            ++cyclesSick;
        }
        else
        {
            cyclesSick = 0;
        }

        if (cyclesSick > 5)
        {
            // Dead;
            Die();
        }

        SetKeywords();
        SetPathPoints();

        // Change AI behaviour here

        if (job == "Farmer")
        {

        }
        else if (job == "Hunter")
        {

        }
        else if (job == "Fisher")
        {

        }
        wakeUp = false;

    }

    public void WakeUp()
    {
        wakeUp = true;
    }

    public void GetsSick()
    {
        health = "Sick";
    }

    public void EndOfJobCycle()
    {
        pState = playerState.isResting;
    }

    public void Die()
    {
        Destroy(this.gameObject);
    }

	void moove(float coeff)
	{
		if (transform.position != destination) {
			var tmp = destination - transform.position;
			if (tmp.x > 0) {

				animator.SetBool ("WalkingLeft", false);
				animator.SetBool ("Idle", false);
				animator.SetBool ("WalkingRight", true);
			} else if (tmp.x < 0) {

				animator.SetBool ("WalkingRight", false);
				animator.SetBool ("Idle", false);
				animator.SetBool ("WalkingLeft", true);
			} else {
				animator.SetBool ("WalkingLeft", false);
				animator.SetBool ("WalkingRight", false);
				animator.SetBool ("Idle", true);
			}
            transform.position = Vector3.MoveTowards(transform.position, destination, Time.fixedDeltaTime * speed * coeff);
        }

	}

    // Update is called once per frame
    void Update()
    {
		//goBubble.transform.position = new Vector3 (gameObject.transform.position.x + 1f, gameObject.transform.position.y + 1.2f, 0);
        switch (pState)
        {
            case playerState.isGoingToWork:
                destination = villageToWorkplace[i];
				
                if (transform.position == villageToWorkplace[villageToWorkplace.Count - 1])
                {
                    speed = 1;
                    pState = playerState.isWorking;
                }
                else if (transform.position == villageToWorkplace[i])
                    ++i;
                if (i >= villageToWorkplace.Count)
                    i = villageToWorkplace.Count - 1;
                    break;

            case playerState.isGoingBackHome:
                destination = villageToWorkplace[i];
                if (transform.position == villageToWorkplace[0])
                {
                    speed = 1;
                    pState = playerState.isGoingToWork;
                    if (job == "Farmer" || job == "Fisher")
                        Village.GetComponent<Village>().food += food;
                    else if (job == "Hunter")
                        Village.GetComponent<Village>().wood += food;
                    else if (job == "Blacksmith")
                        Village.GetComponent<Village>().intel += food;
                    else if (job == "Miner")
                        Village.GetComponent<Village>().minerals += food;
                    else if (job == "Prayer")
                        Village.GetComponent<Village>().faith += food;

                    food = 0;
                }
                else if (transform.position == villageToWorkplace[i])
                {
                    --i;
                }
                break;

            case playerState.isWorking:
                timer = timer + Time.deltaTime;
                if (food < maxFood && timer >= collectTime)
                {
                    variantX = Random.value - 0.5f;
                    variantY = Random.value - 0.5f;
                    destination = new Vector3(villageToWorkplace[villageToWorkplace.Count - 1].x + variantX, villageToWorkplace[villageToWorkplace.Count - 1].y + variantY, 0);
                    timer = 0f;
                    food++;
                }
                else if (food == maxFood)
                {
                    timer = 0f;
                    pState = playerState.isGoingBackHome;
                    variantX = 0.5f;
                    variantY = 0.5f;
                }
                break;

            case playerState.isResting:
                destination = villageToWorkplace[i];
                if (transform.position == villageToWorkplace[0])
                {
                    Village.GetComponent<Village>().food += food;
                    food = 0;
                    pState = playerState.isSleeping;
                }
                else if (transform.position == villageToWorkplace[i])
                {
                    --i;
                }
                break;

            case playerState.isSleeping:
                if (transform.position == destination)
                {
                    variantX = Random.value - 0.5f;
                    variantY = Random.value - 0.5f;

                    if (wakeUp)
                    {
                        UpdateWakeUp();
                        boosted = false;
                        maxFood = 10;
                    }

                    destination = new Vector3(villageToWorkplace[0].x + variantX, villageToWorkplace[0].y + variantY, 0);
                }
                break;

            default:
                break;

        }

		SpriteRenderer spTmp = goBubble.GetComponent<SpriteRenderer> ();
		switch(job){
			case "Prayer":
				spTmp.sprite = GameController.Instance.bubbles [0];
				break;
			case "Farmer":
				spTmp.sprite = GameController.Instance.bubbles [1];
				break;
			case "Fisher":
				spTmp.sprite = GameController.Instance.bubbles [2];
				break;
			case "Hunter":
				spTmp.sprite = GameController.Instance.bubbles [3];
				break;
			case "Miner":
				spTmp.sprite = GameController.Instance.bubbles [4];
				break;
			case "Blacksmith":
				spTmp.sprite = GameController.Instance.bubbles [5];
				break;
			default:
				break;
		}
			


    }

    void FixedUpdate()
    {
        float coeff = 1.0f * (health == "Sick" ? 0.5f : 1.0f);

        switch (pState)
        {
            case playerState.isGoingToWork:
                moove(coeff);
                break;

            case playerState.isGoingBackHome:
                moove(coeff);
                break;

            case playerState.isResting:
                moove(coeff);
                break;

            case playerState.isWorking:
                moove(coeff);
                break;

            case playerState.isSleeping:
                moove(coeff);
                break;

            default:
                break;
        }
    }


    public void OnMouseDown()
    {

        // If there is no power activated, and there is no popup shown
        if (GameController.Instance != null && !GameController.Instance.IsPowerActive && UIScreenManager.Instance != null && UIScreenManager.Instance.CurrentPopUp == null)
        {
            // Show the info popup
            UIScreenManager.Instance.OpenPopUp("InfoVillager");

            // Setup the popup with info from this village
            ((UIPopupInfoVillager)UIScreenManager.Instance.CurrentPopUp).SetVillagerInfo(this);

        }

    }

	IEnumerator ShowHideBubble() {
		while (true) {
			var rndTmps = Random.Range (2.0F, 10.0F);
			//			yield return new WaitForSeconds(rndTmps);
			if(goBubble.activeSelf){
				goBubble.SetActive (false);
			}else{
				goBubble.SetActive (true);
			}

			yield return new WaitForSeconds(rndTmps);
		}


	}

}
