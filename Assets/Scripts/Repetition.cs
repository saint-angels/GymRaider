using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Repetition : MonoBehaviour
{
    public event Action<Repetition> OnDeath = (r) => { };

    [SerializeField] private int hp = 1;

    private float lifeTime;

    public void Init(float lifeTime)
    {
        this.lifeTime = lifeTime;
    }

    public void Damage(int damageAmount)
    {
        print("Damaged rep " + gameObject.name);

        hp -= damageAmount;
        if (hp <= 0)
        {
            OnDeath(this);
        }
    }

    void Start()
    {
        
    }

    void Update()
    {
        lifeTime -= Time.deltaTime;

        if (lifeTime <= 0)
        {
            OnDeath(this);
        }
    }
}
