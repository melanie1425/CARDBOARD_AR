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


    // Start is called before the first frame update
    void Start()
    {
        indicator.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        var ray = new Vector2(Screen.width / 2, Screen.height / 2);

        if(raycastManager.Raycast(ray, hits, TrackableType.Planes))
        {
            Pose hitpose = hits[0].pose;

            transform.position = hitpose.position;

            transform.rotation = hitpose.rotation;

            if (!indicator.activeInHierarchy)
            {
                indicator.SetActive(true); 
            }
        }
    }
}
