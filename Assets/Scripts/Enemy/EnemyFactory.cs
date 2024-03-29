﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyFactory : MonoBehaviour {

	public List<Enemy> enemies;
	public List<Enemy> bosses;

    private static EnemyFactory instance;

    public static EnemyFactory getInstance() {
        return instance;
    }

    void Start()
    {
        instance = this;
    }

    public EnemyData getEnemy(int roomID, float lifeModifier)
    {
		EnemyData enemyData = new EnemyData();
        
        enemyData.type = getRandomEnemyIndex();
        WeaponData currentPlayerWeapon = Globals.GetPlayerController().WeaponData;
        enemyData.Life = lifeModifier * ((currentPlayerWeapon.Tier * Random.Range(5f, 20f))) + ((roomID * 5) * Random.Range(1f, 1.5f));//Mathf.RoundToInt((27 + (currentPlayerWeapon.Tier * 5)) * 5 / (dmg_calc(roomID, currentPlayerWeapon, enemyData)) * lifeModifier);
		addWeaknessesAndResistences(enemyData);
		enemyData.PowerUpData = PowerUpFactory.getInstance().GetRandomPowerUp();
		enemyData.Scale = Random.Range(Constants.ENEMY_MIN_SCALE, Constants.ENEMY_MAX_SCALE);
		addRandomColorOverlay(enemyData);

		return enemyData;
	}

	private int getRandomEnemyIndex() {
		return Random.Range(0, instance.enemies.Count);
	}

	private void addWeaknessesAndResistences(EnemyData enemy) {
		WeaponType wt1 = RandomEnumPicker.GetRandomWeaponType();
		WeaponType wt2;
		do {
			wt2 = RandomEnumPicker.GetRandomWeaponType();
		} while (wt1 == wt2);

		enemy.Weaknesses.Add(wt1);
		enemy.Resistences.Add(wt2);
	}

	private void addRandomColorOverlay(EnemyData enemyData) {
		enemyData.ColorOverlay = new Color(
			Random.RandomRange(0.25f, 1.0f),
			Random.RandomRange(0.25f, 1.0f),
            Random.RandomRange(0.25f, 1.0f)
		);
	}

	public EnemyData getBoss(int roomID, float lifeModifier) {
		EnemyData bossData = new EnemyData();
        
        bossData.type = getRandomBossIndex();
        WeaponData currentPlayerWeapon = Globals.GetPlayerController().WeaponData;
        bossData.Life = lifeModifier * (((currentPlayerWeapon.Tier * Random.Range(5f, 20f))) + ((roomID * 5) * Random.Range(1f, 1.5f)));//Mathf.RoundToInt((27 + (currentPlayerWeapon.Tier * 5)) * 5 / (dmg_calc(roomID, currentPlayerWeapon, bossData)));
		bossData.PowerUpData = PowerUpFactory.getInstance().GetRandomPowerUp();
		bossData.Scale = Random.Range(Constants.ENEMY_MIN_SCALE, Constants.ENEMY_MAX_SCALE);
		addRandomColorOverlay(bossData);

		return bossData;
	}

	private int getRandomBossIndex() {
        return Random.Range(0, instance.bosses.Count) + enemies.Count;
	}

    public float calculateDamage(Enemy enemy) {
        WeaponData currentPlayerWeapon = Globals.GetPlayerController().WeaponData;
        Weapon weapon = WeaponFactory.getInstance().weapons.Find(w => w.weaponData.Type == currentPlayerWeapon.Type);
        float baseDamage = currentPlayerWeapon.Tier * 10 * Globals.GetPlayerController().BasePower * weapon.baseDamage;
        float damageModifier = Random.Range(0.8f, 1.2f);

        switch (currentPlayerWeapon.Roll) {
            case Roll.Strong:
                damageModifier *= (1);
                break;
            case Roll.Audacious:
                damageModifier *= (1 + 0.22f);
                break;
            case Roll.Cruel:
                damageModifier *= (1 - 0.18f);
                break;
            default:
                break;
        }

        if (enemy.Weaknesses.Contains(currentPlayerWeapon.Type)) {
            damageModifier *= (1 + 0.30f);
        }
        else if (enemy.Resistences.Contains(currentPlayerWeapon.Type)) {
            damageModifier *= (1 - 0.12f);
        }

        return baseDamage * damageModifier;
    }
/*
    private float dmg_calc(int liv, WeaponData arma, EnemyData enemy)
    {
        /*
        float dmg_modif;
        int oldupper = 0;
        int upper = 0;
        int lower = 0;
        int i = 0;
        for (i = 1; i <= arma.Tier; i++)
        {
            upper += (10 + (i - 1) * 2);
            lower = oldupper;
            oldupper = upper;
        }

        dmg_modif = (-0.5f / (upper - lower) * (liv - lower) + 0.25f) + 1;
        if (dmg_modif <= 0.01f)
            dmg_modif = 0.01f;

        switch (arma.Roll)
        {
            case Roll.Strong:
                dmg_modif = dmg_modif * (1);
                break;
            case Roll.Audacious:
                dmg_modif = dmg_modif * (1 + 0.22f);
                break;
            case Roll.Cruel:
                dmg_modif = dmg_modif * (1 - 0.18f);
                break;
            default:
                break;
        }
        
        //Debug.Log("Enemy weaknesses: " + enemy.Weaknesses[0] + ", arma type: " + arma.Type);
        if (enemy.Weaknesses.Contains(arma.Type))
        {
            dmg_modif = dmg_modif * (1 + 0.30f);
        }
        else
        {
            dmg_modif = dmg_modif * (1 - 0.12f);
        }

        return dmg_modif;
    }
*/
    public GameObject InstantiateEnemy(EnemyData enemyData, Vector2 position, Quaternion rotation) {
        Enemy enemy = (enemyData.type < this.enemies.Count) ? this.enemies[enemyData.type] : this.bosses[enemyData.type - enemies.Count];

        GameObject enemyObj = Instantiate(enemy.gameObject, position, rotation) as GameObject;
        enemyObj.transform.parent = GameObject.FindGameObjectWithTag("Room").transform;
        if (enemyData.type != 5) {
            enemyObj.transform.localScale = new Vector3(enemyData.Scale, enemyData.Scale, enemyData.Scale);
        }
        enemyObj.GetComponent<Enemy>().Life = enemyData.Life;
        enemyObj.GetComponent<Enemy>().MaxLife = enemyData.Life;
        enemyObj.GetComponent<SpriteRenderer>().material.SetColor("_Color", enemyData.ColorOverlay);
        enemyObj.GetComponent<Enemy>().Weaknesses = enemyData.Weaknesses;
        enemyObj.GetComponent<Enemy>().Resistences = enemyData.Resistences;
        enemyObj.GetComponent<Enemy>().PowerUpData = enemyData.PowerUpData;

        return enemyObj;
    }
}
