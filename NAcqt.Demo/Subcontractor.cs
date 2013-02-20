namespace NAcqt.Demo
{
    public class Subcontractor
    {
        public void SomeMethod()
        {   
        }

        public string someOtherMethod(string someString)
        {
            return someString;
        }

        public void AndFinallyThisMethod()
        {
            var shouldHaveBeenInjected = new InjectMePlease();
            var t = shouldHaveBeenInjected.ReturnTrue();
        }
    }
}
