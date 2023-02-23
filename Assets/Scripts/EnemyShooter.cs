using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyShooter : MonoBehaviour
{
    public GameObject enemyShooter;
    Transform target;
    public int lifes = 3;
    public Sprite[] sprites;
    public SpriteRenderer spriteRenderer;
    public GameObject spritePivot;
    int speed = 1;

    public Transform firePoint;
    public GameObject bullet;
    int fireRate;
    float timer;

    float distance;

    public Slider lifeBar;

    public AudioClip shootSFX;
    public AudioClip damageSFX;
    public ParticleSystem deathEffect;
    void Start()
    {
        target = GameManager.Instance.player.gameObject.transform;
        fireRate = Random.Range(1, 3);
    }

    void Update()
    {
        lifeBar.transform.position = new Vector3(transform.position.x, transform.position.y + 0.5f, 0);
        Look();
        Fire();
        Move();
    }
    void Look()
    {
        distance = Vector3.Distance(target.position, transform.position);
        Vector3 targetPos = target.position - transform.position;
        float angle = Mathf.Atan2(targetPos.y, targetPos.x) * Mathf.Rad2Deg;
        spritePivot.transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
    }
    void Move()
    {
        if(distance >= 4)
            transform.position += spritePivot.transform.right * speed * Time.deltaTime;
    }
    void Fire()
    {
        timer += Time.deltaTime;
        if(timer >= fireRate && distance <=5)
        {
            Instantiate(bullet, firePoint.position, firePoint.rotation);
            GameManager.Instance.PlayAudio(damageSFX, 0.5f, Random.Range(1.3f,1.6f));
            timer = 0;
        }
    }
    void ChangeSprite()
    {
        lifeBar.value = lifes;
        GameManager.Instance.PlayAudio(damageSFX, 0.5f, Random.Range(0.8f, 1.5f));
        switch (lifes)
        {
            case 3:
                spriteRenderer.sprite = sprites[0];
                break;
            case 2:
                spriteRenderer.sprite = sprites[1];
                break;
            case 1:
                spriteRenderer.sprite = sprites[2];
                break;
        }
    }

    public void Die()
    {
        Instantiate(deathEffect, transform.position, transform.rotation);
        GameManager.Instance.AddPoints(1);
        Destroy(enemyShooter.gameObject);
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("PlayerBullet"))
        {          
            lifes-= 1;
            ChangeSprite();
            Destroy(other.gameObject);
            if (lifes <= 0)
                Die();
        }
    }
}
