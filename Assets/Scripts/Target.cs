using System.Collections;
using System.Collections.Generic;
using System.Linq;

//using UnityEditor.Search;
using UnityEngine;

public class Target : MonoBehaviour
{

    [SerializeField] private float speed = 100f;
    [SerializeField] private GameObject[] target;
    [SerializeField] private int focus = 0;
    [SerializeField] private GameObject playerTarget;

    [SerializeField]
    private GameObject BulletTemplate;
    public float shootPower = 500f;
    public float targetTime = 3f;

    [SerializeField] private bool shoota;
    [SerializeField] private bool stabba;

    [SerializeField] private GameObject leftLeg;
    [SerializeField] private GameObject rightLeg;
    Vector3 forwardLeftRot;
    Vector3 forwardRightRot;
    Vector3 defaultLeftRot;
    Vector3 defaultRightRot;
    Quaternion forwardLeftQuat;
    Quaternion forwardRightQuat;
    Quaternion defaultLeftQuat;
    Quaternion defaultRightQuat;
    Vector3 strafeDir;
    
    void Start()
    {
        defaultLeftRot += leftLeg.transform.eulerAngles;
        defaultLeftQuat.eulerAngles = defaultLeftRot;

        defaultRightRot += rightLeg.transform.eulerAngles;
        defaultRightQuat.eulerAngles = defaultRightRot;

        forwardLeftRot = new Vector3(leftLeg.transform.eulerAngles.x + 55f, leftLeg.transform.eulerAngles.y, leftLeg.transform.eulerAngles.z);
        forwardRightRot = new Vector3(rightLeg.transform.eulerAngles.x + 55f, rightLeg.transform.eulerAngles.y, rightLeg.transform.eulerAngles.z);
        
        forwardLeftQuat.eulerAngles = forwardLeftRot;
        forwardRightQuat.eulerAngles = forwardRightRot;
        strafeDir = transform.right;
    }

    // Update is called once per frame  
    void Update()
    {
        //Debug.Log(Time.timeScale);
        StickToGround();
        var step = speed * Time.deltaTime;
        if (playerTarget != null) {
		    transform.LookAt(playerTarget.transform.position);
            if (shoota == true) {
                transform.position = Vector3.MoveTowards(transform.position, strafeDir * 5f, step);
            }
            else if (stabba == true) {
                transform.position = Vector3.MoveTowards(transform.position, playerTarget.transform.position, step);
            }
            leftLeg.transform.rotation = Quaternion.Lerp(defaultLeftQuat, forwardLeftQuat, Mathf.PingPong(Time.time,1));
            rightLeg.transform.rotation = Quaternion.Lerp(defaultRightQuat, forwardRightQuat, Mathf.PingPong(Time.time,1));
        }

        targetTime -= Time.deltaTime;

        if (targetTime <= 0.1f)
        {
            if (shoota == true){
                Shoot();
                strafeDir = -transform.right;
            }
            targetTime = 3f;
        }
    }

        /*
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
        }*/

    private void OnTriggerEnter(Collider other) {
        
        if (other.tag == "Bullet"){
            Destroy(other);
            Destroy(gameObject);
        }
        else if (other.tag == "Player"){
            Destroy(other);
        }
    }

    private void Shoot() {
        GameObject newBullet = Instantiate(BulletTemplate, transform.GetChild(0).transform.position + transform.GetChild(0).transform.forward * 0.3f, transform.rotation);
        newBullet.GetComponent<Rigidbody>().AddForce(transform.forward * shootPower);
    }

    private void StickToGround() {
        RaycastHit hit;
        if (Physics.Raycast(transform.position, -transform.up, out hit, Mathf.Infinity)){
            transform.position -= new Vector3(0 , hit.distance, 0);
        }
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
