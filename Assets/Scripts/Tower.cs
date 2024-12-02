using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Tower : MonoBehaviour
{
   public static Tower Instance;
   [SerializeField] private int maxHealth = 100;
   [SerializeField] private Image healthBarImage;
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
      UpdateHealthUI();
      if (currentHealth <= 0)
      {
         currentHealth = 0;
         Debug.Log("Tower destroyed! Game Over!");
      }
   }

   private void UpdateHealthUI()
   {
      healthBarImage.fillAmount = (float)currentHealth / maxHealth;
   }
}
