using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class villageois : MonoBehaviour
{
    private Vector3 destination;
    private List<Vector3> villageToWorkplace = new List<Vector3>();
	private Animator animator;

    public float speed;
    public float collectTime = 10f;

    public GameObject Village;

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
        isGoingToPray,
        isPraying,
        isBackFromRituals,
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
        chanceToPray[0] = 50;
        chanceToPray[1] = 40;
        chanceToPray[2] = 20;
        chanceToPray[3] = 0;
    }

    // Use this for initialization
    void Start()
    {
        pState = playerState.isGoingToWork;

		animator = GetComponent<Animator> ();

        SetKeywords();

        SetPathPoints();

        Village.GetComponent<Village>().dwellers.Add(this.gameObject);
    }

    public void Fear()
    {
        pState = playerState.isGoingBackHome;
        speed *= 2;
    }

    public void WakeUp()
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
        switch (pState)
        {
            case playerState.isGoingToWork:
                speed = 1;
                destination = villageToWorkplace[i];
                if (transform.position == villageToWorkplace[villageToWorkplace.Count - 1])
                {
                    pState = playerState.isWorking;
                }
                else if (transform.position == villageToWorkplace[i])
                    ++i;
                if (i >= villageToWorkplace.Count)
                    i = villageToWorkplace.Count - 1;
                    break;

            case playerState.isGoingBackHome:
                if (transform.position == villageToWorkplace[0])
                {
                    if (Random.value >= faith)
                        pState = playerState.isGoingToWork;
                    else
                        pState = playerState.isGoingToPray;
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
                    destination = villageToWorkplace[i];
                }
                break;

            case playerState.isWorking:
                timer = timer + Time.deltaTime;
                destination = new Vector3(villageToWorkplace[villageToWorkplace.Count - 1].x + variantX, villageToWorkplace[villageToWorkplace.Count - 1].y + variantY, 0);
                if (food < maxFood && timer >= collectTime)
                {
                    if (food % 2 == 0)
                        variantX = variantX * -1;
                    else if (food % 3 == 0)
                        variantY = variantY * -1;
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
                    --i;
                break;

            case playerState.isSleeping:
                if (transform.position == destination)
                {
                    variantX = Random.value - 0.5f;
                    variantY = Random.value - 0.5f;
                    destination = new Vector3(villageToWorkplace[0].x + variantX, villageToWorkplace[0].y + variantY, 0);
                }
                break;

            case playerState.isGoingToPray:
                destination = new Vector3(10, 10, 0);
                if (transform.position == destination)
                {
                    pState = playerState.isPraying;
                }
                break;

            case playerState.isPraying:
                break;

            case playerState.isBackFromRituals:
                destination = villageToWorkplace[0];
                if (transform.position == destination)
                {
                    if (Random.value >= faith)
                        pState = playerState.isGoingToWork;
                    else
                        pState = playerState.isGoingToPray;
                }
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

            case playerState.isGoingToPray:
                moove(coeff);
                break;

            case playerState.isPraying:
                moove(coeff);
                break;

            case playerState.isBackFromRituals:
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

}
