using System.ServiceModel;

namespace WcfServiceApplication
{
    [ServiceContract]
    public interface IService1
    {
        [OperationContract]
        string Process(string value);

        [OperationContract]
        void ThrowException();
    }
}
