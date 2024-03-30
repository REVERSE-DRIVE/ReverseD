using UnityEngine;
using EntityManage;

namespace EnemyManage
{
    [System.Serializable]
    [CreateAssetMenu(menuName = "SO/Entity/Enemy")]
    public class EnemyInfo : ScriptableObject
    {
        public int id;
        public Status SettingStatus;
        public GameObject enemyPrefab;

        public GameObject GetNewEnemy()
        {
            GameObject enemy = PoolManager.Get(enemyPrefab);
            enemy.GetComponent<Enemy>().defaultStatus = SettingStatus;
            return enemy;
        }
    }
}