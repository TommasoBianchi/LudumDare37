using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

    public int Speed;
    public int Vita;
    public WeaponTypes WeaponType;
    public float Damage;

    private int MaxVita;
     
	// Use this for initialization
	void Start () {
        Damage = 0;
        WeaponType = WeaponTypes.Sword;
    }
	
	// Update is called once per frame
	void Update () {
        float MoveHorizontal = Input.GetAxis("Horizontal");
        float MoveVertical = Input.GetAxis("Vertical");

        Vector2 Movement = new Vector2(MoveHorizontal, MoveVertical);

        transform.Translate(Movement * Speed * Time.deltaTime, Space.World);
		
	}
}
