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
        return "Character Name is " + CharacterName + 
            "\n\nHealth point: " + HealthPoint.ToString() + "\n\nAttack Point: " + AttackPoint.ToString();
    }


}
