﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyFactory : MonoBehaviour {

	public static List<PowerUp> powerUps;
	public static List<Enemy> enemies;
	public static List<Enemy> bosses;

    public static Enemy getEnemy(int roomID, float lifeModifier)
    {
		Enemy enemy = getRandomEnemy();

        Weapon currentPlayerWeapon = Globals.GetPlayerController().Weapon;
        enemy.Life = Mathf.RoundToInt((27 + (currentPlayerWeapon.Tier * 5)) * 5 / (dmg_calc(roomID, currentPlayerWeapon, enemy)) * lifeModifier); ;
		addWeaknessesAndResistences(enemy);
		enemy.PowerUp = PowerUpFactory.GetRandomPowerUp();
		enemy.Scale = Random.Range(Constants.ENEMY_MIN_SCALE, Constants.ENEMY_MAX_SCALE);
		addRandomMaterialOverlay(enemy);

		return enemy;
	}

	private static Enemy getRandomEnemy() {
		int enemyIndex = Random.Range(0, enemies.Count);
		return enemies[enemyIndex];
	}

	private static void addWeaknessesAndResistences(Enemy enemy) {
		WeaponType wt1 = RandomEnumPicker.GetRandomWeaponType();
		WeaponType wt2;
		do {
			wt2 = RandomEnumPicker.GetRandomWeaponType();
		} while (wt1 == wt2);

		enemy.Weaknesses.Add(wt1);
		enemy.Resistences.Add(wt2);
	}

	private static void addRandomMaterialOverlay(Enemy enemy) {
		Material m = new Material("Sprites_Default");
		m.SetColor("_TintColor", new Color(
			Random.RandomRange(0.0f, 1.0f),
			Random.RandomRange(0.0f, 1.0f),
			Random.RandomRange(0.0f, 1.0f)
		));
	}

	public static Enemy getBoss(int roomID, float lifeModifier) {
		Enemy boss = getRandomBoss();

        Weapon currentPlayerWeapon = Globals.GetPlayerController().Weapon;
        boss.Life = Mathf.RoundToInt((27 + (currentPlayerWeapon.Tier * 5)) * 5 / (dmg_calc(roomID, currentPlayerWeapon, boss)) * lifeModifier);
		addWeaknessesAndResistences(boss);
		boss.PowerUp = PowerUpFactory.GetRandomPowerUp();
		boss.Scale = Random.Range(Constants.ENEMY_MIN_SCALE, Constants.ENEMY_MAX_SCALE);
		addRandomMaterialOverlay(boss);

		return boss;
	}

	private static Enemy getRandomBoss() {
		int bossIndex = Random.Range(0, bosses.Count);
		return bosses[bossIndex];
	}
    private static float dmg_calc(int liv, Weapon arma, Enemy enemy)
    {
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
}
