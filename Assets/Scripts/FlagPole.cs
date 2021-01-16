using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FlagPole : MonoBehaviour
{
    [SerializeField] Material ActiveMaterial;
    [SerializeField] Transform flagTransform;
    [SerializeField] MeshRenderer flagMeshRenderer;
    [SerializeField] Transform flagStopperTransform;
    MeshRenderer mr;

    // Start is called before the first frame update
    void Start()
    {
        mr = GetComponent<MeshRenderer>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            mr.material = ActiveMaterial;
            flagMeshRenderer.material = ActiveMaterial;
            flagTransform.position = new Vector3(flagTransform.position.x, flagStopperTransform.position.y);
            GameManager.Instance.LoadNextScene();
        }
    }
}
