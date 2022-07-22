using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public int speed;
    public int rotationSpeed;
    public GameObject player;


    // (x, y)  = (0,0)
    // 1s  = 60 times
    // (x, y) = (0, 60*speed)
    private void Update()
    {

        if (Input.GetKey(KeyCode.W))
        {
            player.transform.Translate(Vector2.up * speed * Time.deltaTime);
        }
        else if (Input.GetKey(KeyCode.S))
        {
            player.transform.Translate(Vector2.down * speed * Time.deltaTime);
        }
        else if (Input.GetKey(KeyCode.A))
        {
            player.transform.Translate(Vector2.left * speed * Time.deltaTime);
        }
        else if (Input.GetKey(KeyCode.D))
        {
            player.transform.Translate(-Vector2.left * speed * Time.deltaTime);
        }
        else if (Input.GetKey(KeyCode.Space))
        {
            player.transform.Rotate(Vector3.forward * rotationSpeed);
        }


    }
}
