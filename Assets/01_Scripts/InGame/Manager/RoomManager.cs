using EnemyManage;
using UnityEngine;

namespace RoomManage
{
    public class RoomManager : MonoBehaviour
    {
        [SerializeField] private EnemySpawnBase _enemySpawnBase;
        
        
        /**
         * <summary>
         * 페이즈에 있는 적 유닛들을 맵에 소환한다.
         * </summary>
         */
        public void GeneratePhase(Transform roomTrm, float radius, Phase phase)
        {
            for (int i = 0; i < phase.Mobs.Length; i++)
            {
                for (int j = 0; j < phase.Mobs[i].Amount; j++)
                {
                    Transform newEnemy = _enemySpawnBase.FindEnemy(phase.Mobs[i].ID).GetNewEnemy().transform;
                    newEnemy.SetParent(roomTrm);
                    newEnemy.position = new Vector2(Random.Range(-radius, radius), Random.Range(-radius, radius));

                }
            }
        }
        
        
    }
}