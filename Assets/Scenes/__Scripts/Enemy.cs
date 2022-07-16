using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Enemy : MonoBehaviour
{
    [Header("Set in Inspector")]
    public float speed = 10f;
    public float fireRate = 0.3f;
    public float health = 10f;
    public int score = 100;
    public float showDamageDuration = 0.1f;
    protected BoundsCheck bndCheck;
    [Header("Set Dynamically: Enemy")]
    public Color[] originalColors;
    public Material[] materials;
    public bool showingDamage = false;
    public float damageDoneTime;
    public bool notifieldOfDestruction = false;
    public float powerUpDropChance = 1f;
    public int allScore;
    public Main main;

    public Text scoreGT;
    
        
    void Awake()
    {
        main = GetComponent<Main>();
        GameObject scoreGO = GameObject.Find("ScoreCount");
        scoreGT = scoreGO.GetComponent<Text>();
        allScore = int.Parse(scoreGT.text);
        

        bndCheck = GetComponent<BoundsCheck>();
        materials = Utils.GetAllMaterials(gameObject);
        originalColors = new Color[materials.Length];
        for (int i=0; i < materials.Length; i++)
        {
            originalColors[i] = materials[i].color;
        }
    }

    public Vector3 pos
    {
        get
        {
            return(this.transform.position);
        }
        set
        {
            this.transform.position = value;
        }
    }
    // Start is called before the first frame update
   

    // Update is called once per frame
    void Update()
    {
        if (Main.S.bossIsHere && this.gameObject.tag!="Boss")
        {
            Destroy(this.gameObject);
        }
        
        Move();
        if (showingDamage && Time.time > damageDoneTime)
        {
            UnShowDamage();
        }
        if (bndCheck != null && bndCheck.offDown)
        {
            
            Destroy(gameObject);
            
        }
        
        
    }
    public virtual void Move()
    {
        Vector3 tempPos = pos;
        tempPos.y -= speed * Time.deltaTime;
        pos = tempPos;
    }
    void OnCollisionEnter(Collision coll)
    {
        
        GameObject otherGO = coll.gameObject;
        switch (otherGO.tag)
        {
            case "ProjectileHero":
                Projectile p = otherGO.GetComponent<Projectile>();
                ShowDamage();
                
                Destroy(otherGO);
                    
                
                if (otherGO.name != "Laser(Clone)")
                {
                    health -= Main.GetWeaponDefinition(p.type).damageOnHit;
                } else
                {
                    health -= 0.08f;
                }
                
                if (health <= 0)
                {
                    allScore = int.Parse(scoreGT.text);
                    allScore += score;
                    scoreGT.text = allScore.ToString();
                    Destroy(this.gameObject);
                    if (!notifieldOfDestruction)
                    {
                        Main.S.ShipDestroyed(this);
                    }
                    notifieldOfDestruction = true;
                }
                break;
            case "Boss":
                Destroy(this.gameObject);
                break;
            default:
                print("Enemy hit by non-ProjectileHero");
                break;
        }
    }

    void ShowDamage()
    {
        foreach (Material m in materials)
        {
            m.color = Color.red;
        }
        showingDamage = true;
        damageDoneTime = Time.time + showDamageDuration;
    }
    void UnShowDamage()
    {
        for (int i = 0; i < materials.Length; i++)
        {
            materials[i].color = originalColors[i];
        }
        showingDamage = false;
    }
    /*
    public int Score
    {
        get { return score; }
        
    }
    public int AllScore
    {
        get
        {
            return AllScore;
        }
        set
        {
            AllScore += value;
        }
    }
    */
}
