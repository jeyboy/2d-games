using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuHandler : MonoBehaviour
{
    [Serializable]
    public struct KeyValuePair
    {
        public string key;
        public GameObject val;
    }

    public List<KeyValuePair> MyList = new List<KeyValuePair>();
    public Dictionary<string, GameObject> games = new Dictionary<string, GameObject>();

    void Awake()
    {
        foreach (var kvp in MyList)
        {
            games[kvp.key] = kvp.val;
        }
    }

    public void ChooseChess2D() {
        GameObject chess2d;

        if (games.TryGetValue("Chess2D", out chess2d)) {
            if (chess2d != null)
            {
                chess2d.SetActive(true);

                gameObject.SetActive(false);
            }
        }
    }
}
