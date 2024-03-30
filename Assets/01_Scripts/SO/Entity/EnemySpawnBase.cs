using UnityEngine;

namespace EnemyManage
{
    [System.Serializable]
    [CreateAssetMenu(menuName = "SO/Enemy/EnemySpawnBase")]
    public class EnemySpawnBase : ScriptableObject
    {
        public EnemyInfo[] enemyInfos;

        
        public EnemyInfo FindEnemy(int id)
        {
            for (int i = 0; i < enemyInfos.Length; i++)
            {
                if (enemyInfos[i].id == id && enemyInfos[i].enemyTypeTag == EnemyTypeTag.Normal)
                {
                    return enemyInfos[i];
                }
            }
            Debug.LogError($"[{id}] this Enemy Id is not Exist");
            return null;
        }
        
        public EnemyInfo FindEnemy(int id, EnemyTypeTag enemyType)
        {
            for (int i = 0; i < enemyInfos.Length; i++)
            {
                if (enemyInfos[i].id == id && enemyInfos[i].enemyTypeTag == enemyType)
                {
                    return enemyInfos[i];
                }
            }
            Debug.LogError($"[{id}, {enemyType.ToString()}] this Enemy Id or Tag is not Exist");
            return null;
        }
        
        
    }
}