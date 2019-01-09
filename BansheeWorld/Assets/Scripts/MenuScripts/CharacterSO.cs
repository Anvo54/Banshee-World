using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Character", menuName = "Character")]
public class CharacterSO : ScriptableObject {

    public Character character;
    public string CharacterName;
    public Sprite CharacterImage;

    public int AttackPoint;
    public int HealthPoint;

    public string Description;


    public override string ToString()
    {
        return CharacterName + "\n\nHealth: " + HealthPoint.ToString() + "\n\nAttack: " + AttackPoint.ToString();
    }


}
