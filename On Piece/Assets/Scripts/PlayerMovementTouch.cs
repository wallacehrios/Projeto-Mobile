using UnityEngine;

public class PlayerMovementTouch : MonoBehaviour
{
    public float moveSpeed = 5f; // Velocidade do movimento

    private Vector2 startTouchPosition;
    private Vector2 endTouchPosition;
    private Vector2 direction;

    private bool isMoving = false;

    void Update()
    {
        // Detectar toques na tela
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);

            if (touch.phase == TouchPhase.Began)
            {
                // Quando o toque começa, capturamos a posição inicial
                startTouchPosition = touch.position;
            }

            if (touch.phase == TouchPhase.Ended)
            {
                // Quando o toque termina, capturamos a posição final
                endTouchPosition = touch.position;

                // Calcula a direção do movimento
                direction = endTouchPosition - startTouchPosition;

                // Verifica a direção dominante (cima, baixo, esquerda, direita)
                if (Mathf.Abs(direction.x) > Mathf.Abs(direction.y))
                {
                    // Movimento na horizontal
                    if (direction.x > 0)
                    {
                        MoveRight();
                    }
                    else
                    {
                        MoveLeft();
                    }
                }
                else
                {
                    // Movimento na vertical (no plano XZ)
                    if (direction.y > 0)
                    {
                        MoveUp();
                    }
                    else
                    {
                        MoveDown();
                    }
                }
            }
        }
    }

    // Movimentação no plano XZ
    void MoveUp()
    {
        // Mover para frente (Z positivo)
        transform.Translate(Vector3.forward * moveSpeed * Time.deltaTime);
    }

    void MoveDown()
    {
        // Mover para trás (Z negativo)
        transform.Translate(Vector3.back * moveSpeed * Time.deltaTime);
    }

    void MoveLeft()
    {
        // Mover para a esquerda (X negativo)
        transform.Translate(Vector3.left * moveSpeed * Time.deltaTime);
    }

    void MoveRight()
    {
        // Mover para a direita (X positivo)
        transform.Translate(Vector3.right * moveSpeed * Time.deltaTime);
    }
}
