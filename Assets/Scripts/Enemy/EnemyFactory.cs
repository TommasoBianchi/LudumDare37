using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyFactory {

	public List<PowerUp> powerUps;
	public List<Enemy> enemies;
	public List<Enemy> bosses;

	public static Enemy getEnemy(int life) {
		return new Enemy ();
	}

	public static Enemy getBoss(int life) {
		return new Enemy ();
	}
}
