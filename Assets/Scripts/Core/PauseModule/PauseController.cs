namespace Core
{
    public class PauseController
    {
        private readonly RegistrationMap _registrationMap;
        
        private bool _isPaused;
        
        public PauseController(RegistrationMap registrationMap)
        {
            _registrationMap = registrationMap;
        }

        public void ChangePauseState()
        {
            if (_isPaused)
            {
                Unpause();
            }
            else
            {
                Pause();
            }
        }
        
        public void Pause()
        {
            foreach (var obj in _registrationMap.RegisteredObjects)
            {
                var pauseComponents = obj.GetComponents<IPauseable>();

                foreach (var pauseComponent in pauseComponents)
                {
                    pauseComponent.Pause();
                }
            }

            _isPaused = true;
        }

        public void Unpause()
        {
            foreach (var obj in _registrationMap.RegisteredObjects)
            {
                var pauseComponents = obj.GetComponents<IPauseable>();

                foreach (var pauseComponent in pauseComponents)
                {
                    pauseComponent.Unpause();
                }
            }

            _isPaused = false;
        }
    }
}