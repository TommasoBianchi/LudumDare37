using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Burst : IComparable<Burst> {

    public float time { get; private set; }
    public List<EnemyData> enemies { get; private set; }
    public Vector2 position { get; private set; }

    public Burst(float time, List<EnemyData> enemies, Vector2 position)
    {
        this.time = time;
        this.enemies = enemies;
        this.position = position;
    }

    public int CompareTo(Burst other)
    {
        if (this.time > other.time)
            return -1;
        else if (this.time < other.time)
            return 1;
        else
            return 0;
    }
}
