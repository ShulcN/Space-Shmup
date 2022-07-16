using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss_1 : Enemy
{
    [Header("Set in Inspector: Boss")]
    public Vector3 Spos = new Vector3(0, 20, 0);
    public float delayBetweenShots = 3f;
    public float velocity = 25;
    public GameObject projectilePrefab;
    public GameObject laserPrefab;
    private GameObject gol;
    private bool laserOn = false;
    private float timShot;
    private float laserTim;
    // Start is called before the first frame update
    void Start()
    {
        timShot = Time.time;
        Move();
    }
    public void LateUpdate()
    {
        
        if (pos.y <= 20)
        {
            this.transform.position = Spos;
        }
        if (Time.time > timShot+delayBetweenShots)
        {
            Fire();
            timShot = Time.time;
        }
        
        
    }
    public void FixedUpdate()
    {
        if (laserTim + 4 < Time.time)
        {

            return;
        }
        else
        {
            

            float max = 0.8f;
            Quaternion first = Quaternion.AngleAxis(120, Vector3.back);
            Quaternion second = Quaternion.AngleAxis(270, Vector3.back);

            gol.transform.rotation = Quaternion.RotateTowards(gol.transform.rotation, second, max);
        }
    }
    public override void Move()
    {
        Vector3 tempPos = pos;
        
        tempPos.y -= speed * Time.deltaTime;
        
        pos = tempPos;
        
    }
    public void Fire()
    {
        Vector3 vel = Vector3.down * velocity;
        GameObject gun1 = GameObject.Find("Collar1");
        GameObject gun2 = GameObject.Find("Collar2");
        GameObject gun3 = GameObject.Find("Collar3");
        if (Random.Range(0, 10) < 6)
        {
            for (int i = 0; i < 5; i++)
            {

                GameObject p = Instantiate(projectilePrefab);
                p.transform.position = gun1.transform.position;
                if (i < 2)
                {
                    p.transform.rotation = Quaternion.AngleAxis(-7 * Random.Range(0, 10), Vector3.back);
                }
                else
                {
                    p.transform.rotation = Quaternion.AngleAxis(4 * Random.Range(0, 10), Vector3.back);
                }
                p.tag = "ProjectileEnemy";
                p.layer = LayerMask.NameToLayer("ProjectileEnemy");
                Projectile pr = p.GetComponent<Projectile>();
                pr.rigid.velocity = p.transform.rotation * vel;
            }
            for (int i = 0; i < 5; i++)
            {

                GameObject p = Instantiate(projectilePrefab);
                p.transform.position = gun2.transform.position;
                if (i > 2)
                {
                    p.transform.rotation = Quaternion.AngleAxis(4 * Random.Range(0, 10), Vector3.back);
                }
                else
                {
                    p.transform.rotation = Quaternion.AngleAxis(-7 * Random.Range(0, 10), Vector3.back);
                }
                p.tag = "ProjectileEnemy";
                p.layer = LayerMask.NameToLayer("ProjectileEnemy");
                Projectile pr = p.GetComponent<Projectile>();
                pr.rigid.velocity = p.transform.rotation * vel;
            }
        } else
        {
            
            Vector3 pos = gun3.transform.position;

             gol = Instantiate(laserPrefab);
            gol.transform.position = pos;
            var step = Time.deltaTime * velocity;

            Laser l = gol.GetComponent<Laser>();
            //Vector3 rot = new Vector3(0, 0, 210);
            
            Quaternion first = Quaternion.AngleAxis(120, Vector3.back);
            
            gol.transform.rotation = first;
            laserOn = true;
            laserTim = Time.time;


            //Destroy(go);
        }
    }
    
}
