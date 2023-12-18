using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class DialogueManager : MonoBehaviour
{
    public EfeitoDigitador caixaDeDialogo;
    public GameObject botaoContinuar,painelDeFala,painelDeResposta,janelaDeDialogo;
    public bool rodando, proximo;

    public Image imagemPerfil;
    public TextMeshProUGUI nomePersonagem,msgBt1,msgBt2;
    public GameObject bt1, bt2;
    private EventSystem _eventSystem;

    private int _respostaEscolhida = -1;

    private void Awake()
    {
        _eventSystem = FindObjectOfType<EventSystem>();
    }

    public void PlayDialogo(Dialogo diag)
    {
        janelaDeDialogo.SetActive(true);
        imagemPerfil.sprite = diag.personagem.sprite;
        nomePersonagem.text = diag.personagem.nome;
        painelDeFala.SetActive(true);
        painelDeResposta.SetActive(false);
        StartCoroutine(ExibirSequencia(diag));
    }

    public void Resetar()
    {
        imagemPerfil.sprite = null;
        nomePersonagem.text = "";
    }

    IEnumerator DialogueCoroutine(Dialogo diagSO)
    {
        if (rodando) yield break;
        
        
    }

    IEnumerator ExibirSequencia(Dialogo diag)
    {
        if (rodando) yield break;
        rodando = true;
        proximo = false;
        botaoContinuar.SetActive(false);

        foreach (var mensagem in diag.lista)
        {
            caixaDeDialogo.ImprimirMensagem(mensagem);
            while (caixaDeDialogo.imprimindo)
            {
                yield return new WaitForEndOfFrame();
            }
            botaoContinuar.SetActive(true);
            _eventSystem.SetSelectedGameObject(botaoContinuar);
            while (!proximo)
            {
                yield return new WaitForEndOfFrame();
            }
            botaoContinuar.SetActive(false);
            proximo = false;
        }
        //verificar se tem q mostrar botoes de resposta
        if (diag.tipo == TipoDialogo.passarDireto)
        {
            janelaDeDialogo.SetActive(false);
        }
        if (diag.tipo == TipoDialogo.mostrarRespostas)
        {
            painelDeFala.SetActive(false);
            AtualizarBotoes(diag);
            painelDeResposta.SetActive(true);
            while (_respostaEscolhida == -1)
            {
                yield return new WaitForEndOfFrame();
            }
            rodando = false;
            StopCoroutine(ExibirSequencia(diag));
            switch (_respostaEscolhida)
            {
                case 1:
                    ExibirSequencia(diag.diagBotao1);
                    break;
                case 2:
                    ExibirSequencia(diag.diagBotao2);
                    break;
            }
            _eventSystem.SetSelectedGameObject(bt1);
        }
        
        
        rodando = false;
        //janelaDeDialogo.SetActive(false);
        StopCoroutine(ExibirSequencia(diag));
    }

    public void Proximo()
    {
        if (!caixaDeDialogo.imprimindo)
        {
            proximo = true;
        }
    }

    public void Responder(int numeroBotao)
    {
        _respostaEscolhida = numeroBotao;
    }

    public void AtualizarBotoes(Dialogo diag)
    {
        if (diag.tipo != TipoDialogo.mostrarRespostas) return;
        if (diag.botao1)
        {
            msgBt1.text = diag.msgBotao1;
        }
        if (diag.botao2)
        {
            msgBt2.text = diag.msgBotao2;
        }
    }
    
    
}
