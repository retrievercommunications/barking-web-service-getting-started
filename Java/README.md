# Java Getting Started Guide

You can use your preferred Java build tool (e.g maven, gradle) to generate the Barking client code.


## Generating a Barking Client

There are a number of ways that you can generate client code from the WSDL that you were provided.

One of these ways is to use the [Apache CXF](http://cxf.apache.org/) library, gradle and the [wsdl2java gradle plugin](https://github.com/nilsmagnus/wsdl2java).

Here is an example: [build.gradle](build.gradle)
Before running ensure both the java package name of the generated code and the path to the WSDL file are correct.

    wsdl2java{
        wsdlsToGenerate = [
                ["-autoNameResolution", 
                    "-p", "com.barking.webservice.client.generated",
                    "$projectDir/wsdls/BarkingWSImport.wsdl"]
        ]


## Create the Service client

	RetrieverBarkingService service = new RetrieverBarkingService();
	RetrieverBarking port = service.getRetrieverBarkingPort();


## Set the Endpoint and Credentials 

	BindingProvider portBP = (BindingProvider)port;
	
    //TODO set to the correct endpoint
	String endpoint = "http://localhost/barking/integration/webservice?extsys=BarkingWSImport";
	portBP.getRequestContext().put(BindingProvider.ENDPOINT_ADDRESS_PROPERTY, endpoint);
	
    //TODO set the correct credentials
	portBP.getRequestContext().put(BindingProvider.USERNAME_PROPERTY, "username1"); 
	portBP.getRequestContext().put(BindingProvider.PASSWORD_PROPERTY, "pa$$word1");

## Sending Data to Barking

### Prepare the Request

    ObjectFactory objFactory = new ObjectFactory();
    Job job = objFactory.createJob();
    
    //TODO set mandatory fields on job
    job.setJobId("Job" + UUID.randomUUID());
    
    //TODO set optional fields on job


### Call the Web Service Operation

 	Result result = port.createJob(job);


### Check the Response

    if(result.isSuccess())
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

    ExportStatusUpdates2 exportStatusUpdatesRequest = objFactory.createExportStatusUpdates2();
	exportStatusUpdatesRequest.setExport(true);

### Call the Web Service Operation		
		
	ResultExportStatusUpdates exportStatusUpdatesResponse = port.exportStatusUpdates(exportStatusUpdatesRequest);
		
### Check the Response
				
    if(exportStatusUpdatesResponse.isSuccess())
    {
        for(EStatus eStatus : exportStatusUpdatesResponse.getEStatusUpdate())
        {
            //TODO save each returned eStatus and take any required actions
        }
    }