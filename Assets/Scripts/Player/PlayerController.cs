using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

    public GameObject cazzoDiPrefab;

    public float Speed = Constants.PLAYER_BASE_SPEED;
    public float BasePower = 1.0f;
    public int Life;
	public WeaponData WeaponData;
	public PowerUpManager PowerUpManager;
    public List<KeyValuePair<ResourceType, int>> resources;

    public float fireRate;

    private int MaxLife = 3;
    private Animator animator;
    private LifeHUD lifeHUD;

    public GameObject Text;

    private bool invincible = false;
    private float timeInvincible = 0;
     
	void Start () {
		this.WeaponData = WeaponFactory.getInstance().GetWeapon(1);
        this.PowerUpManager = new PowerUpManager();
        this.PowerUpManager.SetPowerUp(PowerUpFactory.GetPowerUpNull());
        this.resources = new List<KeyValuePair<ResourceType, int>>();
        animator = GetComponent<Animator>();

        Life = MaxLife;
        lifeHUD = GetComponent<LifeHUD>();
        lifeHUD.SetLife(Life, MaxLife);
    }
	
	void Update () {
        UpdateMovement();

        PowerUpManager.Update();

        UpdateAttack();

        UpdateInvincibility();
	}

    private void UpdateMovement() {
        float MoveHorizontal = Input.GetAxis("Horizontal");
        float MoveVertical = Input.GetAxis("Vertical");

        Vector2 Movement = new Vector2(MoveHorizontal, MoveVertical);

        transform.Translate(Movement * Speed * Time.deltaTime, Space.World);

        UpdateAnimator(Movement);
    }

    private float timer = 0;

    private void UpdateAttack()
    {
        if (Input.GetButton("Fire1") && timer <= 0)
        {
            Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector3 mouseDelta = mousePosition - transform.position;
            mouseDelta.z = 0;
            WeaponFactory.getInstance().InstantiateShot(WeaponData, transform.position + mouseDelta.normalized, Quaternion.LookRotation(Vector3.forward, mouseDelta));
            timer = 1f / fireRate;
        }
        else
        {
            timer -= Time.deltaTime;
        }
    }

    private void UpdateAnimator(Vector2 Movement)
    {
        animator.SetFloat("AnimationSpeed", Speed);

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

    public void AddResource(Resource resource) {
        bool found = false;
        for (int i = 0; i < this.resources.Count; i++) {
            KeyValuePair<ResourceType, int> pair = this.resources[i];
            if (resource.type == pair.Key) {
                found = true;
                this.resources[i] = new KeyValuePair<ResourceType, int>(pair.Key, pair.Value + 1);
            }
        }
        if (found) {
            this.resources.Add(new KeyValuePair<ResourceType, int>(resource.type, 1));
        }
    }

    public void ClearResources() {
        this.resources = new List<KeyValuePair<ResourceType, int>>();
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Enemy" && !this.invincible)
        {
            Life--;
            if (Life <= 0) {
                Application.LoadLevel("Test");
            }

            lifeHUD.SetLife(Life, MaxLife);

            // Set invincible;
            this.invincible = true;
            this.timeInvincible = 0;
        }
    }

    void UpdateInvincibility() {
        if (this.invincible) {
            this.timeInvincible += Time.deltaTime;
            Color c = gameObject.GetComponent<SpriteRenderer>().color;

            float pingpong = Mathf.PingPong(Time.time, 0.2f);
            if (pingpong <= 0.05f) {
                gameObject.GetComponent<SpriteRenderer>().color = new Color(c.r, c.g, c.b, 0.2f);
            } else if (pingpong >= 0.15f) {
                gameObject.GetComponent<SpriteRenderer>().color = new Color(c.r, c.g, c.b, 1f);
            }

            if (this.timeInvincible > Constants.INVINCIBLE_TIME) {
                this.invincible = false;
                this.timeInvincible = 0;
                gameObject.GetComponent<SpriteRenderer>().color = new Color(c.r, c.g, c.b, 1f);
            }
        }
    }
}
