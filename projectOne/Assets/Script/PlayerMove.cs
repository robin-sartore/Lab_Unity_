using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;


public class PlayerMove : MonoBehaviour 
{
    private CharacterController characterController;

    [SerializeField] private float speed = 5f;
    [SerializeField] private float gravity = -9.81f;
    [SerializeField] private AudioSource coinEffect;
    [SerializeField] private GameManager gameManager;

    [Header("Vite UI")]
    [SerializeField] private GameObject vita1;
    [SerializeField] private GameObject vita2;
    [SerializeField] private GameObject vita3;

    private bool apri = false;
    private bool chiudi = false;
    private bool portaTimerAttivo = false;

    private Vector3 velocity;

    // Porta
    private Transform portaTransform;
    private Vector3 portaPosizioneIniziale;
    private Vector3 portaPosizioneAperta;
    private float distanzaApertura = 4f;
    private float velocitaPorta = 2f;

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        characterController = GetComponent<CharacterController>();

        GameObject portaObj = GameObject.Find("Porta");
        if (portaObj != null)
        {
            portaTransform = portaObj.transform;
            portaPosizioneIniziale = portaTransform.position;
            portaPosizioneAperta = portaPosizioneIniziale + portaTransform.right * distanzaApertura;
        }

        switch (gameManager.vite)
        {
            case 3:
                vita1.SetActive(true);
                vita2.SetActive(true);
                vita3.SetActive(true);
                break;
            case 2:
                vita1.SetActive(false);
                vita2.SetActive(true);
                vita3.SetActive(true);
                break;
            case 1:
                vita1.SetActive(false);
                vita2.SetActive(false);
                vita3.SetActive(true);
                break;
            case 0:
                vita1.SetActive(false);
                vita2.SetActive(false);
                vita3.SetActive(false);
                break;
        }
    }

    private void Update()
    {
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        Vector3 move = transform.right * x + transform.forward * z;
        characterController.Move(move * speed * Time.deltaTime);

        velocity.y += gravity * Time.deltaTime;
        characterController.Move(velocity * Time.deltaTime);

        // Movimento porta
        if (portaTransform != null)
        {
            if (apri)
            {
                portaTransform.position = Vector3.MoveTowards(
                    portaTransform.position,
                    portaPosizioneAperta,
                    velocitaPorta * Time.deltaTime
                );
            }
            else if (chiudi)
            {
                portaTransform.position = Vector3.MoveTowards(
                    portaTransform.position,
                    portaPosizioneIniziale,
                    velocitaPorta * Time.deltaTime
                );
            }
        }
    }

    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
        if (hit.gameObject.name.Equals("Bag"))
        {
            hit.gameObject.GetComponent<BoxCollider>().enabled = false;
            StartCoroutine(DestroyCoin(hit.gameObject));
        }

        if (hit.gameObject.name.Equals("Cat"))
        {
            gameManager.vite--;

            Debug.Log("Hai perso una vita. Vite rimaste: " + gameManager.vite);

            if (gameManager.vite == 2 && vita1 != null) vita1.SetActive(false);
            if (gameManager.vite == 1 && vita2 != null) vita2.SetActive(false);
            if (gameManager.vite <= 0 && vita3 != null) vita3.SetActive(false);

            StartCoroutine(RicaricaDopoPerdita());
        }

        if (hit.gameObject.name.Equals("Porta"))
        {
            apri = true;
            chiudi = false;

            if (!portaTimerAttivo)
            {
                StartCoroutine(RichiudiPortaDopoTempo());
            }
        }

        if (hit.gameObject.name.Equals("Pavimento"))
        {
            apri = false;
            chiudi = true;
        }
    }

    private IEnumerator DestroyCoin(GameObject coin)
    {
        yield return new WaitForSeconds(1);
        coin.GetComponent<MeshRenderer>().material.color = Color.red;

        if (coinEffect != null)
        {
            coinEffect.Play();
        }

        yield return new WaitForSeconds(1);
        Destroy(coin);
        gameManager.UpdateScore();
    }

    private IEnumerator RichiudiPortaDopoTempo()
    {
        portaTimerAttivo = true;
        yield return new WaitForSeconds(5f);
        apri = false;
        chiudi = true;
        portaTimerAttivo = false;
    }

    private IEnumerator RicaricaDopoPerdita()
    {
        yield return new WaitForSeconds(0.3f);

        if (gameManager.vite <= 0)
        {
            gameManager.ResetGame();
            SceneManager.LoadScene("Menu");
        }
        else
        {
            SceneManager.LoadScene("SampleScene");
        }
    }
}
