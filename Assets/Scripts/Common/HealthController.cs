using System;
using System.Collections;
using Effects;
using ReflectionSystem;
using UnityEngine;

namespace Common
{
    [RequireComponent(typeof(StatsController))]
    public class HealthController : MonoBehaviour
    {
        [SerializeField] private ShaderModifier m_DamageModifier = null;
        [SerializeField] private AudioSource m_source = null;
        [SerializeField] private AudioClip m_onHit = null;
        [SerializeField] private AudioClip m_onDestroy = null;
        [SerializeField] private GameObject m_Graphics = null;

        private StatsController m_StatsController = null;

        private void Awake()
        {
            m_StatsController = GetComponent<StatsController>();
            m_StatsController.OnStatsUpdate.AddListener(OnHealthUpdate);
        }

        public void OnHealthUpdate(EStatPropertyName i_UpdatedProperty)
        {
            if (i_UpdatedProperty != EStatPropertyName.Health)
            {
                return;
            }
            
            StatProperty<int> Health = m_StatsController.GetProperty<int>(EStatPropertyName.Health);
            
            if (Health.GetValue() <= 0)
            {
                StartCoroutine(Die());
                return;
            }

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