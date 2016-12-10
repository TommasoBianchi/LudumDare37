using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

    public int Speed;
    public int Vita;
    public WeaponTypes WeaponType;
    public float Damage;

    private int MaxVita;
    private Animator animator;
     
	// Use this for initialization
	void Start () {
        Damage = 0;
        WeaponType = WeaponTypes.Sword;
        animator = GetComponent<Animator>();
    }
	
	// Update is called once per frame
	void Update () {
        float MoveHorizontal = Input.GetAxis("Horizontal");
        float MoveVertical = Input.GetAxis("Vertical");

        Vector2 Movement = new Vector2(MoveHorizontal, MoveVertical);

        transform.Translate(Movement * Speed * Time.deltaTime, Space.World);

        UpdateAnimator(Movement);
	}

    private void UpdateAnimator(Vector2 Movement)
    {
        animator.SetFloat("AnimationSpeed", Speed);

        if (Movement.sqrMagnitude > 0)
        {
            if (Mathf.Abs(Movement.x) > Mathf.Abs(Movement.y))
            {
                if (Movement.x > 0)
                    animator.SetInteger("Direction", 1); // right
                else
                    animator.SetInteger("Direction", 3); // left
            }
            else
            {
                if (Movement.y > 0)
                    animator.SetInteger("Direction", 2); // back
                else
                    animator.SetInteger("Direction", 0); // front
            }
        }
        else
        {
            animator.SetInteger("Direction", -1); // idle
        }
    }
}
