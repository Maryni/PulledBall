using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectManager : MonoBehaviour
{
    [SerializeField] private GameObject gameObjectPrefab;
    [SerializeField] private Transform transformPrefabParent;
    [SerializeField] private Transform transformPrefabParentCosmetic;
    [SerializeField] private int count;
    [SerializeField] private Transform gameObjectPool;
    [SerializeField] private GameObject gameObjectPlayer;

    public int GetCount() { return count; }
    public GameObject GameObjectPlayer() { return gameObjectPlayer; }
    public GameObject GameObjectPool() { return gameObjectPool.gameObject; }
    public bool IsParentHaveChilds() { if (transformPrefabParent.childCount == 0) return false; else return true; }

    [ContextMenu("SetCount(Random)")]
    private void SetCount()
    {
        count = Random.Range(10, 40);
    }

    private void Awake()
    {
        SpawnObjects(GetCount());
    }
    private void Start()
    {
        ShowObjects();
    }
    /// <summary>
    /// Spawn GO on pool for not call draw function on using Instantiate();
    /// </summary>
    private void SpawnObjects(int count)
    {
        for(int i=0; i<count; i++)
        {
            Instantiate(gameObjectPrefab, gameObjectPool);
        }
    }
    /// <summary>
    /// Show objects from GameObjects_Pool (take GO from pool and change position on visible part of scene)
    /// </summary>
    [ContextMenu("ShowObjects")]
    private void ShowObjects()
    {
        if(!IsParentHaveChilds())SpawnObjects(GetCount());
        Transform temp; // for not using long words like "gameObjectPool.GetChild(0).SetParent(transformPrefabParent);"
        for (int i=0; i<count; i++)
        {
            temp = gameObjectPool.GetChild(0);
            temp.SetParent(transformPrefabParent);
            temp.localPosition = new Vector3(0f,12f,0f);     
        }
        gameObjectPrefab.GetComponent<Rigidbody>().isKinematic = false; 
    }
    public void ChangeTransformPlayer(float mod)
    {
        gameObjectPlayer.transform.localScale /= mod;
    }
    public void ChangeTransformPrefabParent(float mod, bool reset)
    {
        if (!reset)
        {
            transformPrefabParentCosmetic.localScale = new Vector3(transformPrefabParentCosmetic.localScale.x / mod, 
                                                                   transformPrefabParentCosmetic.localScale.y, 
                                                                   transformPrefabParentCosmetic.localScale.z);
            transformPrefabParent.localScale = new Vector3(transformPrefabParent.localScale.x / mod, 
                                                           transformPrefabParent.localScale.y, 
                                                           transformPrefabParent.localScale.z);
        }
        if (reset)
        {
            transformPrefabParentCosmetic.localScale = new Vector3(.5f, 1f, 1f);
            transformPrefabParent.localScale = new Vector3(.5f, 1f, 1f);
        }
    }
}
