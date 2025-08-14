namespace CustomUI.Controllers
{
    public interface IFPSController
    {
        void Init();
        void ToggleFPSDisplay();
        void SetShowFPS(bool show);
    }
}

