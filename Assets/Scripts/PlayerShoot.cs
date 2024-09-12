using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerShoot : MonoBehaviour
{
    [SerializeField]
    private GameObject BulletTemplate;
    public float shootPower = 500f;

    public InputActionReference trigger;

    void Start()
    {
        trigger.action.performed += Shoot;
    }

    /*void Update()
    {
        if (Input.GetMouseButton(0)){
            Vector3 mousePos = Input.mousePosition;
            mousePos.z = 2.0f;
            GameObject newBullet = Instantiate(BulletTemplate, mousePos, Quaternion.LookRotation(mousePos));
            newBullet.GetComponent<Rigidbody>().AddForce(mousePos * shootPower);
        }
    }*/

    void Shoot(InputAction.CallbackContext __) {
        GameObject newBullet = Instantiate(BulletTemplate, transform.position, transform.rotation);
        newBullet.GetComponent<Rigidbody>().AddForce(transform.forward * shootPower);
    }
}