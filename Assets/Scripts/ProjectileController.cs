using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileController : MonoBehaviour
{
    private SpellBase spellData;
    private Vector3 targetPosition;
    private bool isAreaEffect;
    private Transform targetTransform;
    private float selfDestructTime = 7f;

    public void Initialize(SpellBase spell, Vector3 targetPos, bool areaEffect)
    {
        spellData = spell;
        targetPosition = targetPos;
        isAreaEffect = areaEffect;
        StartCoroutine(SelfDestructTimer());

    }

    private void Update()
    {
        if (targetPosition == null)
        {
            return;
        }
        transform.position = Vector3.MoveTowards(transform.position, targetPosition, spellData.projectileSpeed * Time.deltaTime);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent<EnemyController>(out EnemyController enemy))
        {
            enemy.TakeDamage(spellData.damage);
            Destroy(gameObject);
        }
    }
    
    private IEnumerator SelfDestructTimer()
    {
        yield return new WaitForSeconds(selfDestructTime);
        Destroy(gameObject);
    }
}