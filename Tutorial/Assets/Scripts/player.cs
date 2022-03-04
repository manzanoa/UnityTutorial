using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class player : MonoBehaviour
{

    private bool isJumping = false;
    private bool onGround;
    private float horizontalAxis;
    private float verticalAxis;
    private float mouseX;
    private float mouseY;
    private Rigidbody rigidBody;
    public int ammo = 0;
    public int health = 100;
    public bool dead = false;
    public int maxHealth = 100;
    private float mouseSensitivity = 30;

    private float yRotation = 0;
    public Transform cameraTransform;

    public Text ammoText;

    private Vector3 move;

    public Slider healthBar;

    public Transform checkpoint;

    public GameObject projectile;
    public Transform projectileSpawn;

    public bool isPoisoned = false;
    public bool isHealing = false;



    // Start is called before the first frame update
    void Start()
    {
        //setting the ui stuff in the beginning

        ammoText.text = ammo.ToString();
        rigidBody = GetComponent<Rigidbody>();
        healthBar.maxValue = maxHealth;
        healthBar.value = health;

        //locks the cursor in the center
        Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    void Update()
    {
        //gets the verticle and horizontal input
        horizontalAxis = Input.GetAxis("Horizontal");
        verticalAxis = Input.GetAxis("Vertical");

        //gets the verticle and horizontal input from the mouse
        mouseX = Input.GetAxis("Mouse X") * mouseSensitivity;
        mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity;

        //sets the move vector based on the character's transform
        move = transform.right * horizontalAxis + transform.forward * verticalAxis;

        //sets the rotation of the camera in y direction but restricts its view
        yRotation = yRotation - mouseY;
        yRotation = Mathf.Clamp(yRotation, -45f, 45f);

        //space bar pressed isJumping is true
        if (Input.GetKeyDown(KeyCode.Space))
        {
            isJumping = true;
        }

        
        //update the value of the health bar and ammo counter
        healthBar.value = health;
        ammoText.text = ammo.ToString(); 

        //if dead send player to last checkpoint and reset health
        if (dead)
        {
            dead = false;
            rigidBody.transform.position = checkpoint.position;
            rigidBody.rotation = checkpoint.rotation;
            health = maxHealth;
        }

    }

    //like update but for physics stuff
    private void FixedUpdate()
    {

        //if isJumping and onGround make player jump
        if (isJumping && onGround)
        {
            rigidBody.AddForce(Vector3.up * 600, ForceMode.Impulse);
            isJumping = false;
        }

        //updates the player velocity vector to the move vector * speed factor (aka moves the character)
        rigidBody.velocity = move * 5;

        //updates the camera y rotation(for looking up and down)
        cameraTransform.localRotation = Quaternion.Euler(yRotation, 0, 0);

        //updates the player x rotation
        transform.Rotate(Vector3.up * mouseX);




    }

    
    private void OnCollisionEnter(Collision collision)
    {
        onGround = true;
    }

    private void OnCollisionExit(Collision collision)
    {
        onGround = false;
    }

    
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == 6)
        {
            StartCoroutine(PowerCoreCycle(other));
        }
    }


    IEnumerator PowerCoreCycle(Collider other)
    {
        other.gameObject.SetActive(false);
        ammo++;
        ammoText.text = ammo.ToString();

        yield return new WaitForSeconds(2);
        other.gameObject.SetActive(true);
    }
}