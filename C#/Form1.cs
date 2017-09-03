using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BarkingWebServiceClientApplication
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void createJobButton_Click(object sender, EventArgs e)
        {
            // create the service client
            BarkingWebService.RetrieverBarkingClient barkingClient = new BarkingWebService.RetrieverBarkingClient();

            //TODO set the credentials to a valid username/password
            barkingClient.ClientCredentials.UserName.UserName = "username1";
            barkingClient.ClientCredentials.UserName.Password = "pa$$word";

            // create the job with the mandatory fields
            BarkingWebService.Job job = new BarkingWebService.Job();

            //TODO set mandatory fields on job
            job.jobId = "jobId1";
            job.customerName = "customerName1";
            job.address1 = "address1";
            job.jobDesc = "jobDesc1";

            //TODO set optional fields on job

            // create the request object
            BarkingWebService.createJob createJobRequest = new BarkingWebService.createJob();
            createJobRequest.job = job;

            // call the web service
            BarkingWebService.createJobResponse createJobResponse = barkingClient.createJob(createJobRequest);

            // output the response
            BarkingWebService.Result result = createJobResponse.result;
            Console.WriteLine("Success: " + result.success + " txId: " + result.txId);
        }
    }
}
