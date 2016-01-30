using UnityEngine;
using System.Collections;

public class villageois : MonoBehaviour {
    private Vector3 destination;
    public Vector3 village;
    public Vector3 workplace;

    public float speed;

    private int maxFood;
    public int food = 0;
    public string sex;
    public string job;
    public string health;
    public string age;
    private float timer = 0;

    private enum playerState
    {
        isWorking,
        isGoingToWork,
        isGoingBackHome,
    }

    private playerState pState;

	// Use this for initialization
	void Start () {
        pState = playerState.isGoingToWork;
	}

    void moove()
    {
        if (transform.position != destination)
            transform.position = Vector3.Lerp(transform.position, destination, Time.deltaTime * speed);
    }
	
	// Update is called once per frame
	void Update () {
        switch (pState)
        {
            case playerState.isGoingToWork:
                destination = workplace;
                moove();
                if (transform.position == workplace)
                    pState = playerState.isWorking;
                break;

            case playerState.isGoingBackHome:
                destination = village;
                moove();
                if (transform.position == village)
                {
                    pState = playerState.isGoingToWork;
                    food = 0;
                }
                break;

            case playerState.isWorking:
                timer = timer + Time.deltaTime;
                if (food < maxFood && timer >= 10f)
                {
                    timer = 0f;
                    food++;
                }
                else if (food == maxFood)
                {
                    timer = 0f;
                    pState = playerState.isGoingBackHome;
                }
                break;

            default:
                break;

            }
    }

    void FixedUpdate()
    {

    }
}
