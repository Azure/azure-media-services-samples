This component implements a configurable logging framework with no external dependencies. 
It is intended to be called from Microsoft.SilverlightMediaFramework.Monitoring but could be used on it's own.
LoggingService is the main gateway where all logs are pushed to. Internally it uses MEF to load LogAgents to actually handle the logs.
Additionally included is a high level BatchingLogAgent base class that provides everything needed to implement your own log agent that will efficiently queue and batch logs on a background thread.
Ultimately, BatchingLogAgent needs an implementation of IBatchAgent which will get Batch objects (containers for mulitple Log objects).
An IBatchAgent typically provides the service specific implementation for sending batches to the server.
Also included are the ability to map key value pairs, and validate logs and batches before sending them. This is useful for defining rules that can be imposed on your logs before they are passed to the BatchAgent.