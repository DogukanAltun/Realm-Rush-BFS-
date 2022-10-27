using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DefenderLookAt : MonoBehaviour
{
    private Transform Target;
    private bool IsAttack;
    private ParticleSystem Arrow;
    [SerializeField][Range(20, 50)] private int Range = 20;
    [SerializeField] private Transform Weapon;


    private void Start()
    {
        Arrow = GetComponentInChildren<ParticleSystem>();
    }
    void Update()
    {
        CountDistance();
        LookAt();
        Attack();
    }

    public void CountDistance()
    {
        Enemy[] enemy = FindObjectsOfType<Enemy>();
        float maxDistance = Mathf.Infinity;
        Transform closestTarget = null;
        for (int i = 0; i < enemy.Length; i++) 
        {
            float distance = Vector3.Distance(transform.position, enemy[i].transform.position);
            if (distance < maxDistance)
            {
                closestTarget = enemy[i].transform;
                maxDistance = distance;
            }
        }
        Target = closestTarget;
    }

    void Attack()
    {
       float maxDistance = Vector3.Distance (transform.position, Target.transform.position);
        var EmissionMethod = Arrow.emission;
        if (Range > maxDistance)
        {
            EmissionMethod.enabled = true;
        }
        else
        {
            EmissionMethod.enabled = false;
        }
    }
    void LookAt()
    {
        Weapon.LookAt(Target);
    }
}
