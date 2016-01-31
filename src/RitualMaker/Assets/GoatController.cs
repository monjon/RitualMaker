using UnityEngine;
using System.Collections;

public class GoatController : MonoBehaviour
{

    Vector3 nextDirection;
    public float TimeToWait = 2.0f;
    public float Speed;

	void Start ()
    {
        nextDirection = new Vector3(transform.position.x + Random.Range(-2.0f, 3.0f), transform.position.y + Random.Range(-2.0f, 3.0f), 0.0f);
    }

    void Update()
    {
        if (TimeToWait > 0.0f)
        {
            TimeToWait -= Time.deltaTime;
        }

        if (Vector3.Distance(transform.position, nextDirection) < 0.3f)
        {
            TimeToWait = 2.0f;
            nextDirection = new Vector3(transform.position.x + Random.Range(-2.0f, 3.0f), transform.position.y + Random.Range(-2.0f, 3.0f), 0.0f);
        }

    }

    void FixedUpdate()
    {
        if (TimeToWait <= 0.0f)
        {
            transform.position = Vector3.MoveTowards(transform.position, nextDirection, Time.fixedDeltaTime * Speed);
        }

        if (transform.position.y > 8.5f)
        {
            Vector3 pos = transform.position;
            pos.y = 8.5f;
        }
        if (transform.position.y < -16.0f)
        {
            Vector3 pos = transform.position;
            pos.y = -16.0f;
        }

        if (transform.position.x < -28.0f)
        {
            Vector3 pos = transform.position;
            pos.x = -28.0f;
        }
        if (transform.position.x > 22.5f)
        {
            Vector3 pos = transform.position;
            pos.x = 22.5f;
        }
    }
}
