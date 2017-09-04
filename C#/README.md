# C# Getting Started Guide

Visual Studio simplifies the process to call the Barking Web Service.

## Add the Service Reference

Within your Project, you'll need to add a Service Reference. In the address field, enter the local path to the WSDL that you were provided.

Give the Service an appropriate namespace like BarkingWebService and finish.

## Ensure you are pointing to the correct endpoint

You will need to open the App.config file and make sure the endpoint address value matches what you have been given by Retriever for the server that you want to connect to.

    <client>
        <endpoint address="http://localhost/barking/integration/webservice?extsys=BarkingWSImport"
            ...
    </client>

## Create the Service client

	BarkingWebService.RetrieverBarkingClient barkingClient = new BarkingWebService.RetrieverBarkingClient();

## Set the Credentials 

You will need to open the App.config file again and set the client credential type to Basic by replacing the binding's security tag with:
	
	<security mode="Transport">
		<transport clientCredentialType="Basic" />
	</security>
 

Set the credentials on the Service client.

	//TODO set the credentials to a valid username/password
    barkingClient.ClientCredentials.UserName.UserName = "username1";
    barkingClient.ClientCredentials.UserName.Password = "pa$$word";

## Sending Data to Barking

### Prepare the Request

	// create the job with the mandatory fields
    BarkingWebService.Job job = new BarkingWebService.Job();

    //TODO set mandatory fields on job
    job.jobId = "jobId1";
    
    //TODO set optional fields on job

    // create the request object
    BarkingWebService.createJob createJobRequest = new BarkingWebService.createJob();
    createJobRequest.job = job;

### Call the Web Service Operation

 	BarkingWebService.createJobResponse createJobResponse = barkingClient.createJob(createJobRequest);

### Check the Response

    BarkingWebService.Result result = createJobResponse.result;
    if(result.success)
    {
    	//TODO implement success logic
    }
    else
    {
    	//TODO implement failure logic
    }

## Extracting Data from Barking

### Prepare the Request

The easiest way to extract data is to set the export flag to true so that Barking will return all data that has not already been exported. You can then periodically call this (e.g. every 5 minutes) and you will only get new data each time.

    BarkingWebService.ExportStatusUpdates statusUpdate = new BarkingWebService.ExportStatusUpdates();
    statusUpdate.export = true;
            
    BarkingWebService.exportStatusUpdates exportRequest = new BarkingWebService.exportStatusUpdates();
    exportRequest.statusUpdate = statusUpdate;

### Call the Web Service Operation

	BarkingWebService.exportStatusUpdatesResponse exportResponse = barkingClient.exportStatusUpdates(exportRequest);

### Check the Response

	BarkingWebService.ResultExportStatusUpdates result = exportResponse.result;
    if (result.success)
    {
        foreach (BarkingWebService.EStatus eStatusUpdate in result.eStatusUpdate)
        {
            //TODO save each returned eStatus and take any required actions
        }
    }

## Set Datetime Fields

Datetime fields in .NET, come by default with milliseconds (e.g. 2017-09-04T15:54:25.5965803+10:00)
When setting datetime fields in C#, you will need to zero out the milliseconds using the following code sample:

	DateTime startDate = DateTime.Now;
	// zero out the milliseconds
	job.startDate = startDate.AddTicks(-(startDate.Ticks % TimeSpan.TicksPerSecond));

For datetime fields that are optional, .NET will automatically add an extra boolean field called %dateTimefieldName%Specified for every optional datetime field. You will need to set these to true for any optional datetime field that you set.

	job.startDateSpecified = true;