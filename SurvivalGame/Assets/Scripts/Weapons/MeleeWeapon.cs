﻿using UnityEngine;

public class MeleeWeapon : MonoBehaviour, IWeapon
{
    [Header("Sound settings")]
    [FMODUnity.EventRef]
    [SerializeField] private string attackSFX;

    [SerializeField] private int damage;
    [SerializeField] private Animations.WeaponAnimation weaponAnimation;
    private string weaponAnimationName;

    [Header("Collider")]
    [SerializeField] private Vector3 center;
    [SerializeField] private Vector3 halfExtents;

    public Animator PlayerAnim { get => playerAnim; set => playerAnim = value; }

    protected Animator playerAnim;
    public float AttackInterval => attackInterval;
    public float attackInterval = 1f;

    private void Awake()
    {
        weaponAnimationName = Animations.GetWeaponAnimation(weaponAnimation);
    }

    public virtual void DoAttackAnimation()
    {
        playerAnim.Play(weaponAnimationName);
    }

    private bool CheckForHits(out Collider[] hits)
    {
        hits = Physics.OverlapBox(transform.position + center, halfExtents, transform.rotation, LayerMasks.Enemy);
        return (hits.Length > 0);
    }

    public virtual void Attack()
    {
        if (CheckForHits(out Collider[] hits))
        {
            for (int i = 0; i < hits.Length; i++)
            {
                hits[i].GetComponent<IDamagable>().TakeDamage(damage);
            }
            FMODUnity.RuntimeManager.PlayOneShot(attackSFX, transform.position);
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(transform.position + center, halfExtents);
    }
}

public interface IWeapon
{
    Animator PlayerAnim { get; set; }
    float AttackInterval { get; }
    void DoAttackAnimation();
    void Attack();
}
