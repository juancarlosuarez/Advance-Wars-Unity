using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementCameraBetweenTiles : MonoBehaviour
{
    private Collider2D selectorCollider;
    private GameObject mainCam;
    private GameObject canvas;
    private GameObject colliderMoveCamera;
    
    // [SerializeField] GameObject childReferenceForMenuStartPosition;
    // [SerializeField] TransformReference menuStartPosition;
    // [SerializeField] private TransformReference centerCamera;
    private void Awake()
    {
        selectorCollider = GameObject.Find("Select").GetComponent<Collider2D>();
        mainCam = GameObject.Find("Main Camera");
        canvas = GameObject.Find("Canvas");
        colliderMoveCamera = GameObject.Find("ColliderMoveCamera");
    }
    private void OnTriggerExit2D(Collider2D collision)
    {

        if (collision == selectorCollider)
        {
            Vector2 lastDirection = WorldScriptableObjects.GetInstance().lastDirection.reference;
            //que cojnaoes
            canvas.transform.position = new Vector3(canvas.transform.position.x + lastDirection.x * 2,
                canvas.transform.position.y + lastDirection.y * 2, canvas.transform.position.z);
            colliderMoveCamera.transform.position = new Vector3(
                colliderMoveCamera.transform.position.x + lastDirection.x / 10,
                colliderMoveCamera.transform.position.y + lastDirection.y / 10);
            mainCam.transform.position = new Vector3(mainCam.transform.position.x + lastDirection.x * 2,
                mainCam.transform.position.y + lastDirection.y * 2, mainCam.transform.position.z);
            // Vector3 camPosition = new Vector3((mainCam.transform.position.x + Selector.sharedInstance.lastDirection.x),
            // (mainCam.transform.position.y + Selector.sharedInstance.lastDirection.y), mainCam.transform.position.z);
            //
            // mainCam.transform.position = camPosition;
            // canvas.transform.position = (Vector2)camPosition;
            //
            // menuStartPosition.reference = childReferenceForMenuStartPosition.transform;
        }


    }
}
