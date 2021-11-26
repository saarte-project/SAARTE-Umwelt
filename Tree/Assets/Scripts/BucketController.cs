using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using static LeafCollectSettings;

public enum LeafType { Ahorn, Birke, Eiche, Any };


public class BucketController : MonoBehaviour
{
    public enum GameState { PreGame, Running, PostGame};

    [SerializeField]
    LeafType acceptedLeafType = LeafType.Eiche;

    [SerializeField]
    private TextMeshPro timerObject = null;

    [SerializeField]
    private TextMeshPro counterObject = null;

    [SerializeField]
    private Collider triggerCollider = null;

    GameState gameState = GameState.PreGame;

    float timer = 0;

    public GameObject gObject;
    public Material green;

    int collectingGoal = 10;
    int collectedLeafCount = 0;

    // List to keep track of already collided leaves to avoid multiple collisions
    List<GameObject> collidedLeaves = new List<GameObject>();

/// <summary>
/// Sets all requirements for the collecting leaves challenge before it is triggered.
/// </summary>
    private void Start()
    {
        acceptedLeafType = LeafCollectSettings.GetLeafTypeSelection();

        collectingGoal = LeafCollectSettings.GetLeafGoal();

        timer = 0;

        collectedLeafCount = 0;

        gameState = GameState.PreGame;

        UpdateDisplay();
    }

/// <summary>
/// Updates the timer while the collecting leaves challenge is running .
/// </summary>
    private void Update()
    {
        if (gameState == GameState.Running)
        {
            timer += Time.deltaTime;

            UpdateDisplay();
        }

    }
/// <summary>
/// Updates "Timer" and "Counter" 3D-Text displayed above the bucket.
/// </summary>
    private void UpdateDisplay()
    {
        timerObject.text = "Timer: " + System.Math.Round(timer, 2).ToString();
        counterObject.text = acceptedLeafType.ToString() + " Counter: " + collectedLeafCount.ToString() + " / "  + collectingGoal.ToString();
    }

/// <summary>
/// Called once the requirements for finishing the collecting leaves challenge are met. Updates the game state, relevant visuals and saves the final time.
/// </summary>
    void EndGame()
    {
        gameState = GameState.PostGame;

        timerObject.color = Color.green;

        UpdateDisplay();

        SceneSelect sceneSelect = FindObjectOfType<SceneSelect>();

        sceneSelect.SaveTime(timer);

        sceneSelect.NextSceneIteration();
    }


    void OnGUI()
    {
        GUI.Box(new Rect(100, 100, 100, 100), collectedLeafCount.ToString());
    }

/// <summary>
/// Is called whenever a object collides with the bucket trigger. Checks if collision is a required leaf, 
/// if so, collected leaf count increases and checks if the end condition is met.
/// </summary>
    public void TriggerCollision(Collider otherCollider)
    {
            Debug.Log("Bucket collision triggered!");

            if (((otherCollider.tag.ToString() == acceptedLeafType.ToString()) || acceptedLeafType == LeafType.Any) && !HasAlreadyCollided(otherCollider.gameObject))
            {
                if (gameState == GameState.PreGame)
                    gameState = GameState.Running;

                collectedLeafCount += 1;

                collidedLeaves.Add(otherCollider.gameObject);

                gObject.GetComponent<Renderer>().material = green;

                if (collectedLeafCount >= collectingGoal)
                {
                    EndGame();
                }
            }

            Debug.Log("Counter:" + collectedLeafCount);       
    }

/// <summary>
/// Called by "TriggerCollision" method to ensure the same leaf does not count multiple times.
/// </summary>
    private bool HasAlreadyCollided(GameObject gameObject)
    {
        if (collidedLeaves.Contains(gameObject))
            return true;
        else
            return false;
    }

}
