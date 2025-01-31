using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public UiHandler UIHandler;
    public CharacterHandler CharacterHandler;
    public bool IsGameStarted = false;
    public int Score;
    public int HighScore;
    public int GameplayCoins;
    public int Coins;
    public GameObject Chunk;
    public Transform LevelChunkHolder;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
    }
    void Start()
    {
       
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    [ContextMenu("Test")]
    public void ResetGame()
    {
        Score = 0;
        CharacterHandler.PlayerBoostOff();
        CharacterHandler.BaseSpeed = 1;
        CharacterHandler.transform.position = new Vector3(0,0.5f,-5f);
        for(int i=0; i< LevelChunkHolder.childCount; i++)
        {
            LevelChunkHolder.GetChild(i).GetComponent<ChunkHandler>().DestroyChunk(LevelChunkHolder.GetChild(i).gameObject);
        }

        for (int i = 0; i < 4; i++)
        {
            if (i == 0)
            {
                GameObject LevelChunk = Instantiate(Chunk, Vector3.zero, Quaternion.identity);
                LevelChunk.transform.parent = LevelChunkHolder;
                LevelChunk.GetComponent<ChunkHandler>().BaseGameObject.SetActive(true);
            }
            else
            {
                int LastChunkId = LevelChunkHolder.childCount - 1;
                Transform LastChunkTransform = LevelChunkHolder.GetChild(LastChunkId).transform;
                LastChunkTransform.GetComponent<ChunkHandler>().SpawnChunk(LastChunkTransform);
            }
        }

    }
}
