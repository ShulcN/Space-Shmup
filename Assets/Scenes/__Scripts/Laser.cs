using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Laser : MonoBehaviour
{
    private BoundsCheck bndCheck;
    private Renderer rend;
    private float lifeTimeLaser;
    private float lifeTimeLaserE;
    [Header("Set in Inspector")]
    public Rigidbody rigid;
    [SerializeField]
    private WeaponType _type;
    public WeaponType type
    {
        get
        {
            return (_type);
        }
        set
        {
            SetType(value);
        }
    }
    void Awake()
    {
        bndCheck = GetComponent<BoundsCheck>();
        rend = GetComponent<Renderer>();
        rigid = GetComponent<Rigidbody>();
        lifeTimeLaserE = Time.time + 4f;
        lifeTimeLaser = Time.time + 0.05f;
    }
    
     
    void Update()
    {
        if (gameObject.tag == "ProjectileHero")
        {
            if (Time.time >= lifeTimeLaser)
            {
                Destroy(gameObject);
            }
        } else
        {
            if (Time.time >= lifeTimeLaserE)
            {
                Destroy(gameObject);
            }
        }
    }
    public void SetType(WeaponType eType)
    {
        _type = eType;
        WeaponDefinition def = Main.GetWeaponDefinition(_type);
        rend.material.color = def.projectileColor;

    }
}
