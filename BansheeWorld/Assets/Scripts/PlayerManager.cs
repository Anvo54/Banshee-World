using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public class PlayerManager
{

    [HideInInspector] public GameObject instance;
    [HideInInspector] public int playerIndex;
    public Transform spawnPosition;

    [HideInInspector] public int numberOfWins;


    public void Reset()
    {
        instance.transform.position = spawnPosition.position;
        instance.transform.rotation = spawnPosition.rotation;

        instance.SetActive(false);
        instance.SetActive(true);
    }
}
