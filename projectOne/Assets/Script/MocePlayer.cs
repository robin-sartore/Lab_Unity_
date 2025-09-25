using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MovePlayer : MonoBehaviour
{
    [SerializeField] private float speed = 5f;
    [SerializeField] private AudioSource coinEffect;
    [SerializeField] private GameManager gameManager;
    [SerializeField] private GameObject vita1;
    [SerializeField] private GameObject vita2;
    [SerializeField] private GameObject vita3;

    private Rigidbody rb;
    private float inputX;
    private float inputZ;
    private bool apri = false;
    private bool chiudi = false;

    // ➕ Vite del giocatore
    private static int vite = 3; // STATICO per mantenerlo tra le scene (opzionale)

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        inputX = Input.GetAxis("Horizontal");
        inputZ = Input.GetAxis("Vertical");

        rb.linearVelocity = new Vector3(inputX * speed, rb.linearVelocity.y, -inputZ * speed); // Usa velocity invece di linearVelocity

        // Movimento porta
        GameObject porta = GameObject.Find("Porta");
        if (porta != null)
        {
            if (apri)
                porta.transform.Translate(Vector3.right * Time.deltaTime);
            if (chiudi)
                porta.transform.Translate(Vector3.left * Time.deltaTime);
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
            vite--; 
            Debug.Log("Hai perso una vita! Vite rimaste: " + vite);
            if (vite == 2){
                Destroy(vita1);
            }
            if (vite == 1){
                Destroy(vita2);
            }
            if (vite <= 0)
            {
                Destroy(vita3);
                vite = 3;
                SceneManager.LoadScene("Menu");
            }
            else
            {
                SceneManager.LoadScene("SampleScene");
            }
      

           
        }

        if (collision.gameObject.name.Equals("Porta"))
        {
            apri = true;
            chiudi = false;
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

        if (coinEffect != null)
            coinEffect.Play();

        yield return new WaitForSeconds(1);
        Destroy(coin);
        gameManager.UpdateScore();
    }
}
