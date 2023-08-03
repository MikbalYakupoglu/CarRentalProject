using Microsoft.Extensions.Configuration;

namespace Core.Utilities.Helpers.EmailHelper;

public class EmailManager
{
    private IConfiguration Configuration { get; }
    private string API_KEY; 

    public EmailManager(IConfiguration configuration)
    {
        Configuration = configuration;
        API_KEY = Configuration.GetSection("Loqate:Key").Get<string>();
    }



    public System.Data.DataSet ValidateEmail(string emails)
    {
        var key = API_KEY;

        //Build the url
        var url = "https://api.addressy.com/EmailValidation/Batch/Validate/v1.20/dataset.ws?";
        url += "&Key=" + System.Web.HttpUtility.UrlEncode(key);
        url += "&Emails=" + System.Web.HttpUtility.UrlEncode(emails);

        //Create the dataset
        var dataSet = new System.Data.DataSet();
        dataSet.ReadXml(url);

        //Check for an error
        if (dataSet.Tables.Count == 1 && dataSet.Tables[0].Columns.Count == 4 && dataSet.Tables[0].Columns[0].ColumnName == "Error")
            throw new Exception(dataSet.Tables[0].Rows[0].ItemArray[1].ToString());

        //Return the dataset
        return dataSet;

        //FYI: The dataset contains the following columns:
        //Status
        //EmailAddress
        //Account
        //Domain
        //IsDisposible
        //IsSystemMailbox
    }
}