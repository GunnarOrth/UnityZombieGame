 using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CustomBullet : MonoBehaviour
{
    //public Rigidbody rb;
    public GameObject explosion;
    public LayerMask whatIsEnemies;

    [Range(0f, 1f)]
    public float bounciness;
    public bool useGravity;

    public int explosionDamage;
    public float explosionRange;

    public int maxCollisions;
    public float maxLifetime;
    public bool explodeOnTouch = true;

    public int damage = 30;
    
    int collisions;
    PhysicMaterial physics_mat;

    public TextMeshProUGUI pointDisplay;
    public int points;

    private void Setup()
    {
        physics_mat = new PhysicMaterial();
        physics_mat.bounciness = bounciness;
        physics_mat.frictionCombine = PhysicMaterialCombine.Minimum;
        physics_mat.bounceCombine = PhysicMaterialCombine.Maximum;

        GetComponent<SphereCollider>().material = physics_mat;
    }

    // Start is called before the first frame update
    void Start()
    {
        Setup();
    }

    // Update is called once per frame
    void Update()
    {
        if(collisions > maxCollisions) Explode();

        maxLifetime -= Time.deltaTime;
        if(maxLifetime <= 0) Explode();
    }


    private void OnCollisionEnter(Collision collision)
    {
        if(!(collision.collider.CompareTag("Bullet"))){collisions++;}

        if(collision.collider.CompareTag("Enemy") && explodeOnTouch){
            collision.gameObject.GetComponent<Enemy>().TakeDamage(damage);
            //collision.TakeDamage(damage);
            Explode();
        } 
    }   

    private void Explode()
    {
        if (explosion != null) Instantiate(explosion, transform.position, Quaternion.identity);

        Collider[] enemies = Physics.OverlapSphere(transform.position, explosionRange, whatIsEnemies);
        for(int i = 0; i < enemies.Length; i++)
        {
            //enemies[i].GetComponent
        }

        Invoke("Delay", 0.01f);
        //Delay();
    }

    private void Delay()
    {
        Destroy(gameObject);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, explosionRange);
    }


}
