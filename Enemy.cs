using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour, IDamageable
{
    public AttackRadius AttackRadius;
    public EnemyMovement Movement;
    public int Health = 100;
    public Spawner spawn;
    public GameObject interactPoints;
    private Coroutine LookCoroutine;
    public bool dead;
    

    [SerializeField] private AudioSource deathSoundEffect;

    private void Awake()
    {
        //Health = 100;
        AttackRadius.OnAttack += OnAttack;
    }

    private void OnAttack(IDamageable Target)
    {
        if (LookCoroutine != null)
        {
            StopCoroutine(LookCoroutine);
        }
        
        LookCoroutine = StartCoroutine(LookAt(Target.GetTransform()));
    }

    private IEnumerator LookAt(Transform Target)
    {
        Quaternion lookRotation = Quaternion.LookRotation(Target.position - transform.position);
        float time = 0;

        while (time < 1)
        {
            transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, time);

            time += Time.deltaTime * 2;
            yield return null;
        }

        transform.rotation = lookRotation;
    }

    public void TakeDamage(int Damage)
    {
        Health -= Damage;
        interactPoints.GetComponent<Interactor>().points += 10;
        if (Health <= 0 && !dead)
        {
            interactPoints.GetComponent<Interactor>().points += 100;
            spawn.amountKilled ++;
            spawn.totalEnemiesKilled ++;
            gameObject.SetActive(false);
            print("walterJr");
            dead = true;
            deathSoundEffect.Play();
        }
    }

    public Transform GetTransform()
    {
        return transform;
    }
}