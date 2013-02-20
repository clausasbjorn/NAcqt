namespace NAcqt.Demo
{
    /// <summary>
    /// This class should always be injected and never instantiated.
    /// </summary>
    public class InjectMePlease : IInjectMePlease
    {
        public bool ReturnTrue()
        {
            return true;
        }
    }
}
