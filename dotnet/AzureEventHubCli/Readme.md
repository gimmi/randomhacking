### Create Azure Resources

```
LOCATION=northeurope
GROUP=my_evthub_group
NS=evthubns5246
HUB=my_evthub
SA=storage5246

az group create --name $GROUP --location $LOCATION

az eventhubs namespace create \
    --resource-group $GROUP \
    --name $NS \
    --location $LOCATION

az eventhubs eventhub create \
    --resource-group $GROUP \
    --namespace-name $NS \
    --name $HUB \
    --partition-count 2 \
    --message-retention 1

az eventhubs namespace authorization-rule keys list \
    --resource-group $GROUP \
    --namespace-name $NS \
    --name RootManageSharedAccessKey
{
  "primaryConnectionString": "***"
}

az storage account create \
    --name $SA \
    --resource-group $GROUP \
    --location $LOCATION \
    --sku Standard_LRS

az storage container create \
    --account-name $SA \
    --name checkpoint-storage

az storage account show-connection-string \
    --resource-group $GROUP \
    --name $SA
{
  "connectionString": "***"
}

# When done, delete everything

az group delete --name $GROUP
```

### Run it

```
dotnet run -- send `
    --conn "***" `
    --name iothubevents `
    "Hello world"

dotnet run -- receive `
    --conn "***" `
    --name iothubevents `
    --bconn "***" `
    --bname iothubevents-checkpoint-storage
```