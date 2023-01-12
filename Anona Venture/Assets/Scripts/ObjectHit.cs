using UnityEngine.SceneManagement;
using UnityEngine;
using System.Reflection;

public class ObjectHit : MonoBehaviour
{
    int currentSceneIndex;
    AudioSource Audio;

    [SerializeField]AudioClip crash;
    [SerializeField]AudioClip success;
    [SerializeField] float delayTime;

    [SerializeField] ParticleSystem successParticles;
    [SerializeField] ParticleSystem crashParticles;
    
    bool isPlaying=false;
    bool iscollision = false;
    bool isEffectsCollision = false;
    private void Start()
    {
        currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        Audio = GetComponent <AudioSource>();
    }
    private void Update()
    {
        debugFuntions();
    }

    void debugFuntions()
    {
        if (Input.GetKey(KeyCode.L))
        {
            nextLvl();
        }
        else if (Input.GetKey(KeyCode.C))
        {
            iscollision=!iscollision;//Toggle *Opposite of whatever the preivous state was*
            toggleCollision();
        }
        else if (Input.GetKey(KeyCode.V))
        {
            isEffectsCollision = !isEffectsCollision;
        }
    }

    private void toggleCollision()
    {
        if (iscollision)
        {
            GetComponent<BoxCollider>().enabled = false;
        }
        if (!iscollision)
        {
            GetComponent<BoxCollider>().enabled = true;
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        if (isPlaying||isEffectsCollision)
        {
            return;
        }
        switch (collision.gameObject.tag)
        {
            case "Friendly":
                break;
            case "Finish":
                Audio.Stop();
                RemoveMovement();
                successParticles.Play();
                Invoke("nextLvl", delayTime);
                Audio.pitch = 1;
                Audio.PlayOneShot(success);
                isPlaying = true;
                break;
            default:
                Audio.Stop();
                ClearLog();
                crashParticles.Play();
                Audio.PlayOneShot(crash);
                startCrash();
                isPlaying = true;
                break;
        }
    }

    private void removeSound()
    {
        GetComponent<AudioSource>().enabled = false;
    }

    void startCrash()
    {
        RemoveMovement();
        Invoke("ReloadLevel", delayTime);
    }

    private void RemoveMovement()
    {
        GetComponent<Movement>().enabled = false;
    }

    public void ClearLog()
    {
        //var assembly = Assembly.GetAssembly(typeof(UnityEditor.Editor));
        //var type = assembly.GetType("UnityEditor.LogEntries");
        //var method = type.GetMethod("Clear");
       // method.Invoke(new object(), null);
    }
    void nextLvl()
    {
        int nextSceanIndex = currentSceneIndex + 1;
        if (nextSceanIndex == SceneManager.sceneCountInBuildSettings)
        {
            nextSceanIndex = 0;
        }
        SceneManager.LoadScene(nextSceanIndex);
    }
    void ReloadLevel()
    {
        SceneManager.LoadScene(currentSceneIndex);
    }
}
