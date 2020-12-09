using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ClickingAction : MonoBehaviour
{
    [SerializeField] private Camera camera;
    [SerializeField] private Transform point;
    [SerializeField] private Rigidbody rigidbody;
    [SerializeField] private LayerMask layerMask;
    [SerializeField] private float timer=1f;
    [SerializeField] private ObjectManager objectManager;
    [SerializeField] private ActionsReactions actionsReactions;
    [SerializeField] private Text text;
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
            
            CheckTimer(true); //just transform a point at localScale =1f;
            text.text = timer.ToString();

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
            print("timer = " + timer);
            ActionsFromTimer();
            Distance();
            CheckTimer(false);
            print("unpressed LMB, late timer =" + timer);
        }
    }
    private void Distance()
    {
        Vector3 playerPos;
        playerPos = objectManager.GameObjectPlayer().transform.position;
        var distance = (point.position - playerPos).normalized;
        rigidbody.AddForce(distance * 200f, ForceMode.Impulse);

        Debug.DrawRay(objectManager.GameObjectPlayer().transform.position, distance*35f);
        
    }
    private void CheckTimer(bool reset)
    {
        if(!reset)
        {
            timer = 1f;
            //point.localScale = new Vector3(1f,1f,1f);
            //objectManager.GameObjectPlayer().transform.localScale= new Vector3(6f, 2f, 6f);

            //actionsReactions.ResetSizeColider();
        }
        if(reset)
        {
            point.localScale = new Vector3(1f, 1f, 1f);
            actionsReactions.ResetSizeColider();
            objectManager.ChangeTransformPrefabParent(timer, true);
        }
    }
    private void ActionsFromTimer()
    {
        point.localScale *= timer;
        objectManager.ChangeTransformPlayer(timer);
        objectManager.ChangeTransformPrefabParent(timer,false);
        actionsReactions.ChangeSizeColider(timer);
        print("actions complete at timer ="+timer);
    }
}
