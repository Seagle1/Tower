using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "EnemySO", fileName = "EnemySO")]
public class EnemySO : ScriptableObject
{
  public float moveSpeed;
  public int health;
  public GameObject enemyPrefab;
  public int damage;
}
