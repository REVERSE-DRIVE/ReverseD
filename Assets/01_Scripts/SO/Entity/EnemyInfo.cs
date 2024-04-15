using UnityEngine;
using EntityManage;

namespace EnemyManage
{
    [System.Serializable]
    [CreateAssetMenu(menuName = "SO/Enemy/Enemy")]
    public class EnemyInfo : ScriptableObject
    {
        public int id;
        public EnemyTypeTag enemyTypeTag = EnemyTypeTag.Normal;
        public Status SettingStatus;
        public GameObject enemyPrefab;

        public GameObject GetNewEnemy()
        {
            GameObject enemyObject = PoolManager.Get(enemyPrefab);
            Enemy enemy = enemyObject.GetComponent<Enemy>();
            enemy.defaultStatus = SettingStatus;
            enemy.SetStatusDefault();
            enemy.GetComponent<EnemyAI>().SetDefault();
            return enemyObject;
        }
    }
}