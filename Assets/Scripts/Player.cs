using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Player : MonoBehaviour
{
    [SerializeField] int JumpForce = 7;
    [SerializeField] Transform groundCheckTransform = null;
    [SerializeField] LayerMask playerMask;
    [SerializeField] TMP_Text score;

    bool isJumping;
    bool isSuperJumping;
    Rigidbody rb;
    float horizontal;
    int superJumps = 0;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        score.text = "";
    }

    // Update is called once per frame
    void Update()
    {
        bool isSpace = Input.GetKeyDown(KeyCode.Space);
        bool isLeftShift = Input.GetKey(KeyCode.LeftShift);
        bool isOne = Input.GetKey(KeyCode.Alpha1);
        bool isTwo = Input.GetKey(KeyCode.Alpha2);
        bool isThree = Input.GetKey(KeyCode.Alpha3);

        if (isOne && isTwo && isThree)
        {
            GameManager.Instance.LoadNextScene();
        }

        if (isSpace)
        {
            isJumping = true;
        }

        if (isLeftShift && isSpace)
        {
            isSuperJumping = true;
        }

        horizontal = Input.GetAxis("Horizontal");
    }

    private void FixedUpdate()
    {
        rb.velocity = new Vector3(horizontal * 1.5f, rb.velocity.y, 0);

        bool isInAir = Physics.OverlapSphere(groundCheckTransform.position, 0.1f, playerMask).Length == 0;

        if (!isInAir && isJumping)
        {
            float augmentedJumpForce = JumpForce;

            if (isSuperJumping && superJumps > 0)
            {
                augmentedJumpForce *= 1.5f;
                superJumps--;
            }

            rb.AddForce(Vector3.up * augmentedJumpForce, ForceMode.VelocityChange);
            isJumping = false;
            isSuperJumping = false;
        }

        if (superJumps > 0)
        {
            var s = superJumps == 1 ? "" : "s";
            score.text = $"{superJumps} Super Jump{s}";
        } else
        {
            score.text = "";
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Nom"))
        {
            Destroy(other.gameObject);
            superJumps++;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("CrushyBox"))
        {
            Destroy(collision.gameObject);
            superJumps += 5;
        }
    }
}
