using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;

namespace Valuator.Pages
{
    public class IndexModel : PageModel
    {
        private readonly IStorage _storage;

        public IndexModel(IStorage storage)
        {
            _storage = storage;
        }

        public void OnGet()
        {

        }

        public IActionResult OnPost(string text)
        {
            if (string.IsNullOrEmpty(text)) Redirect("/");

            var id = Guid.NewGuid().ToString();

            var similarityKey = Contains.SimilarityKeyPrefix + id;
            //Посчитать similarity и сохранить в БД по ключу similarityKey
            var similarity = GetSimilarity(text, id);
            _storage.Store(similarityKey, similarity.ToString());

            var textKey = Contains.TextKeyPrefix + id;
            //Сохранить в БД text по ключу textKey
            _storage.Store(textKey, text);

            var rankKey = Contains.RankKeyPrefix + id;
            //Посчитать rank и сохранить в БД по ключу rankKey
            _storage.Store(rankKey, GetRank(text));

            return Redirect($"summary?id={id}");
        }

        private int GetSimilarity(string text, string id)
        {
            var keys = _storage.GetKeys();

            return keys.Any(item => item.Substring(0, 5) == Contains.TextKeyPrefix && _storage.Load(item) == text)
                ? 1
                : 0;
        }

        private static string GetRank(string text)
        {
            var notLetterCount = text.Count(ch => !char.IsLetter(ch));

            return ((double)notLetterCount / text.Length).ToString(CultureInfo.CurrentCulture);
        }
    }
}
