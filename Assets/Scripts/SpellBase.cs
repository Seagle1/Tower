using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class SpellBase : ScriptableObject
{
   public string spellName;
   public float cooldown;
   public int damage;
   public float projectileSpeed;
   public GameObject projectilePrefab;
   public int numberOfProjectiles; 

   public abstract void CastSpell(Transform towerTransform);
}
