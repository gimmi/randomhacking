using System;

namespace WcfServiceApplication
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "Service1" in code, svc and config file together.
    // NOTE: In order to launch WCF Test Client for testing this service, please select Service1.svc or Service1.svc.cs at the Solution Explorer and start debugging.
    public class Service1 : IService1
    {
        private readonly Dependency1 _dependency1;

        public Service1(Dependency1 dependency1)
        {
            _dependency1 = dependency1;
        }

        public string Process(string value)
        {
            return _dependency1.Process(value);
        }

        public void ThrowException()
        {
            throw new ApplicationException("AHHHH!");
        }
    }
}
