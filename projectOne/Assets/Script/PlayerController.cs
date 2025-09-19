using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{

    CharacterController characterController;

    [SerializeField]
    public float speed;
    [SerializeField]
    public float gravity = -9.8f;
    private bool apri = false;
    private bool chiudi = false;
    [SerializeField]
    public AudioSource coinEffect;
    [SerializeField]
    private GameManager gameManager;

    private void Start()

    {
        Cursor.lockState = CursorLockMode.Locked;
        characterController = GetComponent<CharacterController>();
    }

    void Update()
    {
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        Vector3 move = transform.right * x + transform.up * gravity + transform.forward * z;
        characterController.Move(move * speed * Time.deltaTime);
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
