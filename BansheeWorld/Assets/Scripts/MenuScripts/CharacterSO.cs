using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Character", menuName = "Character")]
public class CharacterSO : ScriptableObject {

    public Character character;
    public GameObject CharacterPrefab;
    public string CharacterName;
    public Sprite CharacterImage;

    public Sprite WeaponBasicImage;
    public Sprite WeaponUp1Image;
    public Sprite WeaponUp2Image;

    public int WeaponTo1UpgradeCost = 500;
    public int WeaponTo2UpgradeCost = 5000;

    public int WeaponBasicPower = 10;
    public int WeaponUp1Power = 20;
    public int WeaponUp2Power = 30;

    public int AttackPoint = 100;
    public int HealthPoint = 100;

    public override string ToString()
    {
        return CharacterName +
                "\nHealth: " + HealthPoint.ToString();
                //   "\nAttack: " + AttackPoint.ToString();
                //   "\nAttack: " + "Basic";
                

    }
}
