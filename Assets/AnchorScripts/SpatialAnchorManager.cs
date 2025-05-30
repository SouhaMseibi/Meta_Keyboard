using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class SpatialAnchorManager : MonoBehaviour
{
    public OVRSpatialAnchor anchorPrefab;

    private Canvas canvas;
    private TextMeshProUGUI uuidText;
    private TextMeshProUGUI savedStatusText;
    private List<OVRSpatialAnchor> anchors = new List<OVRSpatialAnchor>();
    private OVRSpatialAnchor lastCreatedAnchor;
    public const string NumUuidsPlayerPref = "numUuids";

    void Update()
    {
    if (OVRInput.GetDown(OVRInput.Button.PrimaryIndexTrigger, OVRInput.Controller.RTouch))
        {
            CreateSpatialAnchor();
        }

    // if (OVRInput.GetDown(OVRInput.Button.One, OVRInput.Controller.RTouch))
    //     {
    //         SaveLastCreatedAnchor();
    //     }

    // if (OVRInput.GetDown(OVRInput.Button.Two, OVRInput.Controller.RTouch))
    //     {
    //         UnsaveLastCreatedAnchor();
    //     }
    }


    public void CreateSpatialAnchor()
    {
        OVRSpatialAnchor workingAnchor = Instantiate(anchorPrefab, OVRInput.GetLocalControllerPosition(OVRInput.Controller.RTouch), OVRInput.GetLocalControllerRotation(OVRInput.Controller.RTouch));
        canvas = workingAnchor.gameObject.GetComponentInChildren<Canvas>();
        uuidText = canvas.gameObject.transform.GetChild(0).GetComponent<TextMeshProUGUI>();
        savedStatusText = canvas.gameObject.transform.GetChild(1).GetComponent<TextMeshProUGUI>();

    }
    private IEnumerator AnchorCreated(OVRSpatialAnchor workingAnchor)
    {
        while (!workingAnchor.Created && !workingAnchor.Localized)
        {
            yield return new WaitForEndOfFrame();
        }

        Guid anchorGuid = workingAnchor.Uuid;
        anchors.Add(workingAnchor);
        lastCreatedAnchor = workingAnchor;

        uuidText.text = "UUID: " + anchorGuid.ToString();
        savedStatusText.text = "Not Saved";
    }

    // private void SaveLastCreatedAnchor()
    // {
    //     lastCreatedAnchor.Save((lastCreatedAnchor, success) =>
    //     {
    //         if (success)
    //         {
    //             savedStatusText.text = "Saved";
    //         }
    //     });

    //     SaveUuidToPlayerPrefs(lastCreatedAnchor.Uuid);
    // }

    // void SaveUuidToPlayerPrefs(Guid uuid)
    // {
    //     if (!PlayerPrefs.HasKey(NumUuidsPlayerPref))
    //     {
    //         PlayerPrefs.SetInt(NumUuidsPlayerPref, 0);
    //     }

    //     int playerNumUuids = PlayerPrefs.GetInt(NumUuidsPlayerPref);
    //     PlayerPrefs.SetString("uuid" + playerNumUuids, uuid.ToString());
    //     PlayerPrefs.SetInt(NumUuidsPlayerPref, ++playerNumUuids);
    // }

    // private void UnsaveLastCreatedAnchor()
    // {
    //     lastCreatedAnchor.Erase((lastCreatedAnchor, success) =>
    //     {
    //         if (success)
    //         {
    //             savedStatusText.text = "Not Saved";
    //         }
    //     });
    // }

    // private void ClearAllUuidsFromPlayerPrefs()
    // {
    //     if (PlayerPrefs.HasKey(NumUuidsPlayerPref))
    //     {
    //         int playerNumUuids = PlayerPrefs.GetInt(NumUuidsPlayerPref);
    //         for (int i = 0; i < playerNumUuids; i++)
    //         {
    //             PlayerPrefs.DeleteKey("uuid" + i);
    //         }
    //         PlayerPrefs.DeleteKey(NumUuidsPlayerPref);
    //         PlayerPrefs.Save();
    //     }
    // }

    // public void LoadSavedAnchors()
    // {
    //         anchorLoader.LoadAnchorsByUuid();
    // }


}
