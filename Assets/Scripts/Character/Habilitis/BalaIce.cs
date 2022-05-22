using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BalaIce : MonoBehaviour
{
    [SerializeField] public float velocidad, targetTime;
    public float damage;

    void Update()
    {
        transform.position += transform.forward * velocidad * Time.deltaTime;
        targetTime -= Time.deltaTime;
        if (targetTime <= 0.0f)
        {
            Destroy(this.gameObject);
        }
    }

    private void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.tag == "Enemy")
            collision.GetComponent<EnemyActor>().salud -= damage;

        if (collision.gameObject.tag == "Enemy" || collision.gameObject.tag == "terrain" || collision.gameObject.tag == "Wall")
            Destroy(this.gameObject);

    }
}
