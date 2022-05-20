using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.ComponentModel;
using System.Net.Http;
using System.Windows.Input;
using Xamarin.Forms;

namespace Martynova_18
{
    public class RateViewModel : INotifyPropertyChanged
    {
        private decimal rate;
        private decimal ask;
        private decimal bid;

        public decimal Rate
        {
            get { return rate; }
            private set
            {
                rate = value;
                OnPropertyChanged("Rate");
            }
        }

        public decimal Ask
        {
            get { return ask; }
            private set
            {
                ask = value;
                OnPropertyChanged("Ask");
            }
        }
        public decimal Bid
        {
            get { return bid; }
            private set
            {
                bid = value;
                OnPropertyChanged("Bid");
            }
        }

        public ICommand LoadDataCommand { protected set; get; }

        public RateViewModel()
        {
            this.LoadDataCommand = new Command(LoadData);
        }

        private async void LoadData()
        {
            string url = "https://query.yahooapis.com/v1/public/yql?q=select+*+from+yahoo.finance.xchange+where+pair+=+%22USDRUB%22&format=json&env=store%3A%2F%2Fdatatables.org%2Falltableswithkeys&callback=";
 
            try
            {
                HttpClient client = new HttpClient();
                client.BaseAddress = new Uri(url);
                var response = await client.GetAsync(client.BaseAddress);
                response.EnsureSuccessStatusCode(); // выброс исключения, если произошла ошибка
 
                // десериализация ответа в формате json
                var content = await response.Content.ReadAsStringAsync();
                JObject o = JObject.Parse(content);
 
                var str = o.SelectToken(@"$.query.results.rate");
                var rateInfo = JsonConvert.DeserializeObject<RateInfo>(str.ToString());
 
                this.Rate = rateInfo.Rate;
                this.Ask = rateInfo.Ask;
                this.Bid = rateInfo.Bid;
            }
            catch(Exception ex)
            { }
        }
 
        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged(string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }
    }

}
