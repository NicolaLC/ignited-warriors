using System;
using UnityEngine;
using UnityEngine.AI;

namespace Player
{
    public class PlayerMovementController : MonoBehaviour
    {
        [SerializeField] private NavMeshAgent m_Agent = null;
        [SerializeField] private LayerMask floorLayer;

        private float m_LastFire2 = 0f;

        // Update is called once per frame
        void Update()
        {
            CheckClickOnFloor();
        }

        private void CheckClickOnFloor()
        {
            float actualFire2 = Input.GetAxis("Fire2");

            if (Math.Abs(actualFire2 - m_LastFire2) > Mathf.Epsilon)
            {
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                if (Physics.Raycast(ray, out RaycastHit hit, Mathf.Infinity, floorLayer))
                {
                    // Handle click on floor
                    Vector3 clickPosition = hit.point;
                    m_Agent.destination = clickPosition;
                }
            }

            m_LastFire2 = actualFire2;
        }
    }
}