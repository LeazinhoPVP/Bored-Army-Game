using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    public GameObject playerSatic;
    SpriteRenderer spriteRenderer;
    public Sprite[] sprites;

    public int maxSpeed;
    public float speed;

    public GameObject bullet;
    public Transform gunPos;

    public GameObject canon;

    float timer;
    public int fireRate;

    float specialTimer;
    public int specialFireRate;
    public Transform[] specialFirePoints;

    public int lifes;
    public Slider lifeSlider;

    public AudioClip shootSFX;
    public AudioClip damageSFX;
    public ParticleSystem deathEffect;

    bool alive;
    void Start()
    {
        alive = true;
        spriteRenderer = GetComponent<SpriteRenderer>();
        GameManager.Instance.player = this.gameObject;
    }
    void FixedUpdate()
    {
        lifeSlider.transform.position = new Vector3(transform.position.x, transform.position.y + 0.5f, 0);
        if (alive)
        {
            Move();
            Fire();
            TripleFire();
        }  
    }
    void Move()
    {
        if (speed > 0)
            speed -= 0.3f * Time.fixedDeltaTime;

        if (transform.position.x <= -9)
        {
            transform.position = new Vector3(-9, transform.position.y, 0);
        }
        if (transform.position.x >= 9)
        {
            transform.position = new Vector3(9, transform.position.y, 0);
        }
        if (transform.position.y <= -5)
        {
            transform.position = new Vector3(transform.position.x,-5 , 0);
        }
        if (transform.position.y >= 5)
        {
            transform.position = new Vector3(transform.position.x,5, 0);
        }



            transform.position += transform.up * speed * Time.fixedDeltaTime;

        if (Input.GetKey(KeyCode.W))
        {
            if(speed <= maxSpeed)
                speed += 0.5f * Time.fixedDeltaTime;
        }
        if (Input.GetKey(KeyCode.S))
        {
            if (speed > -1)
                speed -= 0.5f * Time.fixedDeltaTime;
        }

        if (Input.GetKey(KeyCode.A))
        {
            transform.Rotate(0, 0, 100 * Time.deltaTime);
        }

        if (Input.GetKey(KeyCode.D))
        {
            transform.Rotate(0, 0, -100 * Time.deltaTime);
        }
    }
    void ChangeSprite()
    {
        GameManager.Instance.PlayAudio(damageSFX, 0.5f, 1f);
        lifeSlider.value = lifes;
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
    void Fire()
    {
        timer += Time.fixedDeltaTime;
        if (Input.GetMouseButton(0) && timer >= fireRate)
        {
            Instantiate(bullet, gunPos.position, gunPos.rotation);
            GameManager.Instance.PlayAudio(shootSFX, 0.7f, Random.Range(0.6f, 0.8f));
            timer = 0;
        }
    }
    void TripleFire()
    {
        specialTimer += Time.fixedDeltaTime;
        if (Input.GetMouseButton(1) && specialTimer >= specialFireRate)
        {
            for (int i = 0; i < specialFirePoints.Length; i++)
            {
                GameManager.Instance.PlayAudio(shootSFX, 0.3f, 0.8f);
                Instantiate(bullet, specialFirePoints[i].position, specialFirePoints[i].rotation);
            }
            specialTimer = 0;
        }
    }

    void Die()
    {
        Instantiate(deathEffect, transform.position, transform.rotation);
        playerSatic.SetActive(false);
        alive = false;
        spriteRenderer.enabled = false;
        Invoke("EndGame", 2f);
        canon.gameObject.SetActive(false);
    }
    private void EndGame()
    {
        GameManager.Instance.GameOver();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Enemy") && alive)
        {
            lifes -= 1;
            ChangeSprite();
            Destroy(other.gameObject);
            if (lifes <= 0)
                Die();
        }
    }
}
