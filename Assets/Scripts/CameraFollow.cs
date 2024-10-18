using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform player; // O transform do jogador que a câmera vai seguir
    public Vector3 offset;   // A distância entre a câmera e o jogador

    void Start()
    {
        // Se não houver um offset definido, inicializa com uma distância padrão
        if (offset == Vector3.zero)
        {
            offset = transform.position - player.position;
        }
    }

    void LateUpdate()
    {
        // Atualiza a posição da câmera para seguir o jogador, mantendo o offset
        transform.position = player.position + offset;
    }
}
