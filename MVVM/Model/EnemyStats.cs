using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStats
{
    public string Name { get; set; }
    public int Attack { get; set; }
    public int Health { get; set; }
    public int Speed { get; set; }
    public int IdleTimer { get; set; }
    public bool IsMelee { get; set; }
    public Vector3 SpawnPoint { get; set; }
    public Vector3 WaypointA { get; set; }
    public Vector3 WaypointB { get; set; }

    public EnemyStats(string name, int attack, int health, int speed, int idleTimer, bool isMelee,
        Vector3 spawnPoint, Vector3 waypointA, Vector3 waypointB)
    {
        Name = name ?? throw new ArgumentNullException(nameof(name));
        Attack = attack;
        Health = health;
        Speed = speed;
        IdleTimer = idleTimer;
        IsMelee = isMelee;
        SpawnPoint = spawnPoint;
        WaypointA = waypointA;
        WaypointB = waypointB;
    }
}