using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="EnemyDataAI", menuName ="AI/EnemyDataAI")]
public class enemyDataAI : ScriptableObject
{
    [Header("Must be filled all these attribuites")]
    public string DATA_objectName;
    public string DATA_LOCALNAME;
    public float DATA_playerDetectionRange;
    public float DATA_AIenemyDetectionRange;
    public float DATA_enemyHealth;
    public float DATA_coreDetectionRangeAI;
    public float DATA_coreMeleeRangeAI;
    public float DATA_coreShootRangeAI;
    public float DATA_coreTurnSpeedAI;
    public float DATA_coreMoveSpeedAI;
    public float DATA_enemyAttackTime;
    public LayerMask DATA_playerMask;
    public LayerMask DATA_AIenemyMask;
    public AudioSource DATA_audioSource;
    public Animator DATA_enemyAnimator;
    public float DATA_backRadius;
    [Header("damage View Object")]
    public GameObject DATA_damageStatusObject;
}
