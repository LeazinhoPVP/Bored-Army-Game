using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class EnemyChaser : MonoBehaviour
{
    public GameObject enemyChaser;
    public Transform target;
    public int lifes = 3;
    public Sprite[] sprites;
    public SpriteRenderer spriteRenderer;
    public GameObject spritePivot;
    public ParticleSystem deathEffect;

    public int speed;

    public Slider lifeBar;
   // public GameObject slider;

    float distance;

    

    public AudioClip damageSFX;
    void Start()
    {
        //lifeBar = slider.GetComponent<Slider>();
        target = GameManager.Instance.player.transform;
    }

    void FixedUpdate()
    {
        lifeBar.transform.position = new Vector3(transform.position.x, transform.position.y + 0.5f, 0);
        Look();
        Move();
    }
    void Look()
    {
        distance = Vector3.Distance(target.position, transform.position);
        Vector3 targetPos = target.position - transform.position;
        float angle = Mathf.Atan2(targetPos.y, targetPos.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
    }
    void Move()
    {
        transform.position += transform.right * speed * Time.fixedDeltaTime;   
    }
    void ChangeSprite()
    {
        GameManager.Instance.PlayAudio(damageSFX, 0.5f, Random.Range(0.9f, 1.4f));
        lifeBar.value = lifes;
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
        Destroy(enemyChaser.gameObject);
    }
    private void AutoDestroy()
    {
        Instantiate(deathEffect, transform.position, transform.rotation);
        Destroy(enemyChaser.gameObject);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("PlayerBullet"))
        {
            lifes -= 1;
            ChangeSprite();
            Destroy(other.gameObject);
            if (lifes <= 0)           
                Die();                     
        }
        if (other.gameObject.CompareTag("Player"))
        {
            AutoDestroy();
        }
    }
}
