using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shot : MonoBehaviour {

    public Camera gunCam;
    public Transform gunEnd;
    private LineRenderer lineRenderer;
    private float weaponRange = 35f;

    private SceneManager sceneManager;

	void Start () {
        lineRenderer = GetComponent<LineRenderer>();
        sceneManager = GameObject.FindGameObjectWithTag("Manager").GetComponent<SceneManager>();
	}
	
	// Update is called once per frame
	void Update () {

        if (Input.GetMouseButtonDown(0) && !sceneManager.gameOver) {
            Vector3 rayOrigin = gunCam.ViewportToWorldPoint(new Vector3(0.5f, 0.5f, 0f));
            RaycastHit hit;

            StartCoroutine("ShotEffects");

            lineRenderer.SetPosition(0, gunEnd.position);

            if(Physics.Raycast(rayOrigin, gunCam.transform.forward, out hit, weaponRange)) {
                lineRenderer.SetPosition(1, hit.point);
                hit.collider.GetComponent<Asteroid>().Hit();
            } else {
                lineRenderer.SetPosition(1, rayOrigin + (gunCam.transform.forward * weaponRange));
            }
        }
    }

    IEnumerator ShotEffects() {
        lineRenderer.enabled = true;
        GetComponent<AudioSource>().Play();
        yield return new WaitForSeconds(0.2f);
        lineRenderer.enabled = false;
    }
}
