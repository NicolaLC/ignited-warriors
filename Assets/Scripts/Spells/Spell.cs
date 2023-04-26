using System;
using System.Collections;
using UnityEngine;

namespace Spells
{
    public enum ESpellType
    {
        None,
        AOE
    }

    public abstract class Spell : MonoBehaviour
    {
        public string spellName;
        public float lifeTime = 5f;
        public LayerMask targetLayer;
        public float applyAfter = 0f;
        public float repeatAfter = 0f;

        [field: HideInInspector] public ESpellType Type { get; } = ESpellType.None;
        private bool m_bIsActive = true;

        protected void Start()
        {
            StartCoroutine(Internal_Apply());
            Invoke(nameof(DestroyMe), lifeTime);
        }

        protected virtual void DestroyMe()
        {
            m_bIsActive = false;
            Destroy(gameObject);
        }

        public virtual void Apply(Vector3 i_HitPoint)
        {
            throw new NotImplementedException();
        }

        private IEnumerator Internal_Apply()
        {
            yield return new WaitForSeconds(applyAfter);

            do
            {
                Run();
                yield return new WaitForSeconds(repeatAfter);
            } while (m_bIsActive && repeatAfter > 0f);
        }

        protected virtual void Run()
        {
            throw new NotImplementedException();
        }

#if UNITY_EDITOR
        protected virtual void OnDrawGizmos()
        {
        }
#endif
    }
}