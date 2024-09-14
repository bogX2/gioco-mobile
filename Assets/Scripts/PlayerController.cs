using System;
using UnityEngine;
using UnityEngine.InputSystem;
[RequireComponent(typeof(Rigidbody2D),typeof(TouchingDirections))]

public class PlayerController : MonoBehaviour
{
    public bool CanMove{
        get{
            return animator.GetBool(AnimationStrings.canMove);
        }
    } 

    public bool IsAlive{
        get{
            return animator.GetBool(AnimationStrings.isAlive);
        }
    }
    Rigidbody2D rb;

    Animator animator;

    public float walkSpeed= 5f;
    public float runSpeed=8f;
    public float jumpImpulse = 10f;

    public float airWalkSpeed= 3f;
    TouchingDirections touchingDirections;

    
    public bool _isFacingRight=true;

    public bool IsFacingRight{get{
        return _isFacingRight;
    } private set{
        if(_isFacingRight!=value){
            transform.localScale*= new Vector2(-1, 1);

        }
        _isFacingRight=value;

    }}


    public float CurrentMoveSpeed{
        get{
            if(CanMove){
                if(IsMoving && !touchingDirections.IsOnWall){

                if(touchingDirections.IsGrounded){
                    if(IsRunning){
                    return runSpeed;
                }else{
                    return walkSpeed;
                }
                } else
                {
                    //Air Move
                    return airWalkSpeed;
                }

                
            }else{
                return 0;
            }
        } else
        {
            //Movement lock
            return 0;
        }
            }
            
    }

    Vector2 moveInput;
     
     [SerializeField]
    private bool _isMoving= false;

    public bool IsMoving { get{
        return _isMoving;
    }
     private set{
        _isMoving=value;
        animator.SetBool(AnimationStrings.isMoving,value);
        
    }
    }


    [SerializeField]
    private bool _isRunning=false;
    public bool IsRunning{
        get{
            return _isRunning;
        }

        set{
            _isRunning=value;
            animator.SetBool(AnimationStrings.isRunning,value);
        }
    }

    private void Awake(){
        rb=GetComponent<Rigidbody2D>();
        animator=GetComponent<Animator>();
        touchingDirections = GetComponent<TouchingDirections>();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void FixedUpdate(){
        rb.velocity = new Vector2(moveInput.x * CurrentMoveSpeed, rb.velocity.y);
        animator.SetFloat(AnimationStrings.yVelocity,rb.velocity.y);
    }

     public  void OnMove(InputAction.CallbackContext context){

      
      moveInput=context.ReadValue<Vector2>();

      if(IsAlive){
        IsMoving= moveInput != Vector2.zero;

         SetFacingDirection(moveInput);
      } else {
        IsMoving=false;
      }

      
      
    }

    private void SetFacingDirection(Vector2 moveInput)
    {
        if(moveInput.x >0 && !IsFacingRight){
            IsFacingRight=true;

        }else if(moveInput.x <0 && IsFacingRight ){
            IsFacingRight=false;

        }
    }

    public void OnRun(InputAction.CallbackContext context){
        if(context.started){
            IsRunning=true;
        }else if(context.canceled){
            IsRunning=false;
        }
    }

    public void onJump(InputAction.CallbackContext context){

        //check if alive as well
        if(context.started && touchingDirections.IsGrounded && CanMove){

            animator.SetTrigger(AnimationStrings.jumpTrigger);
            rb.velocity= new Vector2(rb.velocity.x, jumpImpulse);
        }
    }

    public void OnAttack(InputAction.CallbackContext context){
        if (context.started)
        {
            animator.SetTrigger(AnimationStrings.attackTrigger);
        }
    }
}
