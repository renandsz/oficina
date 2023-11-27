using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class DialogueManager : MonoBehaviour
{
    public EfeitoDigitadorB janelaTexto;
    public GameObject continuar;
    public bool proximo = false;
    public bool rodando = false;

    public List<string> lista,lista2;
    // Start is called before the first frame update
    void Start()
    {
       // StartCoroutine(ExibirSequencia(lista));
    }


    IEnumerator ExibirSequencia(List<string> lista)
    {
        if (rodando)
        {
            yield break;
        }
        rodando = true;
        proximo = false;
        foreach (var mensagem in lista)
        {
            continuar.SetActive(false);
            janelaTexto.ImprimirMensagem(mensagem);
            while (janelaTexto.imprimindo)
            {
                yield return new WaitForEndOfFrame();
            }
            continuar.SetActive(true);
            while (!proximo)
            {
                yield return new WaitForEndOfFrame();
            }
            proximo = false;
        }
        janelaTexto.Limpar();
        continuar.SetActive(false);
        rodando = false;
        StopCoroutine(ExibirSequencia(lista));
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            StartCoroutine(ExibirSequencia(lista));
        }
        if (Input.GetKeyDown(KeyCode.W))
        {
            StartCoroutine(ExibirSequencia(lista2));
        }
        
        
        
        if (!janelaTexto.imprimindo)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                proximo = true;
            }
        }
    }
}
