using System;
using System.IO;
using System.Media;
using System.Windows.Forms;

namespace VTCManager_1._0._0
{
    class Sound
    {
        public SoundPlayer ton_erfolg;
        public SoundPlayer ton_fehler;
        public SoundPlayer ton_tour_gestartet;
        public SoundPlayer ton_tour_beendet;

        private bool missing_file = false;

        public Sound(Translation translation)
        {
            try
            {
                if (File.Exists(Environment.CurrentDirectory + @"\Ressources\insight.wav"))
                {
                    this.ton_erfolg = new SoundPlayer(Environment.CurrentDirectory + @"\Ressources\insight.wav");
                }
                else
                {
                    this.missing_file = true;
                }
                if (File.Exists(Environment.CurrentDirectory + @"\Ressources\time-is-now.wav.wav"))
                {
                    this.ton_fehler = new SoundPlayer(Environment.CurrentDirectory + @"\Ressources\time-is-now.wav");
                }
                else
                {
                    this.missing_file = true;
                }
                if (File.Exists(Environment.CurrentDirectory + @"\Ressources\AutopilotStart_fx.wav"))
                {
                    this.ton_tour_gestartet = new SoundPlayer(Environment.CurrentDirectory + @"\Ressources\AutopilotStart_fx.wav");
                }
                else
                {
                    this.missing_file = true;
                }
                if (File.Exists(Environment.CurrentDirectory + @"\Ressources\AutopilotEnd_fx.wav"))
                {
                    this.ton_tour_beendet = new SoundPlayer(Environment.CurrentDirectory + @"\Ressources\AutopilotEnd_fx.wav");
                }
                else
                {
                    this.missing_file = true;
                }
                if (this.missing_file)
                {
                    //MessageBox.Show(translation.error_sound_missing_file, translation.warning, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            catch
            {
                MessageBox.Show(translation.error_sound_load,translation.error,MessageBoxButtons.OK,MessageBoxIcon.Error);
            }
        }

        public void Play(SoundPlayer sound)
        {

        }
    }
}
