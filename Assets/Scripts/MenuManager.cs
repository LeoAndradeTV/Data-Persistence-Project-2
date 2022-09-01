using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Unity.IO;
using UnityEngine.SceneManagement;
using TMPro;
using System.IO;

public class MenuManager : MonoBehaviour
{
    public static MenuManager Instance;
    public InputField playerNameInput;
    public TextMeshProUGUI bestScoreText;
    public string playerName;
    private Button startButton;
    private Button quitButton;
    public string topPlayer;
    public int bestScore;

    public void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        DontDestroyOnLoad(gameObject);

        startButton = GameObject.Find("Start Button").GetComponent<Button>();
        quitButton = GameObject.Find("Quit Button").GetComponent<Button>();
        startButton.onClick.AddListener(StartButtonClicked);
        LoadScore();

    }

    // Start is called before the first frame update
    void Start()
    {
        bestScoreText = GameObject.Find("Best Score").GetComponent<TextMeshProUGUI>();
        bestScoreText.text = "Best Score : " + topPlayer + " : " + bestScore;

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void StartButtonClicked()
    {
        playerName = playerNameInput.text;
        SceneManager.LoadScene(1);
    }

    [System.Serializable]
    class SaveData
    {
        public int bestScore;
        public string bestName;
    }

    public void LoadScore()
    {
        string path = Application.persistentDataPath + "/savefile.json";
        if (File.Exists(path))
        {
            string json = File.ReadAllText(path);
            SaveData data = JsonUtility.FromJson<SaveData>(json);

            bestScore = data.bestScore;
            topPlayer = data.bestName;

        }
    }
}
