using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Portal : MonoBehaviour
{
    private bool portalOpened = true; // true if the portal is oponed

    public Portal portalBis; // Portal connected to this

	void OnTriggerEnter(Collider other) // Upon collision with another GameObject 
	{
		if (portalOpened) // we verify if the portal is opened 
		{
			portalBis.UpdatePortal(); // Deactivate the other portal so we don't get teleported back and forth

			UpdatePortal();

			// cache player rotation to revert after teleport
			float xRot = other.transform.rotation.x;
			float zRot = other.transform.rotation.z;
			float yRot = other.transform.eulerAngles.y;
			other.transform.SetPositionAndRotation(portalBis.transform.position, Quaternion.identity); // set the new player's position
			other.transform.rotation = portalBis.transform.parent.transform.rotation; // set the new player's rotation 
			other.transform.eulerAngles = new Vector3(xRot, yRot, zRot);
		}
	}

	void OnTriggerExit(Collider other)
	{
		UpdatePortal(); // reactivating the portal after we enter it
	}

	void UpdatePortal()
    {
		portalOpened = !portalOpened; // we switch the state of the portal
	}
} 
