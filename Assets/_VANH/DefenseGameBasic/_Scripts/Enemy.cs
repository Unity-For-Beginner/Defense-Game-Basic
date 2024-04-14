using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace VANH.DefenseBasic
{
    public class Enemy : MonoBehaviour, IComponentChecking
    {
        [SerializeField] private float m_speed;
        [SerializeField] private float atkDistance;
        private Animator m_anim;
        private Rigidbody2D m_rb;
        private Player m_player;
        
        private void Awake()
        {
            m_anim = GetComponent<Animator>();
            m_rb = GetComponent<Rigidbody2D>();
            m_player = FindObjectOfType<Player>();
        }
        public bool IsComponentsNull()
        {
            return m_anim == null || m_rb == null || m_player == null;
        }
        void Update()
        {
            if (IsComponentsNull())
            {
                return;
            }

            float distanceToPlayer = Vector2.Distance(m_player.transform.position, transform.position);

            if (distanceToPlayer <= atkDistance)
            {
                m_anim.SetBool(Const.ATTACK_ANIM, true);
                m_rb.velocity = Vector2.zero;
            }
            else
            {
                m_rb.velocity = new Vector2(-m_speed, m_rb.velocity.y);
            }
        }

        public void Die()
        {
            if (IsComponentsNull())
            {
                return;
            }
            m_anim.SetTrigger(Const.DEAD_ANIM);
            m_rb.velocity = Vector2.zero;
            gameObject.layer = LayerMask.NameToLayer(Const.DEAD_ANIM);
        }
    }
}