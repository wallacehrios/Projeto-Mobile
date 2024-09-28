using UnityEngine;

public class BoxInteraction : MonoBehaviour
{
    public float gridSize = 1f; // Tamanho do grid para movimento "rob�tico"
    public float moveSpeed = 5f; // Velocidade de movimento da caixa ao ser puxada
    public float interactionRange = 2f; // Dist�ncia m�xima para ativar a caixa

    private bool isPullingBox = false; // Se a caixa est� sendo puxada
    private Transform player; // Refer�ncia ao jogador
    private Vector3 lastPlayerPosition; // �ltima posi��o do jogador

    private static GameObject currentlyPulledBox = null; // Vari�vel est�tica para rastrear a caixa que est� sendo puxada

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        lastPlayerPosition = player.position; // Inicializa com a posi��o inicial do jogador
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
                    // Verifica se a caixa est� sendo puxada atualmente
                    if (isPullingBox)
                    {
                        // Solta a caixa
                        isPullingBox = false;
                        currentlyPulledBox = null;
                        GetComponent<Renderer>().material.color = Color.white; // Reseta a cor da caixa
                    }
                    else
                    {
                        // Se a caixa n�o est� sendo puxada, verifica a dist�ncia ao jogador
                        float distanceToPlayer = Vector3.Distance(player.position, transform.position);
                        if (distanceToPlayer <= interactionRange)
                        {
                            // Se j� h� uma caixa sendo puxada, n�o faz nada
                            if (currentlyPulledBox == null)
                            {
                                // Puxa a caixa
                                isPullingBox = true;
                                currentlyPulledBox = gameObject; // Define esta caixa como a que est� sendo puxada
                                GetComponent<Renderer>().material.color = Color.red; // Muda a cor da caixa
                                lastPlayerPosition = player.position; // Atualiza a posi��o inicial do jogador
                            }
                        }
                    }
                }
            }
        }

        // Se est� puxando a caixa, mova-a apenas quando o jogador se mover
        if (isPullingBox)
        {
            Vector3 playerMovement = player.position - lastPlayerPosition;

            if (playerMovement.magnitude > 0)
            {
                // Move a caixa na mesma dire��o do movimento do jogador
                Vector3 targetPosition = CalculateGridPosition(transform.position + playerMovement);
                transform.position = Vector3.MoveTowards(transform.position, targetPosition, moveSpeed * Time.deltaTime);

                // Atualiza a posi��o do jogador
                lastPlayerPosition = player.position;
            }
        }
    }

    // Calcula a posi��o da caixa no grid (movimento rob�tico em linhas retas)
    Vector3 CalculateGridPosition(Vector3 targetPosition)
    {
        // Arredonda a posi��o alvo para o grid mais pr�ximo
        targetPosition.x = Mathf.Round(targetPosition.x / gridSize) * gridSize;
        targetPosition.z = Mathf.Round(targetPosition.z / gridSize) * gridSize;

        return targetPosition;
    }
    public void Pegar()
    {
        isPullingBox = true;
    }
}
