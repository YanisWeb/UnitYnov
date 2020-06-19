using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Powerups : MonoBehaviour
{
    // vitesse de descente du powerup
    public float speed;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // Permet de faire descendre le powerup à une certaine vitesse
        transform.Translate(new Vector2(0f, -1f) * Time.deltaTime * speed);
        // detruit le powerup lorsqu'il quitte l'écran.
        if(transform.position.y < -6f) {
            Destroy(gameObject);
        }

    }
}
