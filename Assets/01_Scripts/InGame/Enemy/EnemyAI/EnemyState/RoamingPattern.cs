using System.Collections;
using UnityEngine;

namespace EnemyManage
{
    public class RoamingPattern : EnemyAttackPattern
    {
        private EnemyAI _enemyAI;
        public override void Enter()
        {
            base.Enter();
        }

        private void Roaming()
        {
            _enemyAI.StartCoroutine(RoamingCoroutine());
        }

        private IEnumerator RoamingCoroutine()
        {
            while (true)
            {
                Vector3 direction = new Vector3(Random.Range(-1f, 1f), Random.Range(-1f, 1f), 0);
                _enemyAI.transform.Translate(direction * 0.1f);
                yield return new WaitForSeconds(1f);
            }
        }

        public override void Update()
        {
            base.Update();
        }

        public override void Exit()
        {
            base.Exit();
        }
    }
}