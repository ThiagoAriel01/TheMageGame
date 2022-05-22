using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpellC : MonoBehaviour
{
    [SerializeField] public float targetTime;
    public float costoMana, damage;
    private void Start()
    {
        HealthLife.mana -= costoMana;
    }
    void Update()
    {
        targetTime -= Time.deltaTime;
        if (targetTime <= 0.0f)
        {
            Destroy(this.gameObject);
        }
    }

    private void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            collision.GetComponent<EnemyActor>().salud -= damage;
            collision.GetComponent<EnemyActor>().quemadura = true;
        }
    }
}
