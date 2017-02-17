using PCLStorage;
using System;
using System.ComponentModel;
using System.Net.Http;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace FileSaver.ViewModel
{
    class FileSaverViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string name)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
       
        private string _url;
        public string urlPath
        {
            get
            {
                return _url;
            }

            set
            {
                _url = value;
                OnPropertyChanged("urlPath");
            }
        }

        public FileSaverViewModel()
        {
            DownloadCommand = new Command(Start);
        }

        private async void Start()
        {
            await LaunchURLForSync();
        }
        private async Task LaunchURLForSync()
        {
            if (urlPath != null)
            {
                try
                {
                    HttpClient client = new HttpClient();
                    Uri uri = new Uri(urlPath);
                    IFolder rootFolder = FileSystem.Current.LocalStorage;
                    IFolder folder = await rootFolder.CreateFolderAsync("MySubFolder", CreationCollisionOption.OpenIfExists);
                    IFile file = await folder.CreateFileAsync("README.txt", CreationCollisionOption.ReplaceExisting);
                    using (var fileHandler = await file.OpenAsync(FileAccess.ReadAndWrite))
                    {
                        var httpResponse = await client.GetAsync(uri);
                        byte[] dataBuffer = await httpResponse.Content.ReadAsByteArrayAsync();
                        await fileHandler.WriteAsync(dataBuffer, 0, dataBuffer.Length);
                    }
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }


        public async Task CreateRealFileAsync()
        {
            // get hold of the file system
            IFolder rootFolder = FileSystem.Current.LocalStorage;

            // create a folder, if one does not exist already
            IFolder folder = await rootFolder.CreateFolderAsync("MySubFolder", CreationCollisionOption.OpenIfExists);

            // create a file, overwriting any existing file
            IFile file = await folder.CreateFileAsync("README.txt", CreationCollisionOption.ReplaceExisting);

            // populate the file with some text
            await file.WriteAllTextAsync("Sample Text...");
        }
        public ICommand DownloadCommand { private set; get; }
    }
}
