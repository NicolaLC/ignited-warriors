using System;
using Spells;
using UnityEngine;

namespace Player
{
    public class PlayerSpellController : MonoBehaviour
    {
        [SerializeField] private Spell QSpell = null;
        [SerializeField] private Spell WSpell = null;
        [SerializeField] private Spell ESpell = null;
        [SerializeField] private Spell RSpell = null;
        [SerializeField] private LayerMask m_spellTargetMask;

        private Camera m_MainCam = null;

        private void Awake()
        {
            m_MainCam = Camera.main;
        }

        private void Update()
        {
            Check(KeyCode.Q, QSpell);
            Check(KeyCode.W, WSpell);
            Check(KeyCode.E, ESpell);
            Check(KeyCode.R, RSpell);
        }

        private void Check(KeyCode i_Key, Spell i_Spell)
        {
            if (Input.GetKeyDown(i_Key))
            {
                Spawn(i_Spell);
            }
        }

        private void Spawn(Spell i_Spell)
        {
            Ray ray = m_MainCam.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit, 100f, m_spellTargetMask))
            {
                Spell spell = Instantiate(i_Spell, hit.point, Quaternion.identity);
                Vector3 targetPos = new Vector3(transform.position.x, spell.transform.position.y, transform.position.z);
                spell.transform.LookAt(targetPos, Vector3.up);
            }
        }
        
    }
}