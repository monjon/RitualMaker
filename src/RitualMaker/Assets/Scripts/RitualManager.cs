using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class RitualManager : MonoBehaviour
{

    public GameObject RitualPrefab;

    void Start()
    {

    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Vector3 worldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

            Vector2 pos = new Vector2(worldPos.x, worldPos.y);

            CreateRitual(pos, 10, "Kikoo", 1);
            Debug.Log("KABOOM");
        }
    }

    void CreateRitual(Vector2 position, float range, string powerName, int good)
    {
        GameObject newRitual = (GameObject)GameObject.Instantiate(RitualPrefab, Vector3.zero, Quaternion.identity);

        RaycastHit2D[] hits = Physics2D.CircleCastAll(position, range, Vector2.right);

        List<string> words = new List<string>();

        foreach (RaycastHit2D hit in hits)
        {
            GameObject touched = hit.collider.gameObject;


            words.AddRange(touched.GetComponent<KeyWords>().KeyWordsList);
        }

        for (int i = 0; i < words.Count; i++)
        {
            string temp = words[i];
            int randomIndex = Random.Range(i, words.Count);
            words[i] = words[randomIndex];
            words[randomIndex] = temp;
        }

        for (int i = 0; i < 3; ++i)
        {
            newRitual.GetComponent<Ritual>().keywords.Add(words[i]);
        }
        newRitual.GetComponent<Ritual>().godAction = powerName;
        newRitual.GetComponent<Ritual>().faith = 42;// Change to the action type;

        foreach (RaycastHit2D hit in hits)
        {
            GameObject touched = hit.collider.gameObject;
            if (touched.CompareTag("peasant"))
            {
                foreach (string condition in newRitual.GetComponent<Ritual>().keywords)
                {
                    if (touched.GetComponent<villageois>().Ritual.ContainsKey(condition))
                    {
                        touched.GetComponent<villageois>().Ritual[condition] += good;
                        if (touched.GetComponent<villageois>().Ritual[condition] > 3)
                            touched.GetComponent<villageois>().Ritual[condition] = 3;
                        if (touched.GetComponent<villageois>().Ritual[condition] < -3)
                            touched.GetComponent<villageois>().Ritual[condition] = -3;
                    }
                    else
                    {
                        touched.GetComponent<villageois>().Ritual[condition] = good;
                    }
                }
            }

            // Si l'action n'est pas cool, le peasant flip et se casse.
            if (good < 0)
                touched.GetComponent<villageois>().Fear();
        }
    }
}
