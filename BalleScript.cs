using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class BalleScript : MonoBehaviour
{

    public Rigidbody2D rb;
    public bool inPlay;
    public Transform plateforme;
    public float speed;
    public Transform explosion;
    AudioSource audio;
    public GameManager gm;
    public Transform powerup;
    public int dropchancepowerup;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D> ();
        audio = GetComponent<AudioSource> ();
    }

    // Update is called once per frame
    void Update()
    {
        // Si gameOver, return
        if(gm.gameOver){
            return;
        }
        // !inPlay = inPlay = false.
        if(!inPlay){
            //Place la balle sur la plateforme
            transform.position = plateforme.position;
        }

        if(Input.GetButtonDown("Jump") && !inPlay){
            //Après la pression sur espace lance la balle (AddForce) et passe inPlay en true.
            inPlay = true;
            rb.AddForce(Vector2.up * speed);
        }
    }

    void OnTriggerEnter2D(Collider2D other) {
        // Compare au tag bottom (bas du screen et retire les mouvement de la balle avant de la placer)
        if (other.CompareTag("bottom")) {
            rb.velocity = Vector2.zero;
            // Passe inPlay en false pour la replacer et retire une vie
            inPlay = false;
            gm.UpdateLives(-1);
        }
    }
    void OnCollisionEnter2D(Collision2D other) {
        if (other.transform.CompareTag("brick")) {
            BrickScript brickScript = other.gameObject.GetComponent<BrickScript> ();
            if(brickScript.hitsBreak > 1) {
                brickScript.BreakBrick();
            } else {
                int randChance = Random.Range(1,101);
                if(randChance < dropchancepowerup) {
                    Instantiate(powerup,other.transform.position, other.transform.rotation);
                }
                //Instancie l'explosion (effet) au niveau de la brique, puis detruit l'explosion au bout d'une seconde
                Transform newExplosion = Instantiate(explosion, other.transform.position, other.transform.rotation);
                Destroy(newExplosion.gameObject,1f);

                gm.UpdateScore(brickScript.points);
                gm.UpdateBricks();
                // Detruit la brique après avoir update le score et le nombre de briques
                Destroy(other.gameObject);
            }
            
            audio.Play();
        }
    }
}