using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TouchingDirections : MonoBehaviour
{

    public ContactFilter2D castFilter;
     CapsuleCollider2D touchingCol;
     Animator animator;

     RaycastHit2D[] groundHits= new RaycastHit2D[5];
     public float groundDistance=0.05f;

     Rigidbody2D rb;

    [SerializeField]
     private bool _isGrounded;

    public bool IsGrounded { get{
        return _isGrounded;
    } private set{
        _isGrounded=value;
        animator.SetBool("isGrounded",value);
    } }

    private void Awake(){
        rb=GetComponent<Rigidbody2D>();
        touchingCol=GetComponent<CapsuleCollider2D>();
        animator=GetComponent<Animator>();
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame

    void FixedUpdate(){

        IsGrounded= touchingCol.Cast(Vector2.down,castFilter,groundHits,groundDistance)>0;

    }
}
