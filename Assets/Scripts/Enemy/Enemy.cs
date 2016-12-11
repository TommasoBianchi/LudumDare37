using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour {

	public List<WeaponType> Weaknesses = new List<WeaponType>();
	public List<WeaponType> Resistences = new List<WeaponType>();
	public float Life;
	public PowerUpData PowerUpData = PowerUpFactory.GetPowerUpNull();

    private Rigidbody2D myRigidbody2D;
    private Animator animator;

    public EnemyData enemyData;

    public GameObject DamageText;

	void Start () {
        myRigidbody2D = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
	}
	
	void Update () {
		Move ();
		Attack ();
	}

	void Move() {

	}

	void Attack() {
        Vector2 velocity = myRigidbody2D.velocity;
        UpdateAnimator(velocity);
	}

    private void UpdateAnimator(Vector2 Movement)
    {
        animator.SetFloat("AnimationSpeed", 1);

        if (Movement.sqrMagnitude > 0.01f)
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

    public void Hit(Bullet bullet) {
        this.gameObject.GetComponent<Rigidbody2D>().velocity = bullet.gameObject.GetComponent<Rigidbody2D>().velocity;
        float damage = EnemyFactory.getInstance().calculateDamage(this.enemyData);
        this.Life -= damage;
        GameObject text = Instantiate(DamageText, transform.position, Quaternion.identity) as GameObject;
        text.transform.parent = GameObject.Find("OverlayCanvas").transform;
        Debug.Log("Damage " + damage + ", remaining life " + this.Life);
        if (this.Life <= 0) {
            Destroy(this.gameObject);
        }
        Destroy(bullet.gameObject);
    }
}
