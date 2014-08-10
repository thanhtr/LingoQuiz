using UnityEngine;
using System.Collections;

[RequireComponent(typeof(BoxCollider))]
public class SwipeScrollerContent : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void SetColliderHeight(float height)
	{
		Debug.Log("SetColliderHeight() height= "+height);
		BoxCollider col = this.collider as BoxCollider;
		
		Vector3 newSize = col.size;
		Vector3 newCenter = col.center;
		
		newSize.y = height;
		newCenter.y = (height / 2);
		
		col.size = newSize;
		col.center = newCenter;
	}
}
