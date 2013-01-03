using System.ServiceModel;

namespace WcfServiceLib
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
