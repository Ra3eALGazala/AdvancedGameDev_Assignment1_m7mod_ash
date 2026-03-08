
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    public Animator animator;
    [Header("Movement")]
    private float horizontal;
    
    private bool isfacingRight = true;

    [Header("SerializeFields")]
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private Transform groundCheck;
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private TrailRenderer tr;
    [SerializeField] private Transform wallCheck;
    [SerializeField] private LayerMask wall;
    [SerializeField] private Transform sword;
    [Header("dash")]
    private bool canDash = true;
    private bool isDashing;
    private float dashingPower = 24f;
    private float dashingTime = 0.2f;
    private float dashingCooldown = 1f;

    [Header("Wall")]
    private bool isWallSliding;
    private float wallSlidingSpeed = 2f;

    private bool iswallJumping;
    private float wallJumpingDirection;
    private float wallJumpingTime = 0.2f;
    private float wallJumpingCounter;
    private float wallJumpingDuration = 0.2f;
    private Vector2 wallJumpingPower = new Vector2(8f, 16f);
     [Header("stats")]
     public float hp=100f;
     public float speed = 8.0f;
     public float jumpForce = 16.0f;
     public float attacks = 5f;
     
    //__________________________________________________________________________________________________
    void Update()
    {
        horizontal = Input.GetAxisRaw("Horizontal");

        if (isDashing) { return; } // توقف عن معالجة الحركة أثناء الاندفاع

        if (Input.GetButtonDown("Jump") && IsGrounded())
        { rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpForce); }

        if (Input.GetButtonUp("Jump") && rb.linearVelocity.y > 0f)
        { rb.linearVelocity = new Vector2(rb.linearVelocity.x, rb.linearVelocity.y * 0.5f); }

        if (Input.GetKeyDown(KeyCode.LeftShift) && canDash)
        { StartCoroutine(Dash()); }

        // الترتيب مهم: يجب تحديث حالة الانزلاق قبل معالجة القفز
        WallSlide();
        WallJump();
        attack();

        if (!iswallJumping && !isDashing) { Flip(); }
    }

    void FixedUpdate()
    {
        if (isDashing)
        { return; }
        if (!iswallJumping)
        { rb.linearVelocity = new Vector2(horizontal * speed, rb.linearVelocity.y); }
    }
    //__________________________________________________________________________________________________

    private void WallJump()
    {
        if (isWallSliding)
        {
            iswallJumping = false;
            wallJumpingDirection = transform.localScale.x;
            wallJumpingCounter = wallJumpingTime;

            CancelInvoke(nameof(StopWallJumping));
        }
        else
        {
            wallJumpingCounter -= Time.deltaTime;
        }

        if (Input.GetButtonDown("Jump") && wallJumpingCounter > 0f)
        {
            iswallJumping = true;
            rb.linearVelocity = new Vector2(wallJumpingDirection * wallJumpingPower.x, wallJumpingPower.y);
            wallJumpingCounter = 0f;

            // للتأكد من قلب الشخصية إذا قفز عكس الاتجاه الذي كانت تواجهه
            if (transform.localScale.x != wallJumpingDirection)
            {
                isfacingRight = !isfacingRight;
                Vector3 localScale = transform.localScale;
                localScale.x *= -1f;
                transform.localScale = localScale;
            }

            Invoke(nameof(StopWallJumping), wallJumpingDuration);
        }
    }

    private void StopWallJumping() // الدالة الوحيدة المستخدمة لإنهاء قفز الجدار
    {
        iswallJumping = false;
    }

    private bool IsWalled()
    {
        return Physics2D.OverlapCircle(wallCheck.position, 0.2f, wall);
    }

    private void WallSlide()
    {
        if (IsWalled() && !IsGrounded() && horizontal != 0f)
        {
            isWallSliding = true;
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, Mathf.Clamp(rb.linearVelocity.y, -wallSlidingSpeed, float.MaxValue));
        }
        else
        {
            isWallSliding = false;
        }
    }

    private IEnumerator Dash()
    {
        canDash = false;
        isDashing = true;
        float originalGravity = rb.gravityScale;
        rb.gravityScale = 0f;
        // استخدام transform.localScale.x للاندفاع في الاتجاه الذي يواجهه اللاعب
        rb.linearVelocity = new Vector2(transform.localScale.x * dashingPower*-1, 0f);
        tr.emitting = true;
        yield return new WaitForSeconds(dashingTime);
        tr.emitting = false;
        rb.gravityScale = originalGravity;
        isDashing = false;
        yield return new WaitForSeconds(dashingCooldown);
        canDash = true;
    }

    private bool IsGrounded()
    {
        return Physics2D.OverlapCircle(groundCheck.position, 0.2f, groundLayer);
    }

    private void Flip()
    {
        if (isfacingRight && horizontal < 0f || !isfacingRight && horizontal > 0f)
        {
            isfacingRight = !isfacingRight;
            Vector3 localScale = transform.localScale;
            localScale.x *= -1f;
            transform.localScale = localScale;
        }
    }

private void attack()
    {
        if (Input.GetMouseButton(1))
        {
           animator.SetBool("att", true);
        }
        else
        {
            animator.SetBool("att", false);
        }
        if (Input.GetAxisRaw("Horizontal")!=0)
        {
           animator.SetBool("move", true);
        }
        else
        {
            animator.SetBool("move", false);
        }
        
    }




}