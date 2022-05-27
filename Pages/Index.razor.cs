using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using System.Net.Http.Json;
using System.Threading;
using FgoCardDB.Datas;

namespace FgoCardDB.Pages
{
    public partial class Index
    {

        private int imagesByPage = 30;

        private string? searchInputValue { get; set; }
        private List<Card>? AllCards { get; set; }
        private Card[]? Cards;
        private int lastPage;

        bool wait = true;
        protected override async Task OnInitializedAsync()
        {
            AllCards = await Http.GetFromJsonAsync<List<Card>>("data/cards.json");
            lastPage = (int)Math.Ceiling(AllCards!.Count() / (double)imagesByPage);
            OnPageChanged(0);
            wait = false;
        }
        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            while(wait) await Task.Delay(100);
            if(firstRender)
            {
                await JS.InvokeVoidAsync("OnRadioChange", $"radio-0");
            }
        }
        void OnSearchInputEvent(ChangeEventArgs changeEvent)
        {
            JS.InvokeVoidAsync("OnRadioChange", "");
            searchInputValue = (string?)changeEvent.Value;
            
            if(String.IsNullOrEmpty(searchInputValue))
            {
                OnPageChanged(lastRadioIndex);
                return;
            }
            Cards = AllCards!
                .FindAll(c => c.Name!.ToLower().Contains(searchInputValue!.Trim().ToLower()) 
                    || c.Class!.ToLower().Contains(searchInputValue!.Trim().ToLower()))
                .Where((n, i) => i < imagesByPage)
                .ToArray();
        }
        int lastRadioIndex;
        async void OnPageChanged(int index)
        {
            lastRadioIndex = index;
            LoadCardsByPage(index);
            await JS.InvokeVoidAsync("OnRadioChange", $"radio-{index}");
        }
        void LoadCardsByPage(int page = 0) { Cards = AllCards!.Where((n, i) => i >= imagesByPage * (page) && i < imagesByPage * (page + 1)).ToArray(); }
    }
}