using UnityEngine;

public class BoxInteraction : MonoBehaviour
{
    public float gridSize = 1f; // Tamanho do grid para movimento "robótico"
    public float moveSpeed = 5f; // Velocidade de movimento da caixa ao ser puxada
    public float interactionRange = 2f; // Distância máxima para ativar a caixa

    private bool isPullingBox = false; // Se a caixa está sendo puxada
    private Transform player; // Referência ao jogador
    private Vector3 lastPlayerPosition; // Última posição do jogador

    private static GameObject currentlyPulledBox = null; // Variável estática para rastrear a caixa que está sendo puxada

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        lastPlayerPosition = player.position; // Inicializa com a posição inicial do jogador
    }

    void Update()
    {
        // Detecta toques na tela
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);

            if (touch.phase == TouchPhase.Began)
            {
                // Detecta se a caixa foi tocada
                Ray ray = Camera.main.ScreenPointToRay(touch.position);
                RaycastHit hit;

                if (Physics.Raycast(ray, out hit) && hit.collider.gameObject == gameObject)
                {
                    // Verifica se a caixa está sendo puxada atualmente
                    if (isPullingBox)
                    {
                        // Solta a caixa
                        isPullingBox = false;
                        currentlyPulledBox = null;
                        GetComponent<Renderer>().material.color = Color.white; // Reseta a cor da caixa
                    }
                    else
                    {
                        // Se a caixa não está sendo puxada, verifica a distância ao jogador
                        float distanceToPlayer = Vector3.Distance(player.position, transform.position);
                        if (distanceToPlayer <= interactionRange)
                        {
                            // Se já há uma caixa sendo puxada, não faz nada
                            if (currentlyPulledBox == null)
                            {
                                // Puxa a caixa
                                isPullingBox = true;
                                currentlyPulledBox = gameObject; // Define esta caixa como a que está sendo puxada
                                GetComponent<Renderer>().material.color = Color.red; // Muda a cor da caixa
                                lastPlayerPosition = player.position; // Atualiza a posição inicial do jogador
                            }
                        }
                    }
                }
            }
        }

        // Se está puxando a caixa, mova-a apenas quando o jogador se mover
        if (isPullingBox)
        {
            Vector3 playerMovement = player.position - lastPlayerPosition;

            if (playerMovement.magnitude > 0)
            {
                // Move a caixa na mesma direção do movimento do jogador
                Vector3 targetPosition = CalculateGridPosition(transform.position + playerMovement);
                transform.position = Vector3.MoveTowards(transform.position, targetPosition, moveSpeed * Time.deltaTime);

                // Atualiza a posição do jogador
                lastPlayerPosition = player.position;
            }
        }
    }

    // Calcula a posição da caixa no grid (movimento robótico em linhas retas)
    Vector3 CalculateGridPosition(Vector3 targetPosition)
    {
        // Arredonda a posição alvo para o grid mais próximo
        targetPosition.x = Mathf.Round(targetPosition.x / gridSize) * gridSize;
        targetPosition.z = Mathf.Round(targetPosition.z / gridSize) * gridSize;

        return targetPosition;
    }
}
