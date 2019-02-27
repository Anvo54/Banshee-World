using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class PlayerStats : MonoBehaviour
{
    public CharacterSO Character1;
    public Text health;
    int healthINT;
    int pnum;
    string textTag;

    // Start is called before the first frame update
    void Start()
    {
        pnum = gameObject.GetComponent<PlayerMovement>().playerNumber;
        textTag = "P" + pnum + "H";
        healthINT = Character1.HealthPoint;
        health = GameObject.FindGameObjectWithTag(textTag).GetComponent<Text>();
        Debug.Log(textTag);
    } 

    // Update is called once per frame
    void Update()
    {
        health.text = "Health: " + healthINT;
        Die();
    }

    public void GetHit(int damage)
    {
        healthINT -= damage;
    }

    void Die()
    {
        if(healthINT <= 0)
        {
            Destroy(gameObject);
        }
    }


}
