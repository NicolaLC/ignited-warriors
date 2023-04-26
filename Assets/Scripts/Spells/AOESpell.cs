using Common;
using ReflectionSystem;
using UnityEngine;

namespace Spells
{
    public class AOESpell : Spell
    {
        public int damage = 25;
        public float damageRadius = 10f;
        [field: HideInInspector] public ESpellType Type { get; } = ESpellType.AOE;

        protected override void Run()
        {
            Collider[] colliders = Physics.OverlapSphere(transform.position, damageRadius, targetLayer);

            // Loop through the colliders and print their names
            foreach (Collider collider in colliders)
            {
                StatsController sc = collider.transform.gameObject.GetComponent<StatsController>();
                if (!sc)
                {
                    continue;
                }

                StatProperty<int> Health = sc.GetProperty<int>(EStatPropertyName.Health);
                if (Health == null)
                {
                    return;
                }
                
                sc.SetProperty<int>(EStatPropertyName.Health, Health.GetValue() - damage);
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