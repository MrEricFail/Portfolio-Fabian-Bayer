using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;

public class EnemyVM : BaseVM
{
    Vector3 spawn = new Vector3(-55, 0, 20); //Specific spawnpoints for the world this was created in
    Vector3 waypoint1 = new Vector3(-68, 0, 0.5f); //with Specific waypoints for the enemyAI navmesh
    Vector3 waypoint2 = new Vector3(-41, 0, 29.5f);
    EnemyStats model = null;

    public EnemyStats Model
    {
        get => model;
        set => model = value;
    }

    public string Name
    {
        get => model.Name;
        set => InvokeUpdate(() => model.Name, val => model.Name = val, value);
    }

    public int Health
    {
        get => model.Health;
        set => InvokeUpdate(() => model.Health, val => model.Health = val, value);
    }

    public int Attack
    {
        get => model.Attack;
        set => InvokeUpdate(() => model.Attack, val => model.Attack = val, value);
    }

    public int Speed
    {
        get => model.Speed;
        set => InvokeUpdate(() => model.Speed, val => model.Speed = val, value);
    }

    public int IdleTime
    {
        get => model.IdleTimer;
        set => InvokeUpdate(() => model.IdleTimer, val => model.IdleTimer = val, value);
    }

    public bool IsMelee
    {
        get => model.IsMelee;
        set => InvokeUpdate(() => model.IsMelee, val => model.IsMelee = val, value);
    }

    public Vector3 SpawnPoint
    {
        get => model.SpawnPoint;
        set => InvokeUpdate(() => model.SpawnPoint, val => model.SpawnPoint = val, value);
    }

    public Vector3 WaypointA
    {
        get => model.WaypointA;
        set => InvokeUpdate(() => model.WaypointA, val => model.WaypointA = val, value);
    }

    public Vector3 WaypointB
    {
        get => model.WaypointB;
        set => InvokeUpdate(() => model.WaypointB, val => model.WaypointB = val, value);
    }

    public EnemyVM()
    {
        model = new("Skelly", 10, 10, 10, 5, true, spawn, waypoint1, waypoint2);
        //PropertyChanged += PrintData;
    }

    void PrintData(object sender, PropertyChangedEventArgs e)
    {
        //Debug.Log(e.PropertyName.ToString());
    }
}