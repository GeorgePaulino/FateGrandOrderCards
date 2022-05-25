using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using System.Net.Http.Json;

namespace FgoCardDB.Pages
{
    public partial class Index
    {

        private string? searchInputValue { get; set; }
        private List<Card>? AllCards { get; set; }
        private Card[]? Cards;
        private int pagePlaceHolder;
        private int pages;

        protected override async Task OnInitializedAsync()
        {
            AllCards = await Http.GetFromJsonAsync<List<Card>>("data/cards.json");
            Cards = AllCards!.ToArray();
            pages = 3;
        }
        void OnClick()
        {
            Console.WriteLine("Message");
        }
        void OnSearchInputEvent(ChangeEventArgs changeEvent)
        {
            searchInputValue = (string)changeEvent.Value;
            Cards = AllCards!.FindAll(c => c.Name!.ToLower().Contains(searchInputValue!.ToLower()) || c.Class!.ToLower().Contains(searchInputValue!.ToLower())).ToArray();

        }
        public class Card
        {
            public string? AKA { get; set; }
            public string? ATK { get; set; }
            public string? Alignments { get; set; }
            public string? Attribute { get; set; }
            public string? Class { get; set; }
            public int Cost { get; set; }
            public string? Gender { get; set; }
            public string? GrailATKLV100 { get; set; }
            public string? GrailATKLV120 { get; set; }
            public string? GrailHPLV100 { get; set; }
            public string? GrailHPLV120 { get; set; }
            public string? Growth { get; set; }
            public string? HP { get; set; }
            public int Id { get; set; }
            public string? Japanese { get; set; }
            public string? Name { get; set; }
            public int Star { get; set; }
            public string? Traits { get; set; }
            public int Type { get; set; }
        }
    }
}