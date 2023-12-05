using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DialogueManager : MonoBehaviour
{
    public EfeitoDigitador caixaDeDialogo;
    public GameObject botaoContinuar;
    public bool rodando, proximo;

    public Image imagemPerfil;
    public TextMeshProUGUI nomePersonagem;
   
    public void PlayDialogo(Dialogo diag)
    {
        diag.personagem.DebugarQuemTaFalando();
        imagemPerfil.sprite = diag.personagem.sprite;
        nomePersonagem.text = diag.personagem.nome;
        StartCoroutine(ExibirSequencia(diag.lista));
    }

    public void Resetar()
    {
        imagemPerfil.sprite = null;
        nomePersonagem.text = "";
    }

    IEnumerator ExibirSequencia(List<string> lista)
    {
        if (rodando) yield break;
        rodando = true;
        proximo = false;
        botaoContinuar.SetActive(false);

        foreach (var mensagem in lista)
        {
            caixaDeDialogo.ImprimirMensagem(mensagem);
            while (caixaDeDialogo.imprimindo)
            {
                yield return new WaitForEndOfFrame();
            }
            botaoContinuar.SetActive(true);
            while (!proximo)
            {
                yield return new WaitForEndOfFrame();
            }
            botaoContinuar.SetActive(false);
            proximo = false;
        }
        caixaDeDialogo.Limpar();
        rodando = false;
        Resetar();
        StopCoroutine(ExibirSequencia(lista));
    }

    // Update is called once per frame
    void Update()
    {
        
        if (!caixaDeDialogo.imprimindo)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                proximo = true;
            }
        }
    }
}
