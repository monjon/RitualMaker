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

    }

    void CreateRitual(Vector2 position, float range, string powerName)
    {
        GameObject newRitual = (GameObject)GameObject.Instantiate(RitualPrefab, Vector3.zero, Quaternion.identity);

        RaycastHit2D[] hits = Physics2D.CircleCastAll(position, range, Vector2.right);

        List<string> words = new List<string>();

        foreach (RaycastHit2D hit in hits)
        {
            GameObject touched = hit.collider.gameObject;

            if (touched.CompareTag("Villager"))
            {
                //Add the ritual to the villager
            }
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
    }
}
