using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class StoreInputStreamUrl : MonoBehaviour {

	private InputField inputField;

	void Start () {
		inputField = gameObject.GetComponent<InputField> ();
		inputField.onEndEdit.AddListener (StoreUrlAndChangeScene);
	}

	void Update () {
		
	}

	void StoreUrlAndChangeScene(string stringInputByUser) {
		print ("User input URL: " + stringInputByUser);
		StreamUrl.UserInputUrl = stringInputByUser;
		SceneManager.LoadScene ("VideoStreamDemo");
	}
}
