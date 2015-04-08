using Microsoft.WindowsAzure.Storage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace AzureStorageVersioner
{
  /// <summary>
  /// Interaction logic for MainWindow.xaml
  /// </summary>
  public partial class MainWindow : Window
  {
    public MainWindow()
    {
      InitializeComponent();
    }

    private async void GetButton_Click(object sender, RoutedEventArgs e)
    {
      string accountName = AccountName.Text;
      string accountKey = AccountKey.Text;
      string accountString = string.Format("AccountName={0};AccountKey={1};DefaultEndpointsProtocol=http",accountName,accountKey);
      
      var storageAccount = CloudStorageAccount.Parse(accountString);
      var blobClient = storageAccount.CreateCloudBlobClient();

      var serviceProperties = await blobClient.GetServicePropertiesAsync();

      string defaultVersion = serviceProperties.DefaultServiceVersion;

      defaultVersion = String.IsNullOrWhiteSpace(defaultVersion) ? "none" : defaultVersion;

      DefaultServiceVersion.Text = defaultVersion;

    }

    private async void SetButton_Click(object sender, RoutedEventArgs e)
    {
      string accountName = AccountName.Text;
      string accountKey = AccountKey.Text;
      string accountString = string.Format("AccountName={0};AccountKey={1};DefaultEndpointsProtocol=http", accountName, accountKey);

      var storageAccount = CloudStorageAccount.Parse(accountString);
      var blobClient = storageAccount.CreateCloudBlobClient();

      var serviceProperties = await blobClient.GetServicePropertiesAsync();

      serviceProperties.DefaultServiceVersion = DefaultServiceVersion.Text;

      blobClient.SetServiceProperties(serviceProperties);
    }

    private void SetDefaultLink_Click(object sender, RoutedEventArgs e)
    {
      DefaultServiceVersion.Text = "2014-02-14";
    }
  }
}
