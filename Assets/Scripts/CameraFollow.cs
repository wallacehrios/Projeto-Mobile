using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform player; // O transform do jogador que a c�mera vai seguir
    public Vector3 offset;   // A dist�ncia entre a c�mera e o jogador

    void Start()
    {
        // Se n�o houver um offset definido, inicializa com uma dist�ncia padr�o
        if (offset == Vector3.zero)
        {
            offset = transform.position - player.position;
        }
    }

    void LateUpdate()
    {
        // Atualiza a posi��o da c�mera para seguir o jogador, mantendo o offset
        transform.position = player.position + offset;
    }
}
