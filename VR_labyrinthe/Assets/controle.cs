using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VR;
using UnityEngine.XR;


public class controle : MonoBehaviour {
    private bool walking = false;
   
    private Vector3 spawn;
    public float Speed;
  
    private Transform cameraTransform;


    // Use this for initialization
    void Start () {
        spawn = transform.position;
        cameraTransform = Camera.main.transform;
        

        StartCoroutine(SwitchToVR("Cardboard"));
    }
    public IEnumerator SwitchToVR(string desiredDevice )
    {
        // Device names are lowercase, as returned by `XRSettings.supportedDevices`.
       

        // Some VR Devices do not support reloading when already active, see
        // https://docs.unity3d.com/ScriptReference/XR.XRSettings.LoadDeviceByName.html
        if (System.String.Compare(UnityEngine.XR.XRSettings.loadedDeviceName, desiredDevice, true) != 0)
        {
            UnityEngine.XR.XRSettings.LoadDeviceByName(desiredDevice);
            

            // Must wait one frame after calling `XRSettings.LoadDeviceByName()`.
            yield return null;
        }

        // Now it's ok to enable VR mode.
        UnityEngine.XR.XRSettings.enabled = true;
    }

    // Update is called once per frame
    void Update () {
		if (walking)
        {
            transform.position = transform.position + cameraTransform.forward * Speed * Time.deltaTime;
           
        }

        if (transform.position.y < -5f)
        { 
        transform.position = spawn;   

        }

        Ray r = Camera.main.ViewportPointToRay(new Vector3 (.5f, .7f, 0));
        RaycastHit hit;

        if (Physics.Raycast ( r , out hit))
        {
            if (hit.collider.name.Contains("surface"))
            {
                walking = false;
            }
        }

        else
        {
            walking = true;

        }





   



    }
   
}
