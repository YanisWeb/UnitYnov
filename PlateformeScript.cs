using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlateformeScript : MonoBehaviour
{
    public float speed;
    public float rightWall;
    public float leftWall;
    public GameManager gm;
    AudioSource audio;

    // Start is called before the first frame update
    void Start()
    {
        audio = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if(gm.gameOver){
            return;
        }
        float horizontal = Input.GetAxis ("Horizontal");
    
        transform.Translate (Vector2.right * horizontal * Time.deltaTime * speed);
        if(transform.position.x < leftWall) {
            transform.position = new Vector2 (leftWall, transform.position.y);
        }
        if(transform.position.x > rightWall) {
            transform.position = new Vector2 (rightWall, transform.position.y);
        }
    }
    // Lorsque le pannel touche un bonus, le joueur gagne une vie.
    void OnTriggerEnter2D(Collider2D other) {
        if(other.CompareTag("extralife")) {
        gm.UpdateLives(1);
        Destroy(other.gameObject);
        }
        audio.Play();
    }
}
