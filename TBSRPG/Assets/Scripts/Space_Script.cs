using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Space_Script : MonoBehaviour {

    [SerializeField] private GameObject north, south, east, west;
    [SerializeField] private Material neutral, selectable, selected;

    private Renderer my_render;

    private bool norm = true;

	// Use this for initialization
	void Start () {
        my_render = GetComponent<Renderer>();
        Collider[] colliders;
        if((colliders = Physics.OverlapSphere(transform.position, 1f)).Length > 1)
        {
            foreach (var collider in colliders)
            {
                var obj = collider.gameObject;
                if (obj.CompareTag("Space"))
                {
                    if(obj.transform.position.x < transform.position.x && obj.transform.position.z == transform.position.z)
                    {
                        west = obj;
                    }
                    else if(obj.transform.position.x > transform.position.x && obj.transform.position.z == transform.position.z)
                    {
                        east = obj;
                    }
                    else if (obj.transform.position.z < transform.position.z && obj.transform.position.x == transform.position.x)
                    {
                        south = obj;
                    }
                    else if (obj.transform.position.z > transform.position.z && obj.transform.position.x == transform.position.x)
                    {
                        north = obj;
                    }
                }
            }
        }
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            SetSelectable(5);
        }
    }

    public void Refresh()
    {
        if (!norm)
        {
            norm = true;
            my_render.material = neutral;
        }
    }

    public GameObject SetSelectable(int range)
    {
        if (norm)
        {
            norm = false;
            my_render.material = selectable;
            if(range > 1)
            {
                if(east != null)
                    east.GetComponent<Space_Script>().SetSelectable(range - 1);
                if(west != null)
                    west.GetComponent<Space_Script>().SetSelectable(range - 1);
                if(north != null)
                    north.GetComponent<Space_Script>().SetSelectable(range - 1);
                if(south != null)
                    south.GetComponent<Space_Script>().SetSelectable(range - 1);
            }
        }
        return gameObject;
    }

    public GameObject SetSelected()
    {
        my_render.material = selected;
        return gameObject;
    }

    public void SetUnselected()
    {
        if (!norm)
        {
            my_render.material = selectable;
        }
        else
        {
            my_render.material = neutral;
        }
    }
}
