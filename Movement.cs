using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Speeds { Slow = 0, Normal = 1, Fast = 2, Faster = 3, Fastest = 4 };
public class Movement : MonoBehaviour
{
        public List<string> inventory;

    public Speeds CurrentSpeed;
    //                       0      1      2       3      4
    float[] SpeedValues = { 3f, 10.4f, 12.96f, 15.6f, 19.27f };

    public Transform GroundCheckTransform;
    public float GroundCheckRadius;
    public LayerMask GroundMask;
    public float jumpVelocity;
    public float fallMultiplier = 2.5f;
    public float lowJumpMultiplier = 2f;

    Rigidbody2D rb;

    void Start()
    {
        inventory = new List<string>();
        
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
           if (rb.velocity.y < 0){
                rb.velocity += Vector2.up * Physics2D.gravity.y * (fallMultiplier - 1) * Time.deltaTime;
            }
            else if (rb.velocity.y > 0 && !Input.GetButton ("Jump")){
                rb.velocity += Vector2.up * Physics2D.gravity.y * (lowJumpMultiplier -1) * Time.deltaTime;
            }        

        transform.position += Vector3.right * SpeedValues[(int)CurrentSpeed] * Time.deltaTime;
        if (Input.GetButtonDown ("Jump")){
            GetComponent<Rigidbody2D>().velocity = Vector2.up * jumpVelocity;
        }
        if(Input.GetMouseButton(0)) 
        {
            //Jump
            if (OnGround())
            {
                rb.velocity = Vector2.zero;
                rb.AddForce(Vector2.up * 26.6581f, ForceMode2D.Impulse);
            }
        }
    }

    bool OnGround()
    {
        return Physics2D.OverlapCircle(GroundCheckTransform.position, GroundCheckRadius, GroundMask);
    }
     private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("collectable"))
         {
        string itemType = collision.gameObject.GetComponent<CollectableScript>().itemType;
        print("we have collected a: " + itemType);

        inventory.Add(itemType);
        print("Inventory length:" + inventory.Count);

        Destroy(collision.gameObject);
        }
    }
}