using System;

namespace MercedesBenzServerWeb.Helpers
{
    public class AppState
    {
        //notifica cuando cambia el estatus de algun componente y se actualice
        public bool PopupLogout { get; private set; }

        public void ShowPopup(bool Show)
        {
            PopupLogout = Show;
            NotifyStateChanged();
        }

        public event Action OnChange;
        private void NotifyStateChanged() => OnChange?.Invoke();

        public void RefreshNotify()
        {
            OnUpdateNotify?.Invoke();
        }

        public event Action OnUpdateNotify;

    }
}
