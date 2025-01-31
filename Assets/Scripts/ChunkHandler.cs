using DG.Tweening;
using System;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using Random = UnityEngine.Random;

[Serializable]
public class GameLevels
{
   [SerializeField] private string _levelName;
   public List<SetList> SetList;
}
[Serializable]
public class SetList
{
    [SerializeField] private string _setName;
    public List<GameObject> SetItems;
}
public class ChunkHandler : MonoBehaviour
{
    public GameObject BaseGameObject;
    [SerializeField] private List<GameLevels> _gameLevels;
    [SerializeField] private List<GameObject> _coinsList;
    [SerializeField] private GameObject _itemsHolder;
    [SerializeField] private Vector2 _minMaxSpinSpeed;
    [SerializeField] private float _spinSpeed;

   
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if (GameManager.Instance.Score <= 3) //Easy
        {
            Debug.Log("lets Spawn Easy");
            int _randomEasySetNumber = Random.Range(0, _gameLevels[0].SetList.Count);
            Debug.Log(_randomEasySetNumber);
            for (int i = 0; i < _gameLevels[0].SetList[_randomEasySetNumber].SetItems.Count; i++)
            {
                _gameLevels[0].SetList[_randomEasySetNumber].SetItems[i].gameObject.SetActive(true);
            }
        }
        else if (GameManager.Instance.Score > 3 && GameManager.Instance.Score <= 6) //Medium
        {
            Debug.Log("lets Spawn Med");
            int _randomMediumSetNumber = Random.Range(0, _gameLevels[1].SetList.Count);
            for (int i = 0; i < _gameLevels[1].SetList[_randomMediumSetNumber].SetItems.Count; i++)
            {
                _gameLevels[1].SetList[_randomMediumSetNumber].SetItems[i].gameObject.SetActive(true);
            }
        }
        else if (GameManager.Instance.Score > 6 && GameManager.Instance.Score <= 10)//Hard
        {
            Debug.Log("lets Spawn Hard");
            int _randomHardSetNumber = Random.Range(0, _gameLevels[2].SetList.Count);
            for (int i = 0; i < _gameLevels[2].SetList[_randomHardSetNumber].SetItems.Count; i++)
            {
                _gameLevels[2].SetList[_randomHardSetNumber].SetItems[i].gameObject.SetActive(true);
            }
        }
        else if (GameManager.Instance.Score > 10) // Random Difficulty
        {

            int _randomDifficultyNumber = Random.Range(0, 3);
            int _randomHardSetNumber = Random.Range(0, _gameLevels[_randomDifficultyNumber].SetList.Count);
            Debug.Log("lets Spawn Mixed"+ _randomDifficultyNumber +"  SetNo: "+ _randomHardSetNumber);
            for (int i = 0; i < _gameLevels[_randomDifficultyNumber].SetList[_randomHardSetNumber].SetItems.Count; i++)
            {
                _gameLevels[_randomDifficultyNumber].SetList[_randomHardSetNumber].SetItems[i].gameObject.SetActive(true);
            }
        }
        int _randomCoinsNumber = Random.Range(0, 5);
        Debug.Log("RAND COIN NUM" + _randomCoinsNumber);
        if(_randomCoinsNumber!= 0)
        {
            for (int i = 0; i < _randomCoinsNumber; i++)
            {
                _coinsList[i].gameObject.SetActive(true);
            }

        }
        _spinSpeed = Random.Range(_minMaxSpinSpeed.x, _minMaxSpinSpeed.y);
        
    }

    // Update is called once per frame
    void Update()
    {
        if (GameManager.Instance.IsGameStarted)
        {
            _itemsHolder.transform.Rotate(0, _spinSpeed * Time.deltaTime, 0);
        }
    }

    public void SpawnChunk(Transform LastChunkTransform)
    {
       GameObject LevelChunk= Instantiate(GameManager.Instance.Chunk, new Vector3(0, 0, LastChunkTransform.position.z + 7.5f), Quaternion.identity);
        LevelChunk.transform.parent = GameManager.Instance.LevelChunkHolder;
    }
    public void DestroyChunk(GameObject DestroyObject)
    {
        Destroy(this.gameObject);
    }


}
