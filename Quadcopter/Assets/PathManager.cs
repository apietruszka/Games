using UnityEngine;
using System.Collections;

public class PathManager : MonoBehaviour {

    public int currentIndex = 0;
    public GameObject selectedObject;
    public Circle[] path;
    Transform quadcopter;
    public Material circle;

	void Start ()
    {
        //path = GameObject.FindObjectsOfType<Circle>();
        quadcopter = GameObject.FindObjectOfType<DroneController>().transform;
	}
	
	void Update ()
    {
        if(currentIndex<path.Length)
        {
            lightUpSelected();
            if (Vector3.Distance(path[currentIndex].transform.position, quadcopter.position) < 4f)
            //if (Vector3.Distance(quadcopter.transform.position,path[currentIndex].transform.position)<10f)
            {
                Debug.Log("Circle crossed");
                ClearSelection();
                currentIndex++;
            }
                
        }
        
	}

    void lightUpSelected()
    {
        //Debug.Log("selected: " + path[currentIndex]);
        selectedObject = path[currentIndex].gameObject;

        Renderer[] rs = selectedObject.GetComponentsInChildren<Renderer>();
        foreach (Renderer r in rs)
        {
            Material m = r.material;
            m.color = Color.green;
            r.material = m;
        }
    }
    void ClearSelection()
    {
        Color cToComeBack = circle.color;

        if (selectedObject == null)
            return;

        Renderer[] rs = selectedObject.GetComponentsInChildren<Renderer>();
        foreach (Renderer r in rs)
        {
            Material m = r.material;
            m.color = cToComeBack;
            r.material = m;
        }

        selectedObject = null;
    }
}
