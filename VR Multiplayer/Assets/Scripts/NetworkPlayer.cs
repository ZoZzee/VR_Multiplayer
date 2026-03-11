using UnityEngine;
using Photon.Pun;
using Unity.XR.CoreUtils;

public class NetworkPlayer : MonoBehaviour
{
    public Transform head;
    public Transform leftHand;
    public Transform rightHand;

    private Transform _localHead;
    private Transform _localLeftHand;
    private Transform _localRightHand;
    public void Start()
    {
        if (GetComponent<PhotonView>().IsMine)
        {
            XROrigin rig = FindFirstObjectByType<XROrigin>();
            _localHead = rig.Camera.transform;
            _localLeftHand = rig.transform.Find("Camera Offset/Left Controller");
            _localRightHand = rig.transform.Find("Camera Offset/Right Controller");
            
            head.gameObject.layer = LayerMask.NameToLayer("Head");
            NewLayer(leftHand);
            NewLayer(rightHand);
        }

    }
    public void Update()
    {
        if (GetComponent<PhotonView>().IsMine)
        {
            SyncPosition(head, _localHead);
            SyncPosition(leftHand, _localLeftHand);
            SyncPosition(rightHand, _localRightHand);
        }
    }

    public void SyncPosition(Transform visual, Transform realSource)
    {
        if (realSource != null)
        {
            visual.position = realSource.position;
            visual.rotation = realSource.rotation;
        }
    }

    public void SetLayerRecursively(GameObject obj, int newLayer)
    {
        obj.layer = newLayer;
        foreach (Transform child in obj.transform)
        {
            SetLayerRecursively(child.gameObject, newLayer);
        }
    }

    private void NewLayer(Transform Object,int newLayer = 6)
    {
        foreach (Transform child in Object.transform)
        {
            SetLayerRecursively(child.gameObject, newLayer);
        }
    }
}

