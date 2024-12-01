using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Media;

namespace Tetris.Game_Components
{
    public static class Sound_Manager
    {
        //Dźwięki
        private static SoundEffect sound_move;
        private static SoundEffect sound_rotate;
        private static SoundEffect sound_drop;
        private static SoundEffect sound_line;
        private static SoundEffect sound_start_resume;
        private static SoundEffect sound_gameover;
        private static SoundEffect sound_levelup;
        private static SoundEffect sound_button;
        private static Song music;
        private static Song menu_music;

        public static SoundEffect Sound_Move { get => sound_move; }
        public static SoundEffect Sound_Rotate { get => sound_rotate; }
        public static SoundEffect Sound_Drop { get => sound_drop; }
        public static SoundEffect Sound_Line { get => sound_line; }
        public static SoundEffect Sound_Start_Resume { get => sound_start_resume; }
        public static SoundEffect Sound_Gameover { get => sound_gameover; }
        public static SoundEffect Sound_Levelup { get => sound_levelup; }
        public static SoundEffect Sound_Button { get => sound_button; }
        public static Song Music { get => music; }
        public static Song Menu_Music { get => menu_music; }

        public static void Load(ContentManager content)
        {
            sound_move = content.Load<SoundEffect>("Audio/move");
            sound_rotate = content.Load<SoundEffect>("Audio/rotate");
            sound_drop = content.Load<SoundEffect>("Audio/drop");
            sound_line = content.Load<SoundEffect>("Audio/line");
            sound_start_resume = content.Load<SoundEffect>("Audio/start");
            sound_gameover = content.Load<SoundEffect>("Audio/gameover");
            sound_levelup = content.Load<SoundEffect>("Audio/levelup");
            sound_button = content.Load<SoundEffect>("Audio/button");
            menu_music = content.Load<Song>("Audio/menu");
            music = content.Load<Song>("Audio/music");
        }

    }
}
