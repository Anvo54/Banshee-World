using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;


[Serializable]
public class PlayerManager
{

    [HideInInspector] public GameObject instance;
    [HideInInspector] public int playerNumber;
    public Transform spawnPosition;


}
