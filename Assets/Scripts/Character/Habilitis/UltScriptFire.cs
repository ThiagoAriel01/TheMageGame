using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UltScriptFire : MonoBehaviour
{
    [SerializeField] public float targetTime;
    public float damage;

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
