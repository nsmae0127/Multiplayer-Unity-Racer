﻿using UnityEngine;
using System.Collections;

public class NetworkPlayer : Photon.MonoBehaviour {
    public GameObject myCamera;

    bool isAlive = true;
    Vector3 position;
    Quaternion rotation;
    float lerpSmoothing = 5f;

	// Use this for initialization
	void Start () {
        if (photonView.isMine)
        {
            gameObject.name = "Me";

            myCamera.SetActive(true);
            GetComponent<Motor>().enabled = true;
            GetComponent<Rigidbody>().useGravity = true;

            SwayBar[] sb = GetComponentsInChildren<SwayBar>();
            foreach (SwayBar bar in sb)
                bar.enabled = true;

            WheelCollider[] wheelCollider = GetComponentsInChildren<WheelCollider>();
            foreach (WheelCollider wc in wheelCollider)
                wc.enabled = true;

        } else
        {
            gameObject.name = "Network Player";

            StartCoroutine("Alive");
        }
	}

    void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (stream.isWriting)
        {
            stream.SendNext(transform.position);
            stream.SendNext(transform.rotation);
        } else
        {
            position = (Vector3)stream.ReceiveNext();
            rotation = (Quaternion) stream.ReceiveNext();
        }
    }

    // while alive do this state-machine
    IEnumerator Alive()
    {
        while(isAlive)
        {
            transform.position = Vector3.Lerp(transform.position, position, Time.deltaTime * lerpSmoothing);
            transform.rotation = Quaternion.Lerp(transform.rotation, rotation, Time.deltaTime * lerpSmoothing);

            yield return null;
        }
    }
}
