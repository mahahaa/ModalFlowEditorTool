using Cysharp.Threading.Tasks;

public interface IModalController
{
    UniTask<ModalResults.ModalResult> ShowAsync();
}
