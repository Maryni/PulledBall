using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActionsReactions : MonoBehaviour
{
    [SerializeField] private Transform point;
    [SerializeField] private GameObject gameObjectMain;
    [SerializeField] private SphereCollider sphereCollider;
    [SerializeField] private ObjectManager objectManager;
    [SerializeField] private MoveToPortal moveToPortal;
    private Transform transformPool;
    public void ResetSizeColider() { sphereCollider.radius = .5f; }
    public void ChangeSizeColider(float mod) { sphereCollider.radius *= mod; }
    private void Start() { transformPool = objectManager.GameObjectPool().transform; }
    private void OnTriggerEnter(Collider other)
    {
        if(other.tag =="Enemy")
        {
            other.gameObject.transform.SetParent(transformPool);
            other.gameObject.transform.localPosition = new Vector3(1f,1f,1f);
            CheckChildCount();
        }
    }
    private void CheckChildCount()
    {
        if(!objectManager.IsParentHaveChilds())
        {
            moveToPortal.Move();
        }
    }
}
