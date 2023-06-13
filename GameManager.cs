using UnityEngine;

// ������ ���� ���� ����, ���� UI�� �����ϴ� ���� �Ŵ���
public class GameManager : MonoBehaviour
{
    private static GameManager instance; // �̱����� �Ҵ�� static ����

    // �ܺο��� �̱��� ������Ʈ�� �����ö� ����� ������Ƽ
    public static GameManager Instance
    {
        get
        {
            // ���� �̱��� ������ ���� ������Ʈ�� �Ҵ���� �ʾҴٸ�
            if (instance == null)
                // ������ GameManager ������Ʈ�� ã�� �Ҵ�
                instance = FindObjectOfType<GameManager>();

            // �̱��� ������Ʈ�� ��ȯ
            return instance;
        }
    }

    private int score; // ���� ���� ����
    public bool isGameover { get; private set; } // ���� ���� ����

    private void Awake()
    {
        // ���� �̱��� ������Ʈ�� �� �ٸ� GameManager ������Ʈ�� �ִٸ� �ڽ��� �ı�
        if (Instance != this) Destroy(gameObject);
    }


    // ������ �߰��ϰ� UI ����
    public void AddScore(int newScore)
    {
        // ���� ������ �ƴ� ���¿����� ���� ���� ����
        if (!isGameover)
        {
            // ���� �߰�
            score += newScore;
            // ���� UI �ؽ�Ʈ ����
            //UIManager.Instance.UpdateScoreText(score);
        }
    }

    // ���� ���� ó��
    public void EndGame()
    {
        // ���� ���� ���¸� ������ ����
        isGameover = true;
        // ���� ���� UI�� Ȱ��ȭ
        //UIManager.Instance.SetActiveGameoverUI(true);
    }
}