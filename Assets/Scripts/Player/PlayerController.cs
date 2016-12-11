using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

    public GameObject cazzoDiPrefab;

    public float Speed;
    public int Life;
	public WeaponData WeaponData;
	public PowerUpManager PowerUpManager;
    public List<KeyValuePair<ResourceType, int>> resources;

    public float fireRate;

    private int MaxLife;
    private Animator animator;
     
	void Start () {
		this.WeaponData = new WeaponData(WeaponType.Umbrella, 1, Roll.None);
        this.PowerUpManager = new PowerUpManager();
        this.PowerUpManager.SetPowerUp(PowerUpFactory.GetPowerUpNull());
        this.resources = new List<KeyValuePair<ResourceType, int>>();
        animator = GetComponent<Animator>();
    }
	
	void Update () {
        UpdateMovement();
        PowerUpManager.Update();
        Mouseclicked();
	}

    void Mouseclicked()
    {
        Vector3 mous= Input.mousePosition;
        if(Input.GetMouseButton(0))
        {
            //GameObject aaaaa 

        }


    }


    private void UpdateMovement() {
        float MoveHorizontal = Input.GetAxis("Horizontal");
        float MoveVertical = Input.GetAxis("Vertical");

        Vector2 Movement = new Vector2(MoveHorizontal, MoveVertical);

        transform.Translate(Movement * Speed * Time.deltaTime, Space.World);

        UpdateAnimator(Movement);

        UpdateAttack();
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
}
