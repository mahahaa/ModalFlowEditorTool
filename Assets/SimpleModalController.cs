
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

public class SimpleModalController : MonoBehaviour, IModalController
{
    [SerializeField] private Button continueButton;
    [SerializeField] private Button cancelButton;

    public async UniTask<ModalResults.ModalResult> ShowAsync()
    {
        ModalResults.ModalResult? result = null;

        if (continueButton != null)
        {
            continueButton.onClick.AddListener(() => result = ModalResults.ModalResult.Continue);
        }
        if (cancelButton != null)
        {
            cancelButton.onClick.AddListener(() => result = ModalResults.ModalResult.Cancel);
        }

        await UniTask.WaitUntil(() => result.HasValue);
        return result.Value;
    }
}
