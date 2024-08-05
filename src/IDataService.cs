using System.Collections.Generic;
using System.ServiceModel;
using WcfService.Models;

[ServiceContract]
public interface IDataService
{
    [OperationContract]
    List<Lookup> GetLookups();

    [OperationContract]
    void AddLookup(Lookup lookup);

    [OperationContract]
    void UpdateLookup(Lookup lookup);

    [OperationContract]
    void DeleteLookup(int lookupId);
}