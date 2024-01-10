using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

public class GazeReticle : MonoBehaviour
{
    [SerializeField]
    private ARRaycastManager raycastManager;

    [SerializeField]
    private GameObject indicator;
    private List<ARRaycastHit> hits = new List<ARRaycastHit>();

    private Camera arCamera;


    // Start is called before the first frame update
    void Start()
    {
        indicator.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        var center = new Vector2(Screen.width / 2, Screen.height / 2);
        Ray ray = arCamera.ScreenPointToRay(center);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit))
        {


      

            indicator.transform.position = hit.point;

           
        }
    }
}
