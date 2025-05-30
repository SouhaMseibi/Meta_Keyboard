using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnchorPlacement : MonoBehaviour
{

    public GameObject anchorPrefab ; 
    // Start is called before the first frame update
    

    // Update is called once per frame
    void Update()
    {
        if (OVRInput.GetDown(OVRInput.Button.PrimaryIndexTrigger, OVRInput.Controller.RTouch))
        {
            CreateSpatialAnchor();
        }
    }
    public void CreateSpatialAnchor()
    {
        GameObject prefab = Instantiate(anchorPrefab, OVRInput.GetLocalControllerPosition(OVRInput.Controller.RTouch), OVRInput.GetLocalControllerRotation(OVRInput.Controller.RTouch));
        prefab.AddComponent<OVRSpatialAnchor>();
    }
}
