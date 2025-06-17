using Cysharp.Threading.Tasks;
using UnityEngine;

namespace Code.Services.UIService
{
    public interface IScreen
    {
        Canvas Canvas { get; }
        UniTask Show();
        UniTask Hide();
        bool IsVisible { get; }
    }
}