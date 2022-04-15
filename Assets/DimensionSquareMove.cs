using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DimensionSquareMove : MonoBehaviour
{
    // Start is called before the first frame update
    void Update() {
        if (Input.GetKey(KeyCode.J))
        {
            transform.position=new Vector3(transform.position.x+0.3f, transform.position.y, transform.position.z+1.09f);
        }
        if (Input.GetKey(KeyCode.H))
        {
            transform.position=new Vector3(transform.position.x-0.3f, transform.position.y, transform.position.z-1.09f);
        }

        Debug.DrawRay(transform.position,
            transform.TransformDirection(-new Vector3(0,0, 30)), Color.magenta);
    }
}
