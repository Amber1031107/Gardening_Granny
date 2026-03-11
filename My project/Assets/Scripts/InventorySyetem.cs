using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[CreateAssetMenu(fileName = "Item", menuName = "Scriptable Objects/Items")]

public class InventorySyetem : ScriptableObject
{
    [Header("Properties")]
    public float cooldown;
    public itemType item_type;
    public Sprite item_sprite;
}

public enum itemType {FlowerSpringPlant1, FlowerSpringPlant2, FlowerSpringPlant3, FlowerSpringPlant4, FlowerSpringPlant5, FlowerSpringPlant6, FlowerSummer, FlowerAutumn, FlowerWinter, Trap, Shovel};