using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

[CreateAssetMenu(menuName ="Scriptable Object/Item")]
public class ItemScript : ScriptableObject
{
    [Header("Gameplay")]
    public ItemType type;
    public ActionType actionType;
    public GameObject prefabObject;

    [Header("UI")]
    public bool stackable = true;

    [Header("Both")]
    public Sprite image;

    public enum ItemType
    {
        Cosmetic,
        Weapon
    }

    public enum ActionType
    {
        Equip,
        Use,
        Melee
    }
}
