using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyFactory {

	public List<PowerUp> powerUps;
	public List<Enemy> enemies;
	public List<Enemy> bosses;

	public static Enemy getEnemy(int life) {
		Enemy enemy = getRandomEnemy();

		enemy.Life = life;
		addWeaknessesAndResistences(enemy);
		enemy.PowerUp = PowerUpFactory.GetRandomPowerUp();
		enemy.scale = Random.Range(Constants.ENEMY_MIN_SCALE, Constants.ENEMY_MAX_SCALE);
		addRandomMaterialOverlay(enemy);

		return enemy;
	}

	private Enemy getRandomEnemy() {
		int enemyIndex = Random.Range(0, enemies.Count);
		return enemies[enemyIndex];
	}

	private void addWeaknessesAndResistences(Enemy enemy) {
		WeaponType wt1 = RandomEnumPicker.GetRandomWeaponType();
		WeaponType wt2;
		do {
			wt2 = RandomEnumPicker.GetRandomWeaponType();
		} while (wt1 == wt2);

		enemy.Weaknesses.Add(wt1);
		enemy.Resistences.Add(wt2);
	}

	private void addRandomMaterialOverlay(Enemy enemy) {
		Material m = new Material("Sprites_Default");
		m.setColor(new Color(
			Random.RandomRange(0, 255),
			Random.RandomRange(0, 255),
			Random.RandomRange(0, 255)
		));
	}

	public static Enemy getBoss(int life) {
		Enemy boss = getRandomBoss();

		boss.Life = life;
		addWeaknessesAndResistences(boss);
		boss.PowerUp = PowerUpFactory.GetRandomPowerUp();
		boss.scale = Random.Range(Constants.ENEMY_MIN_SCALE, Constants.ENEMY_MAX_SCALE);
		addRandomMaterialOverlay(boss);

		return boss;
	}

	private Enemy getRandomBoss() {
		int bossIndex = Random.Range(0, bosses.Count);
		return bosses[bossIndex];
	}
}
