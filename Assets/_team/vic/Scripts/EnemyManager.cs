using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public static class EnemyManager
{

    public static List<Transform> enemies;
    public static UnityAction AddEnemyAction;
    public static UnityAction RemoveEnemyAction;
    public static UnityAction ClearEnemyAction;

    public static void AddEnemy(Transform enemy)
    {
        enemies.Add(enemy);
        AddEnemyAction();
    }

    public static void RemoveEnemy(Transform enemy)
    {
        enemies.Remove(enemy);
        RemoveEnemyAction();
    }

    public static void ClearEnemies(Transform enemy)
    {
        enemies.Clear();
        ClearEnemyAction();
    }
}
