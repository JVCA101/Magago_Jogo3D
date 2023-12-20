using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GhostCounter : MonoBehaviour
{
    [SerializeField] private GameObject gameObject;
    private int ghostCount = 0;
    private TMP_Text text;

    // Start is called before the first frame update
    void Start()
    {
        ghostCount = GetComponentsInChildren<GhostController>().Length;
        gameObject = GameObject.FindGameObjectWithTag("Counter");
        text = gameObject.GetComponent<TMP_Text>();
        text.text = "Fantasmas vivos: " + ghostCount;
    }

    // Update is called once per frame
    void Update()
    {
        text.text = "Fantasmas vivos: " + ghostCount;
    }

    public void GhostDied()
    {
        ghostCount--;
        if(ghostCount <= 0)
            ScenesController.Victory();
    }
}
