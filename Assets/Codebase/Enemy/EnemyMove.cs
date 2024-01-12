using UnityEngine;

namespace Codebase.Enemy
{
    public class EnemyMove : MonoBehaviour
    {
        public float Speed;
        public Transform Target;

        private void Update()
        {
            if (Target != null)
                MoveToTarget();
        }

        private void MoveToTarget()
        {
            transform.position = Vector3.MoveTowards(transform.position, Target.position, Speed * Time.deltaTime);
        }
    }
}