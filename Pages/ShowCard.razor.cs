using Microsoft.AspNetCore.Components;
using System.Net.Http.Json;
using FgoCardDB.Datas;

namespace FgoCardDB.Pages
{
    public partial class ShowCard
    {
        [ParameterAttribute]
        public int Id { get; set; }
        private Card? Card { get; set; }

        protected override async Task OnInitializedAsync()
        {
            List<Card>? cards = await Http.GetFromJsonAsync<List<Card>>("data/cards.json");
            Card = cards!.Find(c => c.Id == Id);
        }
    }
}