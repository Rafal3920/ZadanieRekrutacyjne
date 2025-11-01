using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameplayController : MonoBehaviour
{
    [SerializeField] private PlayerController playerController;
    [SerializeField] private GameObject enemyPrefab;
    [SerializeField] private Transform enemiesContainer;
    [SerializeField] private int enemiesNumber = 1000;

    private List<EnemyController> enemies = new List<EnemyController>();
    private Vector2 minBounds;
    private Vector2 maxBounds;

    private void Start ()
    {
        if (!CheckDependencies ())
        {
            this.enabled = false;
            return;
        }

        minBounds = Camera.main.ScreenToWorldPoint (Vector3.zero);
        maxBounds = Camera.main.ScreenToWorldPoint (new Vector3(Screen.width, Screen.height, 0));

        StartGame ();
    }

    private void StartGame ()
    {
        playerController.Init (minBounds, maxBounds);

        for (int i = 0; i < enemiesNumber; i++)
        {
            GameObject enemyGO = Instantiate (enemyPrefab, enemiesContainer);
            EnemyController enemy = enemyGO.GetComponent<EnemyController>();
            enemies.Add (enemy);
            enemy.Init (minBounds, maxBounds);
        }
    }

    private void Update ()
    {
        if (Input.GetKeyUp (KeyCode.Escape))
        {
            SceneManager.LoadScene ("MainMenu");
        }

        for (int i = 0; i < enemies.Count; i++)
        {
            enemies[i].RunAway (playerController.transform.position);
        }
    }

    private bool CheckDependencies ()
    {
        bool result = true;

        if (playerController == null)
        {
            Debug.LogError ("PlayerController in GameplayController is NULL!");
            result = false;
        }
        if (enemyPrefab == null)
        {
            Debug.LogError ("EnemyPrefab in GameplayController is NULL!");
            result = false;
        }
        if (enemiesContainer == null)
        {
            Debug.LogError ("EnemiesContainer in GameplayController is NULL!");
            result = false;
        }
        return result;
    }
}
