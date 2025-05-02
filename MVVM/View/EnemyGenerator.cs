using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.AI;

public class EnemyGenerator : EditorWindow
{
    const int WindowSizeX = 300;
    const int WindowSizeY = 400;
    private const string AssetPathBlank = "Assets/Resources/Blank.png";
    private const string AssetPathRanged = "Assets/Resources/Ranged.png";
    private const string AssetPathMelee = "Assets/Resources/Melee.png";
    private const string AssetPathPrefab = "Assets/Resources/LowPolySkeleton.prefab";

    /// <summary>
    /// The enemy view model.
    /// </summary>
    public EnemyVM EnemyVM
    {
        get => enemyVM;
        set
        {
            if (enemyVM != null) enemyVM.PropertyChanged -= UpdateViewFromPropertyChanged;
            enemyVM = value;
            if (enemyVM != null) enemyVM.PropertyChanged += UpdateViewFromPropertyChanged;
        }
    }


    private void UpdateViewFromPropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
    {
        throw new System.NotImplementedException();
    }

    Texture meleeTexture = null;
    Texture rangedTexture = null;
    Texture blankTexture = null;
    GameObject prefab = null;

    EnemyVM enemyVM;

    bool melee = false;
    bool ranged = false;

    [MenuItem("Tools/EnemyGenerationMVVM")]
    public static void ShowWindow()
    {
        var window = GetWindow<EnemyGenerator>("Enemy GenerationMVVM");
        window.minSize = new Vector2(WindowSizeX, WindowSizeY);
        window.maxSize = new Vector2(WindowSizeX, WindowSizeY);
        window.enemyVM = new EnemyVM();
        window.Show();
    }

    private void OnEnable()
    {
        blankTexture = (Texture)AssetDatabase.LoadAssetAtPath(AssetPathBlank, typeof(Texture));
        rangedTexture = (Texture)AssetDatabase.LoadAssetAtPath(AssetPathRanged, typeof(Texture));
        meleeTexture = (Texture)AssetDatabase.LoadAssetAtPath(AssetPathMelee, typeof(Texture));
        prefab = (GameObject)AssetDatabase.LoadAssetAtPath(AssetPathPrefab, typeof(GameObject));
    }

    private void OnGUI()
    {
        GUILayout.Label("Enemy Generation Tool", EditorStyles.boldLabel);

        EditorGUILayout.BeginHorizontal();

        if (!melee && !ranged || melee && ranged) GUI.DrawTexture(new Rect(5, 20, 40, 40), blankTexture);

        if (!melee && ranged) GUI.DrawTexture(new Rect(5, 20, 40, 40), rangedTexture);

        if (melee && !ranged) GUI.DrawTexture(new Rect(5, 20, 40, 40), meleeTexture);


        ranged = EditorGUI.Toggle(new Rect(70, 25, 10, 10), ranged);
        melee = EditorGUI.Toggle(new Rect(70, 45, 10, 10), melee);

        GUI.Label(new Rect(90, 19, 90, 20), "Ranged Enemy");
        GUI.Label(new Rect(90, 40, 90, 20), "Melee Enemy");

        EditorGUILayout.EndHorizontal();
        EditorGUILayout.Space(50);


        GUILayout.Label("Stats", EditorStyles.boldLabel);
        EnemyVM.Name = EditorGUILayout.TextField("Name", EnemyVM.Name);
        EnemyVM.Attack = EditorGUILayout.IntField("Attack Damage", EnemyVM.Attack);
        EnemyVM.Health = EditorGUILayout.IntField("Health", EnemyVM.Health);
        EnemyVM.IdleTime = EditorGUILayout.IntField("Idle Timer", EnemyVM.IdleTime);
        EnemyVM.Speed = EditorGUILayout.IntField("Speed", EnemyVM.Speed);

        EditorGUILayout.Space(10);

        GUILayout.Label("Spawnpoint", EditorStyles.boldLabel);
        EnemyVM.SpawnPoint = EditorGUILayout.Vector3Field("Position", EnemyVM.SpawnPoint);
        EnemyVM.WaypointA = EditorGUILayout.Vector3Field("Waypoint A", EnemyVM.WaypointA);
        EnemyVM.WaypointB = EditorGUILayout.Vector3Field("Waypoint B", EnemyVM.WaypointB);

        EditorGUILayout.Space(20);

        if (GUILayout.Button("Create"))
        {
            if (ranged ^ melee)
            {
                CreateEnemy();
                return;
            }            
        }
    }

    void CreateEnemy() //Normally in model as Business logic, but because of Unity in view
    {
        var temp = Instantiate(prefab, EnemyVM.SpawnPoint, Quaternion.identity);
        var navAgent = temp.GetComponent<NavMeshAgent>();
        var fsm = temp.GetComponent<FSM>();
        temp.name = EnemyVM.Name;
        navAgent.speed = EnemyVM.Speed;
        temp.GetComponent<HealthComponent>().Health = EnemyVM.Health;
        temp.GetComponent<AttackComponent>().Damage = EnemyVM.Attack;
        fsm.IdleTime = EnemyVM.IdleTime;
        fsm.RoomXMin = EnemyVM.WaypointA.x;
        fsm.RoomXMax = EnemyVM.WaypointB.x;
        fsm.RoomZMin = EnemyVM.WaypointA.z;
        fsm.RoomZMax = EnemyVM.WaypointB.z;
        navAgent.stoppingDistance = (melee ? .5f : 5f);
        enemyVM.IsMelee = melee;
    }
}