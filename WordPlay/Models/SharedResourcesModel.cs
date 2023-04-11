using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using WordPlay.Enumerables;

namespace WordPlay.Models
{
    public class SharedResourcesModel
    {
        public SoundEffect PositiveSoundEffect;
        public SoundEffect NegativeSoundEffect;
        public Texture2D BackButtonTexture;
        public Texture2D QuitButtonTexture;
        public Texture2D BackgroundTexture;
        public Texture2D ButtonTexture;
        public SpriteFont BasicFont;
        public SpriteFont SmallBasicFont;
        public SpriteFont LargeBasicFont;
        public Texture2D TextboxTexture;
        public Dictionary<GameMode, List<TopScoreModel>> TopScoreDictionary;

        public SharedResourcesModel(ContentManager content)
        {
            PositiveSoundEffect = content.Load<SoundEffect>("Sounds/CorrectChoice");
            NegativeSoundEffect = content.Load<SoundEffect>("Sounds/WrongChoice");
            BackButtonTexture = content.Load<Texture2D>("Images/Controls/backButton");
            QuitButtonTexture = content.Load<Texture2D>("Images/Controls/quitButton");
            BackgroundTexture = content.Load<Texture2D>("Images/Background/Main");
            ButtonTexture = content.Load<Texture2D>("Images/Controls/Button");
            BasicFont = content.Load<SpriteFont>("Fonts/BasicFont");
            SmallBasicFont = content.Load<SpriteFont>("Fonts/SmallBasicFont");
            LargeBasicFont = content.Load<SpriteFont>("Fonts/LargeBasicFont");
            TextboxTexture = content.Load<Texture2D>("images/controls/textbox");
            TopScoreDictionary = new Dictionary<GameMode, List<TopScoreModel>>();
            FillTopScoreDictionary(GameMode.Beginner);
            FillTopScoreDictionary(GameMode.Advanced);
            FillTopScoreDictionary(GameMode.Hardcore);
            FillTopScoreDictionary(GameMode.Nightmare);

        }

        private void FillTopScoreDictionary(GameMode gameMode)
        {
            TopScoreDictionary.Add(gameMode, new List<TopScoreModel>());
            TopScoreDictionary[gameMode].Add(new TopScoreModel() { Name = "NA", Score = 0 });
            TopScoreDictionary[gameMode].Add(new TopScoreModel() { Name = "NA", Score = 0 });
            TopScoreDictionary[gameMode].Add(new TopScoreModel() { Name = "NA", Score = 0 });
            TopScoreDictionary[gameMode].Add(new TopScoreModel() { Name = "NA", Score = 0 });
            TopScoreDictionary[gameMode].Add(new TopScoreModel() { Name = "NA", Score = 0 });
        }


    }
}
