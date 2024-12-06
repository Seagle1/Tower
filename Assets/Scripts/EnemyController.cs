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
    private bool isDamagingTower = false;

    public void Initialize(EnemySO data, Transform towerTransform)
    {
        enemyData = data;
        tower = towerTransform;
        currentHealth = data.health;
        isActive = true;
        Debug.Log($"Enemy initialized with health: {currentHealth} and move speed: {enemyData.moveSpeed}");
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
       // Debug.Log($"Enemy moving towards tower. Current position: {transform.position}");
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        Debug.Log($"Enemy took damage: {damage}. Current health: {currentHealth}");
        if (currentHealth <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        isActive = false;
        Debug.Log("Enemy died.");
        EnemySpawner.Instance.ReturnEnemyToPool(enemyData, this.gameObject);
    }

    private void FixedUpdate()
    {
        float detectionRadius = 0.5f;
        Collider[] hits = Physics.OverlapSphere(transform.position, detectionRadius);
        foreach (var hit in hits)
        {
            if (hit.gameObject == tower.gameObject && !isDamagingTower)
            {
                Debug.Log("Enemy reached tower, starting DamageTower coroutine.");
                StartCoroutine(DamageTower());
                break;
            }
        }
    }

    private IEnumerator DamageTower()
    {
        isDamagingTower = true;
        while (true)
        {
            Debug.Log("Applying damage to tower.");
            Tower.Instance.TakeDamage(enemyData.damage);
            yield return new WaitForSeconds(1f);
        }
    }

    private void OnDisable()
    {
        Debug.Log("Stopping DamageTower coroutine.");
        isDamagingTower = false;
        StopCoroutine(DamageTower());
    }
}
