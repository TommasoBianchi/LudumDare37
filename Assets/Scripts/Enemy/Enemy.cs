using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Enemy : MonoBehaviour {

	public List<WeaponType> Weaknesses = new List<WeaponType>();
	public List<WeaponType> Resistences = new List<WeaponType>();
	public float Life;
    public float MaxLife;
    [Range(0f, 1f)]
    public float stunAmount = 1f;
	public PowerUpData PowerUpData = PowerUpFactory.GetPowerUpNull();

    private Rigidbody2D myRigidbody2D;
    private Animator animator;

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
        if (GetComponent<Animator>() == null) return;

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

    private void Stun(Vector3 direction)
    {
        myRigidbody2D.AddForce(direction * 5 * stunAmount, ForceMode2D.Impulse);
    }

    public void Hit(Bullet bullet) {
        // Stun
        Stun(bullet.transform.up);

        float damage = EnemyFactory.getInstance().calculateDamage(this);
        this.Life -= damage;
        GameObject text = Instantiate(DamageText, transform.position, Quaternion.identity) as GameObject;
        text.transform.SetParent(GameObject.Find("OverlayCanvas").transform);
        text.GetComponent<Text>().text = damage.ToString();
        float scaleX = 4 * (Life / MaxLife) + 0.1f;
        if (scaleX < 0) {
            scaleX = 0;
        }
        transform.FindChild("HealthBar").gameObject.SetActive(true);
        transform.FindChild("HealthBar").localScale = new Vector3(scaleX, 1, 1);
        //Debug.Log("Damage " + damage + ", remaining life " + this.Life);
        if (this.Life <= 0) {
            PowerUpFactory.getInstance().InstantiatePowerUp(this.PowerUpData, transform.position, Quaternion.identity);
            Destroy(this.gameObject);
        } else {

        }
        Destroy(bullet.gameObject);
    }
}
