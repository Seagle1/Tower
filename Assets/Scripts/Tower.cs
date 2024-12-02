using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tower : MonoBehaviour
{
   public static Tower Instance;
   [SerializeField] private int maxHealth = 100;
   private int currentHealth;

   private void Awake()
   {
      Instance = this;
   }

   private void Start()
   {
      currentHealth = maxHealth;
   }

   public void TakeDamage(int damage)
   {
      currentHealth -= damage;
      if (currentHealth <= 0)
      {
         Debug.Log("Tower destroyed! Game Over!");
      }
   }
}
