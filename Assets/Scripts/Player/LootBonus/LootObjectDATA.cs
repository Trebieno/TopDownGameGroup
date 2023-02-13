using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New loot object", menuName = "Loot objects/LootObject")]
public class LootObjectDATA : ScriptableObject
{
    public Sprite Sprite;
    public string Type;
    public float Count;
    public int Percent;
}
