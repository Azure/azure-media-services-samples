using System;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Activation;

namespace Microsoft.SilverlightMediaFramework.Samples.Web.Services
{
    [ServiceContract(Namespace = "")]
    [SilverlightFaultBehavior]
    [AspNetCompatibilityRequirements(RequirementsMode = AspNetCompatibilityRequirementsMode.Allowed)]
    public class LoggingService
    {
        [OperationContract]
        public DateTimeOffset LogBatch(string BatchXml)
        {
            System.Diagnostics.Debug.WriteLine(BatchXml);
            return DateTimeOffset.Now;
        }

        // Add more operations here and mark them with [OperationContract]
    }
}
