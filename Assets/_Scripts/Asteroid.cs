using UnityEngine;

public class Asteroid : MonoBehaviour {

    float limit = 30f;
    Vector3 velocity;
    Vector3 playerPosition;
    public GameObject midAteroid;
    public GameObject smallAteroid;

    void Start () {
         playerPosition = GameObject.FindGameObjectWithTag("Player").transform.position;
        velocity = new Vector3(Random.Range(-4, 4), Random.Range(-4, 4), Random.Range(-4, -4));
    }
	
	void Update () {
        GetComponent<Rigidbody>().velocity = velocity;

        //Teleport object when it reached limit.
        if ((int) Vector3.Distance(transform.position, playerPosition) > limit) {
            float x;
            float y;
            float z;

            //Set new cords. Decrease by 1 for safer distance.
            if (transform.position.x > 0)
                x = -(transform.position.x - 1);
            else
                x = -(transform.position.x + 1);

            if (transform.position.y > 0)
                y = -(transform.position.y - 1);
            else
                y = -(transform.position.y + 1);
            if (transform.position.z > 0)
                z = -(transform.position.z - 1);
            else
                z = -(transform.position.z + 1);
            transform.position = new Vector3(x, y, z);
        }
           
    }

    public void Hit() {
        string name = gameObject.name;

        if (name.Contains("Small")) {
            GameObject.FindGameObjectWithTag("Manager").GetComponent<SceneManager>().AddScore(10);
        } else if (name.Contains("Mid")) {
            GameObject.FindGameObjectWithTag("Manager").GetComponent<SceneManager>().AddScore(20);
            Instantiate(smallAteroid, gameObject.transform.position, Quaternion.identity);
            Instantiate(smallAteroid, gameObject.transform.position, Quaternion.identity);
        } else if (name.Contains("Big")) {
            GameObject.FindGameObjectWithTag("Manager").GetComponent<SceneManager>().AddScore(50);
            Instantiate(midAteroid, gameObject.transform.position, Quaternion.identity);
            Instantiate(midAteroid, gameObject.transform.position, Quaternion.identity);
        }
        Destroy(gameObject);
    }

    void OnCollisionEnter(Collision collision) {
        if (collision.gameObject.name.Contains("Player")) {
            GameObject.FindGameObjectWithTag("Manager").GetComponent<SceneManager>().SubHeart();
            collision.gameObject.GetComponent<AudioSource>().Play();
            Destroy(gameObject);
        }
    }
}
