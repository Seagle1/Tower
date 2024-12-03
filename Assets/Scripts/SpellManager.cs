using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpellManager : MonoBehaviour
{
   public static SpellManager Instance;

   [System.Serializable]
   public class SpellSlot
   {
      public SpellBase spell;
      public float cooldown;
   }

   public List<SpellSlot> spells;
   public Transform towerTransform;
   private void Awake()
   {
      Instance = this;
   }

   private void Update()
   {
      foreach (var spellSlot in spells)
      {
         if (spellSlot.cooldown > 0)
         {
            spellSlot.cooldown -= Time.deltaTime;
         }
      }

      foreach (var spellSlot in spells)
      {
         if (spellSlot.cooldown <= 0)
         {
            spellSlot.spell.CastSpell(towerTransform);
            spellSlot.cooldown = spellSlot.spell.cooldown;
         }
      }
   }
}
