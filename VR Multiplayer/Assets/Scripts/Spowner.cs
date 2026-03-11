using UnityEngine;
using Photon.Pun;

public class Spowner : MonoBehaviour
{
    [SerializeField] private GameObject point;
    void Start()
    {
        GameObject player = PhotonNetwork.Instantiate("PhotonTransformView", point.transform.position,Quaternion.identity);
        
    }
}
