using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spell : MonoBehaviour
{
    public string name;
    public float manaCost;
    public float damage;
    public float areaOfEffect;
    public GameObject projectilePrefab;

    public Spell(string name, float manaCost, float damage, float areaOfEffect, GameObject projectilePrefab)
    {
        this.name = name;
        this.manaCost = manaCost;
        this.damage = damage;
        this.areaOfEffect = areaOfEffect;
        this.projectilePrefab = projectilePrefab;
    }
}
