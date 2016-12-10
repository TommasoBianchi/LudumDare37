using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {

    public int Speed;
    public int Life;
    public Weapon weapon;
    public PowerUpManager powerUpManager;

    private int MaxLife;
     
	// Use this for initialization
	void Start () {
        weapon = WeaponFactory.getWeapon(1);
    }
	
	// Update is called once per frame
	void Update () {
        ManageMovement();
        ManageAttack();
	}

    void ManageMovement() {
        float MoveHorizontal = Input.GetAxis("Horizontal");
        float MoveVertical = Input.GetAxis("Vertical");

        Vector2 Movement = new Vector2(MoveHorizontal, MoveVertical);

        transform.Translate(Movement * Speed * Time.deltaTime, Space.World);
    }

    void ManageAttack() {
        
    }
}
