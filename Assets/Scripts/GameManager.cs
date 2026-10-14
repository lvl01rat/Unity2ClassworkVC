using UnityEngine;
using TMPro;
public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    [Header("UI Elements")]
    public TMP_Text pickupCountText;
    public GameObject winPanel;

    [Header("Pickups")]
    public int totalPickups = 5;
    public int collectedCount = 0;

    bool _gameOver = false;

    private void Awake()
    {
        //Checks to see if the game manager exists
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        //If we want this to presist between scenes -  Use this code
        DontDestroyOnLoad(this.gameObject);
    }

    // Start is called before the first frame update
    void Start()
    {
        UpdateUI();
        if(winPanel != null)
        {
            winPanel.SetActive(false);
        }
    }

    public void OnGoalReached()
    {
        if (_gameOver) return;
        _gameOver = true;
        Debug.Log("Game Over! You reached the goal.");
        if (winPanel != null)
        {
            winPanel.SetActive(true);
            winPanel.GetComponentInChildren<TMP_Text>().text = "Your did got all items and made it to the goal's end";
        }
       
    }
    public void OnPickupCollected()
    {
        if (_gameOver) return;

        
        collectedCount++;
        UpdateUI();

    }

    void UpdateUI()
    {
        if(pickupCountText != null)
        {
            pickupCountText.text = $"Items: {collectedCount}/{totalPickups}";
        }
    }
}
