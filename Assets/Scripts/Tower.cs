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
   [SerializeField] private GameObject gameOverPanel;
   private int currentHealth;

   private void Awake()
   {
      Instance = this;
   }

   private void Start()
   {
      gameOverPanel.SetActive(false);
      currentHealth = maxHealth;
      SpellManager.Instance.towerTransform = this.transform;
   }

   public void TakeDamage(int damage)
   {
      currentHealth -= damage;
      UpdateHealthUI();
      if (currentHealth <= 0)
      {
         currentHealth = 0;
         Debug.Log("Tower destroyed! Game Over!");
         gameOverPanel.SetActive(true);
      }
   }

   private void UpdateHealthUI()
   {
      healthBarImage.fillAmount = (float)currentHealth / maxHealth;
   }
}
