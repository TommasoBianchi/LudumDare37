using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

    public float Speed;
    public int Life;
	public Weapon Weapon;
	public PowerUpManager PowerUpManager;
    public List<KeyValuePair<ResourceType, int>> resources;

    private int MaxLife;
    private Animator animator;
     
	// Use this for initialization
	void Start () {
        this.Speed = Constants.PLAYER_BASE_SPEED;
		this.Weapon = WeaponFactory.GetWeapon (1);
        this.PowerUpManager = new PowerUpManager();
        this.PowerUpManager.SetPowerUp(PowerUpFactory.GetPowerUpNull());
        this.resources = new List<KeyValuePair<ResourceType, int>>();
        animator = GetComponent<Animator>();
    }
	
	// Update is called once per frame
	void Update () {
        UpdateMovement();
        PowerUpManager.Update();
	}

    private void UpdateMovement() {
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
}
