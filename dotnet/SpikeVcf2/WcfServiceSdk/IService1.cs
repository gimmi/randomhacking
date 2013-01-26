using System.ServiceModel;

namespace WcfServiceSdk
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
