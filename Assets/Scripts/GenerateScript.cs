using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GenerateScript : MonoBehaviour {

	public GameObject[] availableSegments;
	
	public List<GameObject> currentSegments;
	
	private float screenWidthInPoints;
	// Use this for initialization
	void Start () {

		float height = 2.0f * Camera.main.orthographicSize;
		screenWidthInPoints = height * Camera.main.aspect;
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void FixedUpdate () 
	{    
		GenerateSegmentIfRequired();
	}

	void AddSegment(float farthestSegmentEndX)
	{
				//1
				int randomSegmentIndex = Random.Range (0, availableSegments.Length);
		
				//2
			if (!currentSegments [currentSegments.Count - 1].name.Equals(availableSegments [randomSegmentIndex].name+"(Clone)")) {

						GameObject segment = (GameObject)Instantiate (availableSegments [randomSegmentIndex]);
		
						//3
						float segmentWidth = 20f;
		
						//4
						float segmentCenter = farthestSegmentEndX + segmentWidth * 0.5f;
		
						//5
						segment.transform.position = new Vector3 (segmentCenter, 0, 0);
		
						//6
						currentSegments.Add (segment);         
				} else {
						AddSegment (farthestSegmentEndX);
				}
		}

	void GenerateSegmentIfRequired()
	{
		//1
		List<GameObject> segmentsToRemove = new List<GameObject>();
		
		//2
		bool addSegments = true;        
		
		//3
		float playerX = transform.position.x;
		
		//4
		float removeSegmentX = playerX - screenWidthInPoints;        
		
		//5
		float addSegmentX = playerX + screenWidthInPoints;
		
		//6
		float farthestSegmentEndX = 0;
		
		foreach(var segment in currentSegments)
		{
			//7
			float segmentWidth = 20;
			float segmentStartX = segment.transform.position.x - (segmentWidth * 0.5f);    
			float segmentEndX = segmentStartX + segmentWidth;                            
			
			//8
			if (segmentStartX > addSegmentX)
				addSegments = false;
			
			//9
			if (segmentEndX < removeSegmentX)
				segmentsToRemove.Add(segment);
			
			//10
			farthestSegmentEndX = Mathf.Max(farthestSegmentEndX, segmentEndX);
		}
		
		//11
		foreach(var segment in segmentsToRemove)
		{
			currentSegments.Remove(segment);
			Destroy(segment);            
		}
		
		//12
		if (addSegments)
			AddSegment(farthestSegmentEndX);
	}
}
