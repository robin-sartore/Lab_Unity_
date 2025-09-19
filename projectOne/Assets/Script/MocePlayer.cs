using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MovePlayer : MonoBehaviour
{
    [SerializeField]
    private float speed = 5f;

    private float inputX;
    private float inputZ;
    private bool apri = false;
    private bool chiudi = false;
    [SerializeField]
    public AudioSource coinEffect;
    private Rigidbody rb;
    [SerializeField]
    private GameManager gameManager;
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        inputX = Input.GetAxis("Horizontal");
        inputZ = Input.GetAxis("Vertical");

        rb.linearVelocity = new Vector3(inputX * speed, 0, -inputZ * speed);

        if (apri)
        {
            GameObject porta = GameObject.Find("Porta");
            if (porta != null)
            {
                porta.transform.Translate(new Vector3(1 * Time.deltaTime, 0, 0));
            }
        }
        if (chiudi)
        {
            GameObject porta = GameObject.Find("Porta");
            if (porta != null)
            {
                porta.transform.Translate(new Vector3(-1 * Time.deltaTime, 0, 0));
            }
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.name.Equals("Bag"))
        {
            collision.gameObject.GetComponent<BoxCollider>().enabled = false;
           StartCoroutine(DestroyCoin(collision.gameObject));
        }
        if (collision.gameObject.name.Equals("Cat"))
        {
            SceneManager.LoadScene("SampleScene");
        }
        if (collision.gameObject.name.Equals("Porta"))
        {
            apri = true;
        }
        if (collision.gameObject.name.Equals("Pavimento"))
        {
            apri = false;
            chiudi = true;
        }

    }
    IEnumerator DestroyCoin(GameObject coin)
    {
        yield return new WaitForSeconds(1);
        coin.GetComponent<MeshRenderer>().material.color = Color.red;
        coinEffect.Play();
        yield return new WaitForSeconds(1);
        Destroy(coin);
        gameManager.UpdateScore();

    }
}
