using System.Collections;
using Effects;
using UnityEngine;

namespace Common
{
    public class HealthController : MonoBehaviour
    {
        [SerializeField] private float m_Health = 100f;
        [SerializeField] private ShaderModifier m_DamageModifier = null;
        [SerializeField] private AudioSource m_source = null;
        [SerializeField] private AudioClip m_onHit = null;
        [SerializeField] private AudioClip m_onDestroy = null;
        [SerializeField] private GameObject m_Graphics = null;

        private float m_CurrentHealth = 100f;

        protected void Awake()
        {
            m_CurrentHealth = m_Health;
        }

        public void ApplyDamage(float i_Amount)
        {
            m_CurrentHealth = Mathf.Max(0, m_CurrentHealth - i_Amount);
            if (m_CurrentHealth <= 0)
            {
                StartCoroutine(Die());
                return;
            }

            print($"[{transform.name}] New health {m_CurrentHealth}");
            m_source.PlayOneShot(m_onHit);
            m_DamageModifier.Apply();
        }

        IEnumerator Die()
        {
            m_Graphics.SetActive(false);
            m_source.PlayOneShot(m_onDestroy);

            while (m_source.volume > 0f)
            {
                yield return new WaitForSeconds(0.1f);
                m_source.volume -= 0.05f;
            }
            
            Destroy(gameObject);
        }
    }
}