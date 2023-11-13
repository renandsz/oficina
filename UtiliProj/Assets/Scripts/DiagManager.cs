using System;
using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;

public class DiagManager : MonoBehaviour
{
    public static DiagManager instance;
    public TypewriterEffect janelaDialogo;
    public GameObject continuar;
    private bool proximo = false;

    public List<string> dialogo1;

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
      //  StartCoroutine(ExibirSequencia(dialogo1));
    }

    public void ExibirDialogo(ListaDialogo lista)
    {
        StartCoroutine(ExibirSequencia(lista.conteudo));
    }


    IEnumerator ExibirSequencia(List<string> dialogo)
    {
        foreach (var msg in dialogo)
        {
            continuar.SetActive(false);
            janelaDialogo.ExibirMensagem(msg);

            while (janelaDialogo.imprimindo)
            {
                Debug.Log("imprimindo");
                yield return new WaitForEndOfFrame();
            }
            continuar.SetActive(true);
            while (!proximo)
            {
                Debug.Log("esperando continuar");
                yield return new WaitForEndOfFrame();
            }
            Debug.Log("proximo");
            proximo = false;
        }
        Debug.Log("acabou");
        janelaDialogo.Limpar();
        continuar.SetActive(false);
        StopCoroutine(ExibirSequencia(dialogo));
    }

    private void Update()
    {
        if (!janelaDialogo.imprimindo)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                proximo = true;
            }
        }
    }
}
