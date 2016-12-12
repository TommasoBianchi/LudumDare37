using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class PlayerController : MonoBehaviour {
    
    public float Speed = Constants.PLAYER_BASE_SPEED;
    public float BasePower = 1.0f;
	public WeaponData WeaponData;
	public PowerUpManager PowerUpManager;
    public Dictionary<ResourceType, int> resources = new Dictionary<ResourceType, int>();
    public LayerMask floorLayerMask;

    public float fireRate = 1.0f;
    private int life;
    public int Life 
    {
        get
        {
            return life;
        }
        private set
        {
            if (lifeHUD == null)
                lifeHUD = GetComponent<LifeHUD>();

            lifeHUD.SetLife(value, MaxLife);
            life = value;
        } 
    }

    private int MaxLife = 3;
    private Animator animator;
    private LifeHUD lifeHUD;

    public GameObject Text;

    private bool invincible = false;
    private float timeInvincible = 0;

    private Rigidbody2D myRigidbody2D;
     
	void Start () {
		this.WeaponData = WeaponFactory.getInstance().GetRandomWeapon(1);
        this.PowerUpManager = new PowerUpManager();
        this.PowerUpManager.SetPowerUp(PowerUpFactory.GetPowerUpNull());
        this.resources = new Dictionary<ResourceType, int>();
        animator = GetComponent<Animator>();
        myRigidbody2D = GetComponent<Rigidbody2D>();

        Life = MaxLife;
    }
	
	void Update () 
    {
        if (FadeScreen.IsAnimating() == false && (RecipeBook.GetInstance() == null || RecipeBook.GetInstance().gameObject.activeSelf == false))
        {
            UpdateMovement();

            PowerUpManager.Update();

            UpdateAttack();

            UpdateInvincibility();
        }
        else
        {
            UpdateAnimator(Vector2.zero);
        }
	}

    private void UpdateMovement() {
        float MoveHorizontal = Input.GetAxis("Horizontal");
        float MoveVertical = Input.GetAxis("Vertical");

        Vector2 Movement = new Vector2(MoveHorizontal, MoveVertical);
        transform.Translate(Movement * Speed * Time.deltaTime, Space.World);
        UpdateAnimator(Movement);

        // Anti-geco code
        if (!Physics.Raycast(transform.position + Vector3.forward * 5, -Vector3.forward, floorLayerMask))
        {
            Room currentRoom = FindObjectsOfType<Room>().Where(r => r.ID != -1).OrderBy(r => r.ID).First() ?? Hub.instance;
            transform.position = currentRoom.ViewportToWorldPoint(Vector2.one / 2f);
            Debug.Log("outside");
        }
    }

    private float timer = 0;

    private void UpdateAttack()
    {
        if (Input.GetButton("Fire1") && timer <= 0)
        {
            Weapon weapon = WeaponFactory.getInstance().weapons.Find(w => w.weaponData.Type == WeaponData.Type);
            Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector3 mouseDelta = mousePosition - transform.position;
            mouseDelta.z = 0;
            WeaponFactory.getInstance().InstantiateShot(WeaponData, transform.position + mouseDelta.normalized, Quaternion.LookRotation(Vector3.forward, mouseDelta));
            timer = 1f / (fireRate * weapon.baseFrequency);
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

    public void AddResource(ResourceType resource) {
        if (resources.ContainsKey(resource))
        {
            resources[resource]++;
        }
        else
        {
            resources[resource] = 1;
        }
    }

    public void ClearResources() {
        this.resources.Clear();
    }

    void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Enemy" && !this.invincible)
        {
            addLife(-1);
            if (Life == 0)
            {
                // Die!
                FadeScreen.Animate(8, () =>
                {
                    ClearResources();
                    transform.position = Hub.instance.topDoor.transform.position - Vector3.up * 2;
                    Life = MaxLife;

                    Room currentRoom = FindObjectsOfType<Room>().Where(r => r.ID != -1).First();
                    Hub.instance.topDoor.linkedRoom = (Instantiate(currentRoom.roomPrefab, Vector3.zero, Quaternion.identity) as GameObject).GetComponent<Room>();
                    Hub.instance.topDoor.linkedRoom.roomPrefab = currentRoom.roomPrefab;
                    Destroy(currentRoom.gameObject);

                    Enemy[] enemies = FindObjectsOfType<Enemy>();
                    for (int i = 0; i < enemies.Length; i++)
                    {
                        Destroy(enemies[i].gameObject);
                    }

                    Globals.CurrentLevel = 0;
                }, "Game over\nYou lost all your resources\nYou idiot");
            }
            else
            {
                // Set invincible;
                this.invincible = true;
                this.timeInvincible = 0;
            }
        }
    }

    public void addLife(int amount) {
        Life += amount;
        if (Life > MaxLife) {
            Life = MaxLife;
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
