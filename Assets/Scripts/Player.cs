using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    private void OnTriggerEnter(Collider other) {
		if (other.gameObject.tag == "Damage") {
			Destroy(other.gameObject);
	      	string currentSceneName = SceneManager.GetActiveScene().name;
	      	SceneManager.LoadScene(currentSceneName);
	  }
	}
}
