using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
//using UnityEngine.AI;

public class Player : MonoBehaviour
{

    public float forcaPulo;
    public float velocidadeMaxima;

    public int lives = 1;
    public int rings;

    public AudioClip moeda;

    public AudioClip pulo;

    public AudioClip audioInicio;
    public AudioClip audioErrou;
    public AudioClip a;
    public AudioClip e;
    public AudioClip i;
    public AudioClip o;
    public AudioClip u;

    public AudioClip audioInicio2;

    public AudioClip b;
    public AudioClip o2;
    public AudioClip l;
    public AudioClip a2;
    
    
    public Text TextLives;
    public Text TextRings;
    private AudioSource audio;

    public bool isGrounded;

    public bool canFly;

    public bool inWater;
    private int fase = 1;

    string[] vogais = { "A", "E", "I", "O", "U" };
    string[] vogaisF2 = { "b", "O", "l", "A" };
    //string[] vogaisCapturadas;
    int qtdVogaisCapturadas = 0;

    public GameObject LastCheckpoint;

    void Start()
    {
        this.audio = GetComponent<AudioSource>();
        TextLives.text = lives.ToString();
        TextRings.text = rings.ToString();
        this.audio.clip = audioInicio;
        this.audio.Play();
    }

    void Update()
    {
        Rigidbody2D rigidbody = GetComponent<Rigidbody2D>();

        float movimento = Input.GetAxis("Horizontal");

        rigidbody.velocity = new Vector2(movimento * velocidadeMaxima, rigidbody.velocity.y);

        if (movimento < 0)
        {
            GetComponent<SpriteRenderer>().flipX = true;
        }
        else if (movimento > 0)
        {
            GetComponent<SpriteRenderer>().flipX = false;
        }

        if (movimento > 0 || movimento < 0)
        {
            GetComponent<Animator>().SetBool("walking", true);
        }
        else
        {
            GetComponent<Animator>().SetBool("walking", false);
        }

        if (!inWater)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                if (isGrounded)
                {
                    rigidbody.AddForce(new Vector2(0, forcaPulo));


                    if (!this.audio.isPlaying)
                    {
                        audio.clip = pulo;
                        this.audio.Play();
                    }

                    canFly = false;
                }
                else
                {
                    canFly = true;
                }
            }

            if (canFly && Input.GetKey(KeyCode.Space))
            {
                GetComponent<Animator>().SetBool("flying", true);
                rigidbody.velocity = new Vector2(rigidbody.velocity.x, -0.5f);
            }
            else
            {
                GetComponent<Animator>().SetBool("flying", false);
            }

