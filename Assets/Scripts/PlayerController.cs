using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;

public class PlayerController : MonoBehaviour
{
    public float speed = 0;
    public TextMeshProUGUI countText;
    public GameObject winTextObject;

    private Rigidbody rb;
    private int count;
    private int jumpCount;
    private float movementX;
    private float movementY;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        count = 0;
        jumpCount = 0;

        SetCountText();
        winTextObject.SetActive(false);
    }

    void OnMove(InputValue movementValue)
    {
        Vector2 movementVector = movementValue.Get<Vector2>();

        movementX = movementVector.x;
        movementY = movementVector.y;
    }

    void SetCountText() {
        countText.text = "Count: " + count.ToString();

        if (count >= 12) {
            winTextObject.SetActive(true);
        }
    }

    void FixedUpdate() {
        Vector3 movement = new Vector3(movementX, 0.0f, movementY);

        rb.AddForce(movement * speed);    

        if (rb.transform.position.y <= 0.5) {
            jumpCount = 0;
        }

        if (jumpCount != 2) {

            if (Input.GetKeyDown ("space")) {
                    Vector3 jump = new Vector3 (0.0f, 200.0f, 0.0f);
                    rb.AddForce (jump);
                    jumpCount = jumpCount + 1;
                }      
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("PickUp")) 
        {
            other.gameObject.SetActive(false);
            count = count + 1;

            SetCountText();
        }
    }
}
