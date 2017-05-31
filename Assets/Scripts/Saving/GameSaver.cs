using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Squares.Game;
using UnityEngine;
using Utils;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using Newtonsoft.Json.Linq;
using System.IO;

namespace Squares.Saving
{
    public class GameSaver : Singletone<GameSaver>
    {
        private static string StorageFile = Application.persistentDataPath + "/game_state.json";

        JsonSerializerSettings settings = new JsonSerializerSettings
        {
            ReferenceLoopHandling = ReferenceLoopHandling.Ignore,
            TypeNameHandling = TypeNameHandling.All,
            Formatting = Formatting.Indented,
            ContractResolver = new UnityContractResolver()
        };

        public bool Load()
        {
            if (!File.Exists(StorageFile))
                return false;

            var json = File.ReadAllText(StorageFile);
            var state = JsonConvert.DeserializeObject<GameState>(json, this.settings);

            ScoreManager.Instance.Score.Value = state.Score;
            ScoreManager.Instance.BestScore.Value = state.BestScore;
            ColorsProvider.Instance.Restore(state.CurrentColors, state.NextColors);
            GameController.Instance.Restore(state.Cells);

            return true;
        }

        public void Save()
        {
            var state = new GameState
            {
                Score = ScoreManager.Instance.Score.Value,
                BestScore = ScoreManager.Instance.BestScore.Value,
                CurrentColors = ColorsProvider.Instance.Colors,
                NextColors = ColorsProvider.Instance.NextColors,
                Cells = GameController.Instance.Cells
            };

            var json = JsonConvert.SerializeObject(state, this.settings);

            File.WriteAllText(StorageFile, json);
        }
    }

    public class UnityContractResolver : DefaultContractResolver
    {
        Type colorType = typeof(Color);

        protected override IList<JsonProperty> CreateProperties(Type type, MemberSerialization memberSerialization)
        {
            if (type == this.colorType)
            {
                var props = new List<JsonProperty>()
                {
                    base.CreateProperty(this.colorType.GetField("r"), memberSerialization),
                    base.CreateProperty(this.colorType.GetField("g"), memberSerialization),
                    base.CreateProperty(this.colorType.GetField("b"), memberSerialization),
                    base.CreateProperty(this.colorType.GetField("a"), memberSerialization)
                };

                props.ForEach(p =>
                {
                    p.Writable = true;
                    p.Readable = true;
                });

                return props;
            }

            return base.CreateProperties(type, memberSerialization);
        }
    }
}
