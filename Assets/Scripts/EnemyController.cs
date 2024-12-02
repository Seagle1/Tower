using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
   private EnemySO enemyData;
   private Transform tower;
   private int currentHealth;
   private bool isActive;

   public void Initialize(EnemySO data, Transform towerTransform)
   {
      enemyData = data;
      tower = towerTransform;
      currentHealth = data.health;
      isActive = true;
   }

   private void Update()
   {
      if (!isActive)
      {
         return;
      }

      MoveToTower();
   }

   private void MoveToTower()
   {
      Vector3 direction = (tower.position - transform.position).normalized;
      transform.position += direction * enemyData.moveSpeed * Time.deltaTime;
   }

   public void TakeDamage(int damage)
   {
      currentHealth -= damage;
      if (currentHealth <= 0)
      {
         Die();
      }
   }

   private void Die()
   {
      isActive = false;
      EnemySpawner.Instance.ReturnEnemyToPool(enemyData,this.gameObject);
   }

   private void FixedUpdate()
   {
      float detectionRadius = 0.5f;
      float maxDistance = 0.5f;
      RaycastHit hit;
      if (Physics.SphereCast(transform.position, detectionRadius, Vector3.forward, out hit, maxDistance))
      {
         if (hit.collider.gameObject == tower.gameObject)
         {
            StartCoroutine(DamageTower());
         }
      }
   }

   private IEnumerator DamageTower()
   {
      while (true)
      {
         Tower.Instance.TakeDamage(enemyData.damage);
         yield return new WaitForSeconds(1f);
      }
   }
}
