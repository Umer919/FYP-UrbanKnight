using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerController : MonoBehaviour
{


    public _Bullet clsBullet;

    public float runSpeed;
    public float walkSpeed;
    Rigidbody myRb;
    Animator myAnim;
    bool facingRight;
    public bool grounded = false;
    Collider[] groundCollitions;
    float groundCheckRadius = 0.2f;
    public LayerMask groundLayer;
    public Transform groundCheck;
    public float jumpHeight;


    void Start()
    {
        myRb = GetComponent<Rigidbody>();
        myAnim = GetComponent<Animator>();
        facingRight = true;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            Instantiate(clsBullet._BulletToSpawn, clsBullet._SpawnPoint.position, clsBullet._SpawnPoint.rotation);
        }
        FncChangeSpawnAngle();
    }
    void FixedUpdate()
    {
        if (grounded && Input.GetAxis("Jump") > 0)
        {
            grounded = false;
            myAnim.SetBool("grounded", false);
            //myRb.AddForce(new Vector3(0, jumpHeight, 0));


        }
        groundCollitions = Physics.OverlapSphere(groundCheck.position, groundCheckRadius, groundLayer);
        if (groundCollitions.Length > 0)
            grounded = true;
        else grounded = false;

        myAnim.SetBool("grounded", grounded);




        float move = Input.GetAxis("Horizontal");
        myAnim.SetFloat("speed", Mathf.Abs(move));

        float sneaking = Input.GetAxisRaw("Fire3");
        myAnim.SetFloat("sneaking", sneaking);



        if (sneaking > 0 && grounded)
        {
            myRb.velocity = new Vector3(move * walkSpeed, myRb.velocity.y, 0);
        }
        else
        {
            myRb.velocity = new Vector3(move * runSpeed, myRb.velocity.y, 0);
        }

        if (move > 0 && !facingRight)
            Flip();
        else if (move < 0 && facingRight)
            Flip();


    }

    void Flip()
    {
        facingRight = !facingRight;
       
        Vector3 theScale = transform.localScale;
        theScale.z *= -1;
        transform.localScale = theScale;
    }

    void FncChangeSpawnAngle()
    {
        Vector3 temp = clsBullet._SpawnPoint.localEulerAngles;
        if (facingRight == false)
        {
            temp.y = 180;
            clsBullet._SpawnPoint.localEulerAngles = new Vector3(clsBullet._SpawnPoint.localEulerAngles.x, temp.y, clsBullet._SpawnPoint.localEulerAngles.z);
        }
        if (facingRight == true)
        {
            temp.y = 0;
            clsBullet._SpawnPoint.localEulerAngles = new Vector3(clsBullet._SpawnPoint.localEulerAngles.x, temp.y, clsBullet._SpawnPoint.localEulerAngles.z);
        }
    }


}
[System.Serializable]
public class _Bullet
{
    public Transform _SpawnPoint;
    public GameObject _BulletToSpawn;
}

