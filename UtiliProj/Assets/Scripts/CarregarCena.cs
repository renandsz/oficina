using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CarregarCena : MonoBehaviour
{
    public string cenaParaCarregar;
    void OnEnable()
    {
        SceneManager.LoadScene(cenaParaCarregar);
    }


}
