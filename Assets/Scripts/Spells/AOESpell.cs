using Common;
using UnityEngine;

namespace Spells
{
    public class AOESpell : Spell
    {
        public float damage = 25f;
        public float damageRadius = 10f;
        [field: HideInInspector] public ESpellType Type { get; } = ESpellType.AOE;

        protected override void Run()
        {
            Collider[] colliders = Physics.OverlapSphere(transform.position, damageRadius, targetLayer);

            // Loop through the colliders and print their names
            foreach (Collider collider in colliders)
            {
                collider.transform.gameObject.GetComponent<HealthController>()?.ApplyDamage(damage);
            }
        }

#if UNITY_EDITOR
        protected override void OnDrawGizmos()
        {
            base.OnDrawGizmos();
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(transform.position, damageRadius);
        }
#endif
    }
}