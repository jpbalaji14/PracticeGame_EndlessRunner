using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class CharacterHandler : MonoBehaviour
{
    public bool IsPlayerBoostActivated;
    public float BaseSpeed;
    [SerializeField] private float _maxSpeed;
    [SerializeField] private float _accelerationSpeed;
    [SerializeField] private float _slowDownSpeed;
    [SerializeField] private float _currentSpeed;
    [SerializeField] private CameraHandler _cameraHandler;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _currentSpeed = BaseSpeed;
    }

    // Update is called once per frame
    void Update()
    {
        if (GameManager.Instance.IsGameStarted)
        {
            this.transform.Translate(Vector3.forward * _currentSpeed * Time.deltaTime);
            if (IsPlayerBoostActivated)
            {
                _currentSpeed += _accelerationSpeed;

                if (_currentSpeed > _maxSpeed)
                    _currentSpeed = _maxSpeed;
            }
            else
            {
                _currentSpeed -= _slowDownSpeed;

                if (_currentSpeed < BaseSpeed)
                    _currentSpeed = BaseSpeed;
            }
        }
    }
    public void PlayerBoostOn()
    {
        IsPlayerBoostActivated = true;
    }
    public void PlayerBoostOff()
    {
        IsPlayerBoostActivated = false;
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Obstacle")
        {
            _currentSpeed = 0;
            BaseSpeed = 0;
            GameManager.Instance.UIHandler.GameOver(GameManager.Instance.Score, GameManager.Instance.HighScore, GameManager.Instance.GameplayCoins);
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Score")
        {
            other.GetComponent<BoxCollider>().enabled = false;
            GameManager.Instance.Score += 1;
            GameManager.Instance.UIHandler.ScoreUpdate(GameManager.Instance.Score);
        }
        if (other.gameObject.tag == "LevelSpawn")
        {
            other.GetComponent<BoxCollider>().enabled = false;
            int LastChunkId = GameManager.Instance.LevelChunkHolder.childCount - 1;
            Transform LastChunkTransform = GameManager.Instance.LevelChunkHolder.GetChild(LastChunkId).transform;
            LastChunkTransform.GetComponent<ChunkHandler>().SpawnChunk(LastChunkTransform);

            if (GameManager.Instance.Score > 1)
            {
                Transform DeleteChunkTransform = GameManager.Instance.LevelChunkHolder.GetChild(0).transform;
                DeleteChunkTransform.GetComponent<ChunkHandler>().DestroyChunk(DeleteChunkTransform.gameObject);
            }
        }
        if (other.gameObject.tag == "Coin")
        {
            other.gameObject.SetActive(false);
            GameManager.Instance.GameplayCoins += 1;
            GameManager.Instance.UIHandler.CoinsUpdate(GameManager.Instance.GameplayCoins);
        }
    }
}
