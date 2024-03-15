using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunScript : MonoBehaviour
{
    // GameObjects
    public GameObject gun;
    public GameObject orangePortal;
    public GameObject bluePortal;
    public SFX sfx;

    void Start()
    {
        sfx = GameObject.Find("SFX").GetComponent<SFX>();
    }

    void Fire(string type)
    {
        // struct object that will hold our raycast information
        RaycastHit hit;

        // if we shoot an object with our gun, spawn a portal there
        if (Physics.Raycast(gun.transform.position, transform.TransformDirection(Vector3.forward), out hit, Mathf.Infinity))
        {
            sfx.openingPortalSound.Play(); ;

            // choose between the correct portals based on string input
            GameObject portal = (type == "orange" ? orangePortal : bluePortal);

            // set the portal
            portal.transform.SetPositionAndRotation(hit.point, Quaternion.FromToRotation(Vector3.forward, hit.normal));
        }
        else
        {
            sfx.errorSound.Play();
        }
    }

    // Update is called once per frame
    void Update()
    {
        // Draw a ray going from the gun to where you are looking at 
        Debug.DrawRay(gun.transform.position, transform.TransformDirection(Vector3.forward) * 1000, Color.red);

        // fire portal based on input
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            Fire("orange");
        }
        else if (Input.GetKeyDown(KeyCode.Mouse1))
        {
            Fire("blue");
        }
    }
}
