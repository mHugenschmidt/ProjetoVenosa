using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class mudarCor : MonoBehaviour
{
    private string endpointURL = "http://localhost/getCor.php"; // URL do endpoint web local
    private float requestInterval = 1f; // Intervalo de solicitação em segundos

    IEnumerator Start()
    {
        while (true)
        {
            // Faz a solicitação ao endpoint web local
            using (UnityWebRequest www = UnityWebRequest.Get(endpointURL))
            {
                yield return www.SendWebRequest();

                if (www.result != UnityWebRequest.Result.Success)
                {
                    Debug.LogError("Erro ao fazer a solicitação: " + www.error);
                }
                else
                {
                    // Obtém a string JSON da resposta
                    string jsonResponse = www.downloadHandler.text;

                    // Analisa a string JSON para extrair a cor
                    Color color = ExtractColorFromJSON(jsonResponse);

                    // Aplica a cor ao objeto na cena
                    GetComponent<Renderer>().material.color = color;
                }
            }

            // Aguarda o intervalo antes de fazer a próxima solicitação
            yield return new WaitForSeconds(requestInterval);
        }
    }

    // Função para extrair a cor da string JSON
    private Color ExtractColorFromJSON(string json)
    {
        // Analisa a string JSON para obter o valor da cor
        try
        {
            // Deserializa a string JSON para um objeto ColorData
            ColorData colorData = JsonUtility.FromJson<ColorData>(json);

            // Converte a string de cor hexadecimal para um tipo Color
            Color color;
            if (ColorUtility.TryParseHtmlString(colorData.color, out color))
            {
                return color;
            }
            else
            {
                Debug.LogError("Erro ao analisar a cor recebida: " + colorData.color);
                return Color.white; // Retorna uma cor padrão em caso de erro
            }
        }
        catch (System.Exception e)
        {
            Debug.LogError("Erro ao analisar a string JSON: " + e.Message);
            return Color.white; // Retorna uma cor padrão em caso de erro
        }
    }

    // Classe auxiliar para armazenar a cor em formato JSON
    [System.Serializable]
    private class ColorData
    {
        public string color;
    }
}