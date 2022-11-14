namespace Shooter.Ui
{
    public abstract class DialogView : SimpleView
    {
        public virtual void Show()
        {
            SetActive(true);
            Init();
        }

        public virtual void Hide()
        {
            Dispose();
            SetActive(false);
        }
    }
}