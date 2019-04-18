using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camera_controller : MonoBehaviour
{

    [SerializeField] private GameObject target;
    private GameObject selected_space;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if(target != null)
            transform.position = Vector3.Slerp(transform.position, new Vector3(target.transform.position.x + 6, target.transform.position.y + 7, target.transform.position.z + 5), 0.3f);
        transform.rotation = Quaternion.RotateTowards(transform.rotation,Quaternion.LookRotation(target.transform.position - transform.position), 3);
        RaycastHit space;
        Ray toMouse = Camera.main.ScreenPointToRay(Input.mousePosition);
        int layer = LayerMask.GetMask("Space");
        if (Physics.Raycast(toMouse, out space, 100, layer, QueryTriggerInteraction.Collide))
        {
            if (selected_space == null)
            {
                selected_space = space.collider.gameObject;
                selected_space.GetComponent<Space_Script>().SetSelected();
            }
            else if (selected_space != space.collider.gameObject)
            {
                selected_space.GetComponent<Space_Script>().SetUnselected();
                selected_space = space.collider.gameObject;
                selected_space.GetComponent<Space_Script>().SetSelected();
            }
        }
        else if (selected_space != null)
        {
            selected_space.GetComponent<Space_Script>().SetUnselected();
            selected_space = null;
        }
    }
}
