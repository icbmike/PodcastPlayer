using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;
using PodcastPlayer.Commands;

namespace PodcastPlayer
{
    public class JsonFileStore<T> : IStore<string, T>
    {
        public string Filename { get; }
        public Func<T, string> KeySelector { get; }

        public JsonFileStore(string filename, Func<T, string> keySelector)
        {
            KeySelector = keySelector;
            Filename = filename;
        }

        public async Task<IImmutableList<T>> ListAsync()
        {
            await EnsureFileExists();
            var items = await ListItems();

            return items.ToImmutableList();
        }

        public async Task<T> SaveAsync(string key, T item)
        {
            await EnsureFileExists();

            var items = (await ListItems())
               .ToDictionary(KeySelector);

            items[key] = item;

            var newItemsJson = JsonConvert.SerializeObject(items.Values.ToList());

            await File.WriteAllTextAsync(Filename, newItemsJson);

            return item;
        }

        public async Task<T> GetAsync(string key)
        {
            await EnsureFileExists();

            var items = (await ListItems())
               .ToDictionary(KeySelector);

            return items[key];
        }

        private async Task EnsureFileExists()
        {
            await FileExtensions.CreateIfNotExists(Filename, "[]");
        }

        private async Task<List<T>> ListItems()
        {
            var itemsJson = await File.ReadAllTextAsync(Filename);

            return JsonConvert.DeserializeObject<List<T>>(itemsJson);
        }
    }
}