            if (isGrounded)
            {
                GetComponent<Animator>().SetBool("jumping", false);
            }
            else
            {
                GetComponent<Animator>().SetBool("jumping", true);
            }
        }
        else
        {
            if (Input.GetKey(KeyCode.UpArrow))
            {
                rigidbody.AddForce(new Vector2(0, 6f * Time.deltaTime), ForceMode2D.Impulse);
            }

            if (Input.GetKey(KeyCode.DownArrow))
            {
                rigidbody.AddForce(new Vector2(0, -6f * Time.deltaTime), ForceMode2D.Impulse);
            }

            rigidbody.AddForce(new Vector2(0, 10f * Time.deltaTime), ForceMode2D.Impulse);
        }

        GetComponent<Animator>().SetBool("swimming", inWater);

        if (Input.GetKey(KeyCode.LeftControl))
        {
            GetComponent<Animator>().SetTrigger("hammer");
            Collider2D[] colliders = new Collider2D[3];
            transform.Find("HammerArea").gameObject.GetComponent<Collider2D>()
                .OverlapCollider(new ContactFilter2D(), colliders);
            for (int i = 0; i < colliders.Length; i++)
            {
                if (colliders[i] != null && colliders[i].gameObject.CompareTag("Monstros"))
                {
                    Destroy(colliders[i].gameObject);
                }
            }
        }

    }

    void OnTriggerEnter2D(Collider2D collision2D)
    {
        if (collision2D.gameObject.CompareTag("Water"))
        {
            inWater = true;
        }

        if (collision2D.gameObject.CompareTag("Moedas"))
        {

            Destroy(collision2D.gameObject);
            rings++;
            TextRings.text = rings.ToString();

            if (!this.audio.isPlaying)
            {
                this.audio.clip = moeda;
                this.audio.Play();

            }

        }

        if (collision2D.gameObject.CompareTag("Checkpoint"))
        {
            LastCheckpoint = collision2D.gameObject;
        }

    }

    void OnTriggerExit2D(Collider2D collision2D)
    {
        if (collision2D.gameObject.CompareTag("Water"))
        {
            inWater = false;
        }
    }

    void OnCollisionEnter2D(Collision2D collision2D)
    {
        if (collision2D.gameObject.CompareTag("Monstros"))
        {
            lives--;
            TextLives.text = lives.ToString();
            if (lives == 0)
            {
                lives++;
                TextLives.text = lives.ToString();
                transform.position = LastCheckpoint.transform.position;
            }
        }

        if (collision2D.gameObject.CompareTag("Plataformas"))
        {
            isGrounded = true;
        }

        if (collision2D.gameObject.CompareTag("Trampolim"))
        {
            GetComponent<Rigidbody2D>().velocity = new Vector2(0f, 10f);
        }

        if (collision2D.gameObject.CompareTag("Teleporter") && qtdVogaisCapturadas == 5)
        {
            GetComponent<Rigidbody2D>().transform.position = collision2D.gameObject.transform.Find("WayPoint").position;
            qtdVogaisCapturadas = 0;
            this.fase = 2;
            this.audio.clip = audioInicio2;
            this.audio.Play();
        }

        if (collision2D.gameObject.CompareTag("letra"))
        {

            //.Equals
            if (this.fase == 1)
            {
                if (qtdVogaisCapturadas == 0)
                {
                    if (collision2D.gameObject.name.Equals(vogais[qtdVogaisCapturadas] + "(Clone)"))
                    {
                        qtdVogaisCapturadas++;
                        GetComponent<AudioSource>().Play();
                        Destroy(collision2D.gameObject);
                        this.audio.clip = a;
                        this.audio.Play();


                    }
                    else
                    {
                        lives--;
                        TextLives.text = lives.ToString();
                        this.audio.clip = audioErrou;
                        this.audio.Play();
                        if (lives == 0)
                        {
                            lives++;
                            TextLives.text = lives.ToString();
                            transform.position = LastCheckpoint.transform.position;
                        }
                    }
                }
                else
                {
                    if (collision2D.gameObject.name.Equals(vogais[qtdVogaisCapturadas] + "(Clone)"))
                    {
                        qtdVogaisCapturadas++;
                        if (vogais[qtdVogaisCapturadas - 1] == "E")
                        {
                            this.audio.clip = e;
                            this.audio.Play();

                        }
                        else if (vogais[qtdVogaisCapturadas - 1] == "I")
                        {
                            this.audio.clip = i;
                            this.audio.Play();

                        }
                        else if (vogais[qtdVogaisCapturadas - 1] == "O")
                        {
                            this.audio.clip = o;
                            this.audio.Play();

                        }
                        else
                        {
                            this.audio.clip = u;
                            this.audio.Play();
                        }
                        Destroy(collision2D.gameObject);
                    }
                    else
                    {
                        lives--;
                        TextLives.text = lives.ToString();
                        this.audio.clip = audioErrou;
                        this.audio.Play();
                        if (lives == 0)
                        {
                            lives++;
                            TextLives.text = lives.ToString();
                            transform.position = LastCheckpoint.transform.position;
                        }
                    }
                }
                Debug.Log("Nome do objeto : " + collision2D.gameObject.name);

            }
            else
            {

                if (qtdVogaisCapturadas == 0)
                {
                    if (collision2D.gameObject.name.Equals(vogaisF2[qtdVogaisCapturadas] + "(Clone)"))
                    {
                        qtdVogaisCapturadas++;
                        Destroy(collision2D.gameObject);
                        this.audio.clip = b;
                        this.audio.Play();
                    }
                }
                else
                {
                    if (collision2D.gameObject.name.Equals(vogaisF2[qtdVogaisCapturadas] + "(Clone)"))
                    {
                        qtdVogaisCapturadas++;
                        Destroy(collision2D.gameObject);
                        Debug.Log("Nome do objeto : " + collision2D.gameObject.name);
                        
                        if (vogaisF2[qtdVogaisCapturadas - 1] == "O")
                        {
                            this.audio.clip = o2;
                            this.audio.Play();

                        }
                        else if (vogaisF2[qtdVogaisCapturadas - 1] == "l")
                        {
                            this.audio.clip = l;
                            this.audio.Play();

                        }
                        else
                        {
                            this.audio.clip = a2;
                            this.audio.Play();
                        }
                    }
                    else
                    {
                        
                    }
                }


            }

        }
    }

    void OnCollisionExit2D(Collision2D collision2D)
    {
        if (collision2D.gameObject.CompareTag("Plataformas"))
        {
            isGrounded = false;
        }
    }
}
