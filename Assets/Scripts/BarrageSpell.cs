using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "BarrageSpell", menuName = "Spells/Barrage Spell")]
public class BarrageSpell : SpellBase
{
    public override void CastSpell(Transform towerTransform)
    {
        EnemyController[] enemies = GameObject.FindObjectsOfType<EnemyController>();
        if (enemies.Length == 0)
        {
            return;
        }

        for (int i = 0; i < numberOfProjectiles; i++)
        {
            EnemyController targetEnemy = enemies[Random.Range(0, enemies.Length)];
            GameObject projectile = Instantiate(projectilePrefab, towerTransform.position, Quaternion.identity);
            ProjectileController projectileController = projectile.GetComponent<ProjectileController>();
            projectileController.Initialize(this, targetEnemy.transform.position, false);
        }
    }
}