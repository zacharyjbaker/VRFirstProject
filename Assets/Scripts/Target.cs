using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Target : MonoBehaviour
{
    [SerializeField] private float speed = 100f;
    [SerializeField] private GameObject[] target;
    [SerializeField] private int focus = 0;
    

    // Update is called once per frame
    void Update()
    {
        var step = speed * Time.deltaTime;
        //Debug.Log("step: " + step);
        //Debug.Log(this.transform.position);
        //Debug.Log(target[focus].transform.position);
        //Debug.Log("Focus: " + target[focus]);
        this.transform.position = Vector3.MoveTowards(this.transform.position, target[focus].transform.position, step);

        if (this.transform.position == target[focus].transform.position)
        {
            if (focus == target.Length - 1)
            {
                focus = 0;
            }
            else {
                focus += 1;
            }
        }
    }

    private void OnCollisionEnter(Collision other) {
        Destroy(gameObject);
    }

    /*public void OnCollisionEnter(Collision other) {
        Debug.Log("Collision");
        if (other.gameObject.CompareTag("Waypoint")) {
            Debug.Log("Waypoint Collision");
            if (focus == target.Length - 1)
            {
                focus = 0;
            }
            else {
                focus += 1;
            }
        }
    }
    */
}
