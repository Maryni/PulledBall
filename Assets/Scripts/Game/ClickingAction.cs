using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClickingAction : MonoBehaviour
{
    [SerializeField] private Camera camera;
    [SerializeField] private Transform point;
    [SerializeField] private Rigidbody rigidbody;
    [SerializeField] private LayerMask layerMask;
    [SerializeField] private float timer=1f;
    [SerializeField] private ObjectManager objectManager;
    [SerializeField] private ActionsReactions actionsReactions;
    private void FixedUpdate()
    {
        TakePosition();
    }
    private void TakePosition()
    {
        if (Input.GetMouseButton(0))
        {
            print("pressed LMB");
            if (timer <= 3.5f) timer += 0.01f;
                else CheckTimer();

            var ray = camera.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit = new RaycastHit();

            if (Physics.Raycast(ray, out hit,100, layerMask))
            {
                //print("hited in " + hit.collider.name + " and his point at " + hit.point);
                point.position = hit.point;
            }
        }
        if (Input.GetMouseButtonUp(0))
        {
            ActionsFromTimer();
            Distance();
            CheckTimer();
            print("unpressed LMB, timer =" + timer);
        }
    }
    private void Distance()
    {
        Vector3 playerPos;
        playerPos = objectManager.GameObjectPlayer().transform.position;
        var distance = (point.position - playerPos).normalized;
        rigidbody.AddForce(distance * 150f, ForceMode.Impulse);

        Debug.DrawRay(objectManager.GameObjectPlayer().transform.position, distance*35f);
        
    }
    private void CheckTimer()
    {
        if(timer>3.5f)
        {
            timer = 1f;
            point.localScale = new Vector3(1f,1f,1f);
            objectManager.GameObjectPlayer().transform.localScale= new Vector3(6f, 2f, 6f);
            objectManager.ChangeTransformPrefabParent(timer,true);
            actionsReactions.ResetSizeColider();
        }
    }
    private void ActionsFromTimer()
    {
        point.localScale *= timer;
        objectManager.ChangeTransformPlayer(timer);
        objectManager.ChangeTransformPrefabParent(timer,false);
        actionsReactions.ChangeSizeColider(timer);
    }
}
