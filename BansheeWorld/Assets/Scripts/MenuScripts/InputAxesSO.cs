using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="Input", menuName = "Axes Name")]
public class InputAxesSO : ScriptableObject
{
    public string fire1;
    public string fire2;
    public string fire3;
    public string jumping;

    public string blocking;
    public string movingHorizontal;
    public string movingVertical;
}
