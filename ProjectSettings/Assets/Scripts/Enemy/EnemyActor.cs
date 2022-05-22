using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyActor : MonoBehaviour
{
    GameObject PC;
    Enemy enemy = null;
    protected int impacts;
    protected int demage;
    protected int random;
    protected Animator anim;
    protected Transform character;
    protected Vector3 auxPos;

    void Awake()
    {
        PC = GameObject.Find("Character");
        character = GameObject.Find("Character").GetComponent<Transform>();
        anim = GetComponent<Animator>();
    }

    void Update(){}

    public void Move() { }
}
